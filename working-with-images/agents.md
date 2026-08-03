---
category: working-with-images
display_name: Working With Images
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 38
pass_rate: 100.0
generated: 2026-08-03
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Images

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Images** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 38 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Images** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with images operations.
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
| `Aspose.Diagram` | 38 | Core diagram API |
| `System` | 38 | Console, Math, DateTime, Exception |
| `System.IO` | 35 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 29 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `Aspose.Drawing` | 4 | Supporting utilities |
| `Aspose.Drawing.Imaging` | 3 | Supporting utilities |
| `System.Collections.Generic` | 2 | List, Dictionary, HashSet |
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
| [add-a-drop-shadow-effect-to-each-exported-png-image-to-create-depth-perception.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/add-a-drop-shadow-effect-to-each-exported-png-image-to-create-depth-perception.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Add a drop shadow effect to each exported png image to create depth perception |
| [apply-a-blur-filter-to-background-images-in-a-vsd-file-before-exporting-to-png-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/apply-a-blur-filter-to-background-images-in-a-vsd-file-before-exporting-to-png-format.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a blur filter to background images in a vsd file before exporting to png format |
| [apply-a-brightness-adjustment-of-twenty-percent-to-all-exported-jpeg-images-for-enhanced-visibility.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/apply-a-brightness-adjustment-of-twenty-percent-to-all-exported-jpeg-images-for-enhanced-visibility.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a brightness adjustment of twenty percent to all exported jpeg images for enhanced visibility |
| [apply-a-color-inversion-filter-to-exported-png-images-to-create-a-negative-visual-effect.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/apply-a-color-inversion-filter-to-exported-png-images-to-create-a-negative-visual-effect.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a color inversion filter to exported png images to create a negative visual effect |
| [apply-a-custom-color-palette-to-exported-png-images-to-match-corporate-branding-guidelines.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/apply-a-custom-color-palette-to-exported-png-images-to-match-corporate-branding-guidelines.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a custom color palette to exported png images to match corporate branding guidelines |
| [apply-a-grayscale-filter-to-all-images-in-a-vsdx-diagram-before-exporting-pages-as-tiff-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/apply-a-grayscale-filter-to-all-images-in-a-vsdx-diagram-before-exporting-pages-as-tiff-files.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Apply a grayscale filter to all images in a vsdx diagram before exporting pages as tiff files |
| [apply-a-sepia-tone-effect-to-all-images-during-export-to-create-a-vintage-visual-style.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/apply-a-sepia-tone-effect-to-all-images-during-export-to-create-a-vintage-visual-style.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a sepia tone effect to all images during export to create a vintage visual style |
| [batch-convert-multiple-vsd-files-to-png-images-using-parallel-processing-to-improve-performance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/batch-convert-multiple-vsd-files-to-png-images-using-parallel-processing-to-improve-performance.cs) | `Diagram`, `Save`, `diagram` | Batch convert multiple vsd files to png images using parallel processing to improve performance |
| [batch-process-a-folder-of-vdx-files-extracting-images-and-saving-them-with-original-file-name-prefixes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/batch-process-a-folder-of-vdx-files-extracting-images-and-saving-them-with-original-file-name-prefixes.cs) | `Diagram`, `Pages`, `Shapes` | Batch process a folder of vdx files extracting images and saving them with original file name prefixes |
| [batch-replace-a-specific-placeholder-image-across-multiple-vsdx-files-using-a-single-source-png.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/batch-replace-a-specific-placeholder-image-across-multiple-vsdx-files-using-a-single-source-png.cs) | `Diagram`, `Pages`, `Save` | Batch replace a specific placeholder image across multiple vsdx files using a single source png |
| [convert-a-vsdx-diagram-to-jpeg-format-while-specifying-a-compression-quality-of-eighty-percent.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/convert-a-vsdx-diagram-to-jpeg-format-while-specifying-a-compression-quality-of-eighty-percent.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Convert a vsdx diagram to jpeg format while specifying a compression quality of eighty percent |
| [export-a-pdf-a-compliant-document-from-a-vsd-file-embedding-all-images-with-lossless-compression.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-a-pdf-a-compliant-document-from-a-vsd-file-embedding-all-images-with-lossless-compression.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export a pdf a compliant document from a vsd file embedding all images with lossless compression |
| [export-a-specific-layer-of-a-visio-diagram-as-a-transparent-png-for-overlay-use.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-a-specific-layer-of-a-visio-diagram-as-a-transparent-png-for-overlay-use.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Export a specific layer of a visio diagram as a transparent png for overlay use |
| [export-a-specific-visio-page-to-svg-format-preserving-vector-data-for-web-rendering.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-a-specific-visio-page-to-svg-format-preserving-vector-data-for-web-rendering.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Export a specific visio page to svg format preserving vector data for web rendering |
| [export-a-visio-diagram-to-a-multi-page-pdf-and-embed-extracted-images-as-separate-attachments.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-a-visio-diagram-to-a-multi-page-pdf-and-embed-extracted-images-as-separate-attachments.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Export a visio diagram to a multi page pdf and embed extracted images as separate attachments |
| [export-diagram-pages-as-jpeg-with-custom-quality-settings-per-page-based-on-content-complexity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-diagram-pages-as-jpeg-with-custom-quality-settings-per-page-based-on-content-complexity.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Export diagram pages as jpeg with custom quality settings per page based on content complexity |
| [export-diagram-pages-as-lossless-png-with-16-bit-color-depth-for-high-fidelity-image-preservation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-diagram-pages-as-lossless-png-with-16-bit-color-depth-for-high-fidelity-image-preservation.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Export diagram pages as lossless png with 16 bit color depth for high fidelity image preservation |
| [export-diagram-pages-as-multi-page-tiff-with-lzw-compression-to-reduce-file-size.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-diagram-pages-as-multi-page-tiff-with-lzw-compression-to-reduce-file-size.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Export diagram pages as multi page tiff with lzw compression to reduce file size |
| [export-diagram-pages-as-png-with-interlaced-option-enabled-for-progressive-rendering-in-browsers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-diagram-pages-as-png-with-interlaced-option-enabled-for-progressive-rendering-in-browsers.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Export diagram pages as png with interlaced option enabled for progressive rendering in browsers |
| [export-diagram-pages-as-progressive-jpegs-to-improve-loading-speed-on-web-browsers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-diagram-pages-as-progressive-jpegs-to-improve-loading-speed-on-web-browsers.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Export diagram pages as progressive jpegs to improve loading speed on web browsers |
| [export-diagram-pages-to-high-resolution-pdf-with-embedded-fonts-and-images-for-print-ready-output.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/export-diagram-pages-to-high-resolution-pdf-with-embedded-fonts-and-images-for-print-ready-output.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export diagram pages to high resolution pdf with embedded fonts and images for print ready output |
| [extract-all-embedded-images-from-a-vdx-file-and-save-each-to-a-designated-output-directory.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/extract-all-embedded-images-from-a-vdx-file-and-save-each-to-a-designated-output-directory.cs) | `Diagram`, `Pages`, `Shapes` | Extract all embedded images from a vdx file and save each to a designated output directory |
| [extract-image-metadata-such-as-dimensions-and-color-depth-from-each-shape-in-a-vdx-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/extract-image-metadata-such-as-dimensions-and-color-depth-from-each-shape-in-a-vdx-diagram.cs) | `Diagram`, `Pages`, `Shapes` | Extract image metadata such as dimensions and color depth from each shape in a vdx diagram |
| [generate-a-pdf-file-from-a-vsdx-diagram-while-embedding-all-extracted-images-as-high-quality-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/generate-a-pdf-file-from-a-vsdx-diagram-while-embedding-all-extracted-images-as-high-quality-resources.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Generate a pdf file from a vsdx diagram while embedding all extracted images as high quality resources |
| [generate-a-sprite-sheet-combining-all-page-images-from-a-vst-diagram-for-game-development-assets.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/generate-a-sprite-sheet-combining-all-page-images-from-a-vst-diagram-for-game-development-assets.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate a sprite sheet combining all page images from a vst diagram for game development assets |
| [generate-thumbnail-images-of-each-page-in-a-vst-file-with-a-maximum-dimension-of-one-hundred-pixels.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/generate-thumbnail-images-of-each-page-in-a-vst-file-with-a-maximum-dimension-of-one-hundred-pixels.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate thumbnail images of each page in a vst file with a maximum dimension of one hundred pixels |
| [load-a-visio-diagram-from-a-file-and-export-the-first-page-as-a-high-resolution-png-image.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/load-a-visio-diagram-from-a-file-and-export-the-first-page-as-a-high-resolution-png-image.cs) | `Diagram`, `Save`, `diagram` | Load a visio diagram from a file and export the first page as a high resolution png image |
| [overlay-a-semi-transparent-watermark-image-onto-each-exported-png-page-to-protect-intellectual-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/overlay-a-semi-transparent-watermark-image-onto-each-exported-png-page-to-protect-intellectual-property.cs) | `AddShape`, `Diagram`, `ImageSaveOptions` | Overlay a semi transparent watermark image onto each exported png page to protect intellectual property |
| [replace-a-placeholder-shape-image-in-a-vsd-diagram-with-a-new-png-file-loaded-from-memory.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/replace-a-placeholder-shape-image-in-a-vsd-diagram-with-a-new-png-file-loaded-from-memory.cs) | `Diagram`, `Pages`, `Save` | Replace a placeholder shape image in a vsd diagram with a new png file loaded from memory |
| [replace-all-jpeg-images-in-a-diagram-with-png-equivalents-to-reduce-compression-artifacts.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/replace-all-jpeg-images-in-a-diagram-with-png-equivalents-to-reduce-compression-artifacts.cs) | `Diagram`, `Pages`, `Save` | Replace all jpeg images in a diagram with png equivalents to reduce compression artifacts |
| [replace-all-low-resolution-images-in-a-diagram-with-high-resolution-versions-sourced-from-a-specified-folder.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/replace-all-low-resolution-images-in-a-diagram-with-high-resolution-versions-sourced-from-a-specified-folder.cs) | `Diagram`, `Pages`, `Save` | Replace all low resolution images in a diagram with high resolution versions sourced from a specified folder |
| [replace-background-images-in-a-vsdx-diagram-with-a-solid-color-fill-to-simplify-visual-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/replace-background-images-in-a-vsdx-diagram-with-a-solid-color-fill-to-simplify-visual-layout.cs) | `Diagram`, `Pages`, `Save` | Replace background images in a vsdx diagram with a solid color fill to simplify visual layout |
| [resize-all-exported-images-to-a-uniform-height-of-five-hundred-pixels-while-preserving-aspect-ratio.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/resize-all-exported-images-to-a-uniform-height-of-five-hundred-pixels-while-preserving-aspect-ratio.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Resize all exported images to a uniform height of five hundred pixels while preserving aspect ratio |
| [resize-an-imported-bitmap-image-within-a-visio-shape-to-fit-the-shape-s-dimensions-proportionally.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/resize-an-imported-bitmap-image-within-a-visio-shape-to-fit-the-shape-s-dimensions-proportionally.cs) | `Diagram`, `Pages`, `Save` | Resize an imported bitmap image within a visio shape to fit the shape s dimensions proportionally |
| [resize-exported-bmp-images-to-a-fixed-width-of-eight-hundred-pixels-while-maintaining-aspect-ratio.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/resize-exported-bmp-images-to-a-fixed-width-of-eight-hundred-pixels-while-maintaining-aspect-ratio.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Resize exported bmp images to a fixed width of eight hundred pixels while maintaining aspect ratio |
| [save-extracted-images-as-base64-strings-and-embed-them-back-into-the-diagram-using-data-uris.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/save-extracted-images-as-base64-strings-and-embed-them-back-into-the-diagram-using-data-uris.cs) | `Diagram`, `Pages`, `Save` | Save extracted images as base64 strings and embed them back into the diagram using data uris |
| [validate-that-all-images-embedded-in-a-vsdx-file-meet-a-minimum-resolution-of-300-dpi.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/validate-that-all-images-embedded-in-a-vsdx-file-meet-a-minimum-resolution-of-300-dpi.cs) | `Diagram`, `Pages`, `Shapes` | Validate that all images embedded in a vsdx file meet a minimum resolution of 300 dpi |
| [validate-that-no-image-exceeds-a-file-size-limit-of-two-megabytes-before-exporting-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images/validate-that-no-image-exceeds-a-file-size-limit-of-two-megabytes-before-exporting-the-diagram.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Validate that no image exceeds a file size limit of two megabytes before exporting the diagram |

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
- `ImageSaveOptions`
- `Pages`
- `PdfSaveOptions`
- `SVGSaveOptions`
- `Save`
- `Shapes`
- `diagram`
- `page`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with images capabilities are applied in production applications:

- Embedding corporate logos and icons into Visio diagrams programmatically
- Adding QR codes and barcodes to process flow diagrams
- Extracting embedded images from Visio files for archival purposes

## Developer Q&A

Frequently asked questions about **Working With Images** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Images in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Diagram Conversions](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions) — exporting to PDF, PNG, SVG, and other formats
- [Working With Pages](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages) — page management and navigation

## Category Statistics

- Total examples: 38
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-08-03 | Examples: 38 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
