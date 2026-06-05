---
category: font-operations
display_name: Font Operations
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 30
pass_rate: 100.0
generated: 2026-06-05
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Font Operations

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Font Operations** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 30 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-05 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Font Operations** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for font operations operations.
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
| `System.IO` | 21 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 20 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `Aspose.Drawing.Text` | 5 | Font enumeration via InstalledFontCollection |
| `System.Collections.Generic` | 4 | List, Dictionary, HashSet |
| `System.Linq` | 4 | LINQ queries on collections |

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
| [apply-a-bold-style-to-all-text-shapes-that-use-a-particular-font-within-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/apply-a-bold-style-to-all-text-shapes-that-use-a-particular-font-within-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Apply a bold style to all text shapes that use a particular font within the diagram |
| [apply-a-shadow-effect-to-all-text-using-a-particular-font-to-enhance-depth-in-the-output-image.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/apply-a-shadow-effect-to-all-text-using-a-particular-font-to-enhance-depth-in-the-output-image.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a shadow effect to all text using a particular font to enhance depth in the output image |
| [apply-kerning-adjustments-to-all-text-elements-using-a-particular-font-to-improve-spacing-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/apply-kerning-adjustments-to-all-text-elements-using-a-particular-font-to-improve-spacing-consistency.cs) | `Diagram`, `Pages`, `Save` | Apply kerning adjustments to all text elements using a particular font to improve spacing consistency |
| [batch-convert-a-collection-of-vdx-files-to-pdf-ensuring-that-each-document-embeds-its-required-fonts.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/batch-convert-a-collection-of-vdx-files-to-pdf-ensuring-that-each-document-embeds-its-required-fonts.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Batch convert a collection of vdx files to pdf ensuring that each document embeds its required fonts |
| [batch-process-multiple-vsd-files-substituting-a-deprecated-font-with-a-modern-equivalent-in-each.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/batch-process-multiple-vsd-files-substituting-a-deprecated-font-with-a-modern-equivalent-in-each.cs) | `Diagram`, `Save`, `diagram` | Batch process multiple vsd files substituting a deprecated font with a modern equivalent in each |
| [configure-a-custom-font-folder-path-to-load-additional-truetype-fonts-for-diagram-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/configure-a-custom-font-folder-path-to-load-additional-truetype-fonts-for-diagram-processing.cs) | `Diagram` | Configure a custom font folder path to load additional truetype fonts for diagram processing |
| [configure-the-library-to-use-a-specific-font-cache-size-to-improve-performance-when-processing-large-diagrams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/configure-the-library-to-use-a-specific-font-cache-size-to-improve-performance-when-processing-large-diagrams.cs) | `Diagram`, `Save`, `diagram` | Configure the library to use a specific font cache size to improve performance when processing large diagrams |
| [convert-a-diagram-to-pdf-while-embedding-only-the-fonts-that-are-actually-used-in-the-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/convert-a-diagram-to-pdf-while-embedding-only-the-fonts-that-are-actually-used-in-the-document.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Convert a diagram to pdf while embedding only the fonts that are actually used in the document |
| [create-a-custom-font-substitution-map-to-replace-unavailable-fonts-with-user-defined-alternatives-during-rendering.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/create-a-custom-font-substitution-map-to-replace-unavailable-fonts-with-user-defined-alternatives-during-rendering.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Create a custom font substitution map to replace unavailable fonts with user defined alternatives during rendering |
| [detect-and-report-any-text-shapes-that-use-fonts-not-supported-by-the-target-output-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/detect-and-report-any-text-shapes-that-use-fonts-not-supported-by-the-target-output-format.cs) | `Diagram`, `Fonts`, `Pages` | Detect and report any text shapes that use fonts not supported by the target output format |
| [embed-missing-fonts-into-the-output-pdf-to-ensure-visual-fidelity-across-different-devices.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/embed-missing-fonts-into-the-output-pdf-to-ensure-visual-fidelity-across-different-devices.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Embed missing fonts into the output pdf to ensure visual fidelity across different devices |
| [export-a-diagram-to-html-embedding-web-safe-fonts-to-ensure-consistent-appearance-across-browsers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/export-a-diagram-to-html-embedding-web-safe-fonts-to-ensure-consistent-appearance-across-browsers.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Export a diagram to html embedding web safe fonts to ensure consistent appearance across browsers |
| [export-the-diagram-to-an-emf-file-that-all-text-is-converted-to-outlines-to-avoid-font-dependencies.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/export-the-diagram-to-an-emf-file-that-all-text-is-converted-to-outlines-to-avoid-font-dependencies.cs) | `Diagram`, `Save`, `diagram` | Export the diagram to an emf file that all text is converted to outlines to avoid font dependencies |
| [extract-a-list-of-unique-font-names-from-a-visio-file-and-export-them-to-a-csv-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/extract-a-list-of-unique-font-names-from-a-visio-file-and-export-them-to-a-csv-file.cs) | `Diagram`, `Fonts`, `diagram` | Extract a list of unique font names from a visio file and export them to a csv file |
| [extract-font-metadata-such-as-family-name-style-and-version-from-each-text-shape-in-a-visio-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/extract-font-metadata-such-as-family-name-style-and-version-from-each-text-shape-in-a-visio-file.cs) | `Diagram`, `Fonts`, `Pages` | Extract font metadata such as family name style and version from each text shape in a visio file |
| [generate-a-preview-image-of-a-diagram-with-a-user-specified-font-applied-to-all-captions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/generate-a-preview-image-of-a-diagram-with-a-user-specified-font-applied-to-all-captions.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate a preview image of a diagram with a user specified font applied to all captions |
| [generate-a-report-listing-each-font-used-in-a-diagram-along-with-the-number-of-occurrences-per-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/generate-a-report-listing-each-font-used-in-a-diagram-along-with-the-number-of-occurrences-per-page.cs) | `Diagram`, `Pages`, `Shapes` | Generate a report listing each font used in a diagram along with the number of occurrences per page |
| [load-a-diagram-change-the-font-color-of-all-titles-to-an-rgb-value-and-save-as-pdf.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/load-a-diagram-change-the-font-color-of-all-titles-to-an-rgb-value-and-save-as-pdf.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Load a diagram change the font color of all titles to an rgb value and save as pdf |
| [load-a-diagram-iterate-through-its-pages-and-replace-the-font-of-header-shapes-with-a-localized-version.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/load-a-diagram-iterate-through-its-pages-and-replace-the-font-of-header-shapes-with-a-localized-version.cs) | `Diagram`, `Pages`, `Save` | Load a diagram iterate through its pages and replace the font of header shapes with a localized version |
| [load-a-diagram-set-the-line-spacing-for-all-paragraphs-using-a-specific-font-and-save-as-docx.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/load-a-diagram-set-the-line-spacing-for-all-paragraphs-using-a-specific-font-and-save-as-docx.cs) | `Diagram`, `Pages`, `Save` | Load a diagram set the line spacing for all paragraphs using a specific font and save as docx |
| [load-a-visio-diagram-from-a-vsdx-file-and-set-a-custom-default-font-for-rendering.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/load-a-visio-diagram-from-a-vsdx-file-and-set-a-custom-default-font-for-rendering.cs) | `Diagram`, `Save`, `diagram` | Load a visio diagram from a vsdx file and set a custom default font for rendering |
| [render-a-diagram-to-a-high-resolution-tiff-image-while-preserving-original-font-styles-and-weights.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/render-a-diagram-to-a-high-resolution-tiff-image-while-preserving-original-font-styles-and-weights.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Render a diagram to a high resolution tiff image while preserving original font styles and weights |
| [render-a-diagram-to-png-format-while-forcing-the-use-of-a-specified-fallback-font-for-missing-glyphs.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/render-a-diagram-to-png-format-while-forcing-the-use-of-a-specified-fallback-font-for-missing-glyphs.cs) | `Diagram`, `Save`, `diagram` | Render a diagram to png format while forcing the use of a specified fallback font for missing glyphs |
| [replace-all-occurrences-of-a-specific-font-in-a-diagram-with-an-alternative-font-before-exporting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/replace-all-occurrences-of-a-specific-font-in-a-diagram-with-an-alternative-font-before-exporting.cs) | `Diagram`, `Save`, `diagram` | Replace all occurrences of a specific font in a diagram with an alternative font before exporting |
| [replace-unicode-characters-in-text-shapes-with-equivalent-glyphs-from-a-fallback-font-to-avoid-missing-symbols.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/replace-unicode-characters-in-text-shapes-with-equivalent-glyphs-from-a-fallback-font-to-avoid-missing-symbols.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Replace unicode characters in text shapes with equivalent glyphs from a fallback font to avoid missing symbols |
| [retrieve-the-font-size-of-each-text-block-in-a-diagram-and-log-the-values-for-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/retrieve-the-font-size-of-each-text-block-in-a-diagram-and-log-the-values-for-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve the font size of each text block in a diagram and log the values for analysis |
| [set-the-default-font-for-newly-created-text-shapes-when-programmatically-adding-annotations-to-a-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/set-the-default-font-for-newly-created-text-shapes-when-programmatically-adding-annotations-to-a-diagram.cs) | `Diagram`, `Page`, `Pages` | Set the default font for newly created text shapes when programmatically adding annotations to a diagram |
| [set-the-font-rendering-mode-to-anti-aliased-for-higher-quality-output-when-saving-as-svg.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/set-the-font-rendering-mode-to-anti-aliased-for-higher-quality-output-when-saving-as-svg.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Set the font rendering mode to anti aliased for higher quality output when saving as svg |
| [validate-that-all-fonts-used-in-a-loaded-diagram-are-available-on-the-system-before-rendering.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/validate-that-all-fonts-used-in-a-loaded-diagram-are-available-on-the-system-before-rendering.cs) | `Diagram`, `Fonts`, `PdfSaveOptions` | Validate that all fonts used in a loaded diagram are available on the system before rendering |
| [validate-that-font-sizes-in-a-diagram-meet-accessibility-guidelines-before-exporting-to-pdf-a.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations/validate-that-font-sizes-in-a-diagram-meet-accessibility-guidelines-before-exporting-to-pdf-a.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Validate that font sizes in a diagram meet accessibility guidelines before exporting to pdf a |

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
