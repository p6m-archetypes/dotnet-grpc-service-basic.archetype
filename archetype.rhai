let context = #{};

// This generally comes from ANSWERS, but prompting for it ensures it is in the context 
context += prompt("Project Author:", "author_full", #{
    placeholder: "Your Name <your.name@example.com>",
});

if switch_enabled("debug-answers") {
    log(Info, archetect::archetype::description() +" Answers:");
    display(as_yaml(ANSWERS));
}

// Use the same switches, defaults, etc passed to this archetype, and pass them to the next
let render_settings = archetect::archetype::settings();

context += render(Archetype("org-prompts"), ANSWERS, render_settings);

context += render(Archetype("project-prompts"), ANSWERS + #{
    "suffix_default": "Service",
    "suffix_options": [
        "Service",
        "Orchestrator",
        "Adapter",
    ],
}, render_settings);

// Persistence configuration removed - was causing archetype generation failures.
// The persistence prompt is now handled by external components (project-prompts).

context += prompt("Solution Name:", "solution-name", #{
  placeholder: "Widgets",
  cased_as: [
    CasedIdentityCasedValue(PROGRAMMING_CASES),
    FixedKeyCasedValue("org-title", TitleCase),
  ],
  help: "Soltution Name",
});

context += prompt("Project Prefix:", "prefix-name", #{
  cased_as: [
    CasedIdentityCasedValue(PROGRAMMING_CASES),
    FixedKeyCasedValue("project-title", TitleCase),
  ],
  help: "Business Function",
});

context += prompt("Project Suffix:", "suffix-name", #{
  defaults_with: "Service",
  cased_as: [
    CasedIdentityCasedValue(PROGRAMMING_CASES),
    FixedKeyCasedValue("suffix-title", TitleCase),
  ],
  help: "Project Type",
});

context += set("org-solution-name", render("{{ org-name }}-{{ solution-name }}", context), #{
  cases: [
    CasedIdentityCasedValue(PROGRAMMING_CASES),
  ],
});

context += set("project-name", render("{{ prefix-name }}-{{ suffix-name }}", context), #{
  cases: [
    CasedIdentityCasedValue(PROGRAMMING_CASES),
  ],
});

// context += render(Archetype("java-project-attributes"), ANSWERS + context + #{
//     "suffix_default": "Service",
//     "suffix_options": [
//         "Service",
//         "Orchestrator",
//         "Adapter",
//     ],
// }, render_settings);

// Persistence configuration removed - was causing archetype generation failures
// when partially uncommented. If persistence options are needed, implement
// proper conditional logic for all options including "None" case.

context += prompt("Artifactory Host:", "artifactory-host", #{
  defaults_with: "p6m.jfrog.io",
});

context += prompt("Service Port:", "service-port", #{
  defaults_with: "5030",
  help: "gRPC service port for the application",
});

context += set("management-port", parse_int(context["service-port"]) + 1);
context += set("database-port", 5432);
context += set("debug-port", parse_int(context["service-port"]) + 9);

if switch_enabled("debug-context") {
    log(Info, `${archetect::archetype::description()} (${context["project-name"]}) Context:`);
    display(as_yaml(context));
}

render(Directory("contents/base"), context);

//render(Archetype("manifests"), context["project-name"], context + #{
//  "language": ".NET",
//  "protocol": "gRPC",
//});

//
// Publishing
//
if switch_enabled("zip") {
  zip(context["project-name"], render("{{ project-name }}.zip", context));
}
if switch_enabled("tarball") {
  tar_gz(context["project-name"], render("{{ project-name }}.tar.gz", context));
}

if switch_enabled("git-init") || switch_enabled("git-publish") {
  git_init(context["project-name"], "main");
  git_add_all(context["project-name"]);
  git_commit(context["project-name"], "initial commit");
  git_remote_add(context["project-name"], "origin", render("https://github.com/{{ org-solution-name }}/{{ project-name }}.git", context));

  if switch_enabled("git-publish") {
    gh_repo_create(render("{{ org-solution-name }}/{{ project-name }}", context), Private);
    git_push(context["project-name"], "origin", "main");
  }
} else {
  display();

  log(Info, "Copy and paste the following commands to publish your project:");

  display();

  display(render("cd {{ project-name }}", context));
  display("git init -b main");
  display(render("gh repo create {{ org-solution-name }}/{{ project-name }} --private --source=. --remote=origin", context));
  display("git add .");
  display("git commit -m 'initial commit'");
  display("git push -u origin HEAD");
}

context
