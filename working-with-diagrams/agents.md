---
category: working-with-diagrams
display_name: Working With Diagrams
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 40
pass_rate: 100.0
generated: 2026-08-03
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Diagrams

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Diagrams** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 40 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Diagrams** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with diagrams operations.
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
| `Aspose.Diagram` | 40 | Core diagram API |
| `System` | 40 | Console, Math, DateTime, Exception |
| `Aspose.Diagram.Saving` | 30 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.IO` | 30 | File, Stream, Path, Directory operations |
| `System.Linq` | 3 | LINQ queries on collections |
| `System.Collections.Generic` | 3 | List, Dictionary, HashSet |
| `System.Threading.Tasks` | 2 | Supporting utilities |
| `Aspose.Drawing.Text` | 2 | Font enumeration via InstalledFontCollection |
| `Aspose.Diagram.AutoLayout` | 1 | Supporting utilities |
| `System.Text.Json` | 1 | JSON serialization |
| `System.Diagnostics` | 1 | Supporting utilities |

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
| [add-a-digital-watermark-to-each-pdf-page-after-conversion-using-a-pdf-library.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/add-a-digital-watermark-to-each-pdf-page-after-conversion-using-a-pdf-library.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Add a digital watermark to each pdf page after conversion using a pdf library |
| [after-merging-diagrams-generate-a-thumbnail-image-of-the-first-page-and-save-as-png.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/after-merging-diagrams-generate-a-thumbnail-image-of-the-first-page-and-save-as-png.cs) | `Diagram`, `ImageSaveOptions` | After merging diagrams generate a thumbnail image of the first page and save as png |
| [after-removing-hidden-data-compare-file-size-before-and-after-to-confirm-reduction.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/after-removing-hidden-data-compare-file-size-before-and-after-to-confirm-reduction.cs) | `Diagram`, `Save`, `diagram` | After removing hidden data compare file size before and after to confirm reduction |
| [apply-a-custom-page-background-color-before-exporting-the-diagram-to-pdf-for-branding.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/apply-a-custom-page-background-color-before-exporting-the-diagram-to-pdf-for-branding.cs) | `AddShape`, `Diagram`, `Page` | Apply a custom page background color before exporting the diagram to pdf for branding |
| [apply-a-global-line-color-change-to-all-connectors-to-match-corporate-palette-before-pdf-export.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/apply-a-global-line-color-change-to-all-connectors-to-match-corporate-palette-before-pdf-export.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Apply a global line color change to all connectors to match corporate palette before pdf export |
| [apply-a-uniform-line-thickness-to-all-connectors-before-exporting-the-diagram-to-pdf.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/apply-a-uniform-line-thickness-to-all-connectors-before-exporting-the-diagram-to-pdf.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Apply a uniform line thickness to all connectors before exporting the diagram to pdf |
| [apply-auto-fit-layout-to-all-shapes-in-the-diagram-before-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/apply-auto-fit-layout-to-all-shapes-in-the-diagram-before-saving.cs) | `Diagram`, `Layout`, `LayoutOptions` | Apply auto fit layout to all shapes in the diagram before saving |
| [batch-process-a-folder-of-vsdx-files-removing-hidden-info-and-converting-each-to-pdf.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/batch-process-a-folder-of-vsdx-files-removing-hidden-info-and-converting-each-to-pdf.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Batch process a folder of vsdx files removing hidden info and converting each to pdf |
| [combine-a-vsd-diagram-with-a-vsdx-diagram-preserving-page-order-from-the-first-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/combine-a-vsd-diagram-with-a-vsdx-diagram-preserving-page-order-from-the-first-file.cs) | `Diagram` | Combine a vsd diagram with a vsdx diagram preserving page order from the first file |
| [configure-diagram-interruptmonitor-with-a-five-second-timeout-before-starting-pdf-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/configure-diagram-interruptmonitor-with-a-five-second-timeout-before-starting-pdf-conversion.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Configure diagram interruptmonitor with a five second timeout before starting pdf conversion |
| [configure-pdf-conversion-to-use-custom-page-margins-matching-corporate-standards.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/configure-pdf-conversion-to-use-custom-page-margins-matching-corporate-standards.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Configure pdf conversion to use custom page margins matching corporate standards |
| [convert-the-diagram-to-html-and-embed-all-shape-images-as-base64-data-uris.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/convert-the-diagram-to-html-and-embed-all-shape-images-as-base64-data-uris.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert the diagram to html and embed all shape images as base64 data uris |
| [convert-the-diagram-to-pdf-format-while-preserving-original-page-orientation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/convert-the-diagram-to-pdf-format-while-preserving-original-page-orientation.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Convert the diagram to pdf format while preserving original page orientation |
| [convert-the-diagram-to-pdf-with-grayscale-color-mode-to-reduce-archival-file-size.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/convert-the-diagram-to-pdf-with-grayscale-color-mode-to-reduce-archival-file-size.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Convert the diagram to pdf with grayscale color mode to reduce archival file size |
| [create-a-pipeline-that-loads-a-diagram-merges-it-with-a-template-and-outputs-html-with-embedded-css.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/create-a-pipeline-that-loads-a-diagram-merges-it-with-a-template-and-outputs-html-with-embedded-css.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Create a pipeline that loads a diagram merges it with a template and outputs html with embedded css |
| [create-a-utility-that-removes-hidden-information-from-diagrams-and-logs-amount-of-data-removed.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/create-a-utility-that-removes-hidden-information-from-diagrams-and-logs-amount-of-data-removed.cs) | `Diagram`, `Save`, `diagram` | Create a utility that removes hidden information from diagrams and logs amount of data removed |
| [embed-fonts-during-pdf-conversion-and-verify-that-all-fonts-are-embedded-in-the-output.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/embed-fonts-during-pdf-conversion-and-verify-that-all-fonts-are-embedded-in-the-output.cs) | `Diagram`, `Fonts`, `PdfSaveOptions` | Embed fonts during pdf conversion and verify that all fonts are embedded in the output |
| [embed-pdf-metadata-such-as-author-and-creation-date-using-diagram-properties-after-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/embed-pdf-metadata-such-as-author-and-creation-date-using-diagram-properties-after-conversion.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Embed pdf metadata such as author and creation date using diagram properties after conversion |
| [export-connector-routing-information-and-generate-a-visual-diagram-of-connections-using-a-third-party-graph-library.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/export-connector-routing-information-and-generate-a-visual-diagram-of-connections-using-a-third-party-graph-library.cs) | `Diagram`, `ImageSaveOptions`, `Layout` | Export connector routing information and generate a visual diagram of connections using a third party graph library |
| [export-connector-start-and-end-points-from-the-diagram-to-a-json-file-for-external-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/export-connector-start-and-end-points-from-the-diagram-to-a-json-file-for-external-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Export connector start and end points from the diagram to a json file for external analysis |
| [extract-font-names-used-in-a-vsd-diagram-and-compare-them-against-a-corporate-whitelist.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/extract-font-names-used-in-a-vsd-diagram-and-compare-them-against-a-corporate-whitelist.cs) | `Diagram`, `Fonts`, `diagram` | Extract font names used in a vsd diagram and compare them against a corporate whitelist |
| [flatten-all-groups-into-individual-shapes-after-merging-to-simplify-further-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/flatten-all-groups-into-individual-shapes-after-merging-to-simplify-further-processing.cs) | `Diagram`, `Shapes`, `page` | Flatten all groups into individual shapes after merging to simplify further processing |
| [generate-a-summary-report-listing-page-counts-and-shape-totals-for-each-source-diagram-after-merging.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/generate-a-summary-report-listing-page-counts-and-shape-totals-for-each-source-diagram-after-merging.cs) | `Diagram`, `Pages`, `Shapes` | Generate a summary report listing page counts and shape totals for each source diagram after merging |
| [implement-error-handling-that-catches-interruptmonitor-exceptions-and-logs-the-aborted-operation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/implement-error-handling-that-catches-interruptmonitor-exceptions-and-logs-the-aborted-operation.cs) | `Diagram`, `Save`, `diagram` | Implement error handling that catches interruptmonitor exceptions and logs the aborted operation |
| [load-a-vsd-file-with-custom-loadoptions-enabling-interruptmonitor-for-cancellation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/load-a-vsd-file-with-custom-loadoptions-enabling-interruptmonitor-for-cancellation.cs) | `Diagram` | Load a vsd file with custom loadoptions enabling interruptmonitor for cancellation |
| [load-a-vsdx-file-into-a-diagram-object-using-default-load-options.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/load-a-vsdx-file-into-a-diagram-object-using-default-load-options.cs) | `Diagram`, `Pages`, `diagram` | Load a vsdx file into a diagram object using default load options |
| [load-multiple-diagrams-concurrently-using-asynchronous-tasks-then-combine-them-into-a-single-output-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/load-multiple-diagrams-concurrently-using-asynchronous-tasks-then-combine-them-into-a-single-output-file.cs) | `Diagram` | Load multiple diagrams concurrently using asynchronous tasks then combine them into a single output file |
| [measure-conversion-time-for-pdf-output-with-and-without-interruptmonitor-to-assess-performance-impact.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/measure-conversion-time-for-pdf-output-with-and-without-interruptmonitor-to-assess-performance-impact.cs) | `Diagram` | Measure conversion time for pdf output with and without interruptmonitor to assess performance impact |
| [merge-two-vsdx-diagrams-into-a-single-diagram-automatically-resolving-duplicate-shape-ids.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/merge-two-vsdx-diagrams-into-a-single-diagram-automatically-resolving-duplicate-shape-ids.cs) | `Diagram` | Merge two vsdx diagrams into a single diagram automatically resolving duplicate shape ids |
| [protect-the-pdf-with-a-password-and-verify-that-opening-without-password-fails.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/protect-the-pdf-with-a-password-and-verify-that-opening-without-password-fails.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Protect the pdf with a password and verify that opening without password fails |
| [remove-hidden-information-from-the-diagram-and-verify-that-hidden-layers-are-absent.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/remove-hidden-information-from-the-diagram-and-verify-that-hidden-layers-are-absent.cs) | `Diagram`, `Save`, `diagram` | Remove hidden information from the diagram and verify that hidden layers are absent |
| [rename-all-pages-sequentially-after-merging-to-avoid-naming-conflicts.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/rename-all-pages-sequentially-after-merging-to-avoid-naming-conflicts.cs) | `Diagram`, `page` | Rename all pages sequentially after merging to avoid naming conflicts |
| [retrieve-and-log-the-font-size-of-each-text-shape-for-accessibility-auditing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/retrieve-and-log-the-font-size-of-each-text-shape-for-accessibility-auditing.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve and log the font size of each text shape for accessibility auditing |
| [retrieve-connector-information-from-the-loaded-diagram-and-export-it-to-a-csv-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/retrieve-connector-information-from-the-loaded-diagram-and-export-it-to-a-csv-report.cs) | `Diagram`, `Pages`, `diagram` | Retrieve connector information from the loaded diagram and export it to a csv report |
| [save-the-converted-pdf-to-a-memory-stream-for-immediate-network-transmission.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/save-the-converted-pdf-to-a-memory-stream-for-immediate-network-transmission.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Save the converted pdf to a memory stream for immediate network transmission |
| [set-loadoptions-interruptmonitor-to-abort-loading-after-ten-seconds-for-large-diagrams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/set-loadoptions-interruptmonitor-to-abort-loading-after-ten-seconds-for-large-diagrams.cs) | `Diagram`, `Save`, `diagram` | Set loadoptions interruptmonitor to abort loading after ten seconds for large diagrams |
| [strip-hidden-metadata-from-a-vsdx-file-then-save-the-cleaned-diagram-back-to-vsdx.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/strip-hidden-metadata-from-a-vsdx-file-then-save-the-cleaned-diagram-back-to-vsdx.cs) | `Diagram`, `Save`, `diagram` | Strip hidden metadata from a vsdx file then save the cleaned diagram back to vsdx |
| [update-shape-text-in-the-diagram-to-include-a-timestamp-before-pdf-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/update-shape-text-in-the-diagram-to-include-a-timestamp-before-pdf-conversion.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Update shape text in the diagram to include a timestamp before pdf conversion |
| [validate-that-no-hidden-comments-remain-after-calling-removehiddeninfo-by-inspecting-diagram-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/validate-that-no-hidden-comments-remain-after-calling-removehiddeninfo-by-inspecting-diagram-metadata.cs) | `Diagram`, `Save`, `diagram` | Validate that no hidden comments remain after calling removehiddeninfo by inspecting diagram metadata |
| [validate-that-the-pdf-conversion-respects-page-orientation-by-checking-width-to-height-ratios.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams/validate-that-the-pdf-conversion-respects-page-orientation-by-checking-width-to-height-ratios.cs) | `Diagram`, `Pages`, `Save` | Validate that the pdf conversion respects page orientation by checking width to height ratios |

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

- `AddShape`
- `Diagram`
- `Fonts`
- `HTMLSaveOptions`
- `ImageSaveOptions`
- `Layout`
- `LayoutOptions`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `Save`
- `Shapes`
- `diagram`
- `page`
- `shape`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with diagrams capabilities are applied in production applications:

- Merging multiple Visio files into a single consolidated diagram
- Inspecting and reporting on diagram structure and properties
- Automating diagram cleanup and normalization operations

## Developer Q&A

Frequently asked questions about **Working With Diagrams** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Diagrams in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

**Q: How do I connect two shapes with a connector?**

A: Add a connector shape: `Shape connector = new Shape(); long connId = diagram.AddShape(connector, "Dynamic connector", pageIndex);` then call `page.ConnectShapesViaConnector(shape1Id, ConnectionPointPlace.Right, shape2Id, ConnectionPointPlace.Bottom, connId);`

## Related Categories

- [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) — loading, saving, and basic diagram operations
- [Working With Pages](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages) — page management and navigation
- [Working With Masters](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters) — master shapes and stencils

## Category Statistics

- Total examples: 40
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-08-03 | Examples: 40 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
