---
category: document-properties
display_name: Document Properties
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 34
pass_rate: 100.0
generated: 2026-07-27
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Document Properties

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Document Properties** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 34 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-07-27 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Document Properties** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for document properties operations.
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
| `Aspose.Diagram` | 34 | Core diagram API |
| `System` | 34 | Console, Math, DateTime, Exception |
| `System.IO` | 25 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 9 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `Aspose.Diagram.Properties` | 8 | Supporting utilities |
| `System.Collections.Generic` | 5 | List, Dictionary, HashSet |
| `System.Linq` | 2 | LINQ queries on collections |
| `System.Text.RegularExpressions` | 1 | Supporting utilities |

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
| [add-a-custom-property-containing-xml-data-and-ensure-it-is-correctly-serialized-within-the-diagram-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/add-a-custom-property-containing-xml-data-and-ensure-it-is-correctly-serialized-within-the-diagram-file.cs) | `Diagram`, `Save`, `SolutionXMLs` | Add a custom property containing xml data and ensure it is correctly serialized within the diagram file |
| [add-a-custom-property-named-projectid-with-numeric-value-12345-to-the-loaded-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/add-a-custom-property-named-projectid-with-numeric-value-12345-to-the-loaded-diagram.cs) | `Diagram`, `Save`, `diagram` | Add a custom property named projectid with numeric value 12345 to the loaded diagram |
| [add-a-custom-property-with-a-date-value-formatted-as-iso-8601-and-verify-correct-storage.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/add-a-custom-property-with-a-date-value-formatted-as-iso-8601-and-verify-correct-storage.cs) | `Diagram`, `Save`, `diagram` | Add a custom property with a date value formatted as iso 8601 and verify correct storage |
| [add-a-custom-property-with-a-long-string-value-exceeding-typical-length-to-test-storage-limits.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/add-a-custom-property-with-a-long-string-value-exceeding-typical-length-to-test-storage-limits.cs) | `Diagram`, `Save`, `diagram` | Add a custom property with a long string value exceeding typical length to test storage limits |
| [after-saving-read-back-the-diagram-to-confirm-that-newly-added-custom-properties-persist-correctly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/after-saving-read-back-the-diagram-to-confirm-that-newly-added-custom-properties-persist-correctly.cs) | `Diagram`, `Save`, `diagram` | After saving read back the diagram to confirm that newly added custom properties persist correctly |
| [apply-a-conditional-update-to-custom-property-priority-setting-it-to-high-when-current-value-is-low.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/apply-a-conditional-update-to-custom-property-priority-setting-it-to-high-when-current-value-is-low.cs) | `Diagram`, `Save`, `diagram` | Apply a conditional update to custom property priority setting it to high when current value is low |
| [batch-process-a-folder-of-visio-files-adding-a-uniform-custom-property-batchid-to-each-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/batch-process-a-folder-of-visio-files-adding-a-uniform-custom-property-batchid-to-each-diagram.cs) | `Diagram`, `Save`, `diagram` | Batch process a folder of visio files adding a uniform custom property batchid to each diagram |
| [clone-the-original-diagram-into-a-new-diagram-object-while-preserving-all-custom-properties-intact.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/clone-the-original-diagram-into-a-new-diagram-object-while-preserving-all-custom-properties-intact.cs) | `Diagram` | Clone the original diagram into a new diagram object while preserving all custom properties intact |
| [compare-two-diagrams-custom-property-sets-to-identify-differences-for-version-control-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/compare-two-diagrams-custom-property-sets-to-identify-differences-for-version-control-purposes.cs) | `Diagram` | Compare two diagrams custom property sets to identify differences for version control purposes |
| [create-a-batch-script-that-updates-the-custom-property-version-across-multiple-diagrams-based-on-a-config-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/create-a-batch-script-that-updates-the-custom-property-version-across-multiple-diagrams-based-on-a-config-file.cs) | `Diagram`, `Save`, `diagram` | Create a batch script that updates the custom property version across multiple diagrams based on a config file |
| [create-a-diagnostic-routine-that-compares-embedded-api-version-against-the-current-library-version-for-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/create-a-diagnostic-routine-that-compares-embedded-api-version-against-the-current-library-version-for-consistency.cs) | `Diagram` | Create a diagnostic routine that compares embedded api version against the current library version for consistency |
| [create-a-utility-that-copies-custom-properties-from-one-diagram-to-another-preserving-original-names.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/create-a-utility-that-copies-custom-properties-from-one-diagram-to-another-preserving-original-names.cs) | `Diagram` | Create a utility that copies custom properties from one diagram to another preserving original names |
| [enumerate-all-custom-properties-in-the-diagram-and-output-each-name-value-pair-to-console.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/enumerate-all-custom-properties-in-the-diagram-and-output-each-name-value-pair-to-console.cs) | `Diagram` | Enumerate all custom properties in the diagram and output each name value pair to console |
| [export-the-diagram-to-pdf-format-ensuring-that-custom-properties-are-retained-in-the-output-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/export-the-diagram-to-pdf-format-ensuring-that-custom-properties-are-retained-in-the-output-file.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export the diagram to pdf format ensuring that custom properties are retained in the output file |
| [generate-a-report-listing-all-diagrams-and-their-respective-custom-property-counts-for-inventory-management.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/generate-a-report-listing-all-diagrams-and-their-respective-custom-property-counts-for-inventory-management.cs) | `Diagram` | Generate a report listing all diagrams and their respective custom property counts for inventory management |
| [generate-a-summary-csv-file-listing-each-diagram-filename-and-its-total-count-of-custom-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/generate-a-summary-csv-file-listing-each-diagram-filename-and-its-total-count-of-custom-properties.cs) | `Diagram` | Generate a summary csv file listing each diagram filename and its total count of custom properties |
| [implement-a-feature-to-bulk-remove-custom-properties-matching-a-regular-expression-pattern-from-multiple-diagrams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/implement-a-feature-to-bulk-remove-custom-properties-matching-a-regular-expression-pattern-from-multiple-diagrams.cs) | `Diagram`, `Save`, `diagram` | Implement a feature to bulk remove custom properties matching a regular expression pattern from multiple diagrams |
| [implement-a-feature-to-prevent-adding-duplicate-custom-property-names-by-checking-existing-collection-first.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/implement-a-feature-to-prevent-adding-duplicate-custom-property-names-by-checking-existing-collection-first.cs) | `Diagram`, `Save`, `diagram` | Implement a feature to prevent adding duplicate custom property names by checking existing collection first |
| [implement-a-method-to-retrieve-all-custom-property-names-sorted-alphabetically-for-display-in-ui.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/implement-a-method-to-retrieve-all-custom-property-names-sorted-alphabetically-for-display-in-ui.cs) | `Diagram` | Implement a method to retrieve all custom property names sorted alphabetically for display in ui |
| [implement-error-handling-to-catch-exceptions-when-attempting-to-modify-read-only-built-in-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/implement-error-handling-to-catch-exceptions-when-attempting-to-modify-read-only-built-in-properties.cs) | `Diagram`, `Save`, `diagram` | Implement error handling to catch exceptions when attempting to modify read only built in properties |
| [iterate-through-diagrams-in-a-collection-removing-any-custom-property-named-deprecatedflag-before-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/iterate-through-diagrams-in-a-collection-removing-any-custom-property-named-deprecatedflag-before-saving.cs) | `Diagram`, `Save`, `diagram` | Iterate through diagrams in a collection removing any custom property named deprecatedflag before saving |
| [load-a-diagram-add-a-timestamp-custom-property-using-current-utc-time-and-save-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/load-a-diagram-add-a-timestamp-custom-property-using-current-utc-time-and-save-changes.cs) | `Diagram`, `Save`, `diagram` | Load a diagram add a timestamp custom property using current utc time and save changes |
| [load-a-diagram-read-the-built-in-title-property-and-compare-it-against-a-predefined-template.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/load-a-diagram-read-the-built-in-title-property-and-compare-it-against-a-predefined-template.cs) | `Diagram` | Load a diagram read the built in title property and compare it against a predefined template |
| [load-a-visio-diagram-read-the-author-built-in-property-and-log-its-value.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/load-a-visio-diagram-read-the-author-built-in-property-and-log-its-value.cs) | `Diagram` | Load a visio diagram read the author built in property and log its value |
| [log-the-embedded-api-version-and-library-build-number-each-time-a-diagram-is-saved-for-tracking.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/log-the-embedded-api-version-and-library-build-number-each-time-a-diagram-is-saved-for-tracking.cs) | `Diagram`, `Save`, `diagram` | Log the embedded api version and library build number each time a diagram is saved for tracking |
| [remove-all-custom-properties-whose-values-are-null-to-clean-up-the-diagram-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/remove-all-custom-properties-whose-values-are-null-to-clean-up-the-diagram-metadata.cs) | `Diagram`, `Save`, `diagram` | Remove all custom properties whose values are null to clean up the diagram metadata |
| [remove-the-custom-property-reviewdate-from-the-diagram-to-clean-up-outdated-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/remove-the-custom-property-reviewdate-from-the-diagram-to-clean-up-outdated-metadata.cs) | `Diagram`, `Save`, `diagram` | Remove the custom property reviewdate from the diagram to clean up outdated metadata |
| [retrieve-the-automatically-embedded-api-version-information-from-the-saved-diagram-file-for-audit-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/retrieve-the-automatically-embedded-api-version-information-from-the-saved-diagram-file-for-audit-purposes.cs) | `Diagram` | Retrieve the automatically embedded api version information from the saved diagram file for audit purposes |
| [set-the-custom-property-reviewstatus-to-pending-and-export-to-svg-preserving-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/set-the-custom-property-reviewstatus-to-pending-and-export-to-svg-preserving-metadata.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Set the custom property reviewstatus to pending and export to svg preserving metadata |
| [test-that-attempting-to-delete-a-built-in-property-throws-the-expected-exception-and-is-properly-handled.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/test-that-attempting-to-delete-a-built-in-property-throws-the-expected-exception-and-is-properly-handled.cs) | `Diagram`, `Save`, `diagram` | Test that attempting to delete a built in property throws the expected exception and is properly handled |
| [update-the-existing-custom-property-status-to-the-string-value-completed-after-processing-steps.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/update-the-existing-custom-property-status-to-the-string-value-completed-after-processing-steps.cs) | `Diagram`, `Save`, `diagram` | Update the existing custom property status to the string value completed after processing steps |
| [use-a-linq-query-to-filter-custom-properties-whose-names-start-with-dept-and-list-their-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/use-a-linq-query-to-filter-custom-properties-whose-names-start-with-dept-and-list-their-values.cs) | `Diagram`, `Save`, `diagram` | Use a linq query to filter custom properties whose names start with dept and list their values |
| [validate-that-after-cloning-the-original-diagram-s-custom-properties-remain-unchanged-while-the-clone-reflects-updates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/validate-that-after-cloning-the-original-diagram-s-custom-properties-remain-unchanged-while-the-clone-reflects-updates.cs) | `Diagram` | Validate that after cloning the original diagram s custom properties remain unchanged while the clone reflects updates |
| [validate-that-built-in-properties-such-as-createddate-remain-unchanged-after-adding-custom-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties/validate-that-built-in-properties-such-as-createddate-remain-unchanged-after-adding-custom-properties.cs) | `Diagram`, `Save`, `diagram` | Validate that built in properties such as createddate remain unchanged after adding custom properties |

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
- `SVGSaveOptions`
- `Save`
- `SolutionXMLs`
- `diagram`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** document properties capabilities are applied in production applications:

- Setting document metadata (author, title, subject) before distribution
- Reading document properties for document management indexing
- Auditing diagram metadata in compliance workflows

## Developer Q&A

Frequently asked questions about **Document Properties** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Document Properties in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) — loading, saving, and basic diagram operations
- [Working With Comments](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments) — adding and reading comments
- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure

## Category Statistics

- Total examples: 34
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-07-27 | Examples: 34 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
