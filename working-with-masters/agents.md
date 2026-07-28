---
category: working-with-masters
display_name: Working With Masters
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 29
pass_rate: 100.0
generated: 2026-07-28
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Masters

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Masters** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 29 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-07-28 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Masters** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with masters operations.
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
| `System` | 29 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 28 | Core diagram API |
| `System.IO` | 20 | File, Stream, Path, Directory operations |
| `System.Collections.Generic` | 8 | List, Dictionary, HashSet |
| `Aspose.Diagram.Saving` | 7 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Xml.Linq` | 2 | Supporting utilities |
| `System.Text.Json` | 1 | JSON serialization |
| `Aspose.Drawing.Imaging` | 1 | Supporting utilities |
| `Aspose.Drawing.Text` | 1 | Font enumeration via InstalledFontCollection |
| `System.Linq` | 1 | LINQ queries on collections |

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

## Domain Knowledge

Category-specific API rules and gotchas:

- Use Diagram.Masters to access the collection of Aspose.Diagram.Master objects.
- Use foreach (Aspose.Diagram.Master master in diagram.Masters) to iterate through masters.
- Retrieve master metadata using master.ID, master.Name, master.UniqueID, master.Hidden, and related properties.
- Use diagram.Masters.GetMaster(int masterId) to retrieve a master by ID.
- Use diagram.Masters.GetMasterByName(string masterName) to retrieve a master by name.
- Use diagram.Masters.IsExist(int masterId) to check whether a master exists by ID.
- Use diagram.Masters.IsExist(string masterName) to check whether a master exists by name.
- Use Diagram.AddMaster(...) to import masters from stencils, streams, or source diagrams.
- Diagram.AddMaster supports stencil file path and master ID.
- Diagram.AddMaster supports stencil file path and master name.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-hyperlink-to-a-master-shape-that-opens-a-help-document-when-clicked.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/add-a-hyperlink-to-a-master-shape-that-opens-a-help-document-when-clicked.cs) | `Diagram`, `Save`, `diagram` | Add a hyperlink to a master shape that opens a help document when clicked |
| [apply-a-custom-style-to-all-masters-including-fill-color-and-shadow-effects.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/apply-a-custom-style-to-all-masters-including-fill-color-and-shadow-effects.cs) | `Diagram`, `Masters`, `Save` | Apply a custom style to all masters including fill color and shadow effects |
| [apply-a-rotation-transformation-to-all-masters-that-have-a-specific-tag-attribute.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/apply-a-rotation-transformation-to-all-masters-that-have-a-specific-tag-attribute.cs) | `Diagram`, `Masters`, `Save` | Apply a rotation transformation to all masters that have a specific tag attribute |
| [batch-process-a-folder-of-visio-files-to-remove-unused-masters-and-reduce-file-size.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/batch-process-a-folder-of-visio-files-to-remove-unused-masters-and-reduce-file-size.cs) | `Diagram`, `Save`, `diagram` | Batch process a folder of visio files to remove unused masters and reduce file size |
| [clone-an-existing-master-modify-its-text-block-and-apply-it-to-selected-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/clone-an-existing-master-modify-its-text-block-and-apply-it-to-selected-shapes.cs) | `Diagram`, `Masters`, `Pages` | Clone an existing master modify its text block and apply it to selected shapes |
| [convert-master-shape-text-to-uppercase-while-preserving-original-formatting-and-alignment.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/convert-master-shape-text-to-uppercase-while-preserving-original-formatting-and-alignment.cs) | `Diagram`, `Masters`, `Save` | Convert master shape text to uppercase while preserving original formatting and alignment |
| [create-a-master-containing-a-data-graphic-and-embed-it-into-a-diagram-for-dynamic-visualization.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/create-a-master-containing-a-data-graphic-and-embed-it-into-a-diagram-for-dynamic-visualization.cs) | `AddMaster`, `AddShape`, `Diagram` | Create a master containing a data graphic and embed it into a diagram for dynamic visualization |
| [create-a-master-that-includes-a-background-image-and-apply-it-to-all-pages-automatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/create-a-master-that-includes-a-background-image-and-apply-it-to-all-pages-automatically.cs) | `AddMaster`, `AddShape`, `Pages` | Create a master that includes a background image and apply it to all pages automatically |
| [create-a-new-master-shape-set-its-geometry-and-add-it-to-an-existing-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/create-a-new-master-shape-set-its-geometry-and-add-it-to-an-existing-diagram.cs) | `AddShape`, `Diagram`, `Master` | Create a new master shape set its geometry and add it to an existing diagram |
| [detect-and-report-any-circular-master-references-that-could-cause-rendering-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/detect-and-report-any-circular-master-references-that-could-cause-rendering-errors.cs) | `Diagram`, `Masters`, `Save` | Detect and report any circular master references that could cause rendering errors |
| [export-master-shape-definitions-to-an-xml-file-for-external-analysis-and-documentation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/export-master-shape-definitions-to-an-xml-file-for-external-analysis-and-documentation.cs) | `Diagram`, `Masters`, `diagram` | Export master shape definitions to an xml file for external analysis and documentation |
| [export-master-shape-geometry-to-a-json-schema-for-integration-with-external-design-tools.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/export-master-shape-geometry-to-a-json-schema-for-integration-with-external-design-tools.cs) | `Diagram`, `Masters`, `diagram` | Export master shape geometry to a json schema for integration with external design tools |
| [export-master-shape-thumbnails-as-png-images-for-use-in-a-custom-shape-picker-ui.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/export-master-shape-thumbnails-as-png-images-for-use-in-a-custom-shape-picker-ui.cs) | `Diagram`, `Masters`, `diagram` | Export master shape thumbnails as png images for use in a custom shape picker ui |
| [generate-a-report-summarizing-master-usage-frequency-across-all-pages-in-a-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/generate-a-report-summarizing-master-usage-frequency-across-all-pages-in-a-diagram.cs) | `Diagram`, `Masters`, `Pages` | Generate a report summarizing master usage frequency across all pages in a diagram |
| [implement-a-filter-to-list-masters-containing-a-specific-custom-property-value.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/implement-a-filter-to-list-masters-containing-a-specific-custom-property-value.cs) | `Diagram`, `Masters`, `diagram` | Implement a filter to list masters containing a specific custom property value |
| [implement-error-handling-to-catch-exceptions-when-loading-diagrams-with-corrupted-master-definitions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/implement-error-handling-to-catch-exceptions-when-loading-diagrams-with-corrupted-master-definitions.cs) | `Diagram`, `Masters`, `diagram` | Implement error handling to catch exceptions when loading diagrams with corrupted master definitions |
| [import-master-definitions-from-an-xml-file-and-integrate-them-into-a-loaded-visio-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/import-master-definitions-from-an-xml-file-and-integrate-them-into-a-loaded-visio-document.cs) | `AddMaster`, `Diagram`, `Save` | Import master definitions from an xml file and integrate them into a loaded visio document |
| [load-a-diagram-change-the-master-of-a-group-shape-and-preserve-its-subshape-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/load-a-diagram-change-the-master-of-a-group-shape-and-preserve-its-subshape-layout.cs) | `Diagram`, `Masters`, `Pages` | Load a diagram change the master of a group shape and preserve its subshape layout |
| [load-a-visio-file-and-list-all-master-shapes-with-their-unique-identifiers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/load-a-visio-file-and-list-all-master-shapes-with-their-unique-identifiers.cs) | `Diagram`, `Masters`, `diagram` | Load a visio file and list all master shapes with their unique identifiers |
| [programmatically-assign-a-custom-id-to-each-master-for-easier-lookup-in-large-diagrams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/programmatically-assign-a-custom-id-to-each-master-for-easier-lookup-in-large-diagrams.cs) | `Diagram`, `Masters`, `Save` | Programmatically assign a custom id to each master for easier lookup in large diagrams |
| [programmatically-reorder-masters-in-the-stencil-to-prioritize-frequently-used-shapes-for-faster-access.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/programmatically-reorder-masters-in-the-stencil-to-prioritize-frequently-used-shapes-for-faster-access.cs) | `Diagram`, `Masters`, `Save` | Programmatically reorder masters in the stencil to prioritize frequently used shapes for faster access |
| [remove-all-masters-not-referenced-on-any-page-to-clean-up-the-stencil.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/remove-all-masters-not-referenced-on-any-page-to-clean-up-the-stencil.cs) | `Diagram`, `Masters`, `Pages` | Remove all masters not referenced on any page to clean up the stencil |
| [replace-all-instances-of-a-specific-master-with-another-master-while-preserving-connections.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/replace-all-instances-of-a-specific-master-with-another-master-while-preserving-connections.cs) | `AddMaster`, `AddShape`, `Diagram` | Replace all instances of a specific master with another master while preserving connections |
| [set-a-default-master-for-newly-added-shapes-when-creating-diagrams-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/set-a-default-master-for-newly-added-shapes-when-creating-diagrams-programmatically.cs) | `AddMaster`, `AddShape`, `Diagram` | Set a default master for newly added shapes when creating diagrams programmatically |
| [set-the-default-line-weight-for-all-masters-to-a-thin-value-for-consistent-styling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/set-the-default-line-weight-for-all-masters-to-a-thin-value-for-consistent-styling.cs) | `Diagram`, `Masters`, `Save` | Set the default line weight for all masters to a thin value for consistent styling |
| [synchronize-master-definitions-across-multiple-visio-files-to-ensure-consistent-branding-organization-wide.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/synchronize-master-definitions-across-multiple-visio-files-to-ensure-consistent-branding-organization-wide.cs) | `Diagram`, `master` | Synchronize master definitions across multiple visio files to ensure consistent branding organization wide |
| [update-the-line-color-of-all-masters-that-use-a-dashed-line-style.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/update-the-line-color-of-all-masters-that-use-a-dashed-line-style.cs) | `Diagram`, `Masters`, `Save` | Update the line color of all masters that use a dashed line style |
| [validate-that-every-master-referenced-in-the-diagram-has-a-corresponding-definition-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/validate-that-every-master-referenced-in-the-diagram-has-a-corresponding-definition-file.cs) | `Diagram`, `Masters`, `Pages` | Validate that every master referenced in the diagram has a corresponding definition file |
| [validate-that-master-shapes-comply-with-company-style-guidelines-by-checking-color-and-font-settings.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters/validate-that-master-shapes-comply-with-company-style-guidelines-by-checking-color-and-font-settings.cs) | `Diagram`, `Masters`, `diagram` | Validate that master shapes comply with company style guidelines by checking color and font settings |

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

- `AddMaster`
- `AddShape`
- `Diagram`
- `Master`
- `Masters`
- `Pages`
- `Save`
- `Shape`
- `Shapes`
- `diagram`
- `master`
- `page`
- `shape`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with masters capabilities are applied in production applications:

- Creating custom shape libraries (stencils) for domain-specific diagrams
- Applying master shapes from stencils to generate consistent diagrams
- Managing master shape metadata in enterprise stencil libraries

## Developer Q&A

Frequently asked questions about **Working With Masters** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Masters in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) — loading, saving, and basic diagram operations
- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure

## Category Statistics

- Total examples: 29
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-07-28 | Examples: 29 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
