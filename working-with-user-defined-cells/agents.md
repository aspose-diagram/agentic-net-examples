---
category: working-with-user-defined-cells
display_name: Working With User Defined Cells
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 30
pass_rate: 100.0
generated: 2026-06-12
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With User Defined Cells

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With User Defined Cells** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 30 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-12 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With User Defined Cells** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with user defined cells operations.
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
| `Aspose.Diagram` | 30 | Core diagram API |
| `System` | 30 | Console, Math, DateTime, Exception |
| `System.IO` | 16 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 11 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 8 | List, Dictionary, HashSet |
| `Aspose.Cells` | 1 | Supporting utilities |
| `System.Text.Json` | 1 | JSON serialization |
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

## Domain Knowledge

Category-specific API rules and gotchas:

- USER-DEFINED CELLS — Accessed via shape.Users collection. Each element is an Aspose.Diagram.User object containing Name, NameU, Value, and Prompt properties.
- READ USER-DEFINED CELLS — Iterate shape.Users: foreach (User user in shape.Users) { Console.WriteLine(user.Name + ": " + user.Value.Val); }
- CREATE USER-DEFINED CELL — Create a User instance, set Name and Value.Val, then add to shape.Users: User user = new User(); user.Name = "UserDefineCell"; user.Value.Val = "800"; shape.Users.Add(user);
- RETRIEVE USER-DEFINED CELLS FROM SHAPESHEET — Iterate all pages and shapes, then iterate shape.Users: foreach (Aspose.Diagram.Page objPage in diagram.Pages) { foreach (Aspose.Diagram.Shape objShape in objPage.Shapes) { foreach (Aspose.Diagram.User objUserField in objShape.Users) { Console.WriteLine(objUserField.NameU + " " + objUserField.Value.Val + " " + objUserField.Prompt.Value); } } }
- User properties: Name (string) — row name; NameU (string) — universal row name; Value.Val (string) — the cell value; Prompt.Value (string) — the prompt/description text.
- To get a specific user cell by name: iterate shape.Users and compare user.Name == "targetName" or user.NameU == "targetNameU".
- shape.Users.Add(user) adds a new User-defined cell row to the shape's ShapeSheet Users section.
- Protection properties that use BOOL.True/BOOL.False via .Value: LockCustProp, LockBegin, LockCalcWH, LockCrop, LockDelete, LockEnd, LockFormat, LockFromGroupFormat, LockGroup, LockHeight, LockMoveX, LockMoveY, LockRotate, LockSelect, LockTextEdit, LockThemeColors, LockThemeConnectors, LockThemeFonts, LockThemeIndex, LockVtxEdit, LockWidth.
- The ONLY correct way to access user-defined cells is via shape.Users collection: foreach (User user in shape.Users). Access user.Name, user.NameU, user.Value.Val, user.Prompt.Value.
- For user-defined cell data type validation, iterate shape.Users and use double.TryParse(), int.TryParse(), or DateTime.TryParse() on user.Value.Val to check data types programmatically.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-new-user-defined-cell-with-a-custom-name-to-every-shape-in-a-loaded-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/add-a-new-user-defined-cell-with-a-custom-name-to-every-shape-in-a-loaded-diagram.cs) | `Diagram`, `Pages`, `Save` | Add a new user defined cell with a custom name to every shape in a loaded diagram |
| [apply-a-custom-namespace-prefix-to-all-user-defined-cells-when-exporting-the-diagram-to-xml.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/apply-a-custom-namespace-prefix-to-all-user-defined-cells-when-exporting-the-diagram-to-xml.cs) | `Diagram`, `Pages`, `Save` | Apply a custom namespace prefix to all user defined cells when exporting the diagram to xml |
| [apply-a-mathematical-expression-to-a-user-defined-cell-based-on-other-cell-values-and-save-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/apply-a-mathematical-expression-to-a-user-defined-cell-based-on-other-cell-values-and-save-changes.cs) | `Diagram`, `Pages`, `Save` | Apply a mathematical expression to a user defined cell based on other cell values and save changes |
| [apply-localization-by-translating-user-defined-cell-text-values-based-on-the-target-language-settings.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/apply-localization-by-translating-user-defined-cell-text-values-based-on-the-target-language-settings.cs) | `Diagram`, `Pages`, `Save` | Apply localization by translating user defined cell text values based on the target language settings |
| [clone-a-shape-copy-its-user-defined-cells-and-insert-the-clone-into-a-different-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/clone-a-shape-copy-its-user-defined-cells-and-insert-the-clone-into-a-different-page.cs) | `Diagram`, `Page`, `Pages` | Clone a shape copy its user defined cells and insert the clone into a different page |
| [create-a-batch-process-that-updates-a-specific-user-defined-cell-across-multiple-visio-files-in-a-folder.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/create-a-batch-process-that-updates-a-specific-user-defined-cell-across-multiple-visio-files-in-a-folder.cs) | `Diagram`, `Pages`, `Save` | Create a batch process that updates a specific user defined cell across multiple visio files in a folder |
| [create-a-diagnostic-tool-that-lists-shapes-with-missing-or-duplicate-user-defined-cell-names.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/create-a-diagnostic-tool-that-lists-shapes-with-missing-or-duplicate-user-defined-cell-names.cs) | `Diagram`, `Pages`, `Shapes` | Create a diagnostic tool that lists shapes with missing or duplicate user defined cell names |
| [create-a-template-diagram-with-predefined-user-defined-cells-for-reuse-in-automated-document-generation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/create-a-template-diagram-with-predefined-user-defined-cells-for-reuse-in-automated-document-generation.cs) | `Diagram`, `Page`, `Pages` | Create a template diagram with predefined user defined cells for reuse in automated document generation |
| [create-a-ui-dialog-that-allows-users-to-edit-user-defined-cell-values-interactively.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/create-a-ui-dialog-that-allows-users-to-edit-user-defined-cell-values-interactively.cs) | `Diagram`, `Pages`, `Save` | Create a ui dialog that allows users to edit user defined cell values interactively |
| [delete-all-user-defined-cells-that-contain-empty-values-across-all-pages-of-a-visio-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/delete-all-user-defined-cells-that-contain-empty-values-across-all-pages-of-a-visio-document.cs) | `Diagram`, `Pages`, `Save` | Delete all user defined cells that contain empty values across all pages of a visio document |
| [export-diagram-with-user-defined-cells-to-svg-preserving-cell-metadata-as-custom-attributes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/export-diagram-with-user-defined-cells-to-svg-preserving-cell-metadata-as-custom-attributes.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Export diagram with user defined cells to svg preserving cell metadata as custom attributes |
| [export-user-defined-cell-data-to-a-csv-file-including-shape-identifiers-and-cell-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/export-user-defined-cell-data-to-a-csv-file-including-shape-identifiers-and-cell-values.cs) | `Diagram`, `Pages`, `Shapes` | Export user defined cell data to a csv file including shape identifiers and cell values |
| [extract-user-defined-cell-formulas-and-evaluate-them-using-the-built-in-expression-engine.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/extract-user-defined-cell-formulas-and-evaluate-them-using-the-built-in-expression-engine.cs) | `Diagram`, `Pages`, `Shapes` | Extract user defined cell formulas and evaluate them using the built in expression engine |
| [filter-shapes-to-process-only-those-containing-a-specific-user-defined-cell-name-pattern.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/filter-shapes-to-process-only-those-containing-a-specific-user-defined-cell-name-pattern.cs) | `Diagram`, `Pages`, `Save` | Filter shapes to process only those containing a specific user defined cell name pattern |
| [generate-a-report-summarizing-the-count-of-user-defined-cells-per-shape-category-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/generate-a-report-summarizing-the-count-of-user-defined-cells-per-shape-category-in-the-diagram.cs) | `Diagram`, `Pages`, `Shapes` | Generate a report summarizing the count of user defined cells per shape category in the diagram |
| [generate-a-visual-preview-of-shape-changes-after-modifying-user-defined-cell-values-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/generate-a-visual-preview-of-shape-changes-after-modifying-user-defined-cell-values-programmatically.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate a visual preview of shape changes after modifying user defined cell values programmatically |
| [implement-a-rollback-mechanism-that-restores-previous-user-defined-cell-values-if-validation-fails.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/implement-a-rollback-mechanism-that-restores-previous-user-defined-cell-values-if-validation-fails.cs) | `Diagram`, `Pages`, `Save` | Implement a rollback mechanism that restores previous user defined cell values if validation fails |
| [implement-a-versioning-system-that-records-changes-to-user-defined-cells-in-a-separate-log-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/implement-a-versioning-system-that-records-changes-to-user-defined-cells-in-a-separate-log-file.cs) | `Diagram`, `Pages`, `Save` | Implement a versioning system that records changes to user defined cells in a separate log file |
| [implement-error-handling-for-missing-user-defined-cells-when-performing-calculations-on-diagram-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/implement-error-handling-for-missing-user-defined-cells-when-performing-calculations-on-diagram-shapes.cs) | `Diagram`, `Pages`, `Save` | Implement error handling for missing user defined cells when performing calculations on diagram shapes |
| [import-a-spreadsheet-and-map-its-columns-to-corresponding-user-defined-cells-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/import-a-spreadsheet-and-map-its-columns-to-corresponding-user-defined-cells-in-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Import a spreadsheet and map its columns to corresponding user defined cells in the diagram |
| [import-user-defined-cell-values-from-a-json-file-and-assign-them-to-matching-shapes-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/import-user-defined-cell-values-from-a-json-file-and-assign-them-to-matching-shapes-in-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Import user defined cell values from a json file and assign them to matching shapes in the diagram |
| [iterate-through-shapes-and-log-each-user-defined-cell-s-name-and-current-value-to-a-text-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/iterate-through-shapes-and-log-each-user-defined-cell-s-name-and-current-value-to-a-text-file.cs) | `Diagram`, `Pages`, `Shapes` | Iterate through shapes and log each user defined cell s name and current value to a text file |
| [load-a-visio-file-and-retrieve-values-of-all-user-defined-cells-from-each-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/load-a-visio-file-and-retrieve-values-of-all-user-defined-cells-from-each-shape.cs) | `Diagram`, `Pages`, `Shapes` | Load a visio file and retrieve values of all user defined cells from each shape |
| [set-a-user-defined-cell-to-read-only-mode-to-prevent-further-modifications-during-runtime.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/set-a-user-defined-cell-to-read-only-mode-to-prevent-further-modifications-during-runtime.cs) | `AddShape`, `Diagram`, `Pages` | Set a user defined cell to read only mode to prevent further modifications during runtime |
| [synchronize-user-defined-cell-values-between-two-diagrams-to-maintain-consistent-metadata-across-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/synchronize-user-defined-cell-values-between-two-diagrams-to-maintain-consistent-metadata-across-files.cs) | `Diagram`, `Shapes`, `User` | Synchronize user defined cell values between two diagrams to maintain consistent metadata across files |
| [update-the-formula-of-a-specific-user-defined-cell-and-save-the-diagram-in-vdx-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/update-the-formula-of-a-specific-user-defined-cell-and-save-the-diagram-in-vdx-format.cs) | `Diagram`, `Pages`, `Save` | Update the formula of a specific user defined cell and save the diagram in vdx format |
| [use-conditional-formatting-to-change-shape-colors-based-on-values-of-associated-user-defined-cells.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/use-conditional-formatting-to-change-shape-colors-based-on-values-of-associated-user-defined-cells.cs) | `Diagram`, `Pages`, `Save` | Use conditional formatting to change shape colors based on values of associated user defined cells |
| [use-multithreading-to-concurrently-update-user-defined-cells-in-large-diagrams-for-performance-improvement.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/use-multithreading-to-concurrently-update-user-defined-cells-in-large-diagrams-for-performance-improvement.cs) | `Diagram`, `Pages`, `Save` | Use multithreading to concurrently update user defined cells in large diagrams for performance improvement |
| [validate-that-required-user-defined-cells-exist-on-each-shape-before-exporting-the-diagram-to-pdf.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/validate-that-required-user-defined-cells-exist-on-each-shape-before-exporting-the-diagram-to-pdf.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Validate that required user defined cells exist on each shape before exporting the diagram to pdf |
| [validate-user-defined-cell-data-types-against-a-schema-before-saving-the-diagram-to-vsdx.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells/validate-user-defined-cell-data-types-against-a-schema-before-saving-the-diagram-to-vsdx.cs) | `Diagram`, `Pages`, `Save` | Validate user defined cell data types against a schema before saving the diagram to vsdx |

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

*Auto-generated by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) · 2026-06-12*
