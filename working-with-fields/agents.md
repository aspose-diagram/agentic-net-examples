---
category: working-with-fields
display_name: Working With Fields
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 33
pass_rate: 100.0
generated: 2026-06-08
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Fields

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Fields** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 33 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-08 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Fields** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with fields operations.
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
| `System` | 33 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 32 | Core diagram API |
| `System.IO` | 18 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 9 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 3 | List, Dictionary, HashSet |
| `System.Text.Json` | 1 | JSON serialization |
| `Aspose.Diagram.Manipulation` | 1 | Supporting utilities |
| `System.Diagnostics` | 1 | Supporting utilities |
| `System.Threading.Tasks` | 1 | Supporting utilities |

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
| [add-a-field-only-to-shapes-that-have-a-specific-custom-property-set-to-true.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/add-a-field-only-to-shapes-that-have-a-specific-custom-property-set-to-true.cs) | `Diagram`, `Pages`, `Save` | Add a field only to shapes that have a specific custom property set to true |
| [assign-a-static-string-value-to-a-field-to-display-custom-text-on-the-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/assign-a-static-string-value-to-a-field-to-display-custom-text-on-the-shape.cs) | `Diagram`, `Pages`, `Save` | Assign a static string value to a field to display custom text on the shape |
| [batch-remove-a-specific-field-from-every-shape-in-multiple-diagrams-located-in-a-shared-folder.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/batch-remove-a-specific-field-from-every-shape-in-multiple-diagrams-located-in-a-shared-folder.cs) | `Diagram`, `Pages`, `Save` | Batch remove a specific field from every shape in multiple diagrams located in a shared folder |
| [catch-exceptions-when-a-specified-field-does-not-exist-and-log-detailed-error-information.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/catch-exceptions-when-a-specified-field-does-not-exist-and-log-detailed-error-information.cs) | `Diagram`, `Pages`, `Save` | Catch exceptions when a specified field does not exist and log detailed error information |
| [check-whether-a-field-already-exists-on-a-shape-before-attempting-to-create-a-duplicate-entry.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/check-whether-a-field-already-exists-on-a-shape-before-attempting-to-create-a-duplicate-entry.cs) | `Diagram`, `Pages`, `Save` | Check whether a field already exists on a shape before attempting to create a duplicate entry |
| [clone-an-existing-shape-and-copy-its-fields-to-the-new-shape-to-preserve-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/clone-an-existing-shape-and-copy-its-fields-to-the-new-shape-to-preserve-metadata.cs) | `AddShape`, `Diagram`, `Pages` | Clone an existing shape and copy its fields to the new shape to preserve metadata |
| [create-a-field-whose-formula-references-another-shape-s-height-to-calculate-proportional-scaling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/create-a-field-whose-formula-references-another-shape-s-height-to-calculate-proportional-scaling.cs) | `AddShape`, `Diagram`, `Pages` | Create a field whose formula references another shape s height to calculate proportional scaling |
| [delete-a-field-from-a-shape-using-its-index-position-within-the-fields-collection-for-precise-removal.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/delete-a-field-from-a-shape-using-its-index-position-within-the-fields-collection-for-precise-removal.cs) | `Diagram`, `Pages`, `Save` | Delete a field from a shape using its index position within the fields collection for precise removal |
| [export-the-extracted-field-information-of-a-shape-to-a-json-file-for-external-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/export-the-extracted-field-information-of-a-shape-to-a-json-file-for-external-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Export the extracted field information of a shape to a json file for external analysis |
| [export-the-updated-diagram-to-a-new-file-name-to-preserve-the-original-version-unchanged.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/export-the-updated-diagram-to-a-new-file-name-to-preserve-the-original-version-unchanged.cs) | `Diagram`, `Pages`, `Save` | Export the updated diagram to a new file name to preserve the original version unchanged |
| [implement-try-catch-blocks-around-field-insertion-code-to-capture-and-log-any-runtime-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/implement-try-catch-blocks-around-field-insertion-code-to-capture-and-log-any-runtime-errors.cs) | `Diagram`, `Pages`, `Save` | Implement try catch blocks around field insertion code to capture and log any runtime errors |
| [insert-a-date-time-field-that-automatically-displays-the-current-system-timestamp-on-the-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/insert-a-date-time-field-that-automatically-displays-the-current-system-timestamp-on-the-shape.cs) | `Diagram`, `Save`, `diagram` | Insert a date time field that automatically displays the current system timestamp on the shape |
| [insert-a-field-into-a-shape-and-connect-it-to-another-shape-using-a-dynamic-connector.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/insert-a-field-into-a-shape-and-connect-it-to-another-shape-using-a-dynamic-connector.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Insert a field into a shape and connect it to another shape using a dynamic connector |
| [insert-a-field-into-a-shape-s-geometry-at-a-defined-coordinate-to-display-dynamic-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/insert-a-field-into-a-shape-s-geometry-at-a-defined-coordinate-to-display-dynamic-text.cs) | `AddShape`, `Diagram`, `Pages` | Insert a field into a shape s geometry at a defined coordinate to display dynamic text |
| [insert-a-field-into-a-shape-using-its-name-property-to-locate-the-target-element.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/insert-a-field-into-a-shape-using-its-name-property-to-locate-the-target-element.cs) | `Diagram`, `Pages`, `Save` | Insert a field into a shape using its name property to locate the target element |
| [insert-a-newly-created-custom-field-into-a-shape-identified-by-its-unique-id.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/insert-a-newly-created-custom-field-into-a-shape-identified-by-its-unique-id.cs) | `Diagram`, `Pages`, `Save` | Insert a newly created custom field into a shape identified by its unique id |
| [iterate-over-all-shapes-in-the-diagram-and-add-a-timestamp-field-to-each-one.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/iterate-over-all-shapes-in-the-diagram-and-add-a-timestamp-field-to-each-one.cs) | `Diagram`, `Pages`, `Save` | Iterate over all shapes in the diagram and add a timestamp field to each one |
| [load-a-visio-diagram-from-a-file-path-and-access-a-specific-shape-by-id.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/load-a-visio-diagram-from-a-file-path-and-access-a-specific-shape-by-id.cs) | `Diagram`, `Pages`, `Shapes` | Load a visio diagram from a file path and access a specific shape by id |
| [log-each-field-operation-result-to-the-console-for-real-time-monitoring-during-development.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/log-each-field-operation-result-to-the-console-for-real-time-monitoring-during-development.cs) | `Diagram`, `Pages`, `Save` | Log each field operation result to the console for real time monitoring during development |
| [loop-through-every-shape-and-update-an-existing-field-s-formula-only-if-the-shape-is-a-connector.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/loop-through-every-shape-and-update-an-existing-field-s-formula-only-if-the-shape-is-a-connector.cs) | `Diagram`, `Pages`, `Save` | Loop through every shape and update an existing field s formula only if the shape is a connector |
| [measure-execution-time-of-batch-field-updates-to-evaluate-performance-improvements-after-code-optimization.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/measure-execution-time-of-batch-field-updates-to-evaluate-performance-improvements-after-code-optimization.cs) | `Diagram`, `Pages`, `Save` | Measure execution time of batch field updates to evaluate performance improvements after code optimization |
| [perform-bulk-updates-of-field-formulas-across-several-diagrams-to-reflect-new-calculation-standards.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/perform-bulk-updates-of-field-formulas-across-several-diagrams-to-reflect-new-calculation-standards.cs) | `Diagram`, `Pages`, `Save` | Perform bulk updates of field formulas across several diagrams to reflect new calculation standards |
| [process-all-visio-files-in-a-directory-adding-a-custom-field-to-each-shape-across-every-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/process-all-visio-files-in-a-directory-adding-a-custom-field-to-each-shape-across-every-diagram.cs) | `Diagram`, `Pages`, `Save` | Process all visio files in a directory adding a custom field to each shape across every diagram |
| [refresh-the-result-of-a-field-after-modifying-the-shape-s-geometry-to-ensure-accurate-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/refresh-the-result-of-a-field-after-modifying-the-shape-s-geometry-to-ensure-accurate-values.cs) | `Diagram`, `Pages`, `Save` | Refresh the result of a field after modifying the shape s geometry to ensure accurate values |
| [remove-a-field-from-a-shape-by-specifying-the-field-s-name-to-clean-up-unused-data.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/remove-a-field-from-a-shape-by-specifying-the-field-s-name-to-clean-up-unused-data.cs) | `Diagram`, `Pages`, `Save` | Remove a field from a shape by specifying the field s name to clean up unused data |
| [remove-all-existing-fields-from-a-shape-before-adding-a-fresh-set-of-updated-fields.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/remove-all-existing-fields-from-a-shape-before-adding-a-fresh-set-of-updated-fields.cs) | `Diagram`, `Pages`, `Save` | Remove all existing fields from a shape before adding a fresh set of updated fields |
| [retrieve-the-list-of-field-names-and-their-data-types-from-a-given-shape-for-inspection.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/retrieve-the-list-of-field-names-and-their-data-types-from-a-given-shape-for-inspection.cs) | `Diagram`, `Pages`, `Save` | Retrieve the list of field names and their data types from a given shape for inspection |
| [save-the-modified-visio-diagram-back-to-its-original-file-location-after-completing-field-operations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/save-the-modified-visio-diagram-back-to-its-original-file-location-after-completing-field-operations.cs) | `Pages`, `Save`, `diagram` | Save the modified visio diagram back to its original file location after completing field operations |
| [set-the-formula-of-an-existing-field-to-calculate-the-shape-s-area-based-on-its-dimensions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/set-the-formula-of-an-existing-field-to-calculate-the-shape-s-area-based-on-its-dimensions.cs) | `Diagram`, `Pages`, `Save` | Set the formula of an existing field to calculate the shape s area based on its dimensions |
| [update-a-field-s-formula-to-reference-another-shape-s-data-for-dynamic-calculations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/update-a-field-s-formula-to-reference-another-shape-s-data-for-dynamic-calculations.cs) | `Diagram`, `Pages`, `Save` | Update a field s formula to reference another shape s data for dynamic calculations |
| [use-asynchronous-methods-to-load-a-visio-file-and-modify-fields-without-blocking-the-ui-thread.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/use-asynchronous-methods-to-load-a-visio-file-and-modify-fields-without-blocking-the-ui-thread.cs) | `Diagram`, `Pages`, `Save` | Use asynchronous methods to load a visio file and modify fields without blocking the ui thread |
| [validate-field-results-after-modification-by-comparing-calculated-values-against-expected-thresholds.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/validate-field-results-after-modification-by-comparing-calculated-values-against-expected-thresholds.cs) | `Diagram`, `Pages`, `Save` | Validate field results after modification by comparing calculated values against expected thresholds |
| [validate-that-a-field-s-formula-syntax-is-correct-before-applying-it-to-avoid-runtime-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields/validate-that-a-field-s-formula-syntax-is-correct-before-applying-it-to-avoid-runtime-errors.cs) | `Diagram`, `Pages`, `Save` | Validate that a field s formula syntax is correct before applying it to avoid runtime errors |

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

---

*Auto-generated by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) · 2026-06-08*
