---
category: working-with-text
display_name: Working With Text
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 89
pass_rate: 100.0
generated: 2026-06-23
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Text

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Text** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 89 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-23 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Text** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with text operations.
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
| `Aspose.Diagram` | 89 | Core diagram API |
| `System` | 89 | Console, Math, DateTime, Exception |
| `System.IO` | 61 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 35 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 5 | List, Dictionary, HashSet |
| `System.Text` | 5 | StringBuilder |
| `System.Text.RegularExpressions` | 4 | Supporting utilities |
| `System.Linq` | 3 | LINQ queries on collections |
| `Aspose.Drawing.Text` | 2 | Font enumeration via InstalledFontCollection |
| `System.Text.Json` | 1 | JSON serialization |
| `Aspose.Diagram.AutoLayout` | 1 | Supporting utilities |

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

- INSERT TEXT SHAPE — Use page.AddText(pinX, pinY, width, height, text) to add a standalone text shape to a page. Returns a Shape object. Example: diagram.Pages[0].AddText(1, 1, 1, 1, "Test text");
- WATERMARK — Use the overloaded page.AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize) to add a watermark. Calculate center position using page.PageSheet.PageProps.PageWidth.Value / 2 and PageHeight.Value / 2. Example: page.AddText(pinx, piny, width, height, "Watermark", "Calibri", "#a5a5a5", 0.25);
- WATERMARK FULL PAGE — Set width and height to the full page dimensions: double width = page.PageSheet.PageProps.PageWidth.Value; double height = page.PageSheet.PageProps.PageHeight.Value; then call page.AddText(pinx, piny, width, height, text, fontName, fontColor, fontSize);
- UPDATE SHAPE TEXT — Clear existing text and add new: shape.Text.Value.Clear(); shape.Text.Value.Add(new Txt("New Text"));
- FIND SHAPE FOR TEXT UPDATE — Iterate page.Shapes and match by shape.NameU.ToLower() == "process" && shape.ID == 1 or by shape.Name == "targetName".
- APPLY STYLESHEET — Set shape.TextStyle, shape.FillStyle, shape.LineStyle to a StyleSheet object found by iterating diagram.StyleSheets and matching by styleSheet.Name == "Basic".
- Example: foreach (StyleSheet ss in diagram.StyleSheets) { if (ss.Name == "Basic") { shape.TextStyle = ss; shape.FillStyle = ss; shape.LineStyle = ss; break; } }
- MULTIPLE TEXT RUNS WITH DIFFERENT STYLES — Clear shape text and chars first: shape.Text.Value.Clear(); shape.Chars.Clear();
- Add character run markers with Cp(index) before each Txt: shape.Text.Value.Add(new Cp(0)); shape.Text.Value.Add(new Txt("Regular text\n")); shape.Text.Value.Add(new Cp(1)); shape.Text.Value.Add(new Txt("Bold text\n"));
- Add matching Char objects to shape.Chars: shape.Chars.Add(new Aspose.Diagram.Char()); — one per Cp index.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-footer-text-watermark-that-includes-document-title-and-version-number.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/add-a-footer-text-watermark-that-includes-document-title-and-version-number.cs) | `Diagram`, `Save`, `diagram` | Add a footer text watermark that includes document title and version number |
| [add-a-timestamp-watermark-that-updates-each-time-the-file-is-saved.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/add-a-timestamp-watermark-that-updates-each-time-the-file-is-saved.cs) | `Diagram`, `Pages`, `Save` | Add a timestamp watermark that updates each time the file is saved |
| [adjust-watermark-opacity-to-a-configurable-value-between-10-and-90.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/adjust-watermark-opacity-to-a-configurable-value-between-10-and-90.cs) | `Diagram`, `Pages`, `Save` | Adjust watermark opacity to a configurable value between 10 and 90 |
| [allow-users-to-specify-watermark-rotation-angle-in-degrees-via-configuration.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/allow-users-to-specify-watermark-rotation-angle-in-degrees-via-configuration.cs) | `Diagram`, `Pages`, `Save` | Allow users to specify watermark rotation angle in degrees via configuration |
| [apply-a-custom-color-to-the-watermark-text-based-on-user-defined-palette.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-color-to-the-watermark-text-based-on-user-defined-palette.cs) | `Diagram`, `Pages`, `Save` | Apply a custom color to the watermark text based on user defined palette |
| [apply-a-custom-stylesheet-that-changes-text-color-based-on-shape-type-then-validate-color-assignments.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-that-changes-text-color-based-on-shape-type-then-validate-color-assignments.cs) | `Diagram`, `Page`, `Pages` | Apply a custom stylesheet that changes text color based on shape type then validate color assignments |
| [apply-a-custom-stylesheet-that-defines-paragraph-spacing-then-verify-spacing-changes-on-target-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-that-defines-paragraph-spacing-then-verify-spacing-changes-on-target-shapes.cs) | `Diagram`, `Pages`, `Save` | Apply a custom stylesheet that defines paragraph spacing then verify spacing changes on target shapes |
| [apply-a-custom-stylesheet-that-defines-text-alignment-then-align-all-paragraph-texts-to-center-on-page-two.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-that-defines-text-alignment-then-align-all-paragraph-texts-to-center-on-page-two.cs) | `Diagram`, `Pages`, `Save` | Apply a custom stylesheet that defines text alignment then align all paragraph texts to center on page two |
| [apply-a-custom-stylesheet-that-defines-underline-style-then-underline-all-headings-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-that-defines-underline-style-then-underline-all-headings-in-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Apply a custom stylesheet that defines underline style then underline all headings in the diagram |
| [apply-a-custom-stylesheet-that-sets-line-height-then-verify-line-spacing-on-multi-line-text-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-that-sets-line-height-then-verify-line-spacing-on-multi-line-text-shapes.cs) | `AddShape`, `Diagram`, `Pages` | Apply a custom stylesheet that sets line height then verify line spacing on multi line text shapes |
| [apply-a-custom-stylesheet-to-all-shapes-containing-the-word-important-to-highlight-them-visually.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-to-all-shapes-containing-the-word-important-to-highlight-them-visually.cs) | `Diagram`, `Pages`, `Save` | Apply a custom stylesheet to all shapes containing the word important to highlight them visually |
| [apply-a-custom-stylesheet-with-specific-font-and-size-to-all-title-shapes-across-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-a-custom-stylesheet-with-specific-font-and-size-to-all-title-shapes-across-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Apply a custom stylesheet with specific font and size to all title shapes across the diagram |
| [apply-built-in-caption-style-to-all-shapes-on-page-five-to-ensure-consistent-footnote-formatting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-caption-style-to-all-shapes-on-page-five-to-ensure-consistent-footnote-formatting.cs) | `Diagram`, `Pages`, `Save` | Apply built in caption style to all shapes on page five to ensure consistent footnote formatting |
| [apply-built-in-caption-style-to-shapes-whose-text-length-is-less-than-ten-characters-for-uniform-captions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-caption-style-to-shapes-whose-text-length-is-less-than-ten-characters-for-uniform-captions.cs) | `Diagram`, `Pages`, `Save` | Apply built in caption style to shapes whose text length is less than ten characters for uniform captions |
| [apply-built-in-emphasis-style-to-all-bullet-list-shapes-to-enhance-readability-in-presentations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-emphasis-style-to-all-bullet-list-shapes-to-enhance-readability-in-presentations.cs) | `Diagram`, `Pages`, `Save` | Apply built in emphasis style to all bullet list shapes to enhance readability in presentations |
| [apply-built-in-emphasis-style-to-shapes-containing-the-word-alert-to-highlight-critical-information.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-emphasis-style-to-shapes-containing-the-word-alert-to-highlight-critical-information.cs) | `Diagram`, `Pages`, `Save` | Apply built in emphasis style to shapes containing the word alert to highlight critical information |
| [apply-built-in-subtitle-style-to-shapes-whose-text-starts-with-a-numeric-prefix-for-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-subtitle-style-to-shapes-whose-text-starts-with-a-numeric-prefix-for-consistency.cs) | `Diagram`, `Pages`, `Save` | Apply built in subtitle style to shapes whose text starts with a numeric prefix for consistency |
| [apply-built-in-title-style-to-shapes-whose-text-length-exceeds-twenty-characters-for-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-title-style-to-shapes-whose-text-length-exceeds-twenty-characters-for-consistency.cs) | `Diagram`, `Pages`, `Save` | Apply built in title style to shapes whose text length exceeds twenty characters for consistency |
| [apply-built-in-title-style-to-shapes-whose-text-matches-a-regular-expression-pattern-for-dates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-built-in-title-style-to-shapes-whose-text-matches-a-regular-expression-pattern-for-dates.cs) | `Diagram`, `Pages`, `Save` | Apply built in title style to shapes whose text matches a regular expression pattern for dates |
| [apply-the-built-in-heading-1-stylesheet-to-shape-id-10-for-standardized-title-formatting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-the-built-in-heading-1-stylesheet-to-shape-id-10-for-standardized-title-formatting.cs) | `Diagram`, `Pages`, `Save` | Apply the built in heading 1 stylesheet to shape id 10 for standardized title formatting |
| [apply-the-watermark-to-all-pages-in-the-loaded-visio-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/apply-the-watermark-to-all-pages-in-the-loaded-visio-document.cs) | `Diagram`, `Pages`, `Save` | Apply the watermark to all pages in the loaded visio document |
| [change-watermark-font-style-and-size-according-to-user-preferences.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/change-watermark-font-style-and-size-according-to-user-preferences.cs) | `Diagram`, `Pages`, `Save` | Change watermark font style and size according to user preferences |
| [create-a-batch-job-that-loads-diagrams-updates-footer-text-with-the-current-timestamp-and-saves-them.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-batch-job-that-loads-diagrams-updates-footer-text-with-the-current-timestamp-and-saves-them.cs) | `Diagram`, `Save`, `diagram` | Create a batch job that loads diagrams updates footer text with the current timestamp and saves them |
| [create-a-batch-operation-that-loads-diagrams-rotates-all-title-shape-texts-by-180-degrees-and-saves.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-batch-operation-that-loads-diagrams-rotates-all-title-shape-texts-by-180-degrees-and-saves.cs) | `Diagram`, `Pages`, `Save` | Create a batch operation that loads diagrams rotates all title shape texts by 180 degrees and saves |
| [create-a-batch-process-that-adds-numbered-text-shapes-to-each-page-using-the-page-index-as-label.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-batch-process-that-adds-numbered-text-shapes-to-each-page-using-the-page-index-as-label.cs) | `Diagram`, `Pages`, `Save` | Create a batch process that adds numbered text shapes to each page using the page index as label |
| [create-a-batch-script-that-loads-diagrams-applies-a-custom-stylesheet-and-saves-them-as-updated-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-batch-script-that-loads-diagrams-applies-a-custom-stylesheet-and-saves-them-as-updated-files.cs) | `Diagram`, `Save`, `StyleSheet` | Create a batch script that loads diagrams applies a custom stylesheet and saves them as updated files |
| [create-a-function-that-adds-a-caption-text-shape-below-each-image-using-the-image-name-as-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-function-that-adds-a-caption-text-shape-below-each-image-using-the-image-name-as-text.cs) | `Diagram`, `Pages`, `Save` | Create a function that adds a caption text shape below each image using the image name as text |
| [create-a-macro-that-iterates-through-pages-and-appends-the-configured-watermark.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-macro-that-iterates-through-pages-and-appends-the-configured-watermark.cs) | `AddShape`, `Diagram`, `Pages` | Create a macro that iterates through pages and appends the configured watermark |
| [create-a-new-diagram-and-add-a-text-shape-at-coordinates-2-3-with-specified-dimensions-and-content.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-new-diagram-and-add-a-text-shape-at-coordinates-2-3-with-specified-dimensions-and-content.cs) | `Diagram`, `Save`, `diagram` | Create a new diagram and add a text shape at coordinates 2 3 with specified dimensions and content |
| [create-a-script-that-adds-a-footer-text-shape-to-every-page-displaying-the-page-number-dynamically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-script-that-adds-a-footer-text-shape-to-every-page-displaying-the-page-number-dynamically.cs) | `Diagram`, `Pages`, `Save` | Create a script that adds a footer text shape to every page displaying the page number dynamically |
| [create-a-semi-transparent-text-watermark-layer-for-the-current-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-semi-transparent-text-watermark-layer-for-the-current-page.cs) | `Diagram`, `Page`, `Pages` | Create a semi transparent text watermark layer for the current page |
| [create-a-utility-that-adds-a-legend-text-shape-describing-color-codes-used-in-styled-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-utility-that-adds-a-legend-text-shape-describing-color-codes-used-in-styled-shapes.cs) | `Diagram`, `Pages`, `Save` | Create a utility that adds a legend text shape describing color codes used in styled shapes |
| [create-a-utility-that-adds-a-watermark-text-shape-diagonally-across-each-page-with-thirty-percent-opacity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/create-a-utility-that-adds-a-watermark-text-shape-diagonally-across-each-page-with-thirty-percent-opacity.cs) | `Diagram`, `Pages`, `Save` | Create a utility that adds a watermark text shape diagonally across each page with thirty percent opacity |
| [document-the-watermark-addition-workflow-with-code-examples-and-usage-guidelines.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/document-the-watermark-addition-workflow-with-code-examples-and-usage-guidelines.cs) | `Diagram`, `Pages`, `Save` | Document the watermark addition workflow with code examples and usage guidelines |
| [ensure-the-watermark-does-not-obscure-shape-text-by-setting-appropriate-layering.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/ensure-the-watermark-does-not-obscure-shape-text-by-setting-appropriate-layering.cs) | `AddShape`, `Diagram`, `Pages` | Ensure the watermark does not obscure shape text by setting appropriate layering |
| [ensure-the-watermark-respects-page-margins-and-does-not-extend-beyond-printable-area.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/ensure-the-watermark-respects-page-margins-and-does-not-extend-beyond-printable-area.cs) | `Diagram`, `Pages`, `Save` | Ensure the watermark respects page margins and does not extend beyond printable area |
| [export-the-modified-diagram-to-a-new-vsdx-file-preserving-original-content.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/export-the-modified-diagram-to-a-new-vsdx-file-preserving-original-content.cs) | `Diagram`, `Save`, `diagram` | Export the modified diagram to a new vsdx file preserving original content |
| [extract-plain-text-from-a-diagram-count-total-word-occurrences-and-generate-a-frequency-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-a-diagram-count-total-word-occurrences-and-generate-a-frequency-report.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from a diagram count total word occurrences and generate a frequency report |
| [extract-plain-text-from-a-diagram-filter-out-numeric-strings-and-save-the-cleaned-content.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-a-diagram-filter-out-numeric-strings-and-save-the-cleaned-content.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from a diagram filter out numeric strings and save the cleaned content |
| [extract-plain-text-from-a-diagram-filter-out-stopwords-and-generate-a-concise-summary-paragraph.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-a-diagram-filter-out-stopwords-and-generate-a-concise-summary-paragraph.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from a diagram filter out stopwords and generate a concise summary paragraph |
| [extract-plain-text-from-a-diagram-remove-all-punctuation-and-save-the-sanitized-text-to-a-log-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-a-diagram-remove-all-punctuation-and-save-the-sanitized-text-to-a-log-file.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from a diagram remove all punctuation and save the sanitized text to a log file |
| [extract-plain-text-from-each-page-concatenate-the-results-and-generate-a-summary-report-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-each-page-concatenate-the-results-and-generate-a-summary-report-file.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from each page concatenate the results and generate a summary report file |
| [extract-plain-text-from-each-page-create-a-csv-file-with-page-number-and-text-content-for-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-each-page-create-a-csv-file-with-page-number-and-text-content-for-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from each page create a csv file with page number and text content for analysis |
| [extract-plain-text-from-each-shape-compute-character-count-and-annotate-each-shape-with-its-length.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-each-shape-compute-character-count-and-annotate-each-shape-with-its-length.cs) | `Diagram`, `Pages`, `Save` | Extract plain text from each shape compute character count and annotate each shape with its length |
| [extract-plain-text-from-each-shape-sort-the-texts-alphabetically-and-output-the-ordered-list-to-a-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-each-shape-sort-the-texts-alphabetically-and-output-the-ordered-list-to-a-file.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from each shape sort the texts alphabetically and output the ordered list to a file |
| [extract-plain-text-from-page-one-of-the-loaded-diagram-and-write-it-to-a-utf-8-encoded-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/extract-plain-text-from-page-one-of-the-loaded-diagram-and-write-it-to-a-utf-8-encoded-file.cs) | `Diagram`, `Pages`, `Shapes` | Extract plain text from page one of the loaded diagram and write it to a utf 8 encoded file |
| [find-and-replace-the-phrase-draft-with-final-across-all-shapes-on-page-two-of-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/find-and-replace-the-phrase-draft-with-final-across-all-shapes-on-page-two-of-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Find and replace the phrase draft with final across all shapes on page two of the diagram |
| [find-and-replace-the-placeholder-date-with-the-current-system-date-in-all-shapes-on-page-three.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/find-and-replace-the-placeholder-date-with-the-current-system-date-in-all-shapes-on-page-three.cs) | `Diagram`, `Pages`, `Save` | Find and replace the placeholder date with the current system date in all shapes on page three |
| [find-and-replace-the-string-v1-0-with-v2-0-in-shapes-that-have-a-custom-property-version-set.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/find-and-replace-the-string-v1-0-with-v2-0-in-shapes-that-have-a-custom-property-version-set.cs) | `Diagram`, `Pages`, `Save` | Find and replace the string v1 0 with v2 0 in shapes that have a custom property version set |
| [find-and-replace-the-string-version-1-0-with-version-2-0-only-on-shapes-on-the-first-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/find-and-replace-the-string-version-1-0-with-version-2-0-only-on-shapes-on-the-first-page.cs) | `Diagram`, `Pages`, `Save` | Find and replace the string version 1 0 with version 2 0 only on shapes on the first page |
| [find-shapes-containing-the-word-confidential-and-replace-it-with-public-while-preserving-other-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/find-shapes-containing-the-word-confidential-and-replace-it-with-public-while-preserving-other-text.cs) | `Diagram`, `Pages`, `Save` | Find shapes containing the word confidential and replace it with public while preserving other text |
| [find-shapes-with-empty-text-fields-and-automatically-populate-them-with-a-generated-unique-identifier.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/find-shapes-with-empty-text-fields-and-automatically-populate-them-with-a-generated-unique-identifier.cs) | `Diagram`, `Pages`, `Save` | Find shapes with empty text fields and automatically populate them with a generated unique identifier |
| [generate-a-preview-image-of-each-page-with-the-watermark-overlay-for-verification.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/generate-a-preview-image-of-each-page-with-the-watermark-overlay-for-verification.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate a preview image of each page with the watermark overlay for verification |
| [handle-missing-font-exceptions-when-rendering-watermark-text-on-loaded-diagrams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/handle-missing-font-exceptions-when-rendering-watermark-text-on-loaded-diagrams.cs) | `Diagram`, `Fonts`, `Pages` | Handle missing font exceptions when rendering watermark text on loaded diagrams |
| [implement-batch-processing-to-add-watermarks-to-all-vsdx-files-in-a-folder.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/implement-batch-processing-to-add-watermarks-to-all-vsdx-files-in-a-folder.cs) | `Diagram`, `Pages`, `Save` | Implement batch processing to add watermarks to all vsdx files in a folder |
| [insert-an-image-watermark-behind-existing-shapes-on-each-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/insert-an-image-watermark-behind-existing-shapes-on-each-page.cs) | `AddShape`, `Diagram`, `Pages` | Insert an image watermark behind existing shapes on each page |
| [iterate-through-characters-of-a-shape-s-text-change-font-size-for-numeric-characters-and-keep-others-unchanged.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/iterate-through-characters-of-a-shape-s-text-change-font-size-for-numeric-characters-and-keep-others-unchanged.cs) | `Diagram`, `Pages`, `Save` | Iterate through characters of a shape s text change font size for numeric characters and keep others unchanged |
| [iterate-through-characters-of-a-shape-s-text-underline-vowels-and-leave-consonants-unchanged.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/iterate-through-characters-of-a-shape-s-text-underline-vowels-and-leave-consonants-unchanged.cs) | `Diagram`, `Pages`, `Save` | Iterate through characters of a shape s text underline vowels and leave consonants unchanged |
| [iterate-through-each-character-in-shape-id-7-assigning-bold-formatting-to-the-first-three-characters.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/iterate-through-each-character-in-shape-id-7-assigning-bold-formatting-to-the-first-three-characters.cs) | `Diagram`, `Pages`, `Save` | Iterate through each character in shape id 7 assigning bold formatting to the first three characters |
| [load-a-custom-vss-file-and-apply-the-customstyle-stylesheet-to-shape-id-12-for-unique-appearance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/load-a-custom-vss-file-and-apply-the-customstyle-stylesheet-to-shape-id-12-for-unique-appearance.cs) | `Diagram`, `Pages`, `Save` | Load a custom vss file and apply the customstyle stylesheet to shape id 12 for unique appearance |
| [load-a-diagram-find-shapes-with-empty-text-and-populate-them-with-default-placeholder-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/load-a-diagram-find-shapes-with-empty-text-and-populate-them-with-default-placeholder-values.cs) | `Diagram`, `Pages`, `Save` | Load a diagram find shapes with empty text and populate them with default placeholder values |
| [load-a-diagram-find-shapes-with-text-longer-than-one-hundred-characters-and-truncate-them-with-ellipsis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/load-a-diagram-find-shapes-with-text-longer-than-one-hundred-characters-and-truncate-them-with-ellipsis.cs) | `Diagram`, `Pages`, `Save` | Load a diagram find shapes with text longer than one hundred characters and truncate them with ellipsis |
| [load-a-visio-diagram-from-a-vsdx-file-into-memory.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/load-a-visio-diagram-from-a-vsdx-file-into-memory.cs) | `Diagram`, `Pages`, `diagram` | Load a visio diagram from a vsdx file into memory |
| [load-an-existing-vdx-file-retrieve-shape-id-5-and-replace-its-text-with-a-dynamic-string.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/load-an-existing-vdx-file-retrieve-shape-id-5-and-replace-its-text-with-a-dynamic-string.cs) | `Diagram`, `Pages`, `Save` | Load an existing vdx file retrieve shape id 5 and replace its text with a dynamic string |
| [load-multiple-vdx-files-from-a-directory-update-their-header-shapes-with-the-current-date-and-save-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/load-multiple-vdx-files-from-a-directory-update-their-header-shapes-with-the-current-date-and-save-changes.cs) | `Diagram`, `Save`, `diagram` | Load multiple vdx files from a directory update their header shapes with the current date and save changes |
| [log-the-success-or-failure-of-watermark-addition-for-each-processed-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/log-the-success-or-failure-of-watermark-addition-for-each-processed-file.cs) | `Diagram`, `Pages`, `Save` | Log the success or failure of watermark addition for each processed file |
| [optimize-watermark-rendering-performance-for-large-diagrams-with-many-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/optimize-watermark-rendering-performance-for-large-diagrams-with-many-pages.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Optimize watermark rendering performance for large diagrams with many pages |
| [position-the-watermark-at-the-center-of-each-page-with-a-45-degree-rotation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/position-the-watermark-at-the-center-of-each-page-with-a-45-degree-rotation.cs) | `Diagram`, `Pages`, `Save` | Position the watermark at the center of each page with a 45 degree rotation |
| [provide-an-api-method-to-retrieve-current-watermark-settings-from-a-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/provide-an-api-method-to-retrieve-current-watermark-settings-from-a-diagram.cs) | `Diagram`, `Pages`, `Shapes` | Provide an api method to retrieve current watermark settings from a diagram |
| [provide-an-option-to-remove-existing-watermarks-from-a-diagram-before-adding-new-ones.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/provide-an-option-to-remove-existing-watermarks-from-a-diagram-before-adding-new-ones.cs) | `Diagram`, `Pages`, `Save` | Provide an option to remove existing watermarks from a diagram before adding new ones |
| [read-watermark-text-and-opacity-settings-from-a-configuration-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/read-watermark-text-and-opacity-settings-from-a-configuration-file.cs) | `Diagram`, `Pages`, `Save` | Read watermark text and opacity settings from a configuration file |
| [replace-all-occurrences-of-the-ampersand-character-with-the-word-and-in-every-shape-s-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-all-occurrences-of-the-ampersand-character-with-the-word-and-in-every-shape-s-text.cs) | `Diagram`, `Pages`, `Save` | Replace all occurrences of the ampersand character with the word and in every shape s text |
| [replace-all-occurrences-of-the-trademark-symbol-with-the-word-trademark-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-all-occurrences-of-the-trademark-symbol-with-the-word-trademark-in-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Replace all occurrences of the trademark symbol with the word trademark in the diagram |
| [replace-all-occurrences-of-the-word-confidential-with-public-only-in-shapes-tagged-as-review.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-all-occurrences-of-the-word-confidential-with-public-only-in-shapes-tagged-as-review.cs) | `Diagram`, `Pages`, `Save` | Replace all occurrences of the word confidential with public only in shapes tagged as review |
| [replace-all-occurrences-of-todo-with-an-empty-string-and-log-affected-shape-ids.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-all-occurrences-of-todo-with-an-empty-string-and-log-affected-shape-ids.cs) | `Diagram`, `Pages`, `Save` | Replace all occurrences of todo with an empty string and log affected shape ids |
| [replace-double-spaces-in-shape-texts-with-single-spaces-to-improve-text-compactness-across-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-double-spaces-in-shape-texts-with-single-spaces-to-improve-text-compactness-across-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Replace double spaces in shape texts with single spaces to improve text compactness across the diagram |
| [replace-line-breaks-in-shape-text-with-spaces-to-ensure-single-line-display-in-exported-reports.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-line-breaks-in-shape-text-with-spaces-to-ensure-single-line-display-in-exported-reports.cs) | `Diagram`, `Pages`, `Save` | Replace line breaks in shape text with spaces to ensure single line display in exported reports |
| [replace-placeholder-text-name-with-actual-user-names-in-every-shape-on-the-diagram-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/replace-placeholder-text-name-with-actual-user-names-in-every-shape-on-the-diagram-page.cs) | `Diagram`, `Save`, `diagram` | Replace placeholder text name with actual user names in every shape on the diagram page |
| [rotate-text-of-all-footer-shapes-by-one-hundred-eighty-degrees-to-display-upside-down-information-on-printed-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/rotate-text-of-all-footer-shapes-by-one-hundred-eighty-degrees-to-display-upside-down-information-on-printed-pages.cs) | `Diagram`, `Pages`, `Save` | Rotate text of all footer shapes by one hundred eighty degrees to display upside down information on printed pages |
| [rotate-text-of-shapes-on-page-five-by-minus-thirty-degrees-to-match-diagonal-layout-of-associated-graphics.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/rotate-text-of-shapes-on-page-five-by-minus-thirty-degrees-to-match-diagonal-layout-of-associated-graphics.cs) | `Diagram`, `Pages`, `Save` | Rotate text of shapes on page five by minus thirty degrees to match diagonal layout of associated graphics |
| [rotate-the-text-of-shape-id-3-by-45-degrees-to-align-with-a-slanted-connector-line.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/rotate-the-text-of-shape-id-3-by-45-degrees-to-align-with-a-slanted-connector-line.cs) | `Diagram`, `Pages`, `Save` | Rotate the text of shape id 3 by 45 degrees to align with a slanted connector line |
| [save-a-backup-copy-of-the-original-diagram-before-applying-any-watermark-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/save-a-backup-copy-of-the-original-diagram-before-applying-any-watermark-changes.cs) | `Diagram`, `Save`, `diagram` | Save a backup copy of the original diagram before applying any watermark changes |
| [set-text-rotation-to-270-degrees-for-all-side-label-shapes-and-verify-orientation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/set-text-rotation-to-270-degrees-for-all-side-label-shapes-and-verify-orientation.cs) | `Diagram`, `Pages`, `Save` | Set text rotation to 270 degrees for all side label shapes and verify orientation |
| [set-text-rotation-to-90-degrees-for-all-shapes-on-page-three-to-create-vertical-labels.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/set-text-rotation-to-90-degrees-for-all-shapes-on-page-three-to-create-vertical-labels.cs) | `Diagram`, `Pages`, `Save` | Set text rotation to 90 degrees for all shapes on page three to create vertical labels |
| [set-watermark-transparency-to-match-corporate-branding-guidelines-for-consistent-appearance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/set-watermark-transparency-to-match-corporate-branding-guidelines-for-consistent-appearance.cs) | `Diagram`, `Pages`, `Save` | Set watermark transparency to match corporate branding guidelines for consistent appearance |
| [update-the-watermark-text-dynamically-based-on-metadata-stored-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/update-the-watermark-text-dynamically-based-on-metadata-stored-in-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Update the watermark text dynamically based on metadata stored in the diagram |
| [use-a-diagonal-watermark-pattern-that-repeats-across-the-page-background.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/use-a-diagonal-watermark-pattern-that-repeats-across-the-page-background.cs) | `Diagram`, `Page`, `Pages` | Use a diagonal watermark pattern that repeats across the page background |
| [validate-that-the-watermark-appears-correctly-on-every-page-after-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/validate-that-the-watermark-appears-correctly-on-every-page-after-saving.cs) | `Diagram`, `Page`, `Pages` | Validate that the watermark appears correctly on every page after saving |
| [verify-that-the-watermark-does-not-increase-file-size-beyond-a-specified-limit.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text/verify-that-the-watermark-does-not-increase-file-size-beyond-a-specified-limit.cs) | `Diagram`, `Pages`, `Save` | Verify that the watermark does not increase file size beyond a specified limit |

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
- `ImageSaveOptions`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `Save`
- `Shapes`
- `StyleSheet`
- `StyleSheets`
- `diagram`
- `page`
- `stylesheet`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with text capabilities are applied in production applications:

- Updating diagram labels from live data feeds or databases
- Localizing diagram text for multi-language documentation
- Searching and replacing text across large batches of Visio files

## Developer Q&A

Frequently asked questions about **Working With Text** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Text in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.5.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

**Q: How do I get the plain text content of a shape?**

A: Use `shape.Text.Value.ToString()` which concatenates all text runs. Never use `shape.IsTextEmpty` — it does not exist. Check emptiness with `string.IsNullOrWhiteSpace(shape.Text.Value.ToString())`.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Working With Text Boxes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes) — standalone text box elements
- [Working With Fields](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields) — dynamic field values in shapes
- [Font Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations) — font configuration and detection

## Category Statistics

- Total examples: 89
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-06-23 | Examples: 89 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
