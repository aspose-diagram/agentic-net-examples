---
category: events-section-in-the-shapesheet
display_name: Events Section In The Shapesheet
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 33
pass_rate: 100.0
generated: 2026-06-05
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Events Section In The Shapesheet

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Events Section In The Shapesheet** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 33 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-05 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Events Section In The Shapesheet** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for events section in the shapesheet operations.
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
| `Aspose.Diagram` | 33 | Core diagram API |
| `System` | 33 | Console, Math, DateTime, Exception |
| `System.IO` | 21 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 8 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 6 | List, Dictionary, HashSet |
| `System.Text.RegularExpressions` | 3 | Supporting utilities |
| `System.Reflection` | 3 | Supporting utilities |
| `Aspose.Diagram.Vba` | 1 | Supporting utilities |

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
| [add-a-timestamp-to-the-eventafterupdate-cell-of-each-shape-to-track-modification-times.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/add-a-timestamp-to-the-eventafterupdate-cell-of-each-shape-to-track-modification-times.cs) | `Diagram`, `Pages`, `Save` | Add a timestamp to the eventafterupdate cell of each shape to track modification times |
| [apply-a-global-eventvalidate-formula-that-enforces-naming-conventions-for-shape-titles.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/apply-a-global-eventvalidate-formula-that-enforces-naming-conventions-for-shape-titles.cs) | `Diagram`, `Pages`, `Save` | Apply a global eventvalidate formula that enforces naming conventions for shape titles |
| [apply-conditional-formatting-to-shapes-based-on-the-result-of-their-eventcalc-cell-evaluation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/apply-conditional-formatting-to-shapes-based-on-the-result-of-their-eventcalc-cell-evaluation.cs) | `Diagram`, `Pages`, `Save` | Apply conditional formatting to shapes based on the result of their eventcalc cell evaluation |
| [automate-the-removal-of-duplicate-eventcomment-cells-to-streamline-diagram-metadata-across-all-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/automate-the-removal-of-duplicate-eventcomment-cells-to-streamline-diagram-metadata-across-all-pages.cs) | `Diagram`, `Pages`, `Save` | Automate the removal of duplicate eventcomment cells to streamline diagram metadata across all pages |
| [batch-update-the-eventshapeadded-cell-to-assign-unique-identifiers-based-on-shape-index.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/batch-update-the-eventshapeadded-cell-to-assign-unique-identifiers-based-on-shape-index.cs) | `Diagram`, `Pages`, `Save` | Batch update the eventshapeadded cell to assign unique identifiers based on shape index |
| [compare-eventcell-values-between-two-versions-of-a-diagram-to-detect-changes-in-event-logic.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/compare-eventcell-values-between-two-versions-of-a-diagram-to-detect-changes-in-event-logic.cs) | `Diagram`, `diagram`, `page` | Compare eventcell values between two versions of a diagram to detect changes in event logic |
| [configure-the-eventshapedeleted-cell-to-trigger-a-cleanup-routine-that-removes-related-custom-data.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/configure-the-eventshapedeleted-cell-to-trigger-a-cleanup-routine-that-removes-related-custom-data.cs) | `Diagram`, `Pages`, `Save` | Configure the eventshapedeleted cell to trigger a cleanup routine that removes related custom data |
| [configure-the-eventshaperesized-cell-to-maintain-aspect-ratio-by-applying-a-scaling-formula.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/configure-the-eventshaperesized-cell-to-maintain-aspect-ratio-by-applying-a-scaling-formula.cs) | `AddShape`, `Diagram`, `Pages` | Configure the eventshaperesized cell to maintain aspect ratio by applying a scaling formula |
| [copy-eventformula-cells-from-a-template-shape-to-newly-created-shapes-in-a-batch-operation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/copy-eventformula-cells-from-a-template-shape-to-newly-created-shapes-in-a-batch-operation.cs) | `Diagram`, `Pages`, `Save` | Copy eventformula cells from a template shape to newly created shapes in a batch operation |
| [create-a-backup-of-the-eventsection-before-performing-bulk-modifications-to-enable-rollback.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/create-a-backup-of-the-eventsection-before-performing-bulk-modifications-to-enable-rollback.cs) | `Diagram`, `Pages`, `Save` | Create a backup of the eventsection before performing bulk modifications to enable rollback |
| [create-a-batch-process-that-disables-eventmousedown-cells-for-shapes-marked-as-read-only.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/create-a-batch-process-that-disables-eventmousedown-cells-for-shapes-marked-as-read-only.cs) | `Diagram`, `Pages`, `Save` | Create a batch process that disables eventmousedown cells for shapes marked as read only |
| [create-a-custom-event-handler-that-logs-shape-ids-whenever-the-eventmouseleave-cell-fires.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/create-a-custom-event-handler-that-logs-shape-ids-whenever-the-eventmouseleave-cell-fires.cs) | `Diagram`, `Pages`, `Save` | Create a custom event handler that logs shape ids whenever the eventmouseleave cell fires |
| [create-a-utility-that-clones-eventsection-cells-from-a-master-shape-to-a-group-of-duplicates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/create-a-utility-that-clones-eventsection-cells-from-a-master-shape-to-a-group-of-duplicates.cs) | `Diagram`, `Masters`, `Pages` | Create a utility that clones eventsection cells from a master shape to a group of duplicates |
| [export-eventcell-values-of-every-shape-to-a-csv-file-for-external-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/export-eventcell-values-of-every-shape-to-a-csv-file-for-external-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Export eventcell values of every shape to a csv file for external analysis |
| [extract-eventcell-formulas-and-visualize-them-as-a-directed-graph-to-analyze-dependencies.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/extract-eventcell-formulas-and-visualize-them-as-a-directed-graph-to-analyze-dependencies.cs) | `Diagram`, `Pages`, `Shapes` | Extract eventcell formulas and visualize them as a directed graph to analyze dependencies |
| [generate-a-report-listing-shapes-with-missing-eventmousedown-definitions-for-quality-assurance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/generate-a-report-listing-shapes-with-missing-eventmousedown-definitions-for-quality-assurance.cs) | `Diagram`, `Pages`, `Shapes` | Generate a report listing shapes with missing eventmousedown definitions for quality assurance |
| [generate-documentation-summarizing-all-custom-eventsection-configurations-applied-to-a-diagram-for-developers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/generate-documentation-summarizing-all-custom-eventsection-configurations-applied-to-a-diagram-for-developers.cs) | `Diagram`, `Pages`, `Shapes` | Generate documentation summarizing all custom eventsection configurations applied to a diagram for developers |
| [implement-a-validation-routine-that-ensures-eventcalc-formulas-reference-only-existing-shape-ids.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/implement-a-validation-routine-that-ensures-eventcalc-formulas-reference-only-existing-shape-ids.cs) | `Diagram`, `Pages`, `Shapes` | Implement a validation routine that ensures eventcalc formulas reference only existing shape ids |
| [import-event-definitions-from-a-csv-file-and-apply-them-to-corresponding-shapes-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/import-event-definitions-from-a-csv-file-and-apply-them-to-corresponding-shapes-programmatically.cs) | `Diagram`, `Pages`, `Save` | Import event definitions from a csv file and apply them to corresponding shapes programmatically |
| [iterate-through-all-shapes-and-prepend-a-prefix-to-their-eventdata1-cell-formulas.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/iterate-through-all-shapes-and-prepend-a-prefix-to-their-eventdata1-cell-formulas.cs) | `Diagram`, `Pages`, `Save` | Iterate through all shapes and prepend a prefix to their eventdata1 cell formulas |
| [load-a-visio-diagram-and-set-the-eventdblclick-cell-for-a-specific-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/load-a-visio-diagram-and-set-the-eventdblclick-cell-for-a-specific-shape.cs) | `Diagram`, `Pages`, `Save` | Load a visio diagram and set the eventdblclick cell for a specific shape |
| [merge-two-diagrams-by-copying-eventsection-cells-from-source-shapes-to-matching-target-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/merge-two-diagrams-by-copying-eventsection-cells-from-source-shapes-to-matching-target-shapes.cs) | `Diagram` | Merge two diagrams by copying eventsection cells from source shapes to matching target shapes |
| [programmatically-disable-all-eventmouseover-cells-to-improve-diagram-rendering-performance-during-batch-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/programmatically-disable-all-eventmouseover-cells-to-improve-diagram-rendering-performance-during-batch-processing.cs) | `Diagram`, `Pages`, `Save` | Programmatically disable all eventmouseover cells to improve diagram rendering performance during batch processing |
| [read-the-eventdrop-cell-values-from-all-shapes-and-log-them-to-a-text-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/read-the-eventdrop-cell-values-from-all-shapes-and-log-them-to-a-text-file.cs) | `Diagram`, `Pages`, `Shapes` | Read the eventdrop cell values from all shapes and log them to a text file |
| [remove-all-eventcomment-cells-from-a-diagram-to-clean-up-unused-event-definitions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/remove-all-eventcomment-cells-from-a-diagram-to-clean-up-unused-event-definitions.cs) | `Diagram`, `Pages`, `Save` | Remove all eventcomment cells from a diagram to clean up unused event definitions |
| [set-the-eventshapeadded-cell-to-automatically-assign-a-default-style-when-new-shapes-are-inserted.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/set-the-eventshapeadded-cell-to-automatically-assign-a-default-style-when-new-shapes-are-inserted.cs) | `AddShape`, `Diagram`, `Pages` | Set the eventshapeadded cell to automatically assign a default style when new shapes are inserted |
| [set-the-eventshapedatachanged-cell-to-invoke-a-custom-script-that-recalculates-dependent-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/set-the-eventshapedatachanged-cell-to-invoke-a-custom-script-that-recalculates-dependent-values.cs) | `Diagram`, `Pages`, `Save` | Set the eventshapedatachanged cell to invoke a custom script that recalculates dependent values |
| [set-the-eventshapedeleted-cell-to-log-deletion-timestamps-into-an-external-audit-log-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/set-the-eventshapedeleted-cell-to-log-deletion-timestamps-into-an-external-audit-log-file.cs) | `Diagram`, `Pages`, `Save` | Set the eventshapedeleted cell to log deletion timestamps into an external audit log file |
| [update-the-eventmouseenter-cell-to-trigger-a-custom-macro-across-multiple-selected-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/update-the-eventmouseenter-cell-to-trigger-a-custom-macro-across-multiple-selected-shapes.cs) | `Diagram`, `Pages`, `Save` | Update the eventmouseenter cell to trigger a custom macro across multiple selected shapes |
| [use-a-regular-expression-to-filter-shapes-whose-eventcomment-cell-contains-a-specific-keyword.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/use-a-regular-expression-to-filter-shapes-whose-eventcomment-cell-contains-a-specific-keyword.cs) | `Diagram`, `Pages`, `Shapes` | Use a regular expression to filter shapes whose eventcomment cell contains a specific keyword |
| [use-conditional-logic-in-the-eventafterupdate-cell-to-trigger-different-actions-based-on-shape-type.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/use-conditional-logic-in-the-eventafterupdate-cell-to-trigger-different-actions-based-on-shape-type.cs) | `Diagram`, `Pages`, `Save` | Use conditional logic in the eventafterupdate cell to trigger different actions based on shape type |
| [validate-that-each-shape-s-eventvalidate-cell-contains-a-non-empty-formula-before-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/validate-that-each-shape-s-eventvalidate-cell-contains-a-non-empty-formula-before-saving.cs) | `Diagram`, `Pages`, `Save` | Validate that each shape s eventvalidate cell contains a non empty formula before saving |
| [write-a-script-that-logs-the-execution-order-of-eventmousedown-cells-across-all-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet/write-a-script-that-logs-the-execution-order-of-eventmousedown-cells-across-all-shapes.cs) | `Diagram`, `Pages`, `Shapes` | Write a script that logs the execution order of eventmousedown cells across all shapes |

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

*Auto-generated by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) · 2026-06-05*
