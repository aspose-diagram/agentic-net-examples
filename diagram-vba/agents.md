---
category: diagram-vba
display_name: Diagram Vba
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 35
pass_rate: 100.0
generated: 2026-08-03
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Diagram Vba

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Diagram Vba** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 35 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Diagram Vba** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for diagram vba operations.
You always use explicit types (never `var`), include all required `using` directives, and follow the patterns established in this category.

## Boundaries

### Always

- Use explicit types — `Diagram diagram = new Diagram(...)` not `var diagram = ...`
- Include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;` in every file
- Use `SaveFileFormat` enum in PascalCase: `SaveFileFormat.Vsdx`, `SaveFileFormat.Pdf`
- Use `BOOL.True` / `BOOL.False` for Aspose BOOL properties — never plain `true`/`false`
- Wrap entry point in `static void Main()` inside `class Program`
- Handle `FileNotFoundException` with try/catch when loading files

### Ask First

- Multi-file projects or solutions
- External dependencies beyond Aspose.Diagram and Aspose.Drawing
- Platform-specific code (Windows Forms, WPF, ASP.NET)

### Never

- Use `var` for any variable declaration
- Use `using (Diagram diagram = ...)` — Diagram does not implement IDisposable
- Use `SaveFileFormat.VSDX` or other ALL_CAPS enum values
- Use `System.Windows.Forms` — not available in net8.0 console project
- Use NUnit `Assert` — not available, use `Console.WriteLine` and manual checks
- Use PowerShell syntax inside C# files

## Required Namespaces

| Namespace | Files | Purpose |
|-----------|-------|---------|
| `Aspose.Diagram` | 35 | Core diagram API |
| `System` | 35 | Console, Math, DateTime, Exception |
| `System.IO` | 32 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Vba` | 27 | Supporting utilities |
| `Aspose.Diagram.Saving` | 6 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 5 | List, Dictionary, HashSet |
| `System.Text` | 1 | StringBuilder |
| `System.Text.RegularExpressions` | 1 | Supporting utilities |
| `System.Text.Json` | 1 | JSON serialization |
| `System.Threading.Tasks` | 1 | Supporting utilities |
| `System.Xml.Linq` | 1 | Supporting utilities |
| `System.Security.Cryptography.X509Certificates` | 1 | Supporting utilities |

## Common Code Pattern

The dominant workflow in this category is: **Load → Modify → Save**

