---
category: basic-operations
display_name: Basic Operations
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 30
pass_rate: 100.0
generated: 2026-07-27
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Basic Operations

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Basic Operations** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 30 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-07-27 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Basic Operations** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for basic operations operations.
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
| `System.IO` | 28 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 18 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `Aspose.Diagram.Manipulation` | 3 | Supporting utilities |
| `System.Diagnostics` | 1 | Supporting utilities |
| `System.Collections.Generic` | 1 | List, Dictionary, HashSet |

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
| [add-a-second-page-to-the-diagram-and-set-its-size-to-a4-dimensions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/add-a-second-page-to-the-diagram-and-set-its-size-to-a4-dimensions.cs) | `Diagram`, `Page`, `Pages` | Add a second page to the diagram and set its size to a4 dimensions |
| [add-a-text-block-to-the-shape-and-set-its-font-to-arial-size-twelve.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/add-a-text-block-to-the-shape-and-set-its-font-to-arial-size-twelve.cs) | `Diagram`, `Pages`, `Save` | Add a text block to the shape and set its font to arial size twelve |
| [adjust-svg-save-options-to-preserve-original-viewbox-and-enable-css-styling-for-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/adjust-svg-save-options-to-preserve-original-viewbox-and-enable-css-styling-for-shapes.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Adjust svg save options to preserve original viewbox and enable css styling for shapes |
| [apply-a-custom-fill-color-to-the-inserted-shape-using-rgb-values-for-teal-shade.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/apply-a-custom-fill-color-to-the-inserted-shape-using-rgb-values-for-teal-shade.cs) | `AddShape`, `Diagram`, `Pages` | Apply a custom fill color to the inserted shape using rgb values for teal shade |
| [batch-convert-a-folder-of-vdx-files-to-pdf-using-a-loop-and-custom-pdf-options.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/batch-convert-a-folder-of-vdx-files-to-pdf-using-a-loop-and-custom-pdf-options.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Batch convert a folder of vdx files to pdf using a loop and custom pdf options |
| [configure-pdf-save-options-to-embed-all-fonts-and-use-pdf-a-1b-conformance-level.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/configure-pdf-save-options-to-embed-all-fonts-and-use-pdf-a-1b-conformance-level.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Configure pdf save options to embed all fonts and use pdf a 1b conformance level |
| [convert-a-visio-file-to-swf-format-and-confirm-playback-works-in-a-web-browser.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/convert-a-visio-file-to-swf-format-and-confirm-playback-works-in-a-web-browser.cs) | `Diagram`, `Save`, `diagram` | Convert a visio file to swf format and confirm playback works in a web browser |
| [create-a-connector-between-two-shapes-and-configure-its-line-style-to-dashed-pattern.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/create-a-connector-between-two-shapes-and-configure-its-line-style-to-dashed-pattern.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Create a connector between two shapes and configure its line style to dashed pattern |
| [create-a-new-diagram-instance-and-verify-it-contains-a-single-empty-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/create-a-new-diagram-instance-and-verify-it-contains-a-single-empty-page.cs) | `Diagram`, `Pages`, `Shapes` | Create a new diagram instance and verify it contains a single empty page |
| [create-an-svg-file-from-the-diagram-and-test-that-hyperlinks-on-shapes-remain-functional.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/create-an-svg-file-from-the-diagram-and-test-that-hyperlinks-on-shapes-remain-functional.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Create an svg file from the diagram and test that hyperlinks on shapes remain functional |
| [define-image-save-options-with-300-dpi-resolution-and-png-format-for-high-quality-output.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/define-image-save-options-with-300-dpi-resolution-and-png-format-for-high-quality-output.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Define image save options with 300 dpi resolution and png format for high quality output |
| [duplicate-an-existing-shape-change-its-position-and-connect-it-to-the-original-using-a-connector.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/duplicate-an-existing-shape-change-its-position-and-connect-it-to-the-original-using-a-connector.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Duplicate an existing shape change its position and connect it to the original using a connector |
| [export-the-diagram-as-a-pdf-document-and-ensure-all-shapes-retain-their-original-colors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/export-the-diagram-as-a-pdf-document-and-ensure-all-shapes-retain-their-original-colors.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export the diagram as a pdf document and ensure all shapes retain their original colors |
| [generate-a-jpeg-image-of-the-diagram-with-150-dpi-and-embed-a-watermark-text-overlay.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/generate-a-jpeg-image-of-the-diagram-with-150-dpi-and-embed-a-watermark-text-overlay.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate a jpeg image of the diagram with 150 dpi and embed a watermark text overlay |
| [import-a-stencil-collection-from-a-vsx-file-and-list-all-available-master-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/import-a-stencil-collection-from-a-vsx-file-and-list-all-available-master-shapes.cs) | `Diagram` | Import a stencil collection from a vsx file and list all available master shapes |
| [insert-a-rectangle-shape-onto-the-first-page-at-coordinates-2-2-with-width-3-centimeters.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/insert-a-rectangle-shape-onto-the-first-page-at-coordinates-2-2-with-width-3-centimeters.cs) | `Diagram`, `Pages`, `Save` | Insert a rectangle shape onto the first page at coordinates 2 2 with width 3 centimeters |
| [iterate-through-multiple-vsdx-files-extract-page-counts-and-write-results-to-a-csv-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/iterate-through-multiple-vsdx-files-extract-page-counts-and-write-results-to-a-csv-report.cs) | `Diagram`, `Pages`, `diagram` | Iterate through multiple vsdx files extract page counts and write results to a csv report |
| [load-a-single-stencil-from-a-vss-file-and-add-a-master-shape-to-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/load-a-single-stencil-from-a-vss-file-and-add-a-master-shape-to-the-diagram.cs) | `AddMaster`, `AddShape`, `Diagram` | Load a single stencil from a vss file and add a master shape to the diagram |
| [load-an-existing-vdx-file-from-disk-using-the-diagram-constructor-and-validate-page-count.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/load-an-existing-vdx-file-from-disk-using-the-diagram-constructor-and-validate-page-count.cs) | `Diagram`, `Pages`, `diagram` | Load an existing vdx file from disk using the diagram constructor and validate page count |
| [open-a-vsd-file-through-a-filestream-then-enumerate-all-shapes-on-the-first-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/open-a-vsd-file-through-a-filestream-then-enumerate-all-shapes-on-the-first-page.cs) | `Diagram`, `Pages`, `diagram` | Open a vsd file through a filestream then enumerate all shapes on the first page |
| [parse-an-mmd-file-into-a-diagram-object-and-convert-its-flowchart-to-visio-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/parse-an-mmd-file-into-a-diagram-object-and-convert-its-flowchart-to-visio-format.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Parse an mmd file into a diagram object and convert its flowchart to visio format |
| [produce-an-html-representation-of-the-diagram-with-embedded-css-for-interactive-navigation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/produce-an-html-representation-of-the-diagram-with-embedded-css-for-interactive-navigation.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Produce an html representation of the diagram with embedded css for interactive navigation |
| [read-a-vsdx-diagram-from-a-memory-stream-and-extract-its-document-title-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/read-a-vsdx-diagram-from-a-memory-stream-and-extract-its-document-title-property.cs) | `Diagram` | Read a vsdx diagram from a memory stream and extract its document title property |
| [remove-a-specific-shape-by-its-id-from-the-diagram-and-re-save-the-updated-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/remove-a-specific-shape-by-its-id-from-the-diagram-and-re-save-the-updated-file.cs) | `Diagram`, `Pages`, `Save` | Remove a specific shape by its id from the diagram and re save the updated file |
| [save-the-current-diagram-to-vsdx-format-and-verify-the-file-size-does-not-exceed-limit.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/save-the-current-diagram-to-vsdx-format-and-verify-the-file-size-does-not-exceed-limit.cs) | `Diagram`, `Save`, `diagram` | Save the current diagram to vsdx format and verify the file size does not exceed limit |
| [set-the-default-font-for-the-entire-diagram-to-times-new-roman-before-adding-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/set-the-default-font-for-the-entire-diagram-to-times-new-roman-before-adding-shapes.cs) | `Diagram`, `Page`, `Pages` | Set the default font for the entire diagram to times new roman before adding shapes |
| [set-the-page-background-color-to-light-gray-for-better-contrast-when-exporting-to-image.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/set-the-page-background-color-to-light-gray-for-better-contrast-when-exporting-to-image.cs) | `Diagram`, `ImageSaveOptions`, `Page` | Set the page background color to light gray for better contrast when exporting to image |
| [specify-a-custom-page-range-in-html-save-options-to-export-only-pages-two-through-four.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/specify-a-custom-page-range-in-html-save-options-to-export-only-pages-two-through-four.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Specify a custom page range in html save options to export only pages two through four |
| [specify-html-save-options-to-include-page-numbers-and-generate-separate-files-for-each-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/specify-html-save-options-to-include-page-numbers-and-generate-separate-files-for-each-page.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Specify html save options to include page numbers and generate separate files for each page |
| [validate-that-each-loaded-diagram-contains-at-least-one-shape-before-proceeding-with-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations/validate-that-each-loaded-diagram-contains-at-least-one-shape-before-proceeding-with-processing.cs) | `Diagram`, `Pages`, `Shapes` | Validate that each loaded diagram contains at least one shape before proceeding with processing |

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
- `ConnectShapesViaConnector`
- `Diagram`
- `HTMLSaveOptions`
- `ImageSaveOptions`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `SVGSaveOptions`
- `Save`
- `Shapes`
- `diagram`
- `page`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** basic operations capabilities are applied in production applications:

- Creating new Visio diagrams programmatically from business process data
- Loading and inspecting existing VSDX files in document management systems
- Batch-processing Visio files for migration or conversion pipelines
- Automating diagram creation in CI/CD pipelines for architecture documentation

## Developer Q&A

Frequently asked questions about **Basic Operations** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Basic Operations in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure
- [Working With Pages](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages) — page management and navigation
- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling

## Category Statistics

- Total examples: 30
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-07-27 | Examples: 30 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
