---
category: working-with-themes
display_name: Working With Themes
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 40
pass_rate: 100.0
generated: 2026-08-03
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Themes

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Themes** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 40 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Themes** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with themes operations.
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
| `System` | 40 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 39 | Core diagram API |
| `System.IO` | 33 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 18 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 6 | List, Dictionary, HashSet |
| `System.Text.Json` | 2 | JSON serialization |
| `System.Threading.Tasks` | 2 | Supporting utilities |
| `System.Linq` | 1 | LINQ queries on collections |
| `System.Data.SqlClient` | 1 | Supporting utilities |
| `System.Diagnostics` | 1 | Supporting utilities |
| `System.Threading` | 1 | Supporting utilities |

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

- APPLY PRESET THEME TO PAGE — Load diagram, get page, set page.PresetTheme: Page page = diagram.Pages[0]; page.PresetTheme = PresetThemeValue.Bubble;
- APPLY PRESET THEME VARIANT TO PAGE — Set both PresetTheme and PresetThemeVariant on the page: page.PresetTheme = PresetThemeValue.Bubble; page.PresetThemeVariant = PresetThemeVariantValue.Variant3;
- APPLY PRESET THEME TO SHAPE — Get shape from page, set shape.PresetTheme: Shape shape = diagram.Pages[0].Shapes[0]; shape.PresetTheme = PresetThemeValue.Bubble;
- NOTE — In the docs the shape is retrieved as: Shape shape = doc.Pages[0].Shapes[0]; — the variable name doc refers to the Diagram instance. Always use your own Diagram variable name.
- APPLY PRESET THEME VARIANT TO SHAPE — Set both PresetTheme and PresetThemeVariant on the shape: shape.PresetTheme = PresetThemeValue.Bubble; shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
- APPLY PRESET THEME QUICKSTYLE TO SHAPE — Set PresetTheme, PresetThemeVariant, and PresetThemeQuickStyle on the shape: shape.PresetTheme = PresetThemeValue.Bubble; shape.PresetThemeVariant = PresetThemeVariantValue.Variant3; shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
- APPLY PRESET THEME STYLE USING SetPresetThemeStyleMatrics — Set PresetTheme, PresetThemeVariant, then call shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color7);
- SetPresetThemeStyleMatrics takes two parameters: PresetStyleMatricsValue (style index) and PresetColorMatricsValue (color index).
- Valid PresetThemeValue members include: Bubble, and others. Bubble is confirmed in all doc examples.
- Valid PresetThemeVariantValue members include: Variant1, Variant2, Variant3, Variant4.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [after-applying-a-theme-extract-the-theme-s-color-palette-and-write-it-to-a-json-configuration-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/after-applying-a-theme-extract-the-theme-s-color-palette-and-write-it-to-a-json-configuration-file.cs) | `Colors`, `Diagram`, `Pages` | After applying a theme extract the theme s color palette and write it to a json configuration file |
| [apply-a-different-preset-theme-variant-to-shapes-on-a-page-based-on-their-layer-membership.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-different-preset-theme-variant-to-shapes-on-a-page-based-on-their-layer-membership.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a different preset theme variant to shapes on a page based on their layer membership |
| [apply-a-preset-theme-quickstyle-to-all-shapes-on-a-page-that-use-a-particular-master-style.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-quickstyle-to-all-shapes-on-a-page-that-use-a-particular-master-style.cs) | `Diagram`, `Pages`, `Save` | Apply a preset theme quickstyle to all shapes on a page that use a particular master style |
| [apply-a-preset-theme-quickstyle-to-shapes-filtered-by-their-geometry-type-such-as-rectangles-only.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-quickstyle-to-shapes-filtered-by-their-geometry-type-such-as-rectangles-only.cs) | `Diagram`, `Pages`, `Save` | Apply a preset theme quickstyle to shapes filtered by their geometry type such as rectangles only |
| [apply-a-preset-theme-quickstyle-to-shapes-selected-via-a-linq-query-based-on-their-text-content.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-quickstyle-to-shapes-selected-via-a-linq-query-based-on-their-text-content.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme quickstyle to shapes selected via a linq query based on their text content |
| [apply-a-preset-theme-to-a-page-then-flatten-the-page-to-ensure-theme-persistence-in-exported-formats.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-to-a-page-then-flatten-the-page-to-ensure-theme-persistence-in-exported-formats.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme to a page then flatten the page to ensure theme persistence in exported formats |
| [apply-a-preset-theme-to-a-shape-only-if-its-current-theme-does-not-match-the-desired-preset.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-to-a-shape-only-if-its-current-theme-does-not-match-the-desired-preset.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme to a shape only if its current theme does not match the desired preset |
| [apply-a-preset-theme-to-all-shapes-that-share-a-specific-custom-property-value.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-to-all-shapes-that-share-a-specific-custom-property-value.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme to all shapes that share a specific custom property value |
| [apply-a-preset-theme-to-shapes-only-when-their-current-theme-matrix-differs-from-the-target-matrix.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-to-shapes-only-when-their-current-theme-matrix-differs-from-the-target-matrix.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme to shapes only when their current theme matrix differs from the target matrix |
| [apply-a-preset-theme-variant-to-a-shape-group-by-iterating-through-its-child-shapes-and-setting-each.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-variant-to-a-shape-group-by-iterating-through-its-child-shapes-and-setting-each.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme variant to a shape group by iterating through its child shapes and setting each |
| [apply-a-preset-theme-variant-to-a-shape-only-if-the-shape-s-existing-theme-matrix-contains-a-color.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/apply-a-preset-theme-variant-to-a-shape-only-if-the-shape-s-existing-theme-matrix-contains-a-color.cs) | `Diagram`, `Pages`, `PresetTheme` | Apply a preset theme variant to a shape only if the shape s existing theme matrix contains a color |
| [batch-process-diagrams-in-parallel-applying-a-common-preset-theme-to-each-and-saving-results-concurrently.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/batch-process-diagrams-in-parallel-applying-a-common-preset-theme-to-each-and-saving-results-concurrently.cs) | `Diagram`, `Save`, `diagram` | Batch process diagrams in parallel applying a common preset theme to each and saving results concurrently |
| [clone-the-theme-settings-from-one-page-and-apply-them-to-another-page-within-the-same-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/clone-the-theme-settings-from-one-page-and-apply-them-to-another-page-within-the-same-diagram.cs) | `Diagram`, `Pages`, `Save` | Clone the theme settings from one page and apply them to another page within the same diagram |
| [compare-the-theme-settings-of-a-shape-before-and-after-applying-a-quickstyle-to-verify-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/compare-the-theme-settings-of-a-shape-before-and-after-applying-a-quickstyle-to-verify-changes.cs) | `Diagram`, `Pages`, `PresetTheme` | Compare the theme settings of a shape before and after applying a quickstyle to verify changes |
| [create-a-configuration-file-that-maps-page-names-to-preset-themes-and-apply-them-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/create-a-configuration-file-that-maps-page-names-to-preset-themes-and-apply-them-programmatically.cs) | `Diagram`, `Pages`, `PresetTheme` | Create a configuration file that maps page names to preset themes and apply them programmatically |
| [create-a-script-that-reads-theme-preferences-from-a-database-and-applies-them-to-corresponding-diagram-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/create-a-script-that-reads-theme-preferences-from-a-database-and-applies-them-to-corresponding-diagram-pages.cs) | `Diagram`, `Pages`, `Save` | Create a script that reads theme preferences from a database and applies them to corresponding diagram pages |
| [create-a-unit-test-that-verifies-a-shape-s-presettheme-property-changes-after-applying-a-new-theme.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/create-a-unit-test-that-verifies-a-shape-s-presettheme-property-changes-after-applying-a-new-theme.cs) | `Diagram`, `PresetTheme`, `shape` | Create a unit test that verifies a shape s presettheme property changes after applying a new theme |
| [export-a-themed-diagram-to-pdf-and-inspect-the-visual-consistency-across-all-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/export-a-themed-diagram-to-pdf-and-inspect-the-visual-consistency-across-all-pages.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Export a themed diagram to pdf and inspect the visual consistency across all pages |
| [export-the-diagram-after-theming-to-both-pdf-and-xps-formats-to-compare-rendering-fidelity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/export-the-diagram-after-theming-to-both-pdf-and-xps-formats-to-compare-rendering-fidelity.cs) | `Diagram`, `Save`, `XPSSaveOptions` | Export the diagram after theming to both pdf and xps formats to compare rendering fidelity |
| [export-the-themed-diagram-to-svg-format-for-web-preview-after-applying-page-level-themes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/export-the-themed-diagram-to-svg-format-for-web-preview-after-applying-page-level-themes.cs) | `Diagram`, `SVGSaveOptions` | Export the themed diagram to svg format for web preview after applying page level themes |
| [generate-a-report-listing-each-shape-s-original-theme-and-the-new-preset-theme-after-modification.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/generate-a-report-listing-each-shape-s-original-theme-and-the-new-preset-theme-after-modification.cs) | `Diagram`, `Pages`, `PresetTheme` | Generate a report listing each shape s original theme and the new preset theme after modification |
| [implement-a-retry-mechanism-when-setting-a-preset-theme-on-a-shape-that-may-be-locked.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/implement-a-retry-mechanism-when-setting-a-preset-theme-on-a-shape-that-may-be-locked.cs) | `Diagram`, `Pages`, `PresetTheme` | Implement a retry mechanism when setting a preset theme on a shape that may be locked |
| [implement-error-handling-to-catch-exceptions-when-applying-a-theme-to-a-shape-lacking-style-data.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/implement-error-handling-to-catch-exceptions-when-applying-a-theme-to-a-shape-lacking-style-data.cs) | `Diagram`, `Pages`, `PresetTheme` | Implement error handling to catch exceptions when applying a theme to a shape lacking style data |
| [implement-logging-to-capture-each-theme-application-step-including-page-name-shape-id-and-applied-preset.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/implement-logging-to-capture-each-theme-application-step-including-page-name-shape-id-and-applied-preset.cs) | `Diagram`, `Pages`, `PresetTheme` | Implement logging to capture each theme application step including page name shape id and applied preset |
| [iterate-through-all-pages-in-a-diagram-and-assign-the-same-preset-theme-variant-to-each-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/iterate-through-all-pages-in-a-diagram-and-assign-the-same-preset-theme-variant-to-each-page.cs) | `Pages`, `PresetThemeVariant`, `Save` | Iterate through all pages in a diagram and assign the same preset theme variant to each page |
| [load-a-diagram-from-a-byte-array-apply-a-theme-variant-and-return-the-modified-byte-array.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/load-a-diagram-from-a-byte-array-apply-a-theme-variant-and-return-the-modified-byte-array.cs) | `Diagram`, `Pages`, `PresetThemeVariant` | Load a diagram from a byte array apply a theme variant and return the modified byte array |
| [load-a-diagram-remove-all-existing-theme-settings-then-apply-a-fresh-preset-theme-to-standardize-appearance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/load-a-diagram-remove-all-existing-theme-settings-then-apply-a-fresh-preset-theme-to-standardize-appearance.cs) | `Diagram`, `Pages`, `PresetTheme` | Load a diagram remove all existing theme settings then apply a fresh preset theme to standardize appearance |
| [load-a-vsdx-diagram-from-disk-and-apply-a-preset-theme-to-its-first-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/load-a-vsdx-diagram-from-disk-and-apply-a-preset-theme-to-its-first-page.cs) | `Diagram` | Load a vsdx diagram from disk and apply a preset theme to its first page |
| [load-multiple-vsdx-files-from-a-folder-apply-a-uniform-preset-theme-and-save-them-back.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/load-multiple-vsdx-files-from-a-folder-apply-a-uniform-preset-theme-and-save-them-back.cs) | `Diagram`, `Save`, `diagram` | Load multiple vsdx files from a folder apply a uniform preset theme and save them back |
| [measure-the-execution-time-of-applying-a-preset-theme-to-a-large-diagram-with-thousands-of-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/measure-the-execution-time-of-applying-a-preset-theme-to-a-large-diagram-with-thousands-of-shapes.cs) | `Diagram`, `Pages`, `PresetTheme` | Measure the execution time of applying a preset theme to a large diagram with thousands of shapes |
| [reset-the-presettheme-of-a-shape-to-the-default-value-to-remove-previously-applied-styling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/reset-the-presettheme-of-a-shape-to-the-default-value-to-remove-previously-applied-styling.cs) | `Diagram`, `Pages`, `PresetTheme` | Reset the presettheme of a shape to the default value to remove previously applied styling |
| [retrieve-the-current-presetthemevariant-from-each-shape-and-log-it-for-diagnostic-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/retrieve-the-current-presetthemevariant-from-each-shape-and-log-it-for-diagnostic-purposes.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve the current presetthemevariant from each shape and log it for diagnostic purposes |
| [save-a-diagram-with-applied-themes-to-a-memory-stream-for-further-processing-without-disk-i-o.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/save-a-diagram-with-applied-themes-to-a-memory-stream-for-further-processing-without-disk-i-o.cs) | `Diagram`, `Save`, `diagram` | Save a diagram with applied themes to a memory stream for further processing without disk i o |
| [set-the-presettheme-property-on-a-specific-shape-identified-by-its-id-to-change-its-appearance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/set-the-presettheme-property-on-a-specific-shape-identified-by-its-id-to-change-its-appearance.cs) | `Diagram`, `Pages`, `PresetTheme` | Set the presettheme property on a specific shape identified by its id to change its appearance |
| [use-a-dictionary-to-map-shape-ids-to-specific-preset-themes-and-apply-them-accordingly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/use-a-dictionary-to-map-shape-ids-to-specific-preset-themes-and-apply-them-accordingly.cs) | `Diagram`, `Pages`, `PresetTheme` | Use a dictionary to map shape ids to specific preset themes and apply them accordingly |
| [use-a-loop-to-apply-different-preset-themes-to-each-page-based-on-its-index-position.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/use-a-loop-to-apply-different-preset-themes-to-each-page-based-on-its-index-position.cs) | `Diagram`, `Pages`, `PresetTheme` | Use a loop to apply different preset themes to each page based on its index position |
| [use-asynchronous-loading-of-a-vsdx-file-then-apply-a-preset-theme-to-its-pages-concurrently.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/use-asynchronous-loading-of-a-vsdx-file-then-apply-a-preset-theme-to-its-pages-concurrently.cs) | `Diagram`, `Pages`, `PresetTheme` | Use asynchronous loading of a vsdx file then apply a preset theme to its pages concurrently |
| [use-setpresetthemestylematrics-to-assign-a-custom-style-matrix-to-a-shape-for-advanced-formatting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/use-setpresetthemestylematrics-to-assign-a-custom-style-matrix-to-a-shape-for-advanced-formatting.cs) | `Diagram`, `Pages`, `Save` | Use setpresetthemestylematrics to assign a custom style matrix to a shape for advanced formatting |
| [validate-that-applying-a-preset-theme-does-not-alter-shape-geometry-by-comparing-bounding-boxes-before-and-after.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/validate-that-applying-a-preset-theme-does-not-alter-shape-geometry-by-comparing-bounding-boxes-before-and-after.cs) | `Diagram`, `Pages`, `PresetTheme` | Validate that applying a preset theme does not alter shape geometry by comparing bounding boxes before and after |
| [validate-that-the-applied-preset-theme-matches-the-expected-theme-by-comparing-theme-identifiers-after-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes/validate-that-the-applied-preset-theme-matches-the-expected-theme-by-comparing-theme-identifiers-after-saving.cs) | `Diagram`, `Page`, `Pages` | Validate that the applied preset theme matches the expected theme by comparing theme identifiers after saving |

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

- `Colors`
- `Diagram`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `PresetTheme`
- `PresetThemeVariant`
- `Prop`
- `SVGSaveOptions`
- `Save`
- `Shapes`
- `XPSSaveOptions`
- `diagram`
- `page`
- `shape`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with themes capabilities are applied in production applications:

- Applying consistent corporate branding themes across all Visio diagrams
- Dynamically switching diagram themes based on presentation context
- Standardizing diagram appearance in automated report generation

## Developer Q&A

Frequently asked questions about **Working With Themes** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Themes in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

**Q: Which `PresetThemeValue` members are confirmed to exist?**

A: `PresetThemeValue.Bubble` is confirmed. Members like `Theme1`, `Theme2`, `Flowchart` do NOT exist — they cause CS0117. Always use confirmed members from the API reference.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Working With Pages](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages) — page management and navigation
- [Visio Shape Gradient](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient) — gradient fill effects on shapes

## Category Statistics

- Total examples: 40
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-08-03 | Examples: 40 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