```csharp
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // 1. Load or create diagram
        Diagram diagram = new Diagram("input.vsdx");

        // 2. Perform category-specific operations
        // ...

        // 3. Save result
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
```

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-new-vba-module-containing-custom-macro-code-to-the-existing-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/add-a-new-vba-module-containing-custom-macro-code-to-the-existing-diagram.cs) | `Diagram`, `Save`, `diagram` | Add a new vba module containing custom macro code to the existing diagram |
| [apply-password-protection-to-the-vba-project-to-restrict-unauthorized-macro-editing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/apply-password-protection-to-the-vba-project-to-restrict-unauthorized-macro-editing.cs) | `Diagram`, `Save`, `diagram` | Apply password protection to the vba project to restrict unauthorized macro editing |
| [batch-process-a-folder-of-visio-files-to-extract-each-vba-module-into-separate-bas-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/batch-process-a-folder-of-visio-files-to-extract-each-vba-module-into-separate-bas-files.cs) | `Diagram` | Batch process a folder of visio files to extract each vba module into separate bas files |
| [batch-sign-vba-projects-in-multiple-diagrams-using-a-common-certificate-for-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/batch-sign-vba-projects-in-multiple-diagrams-using-a-common-certificate-for-consistency.cs) | `Diagram`, `Save`, `diagram` | Batch sign vba projects in multiple diagrams using a common certificate for consistency |
| [check-whether-the-vba-project-in-the-diagram-is-digitally-signed-with-a-certificate.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/check-whether-the-vba-project-in-the-diagram-is-digitally-signed-with-a-certificate.cs) | `Diagram` | Check whether the vba project in the diagram is digitally signed with a certificate |
| [clone-the-vba-project-from-one-diagram-and-attach-it-to-another-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/clone-the-vba-project-from-one-diagram-and-attach-it-to-another-diagram.cs) | `Diagram` | Clone the vba project from one diagram and attach it to another diagram |
| [compare-vba-code-between-two-diagrams-and-highlight-differences-in-a-diff-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/compare-vba-code-between-two-diagrams-and-highlight-differences-in-a-diff-report.cs) | `Diagram` | Compare vba code between two diagrams and highlight differences in a diff report |
| [compress-the-vba-project-within-a-diagram-to-minimize-overall-storage-footprint.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/compress-the-vba-project-within-a-diagram-to-minimize-overall-storage-footprint.cs) | `Diagram`, `Save`, `diagram` | Compress the vba project within a diagram to minimize overall storage footprint |
| [convert-the-diagram-to-pdf-while-embedding-vba-macros-for-interactive-documents.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/convert-the-diagram-to-pdf-while-embedding-vba-macros-for-interactive-documents.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Convert the diagram to pdf while embedding vba macros for interactive documents |
| [count-total-lines-of-code-in-each-vba-module-and-summarize-results-in-a-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/count-total-lines-of-code-in-each-vba-module-and-summarize-results-in-a-report.cs) | `Diagram` | Count total lines of code in each vba module and summarize results in a report |
| [delete-a-vba-module-identified-by-name-from-the-diagram-s-vba-project.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/delete-a-vba-module-identified-by-name-from-the-diagram-s-vba-project.cs) | `Diagram`, `Save`, `diagram` | Delete a vba module identified by name from the diagram s vba project |
| [enumerate-all-vba-modules-in-the-loaded-diagram-and-list-each-module-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/enumerate-all-vba-modules-in-the-loaded-diagram-and-list-each-module-name.cs) | `Diagram`, `Save`, `diagram` | Enumerate all vba modules in the loaded diagram and list each module name |
| [execute-a-specific-macro-programmatically-and-capture-its-output-for-verification.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/execute-a-specific-macro-programmatically-and-capture-its-output-for-verification.cs) | `Diagram`, `Save`, `diagram` | Execute a specific macro programmatically and capture its output for verification |
| [export-the-entire-vba-project-to-an-external-vba-file-for-backup-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/export-the-entire-vba-project-to-an-external-vba-file-for-backup-purposes.cs) | `Diagram` | Export the entire vba project to an external vba file for backup purposes |
| [extract-the-source-code-of-a-specified-vba-module-and-save-it-to-a-bas-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/extract-the-source-code-of-a-specified-vba-module-and-save-it-to-a-bas-file.cs) | `Diagram` | Extract the source code of a specified vba module and save it to a bas file |
| [generate-a-consolidated-csv-report-summarizing-vba-module-counts-across-all-processed-diagrams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/generate-a-consolidated-csv-report-summarizing-vba-module-counts-across-all-processed-diagrams.cs) | `Diagram` | Generate a consolidated csv report summarizing vba module counts across all processed diagrams |
| [import-a-previously-exported-vba-project-file-into-a-diagram-to-restore-macros.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/import-a-previously-exported-vba-project-file-into-a-diagram-to-restore-macros.cs) | `Diagram`, `Save`, `diagram` | Import a previously exported vba project file into a diagram to restore macros |
| [list-all-macro-names-defined-in-the-vba-project-together-with-their-containing-modules.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/list-all-macro-names-defined-in-the-vba-project-together-with-their-containing-modules.cs) | `Diagram` | List all macro names defined in the vba project together with their containing modules |
| [load-a-visio-diagram-from-a-memory-stream-and-access-its-vba-project-without-disk-i-o.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/load-a-visio-diagram-from-a-memory-stream-and-access-its-vba-project-without-disk-i-o.cs) | `Diagram` | Load a visio diagram from a memory stream and access its vba project without disk i o |
| [load-a-visio-diagram-from-file-and-access-its-vba-project-for-manipulation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/load-a-visio-diagram-from-file-and-access-its-vba-project-for-manipulation.cs) | `Diagram`, `Save`, `diagram` | Load a visio diagram from file and access its vba project for manipulation |
| [log-detailed-vba-project-metadata-to-a-json-file-for-auditing-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/log-detailed-vba-project-metadata-to-a-json-file-for-auditing-purposes.cs) | `Diagram` | Log detailed vba project metadata to a json file for auditing purposes |
| [perform-asynchronous-loading-of-a-diagram-and-retrieve-its-vba-project-once-loading-completes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/perform-asynchronous-loading-of-a-diagram-and-retrieve-its-vba-project-once-loading-completes.cs) | `Diagram` | Perform asynchronous loading of a diagram and retrieve its vba project once loading completes |
| [remove-password-protection-from-a-vba-project-after-verifying-user-credentials-successfully.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/remove-password-protection-from-a-vba-project-after-verifying-user-credentials-successfully.cs) | `Diagram`, `Save`, `diagram` | Remove password protection from a vba project after verifying user credentials successfully |
| [remove-the-existing-digital-signature-from-the-vba-project-to-allow-unsigned-deployment.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/remove-the-existing-digital-signature-from-the-vba-project-to-allow-unsigned-deployment.cs) | `Diagram`, `Save`, `diagram` | Remove the existing digital signature from the vba project to allow unsigned deployment |
| [rename-a-vba-module-to-a-more-descriptive-identifier-within-the-same-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/rename-a-vba-module-to-a-more-descriptive-identifier-within-the-same-diagram.cs) | `Diagram`, `Save`, `diagram` | Rename a vba module to a more descriptive identifier within the same diagram |
| [replace-all-instances-of-a-deprecated-function-name-with-the-updated-version-in-vba-modules.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/replace-all-instances-of-a-deprecated-function-name-with-the-updated-version-in-vba-modules.cs) | `Diagram`, `Save`, `diagram` | Replace all instances of a deprecated function name with the updated version in vba modules |
| [retrieve-detailed-information-about-the-vba-project-s-digital-signature-including-signer-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/retrieve-detailed-information-about-the-vba-project-s-digital-signature-including-signer-name.cs) | `Diagram` | Retrieve detailed information about the vba project s digital signature including signer name |
| [save-a-diagram-containing-vba-macros-to-a-memory-stream-for-network-transmission.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/save-a-diagram-containing-vba-macros-to-a-memory-stream-for-network-transmission.cs) | `Diagram`, `Save`, `diagram` | Save a diagram containing vba macros to a memory stream for network transmission |
| [save-the-diagram-preserving-its-vba-project-to-a-new-visio-file-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/save-the-diagram-preserving-its-vba-project-to-a-new-visio-file-format.cs) | `Diagram`, `Save`, `diagram` | Save the diagram preserving its vba project to a new visio file format |
| [save-the-diagram-without-including-the-vba-project-to-reduce-file-size.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/save-the-diagram-without-including-the-vba-project-to-reduce-file-size.cs) | `Diagram`, `Save`, `diagram` | Save the diagram without including the vba project to reduce file size |
| [search-vba-code-for-a-specific-keyword-and-return-module-locations-where-it-appears.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/search-vba-code-for-a-specific-keyword-and-return-module-locations-where-it-appears.cs) | `Diagram` | Search vba code for a specific keyword and return module locations where it appears |
| [serialize-the-vba-project-structure-to-xml-for-integration-with-external-analysis-tools.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/serialize-the-vba-project-structure-to-xml-for-integration-with-external-analysis-tools.cs) | `Diagram` | Serialize the vba project structure to xml for integration with external analysis tools |
| [sign-the-vba-project-using-a-provided-x509-certificate-to-ensure-authenticity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/sign-the-vba-project-using-a-provided-x509-certificate-to-ensure-authenticity.cs) | `Diagram`, `Save`, `diagram` | Sign the vba project using a provided x509 certificate to ensure authenticity |
| [update-an-existing-vba-module-by-replacing-its-content-with-new-macro-statements.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/update-an-existing-vba-module-by-replacing-its-content-with-new-macro-statements.cs) | `Diagram`, `Save`, `diagram` | Update an existing vba module by replacing its content with new macro statements |
| [validate-vba-code-syntax-across-all-modules-and-report-any-compilation-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba/validate-vba-code-syntax-across-all-modules-and-report-any-compilation-errors.cs) | `Diagram` | Validate vba code syntax across all modules and report any compilation errors |

## Command Reference

```bash
# Build the warmup project
cd compiler/CSharpRunner/_warmup
dotnet build

# Run a specific example (copy .cs content to Program.cs first)
dotnet run

# Build with verbose output
dotnet build --verbosity detailed
```

### .csproj Template

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <NoWarn>MSB3277</NoWarn>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Aspose.Diagram">
      <HintPath>..\..\libs\Aspose.Diagram.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>
```

## Testing Guide

### Build Verification

```bash
dotnet build  # Must exit with rc=0, zero compiler errors
```

### Run Verification

```bash
dotnet run    # rc=0 = pass, rc!=0 with unhandled exception = fail
```

### Expected Output Patterns

| Pattern | Meaning |
|---------|---------|
| `rc=0` | ✅ Pass — example compiled and ran successfully |
| `rc=1` compiler errors | ❌ Fail — CS error codes indicate API misuse |
| `rc!=0` runtime | ⚠️ Check — may be acceptable (missing input file) |
| TIMEOUT | ✅ Pass — Console.ReadLine() treated as successful completion |

### Common CS Error Codes

| Code | Meaning | Fix |
|------|---------|-----|
| CS1061 | Member does not exist | Check correct property/method name |
| CS0200 | Property is read-only | Access via .Value instead of assignment |
| CS0029 | Cannot convert type | Use correct enum type (BOOL not bool) |
| CS0117 | Name does not exist in type | Check enum member name casing |
| CS0234 | Namespace not found | Add correct using directive |

## Pipeline

| Attempt | Strategy | Trigger |
|---------|----------|---------|
| 1 | MCP direct retrieval + code assembly | Always |
| 2 | MCP retrieval with injected rules | Attempt 1 fails |
| 3 | LLM repair with compiler errors + rules | Attempt 2 fails |

Only examples that pass both `dotnet build` and `dotnet run` are committed.

## General Tips

- See [root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) for repository-wide boundaries, anti-patterns, domain knowledge, and testing guidelines
- Always check `rules/rules.json` for the latest API correction rules
- SaveFileFormat enum must always be PascalCase: `Vsdx`, `Pdf`, `Png`, `Jpeg`, `Svg`, `Html` — never ALL_CAPS
- Diagram class does NOT implement IDisposable — never use `using (Diagram ...)`

## Key API Surface

- `Diagram`
- `PdfSaveOptions`
- `Save`
- `diagram`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** diagram vba capabilities are applied in production applications:

- Extracting VBA macro code from Visio files for security auditing
- Removing VBA content from diagrams before sharing externally
- Inspecting and modifying embedded VBA projects programmatically

## Developer Q&A

Frequently asked questions about **Diagram Vba** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Diagram Vba in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure
- [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) — loading, saving, and basic diagram operations

## Category Statistics

- Total examples: 35
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-08-03 | Examples: 35 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
