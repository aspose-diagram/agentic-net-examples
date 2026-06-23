---
category: working-with-pages
display_name: Working With Pages
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 168
pass_rate: 100.0
generated: 2026-06-23
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Pages

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Pages** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 168 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-23 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Pages** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with pages operations.
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
| `System` | 168 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 163 | Core diagram API |
| `System.IO` | 117 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 73 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `Aspose.Diagram.AutoLayout` | 20 | Supporting utilities |
| `System.Collections.Generic` | 17 | List, Dictionary, HashSet |
| `System.Text.Json` | 4 | JSON serialization |
| `Aspose.Diagram.Printing` | 3 | Supporting utilities |
| `System.Diagnostics` | 3 | Supporting utilities |
| `Aspose.Diagram.Manipulation` | 3 | Supporting utilities |
| `System.Threading.Tasks` | 3 | Supporting utilities |
| `System.Text` | 2 | StringBuilder |
| `Aspose.Diagram.Properties` | 1 | Supporting utilities |
| `System.Xml.Linq` | 1 | Supporting utilities |
| `System.IO.Compression` | 1 | Supporting utilities |
| `System.Drawing.Drawing2D` | 1 | Supporting utilities |
| `System.Threading` | 1 | Supporting utilities |
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

- PAGE SIZE — Set page dimensions using page.PageSheet.PageProps.PageHeight.Value and page.PageSheet.PageProps.PageWidth.Value (values are in inches as double).
- Example: page.PageSheet.PageProps.PageHeight.Value = 8; page.PageSheet.PageProps.PageWidth.Value = 11;
- PAGE AUTOEXPAND — Read autoexpand state using page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically.
- Set autoexpand using page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;
- Valid DrawingResizeTypeValue members: Automatically, NotAutomatically.
- RETRIEVE PAGE INFO — Iterate pages using foreach (Aspose.Diagram.Page page in diagram.Pages).
- Check background page: page.Background == Aspose.Diagram.BOOL.True
- Access page ID: page.ID
- Access page name: page.Name
- Access universal name: page.NameU

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-custom-shape-from-a-user-defined-master-file-to-page-five-and-set-its-text-label.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/add-a-custom-shape-from-a-user-defined-master-file-to-page-five-and-set-its-text-label.cs) | `AddMaster`, `Diagram`, `Page` | Add a custom shape from a user defined master file to page five and set its text label |
| [add-a-dynamic-connector-shape-to-page-zero-using-the-connector-master-and-name-it-linkconnector.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/add-a-dynamic-connector-shape-to-page-zero-using-the-connector-master-and-name-it-linkconnector.cs) | `AddShape`, `Diagram`, `Pages` | Add a dynamic connector shape to page zero using the connector master and name it linkconnector |
| [add-a-new-blank-page-to-an-existing-diagram-and-set-its-size-to-a4-dimensions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/add-a-new-blank-page-to-an-existing-diagram-and-set-its-size-to-a4-dimensions.cs) | `Diagram`, `Page`, `Pages` | Add a new blank page to an existing diagram and set its size to a4 dimensions |
| [add-a-rectangle-shape-to-page-one-using-the-rectangle-master-and-set-its-width-to-2-inches.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/add-a-rectangle-shape-to-page-one-using-the-rectangle-master-and-set-its-width-to-2-inches.cs) | `AddShape`, `Diagram`, `Pages` | Add a rectangle shape to page one using the rectangle master and set its width to 2 inches |
| [adjust-printpageorientation-of-background-pages-to-portrait-while-leaving-foreground-pages-unchanged.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/adjust-printpageorientation-of-background-pages-to-portrait-while-leaving-foreground-pages-unchanged.cs) | `Diagram`, `Pages`, `Save` | Adjust printpageorientation of background pages to portrait while leaving foreground pages unchanged |
| [after-changing-page-size-refresh-the-diagram-s-page-thumbnails-to-reflect-new-dimensions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/after-changing-page-size-refresh-the-diagram-s-page-thumbnails-to-reflect-new-dimensions.cs) | `Diagram`, `Pages`, `Save` | After changing page size refresh the diagram s page thumbnails to reflect new dimensions |
| [after-inserting-a-blank-page-set-its-background-property-to-true-to-designate-it-as-a-background-layer.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/after-inserting-a-blank-page-set-its-background-property-to-true-to-designate-it-as-a-background-layer.cs) | `Diagram`, `Page`, `Pages` | After inserting a blank page set its background property to true to designate it as a background layer |
| [after-loading-a-diagram-enumerate-pages-and-write-their-ids-and-names-to-a-csv-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/after-loading-a-diagram-enumerate-pages-and-write-their-ids-and-names-to-a-csv-file.cs) | `Diagram`, `Pages`, `diagram` | After loading a diagram enumerate pages and write their ids and names to a csv file |
| [after-resizing-recalculate-connector-routes-to-maintain-diagram-integrity-throughout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/after-resizing-recalculate-connector-routes-to-maintain-diagram-integrity-throughout.cs) | `Diagram`, `Layout`, `LayoutOptions` | After resizing recalculate connector routes to maintain diagram integrity throughout |
| [apply-a-custom-naming-convention-to-all-pages-by-prefixing-existing-names-with-section.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-a-custom-naming-convention-to-all-pages-by-prefixing-existing-names-with-section.cs) | `Diagram`, `Pages`, `Save` | Apply a custom naming convention to all pages by prefixing existing names with section |
| [apply-a-custom-page-border-style-with-dashed-lines-and-specific-thickness-to-selected-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-a-custom-page-border-style-with-dashed-lines-and-specific-thickness-to-selected-pages.cs) | `Diagram`, `Pages`, `Save` | Apply a custom page border style with dashed lines and specific thickness to selected pages |
| [apply-a-minimum-page-height-constraint-to-ensure-no-page-becomes-shorter-than-5-inches.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-a-minimum-page-height-constraint-to-ensure-no-page-becomes-shorter-than-5-inches.cs) | `Diagram`, `Pages`, `Save` | Apply a minimum page height constraint to ensure no page becomes shorter than 5 inches |
| [apply-a-page-level-background-fill-color-to-page-one-by-modifying-the-page-backgroundfill-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-a-page-level-background-fill-color-to-page-one-by-modifying-the-page-backgroundfill-property.cs) | `Diagram`, `Page`, `Pages` | Apply a page level background fill color to page one by modifying the page backgroundfill property |
| [apply-a-theme-stylesheet-that-changes-the-font-family-across-all-pages-to-arial-for-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-a-theme-stylesheet-that-changes-the-font-family-across-all-pages-to-arial-for-consistency.cs) | `Diagram`, `Pages`, `Save` | Apply a theme stylesheet that changes the font family across all pages to arial for consistency |
| [apply-a-watermark-text-to-every-page-in-a-diagram-with-adjustable-opacity-and-rotation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-a-watermark-text-to-every-page-in-a-diagram-with-adjustable-opacity-and-rotation.cs) | `Diagram`, `Pages`, `Save` | Apply a watermark text to every page in a diagram with adjustable opacity and rotation |
| [apply-an-existing-stylesheet-to-page-zero-using-the-page-applystyle-method.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-an-existing-stylesheet-to-page-zero-using-the-page-applystyle-method.cs) | `Pages`, `Save`, `StyleSheets` | Apply an existing stylesheet to page zero using the page applystyle method |
| [apply-auto-spacing-across-multiple-pages-in-a-batch-process-to-standardize-diagram-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-auto-spacing-across-multiple-pages-in-a-batch-process-to-standardize-diagram-layout.cs) | `AutoSpaceShapes`, `Pages`, `Save` | Apply auto spacing across multiple pages in a batch process to standardize diagram layout |
| [apply-auto-spacing-to-every-shape-on-a-page-without-pre-selecting-a-specific-collection.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-auto-spacing-to-every-shape-on-a-page-without-pre-selecting-a-specific-collection.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Apply auto spacing to every shape on a page without pre selecting a specific collection |
| [apply-auto-spacing-to-the-selected-shape-collection-on-a-page-using-configured-autospaceoptions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-auto-spacing-to-the-selected-shape-collection-on-a-page-using-configured-autospaceoptions.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Apply auto spacing to the selected shape collection on a page using configured autospaceoptions |
| [apply-dependency-injection-to-provide-autospaceoptions-to-services-handling-diagram-manipulation-for-testability.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-dependency-injection-to-provide-autospaceoptions-to-services-handling-diagram-manipulation-for-testability.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Apply dependency injection to provide autospaceoptions to services handling diagram manipulation for testability |
| [apply-different-page-orientations-based-on-page-index-rotating-landscape-pages-to-portrait-dimensions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/apply-different-page-orientations-based-on-page-index-rotating-landscape-pages-to-portrait-dimensions.cs) | `Diagram`, `Pages`, `Save` | Apply different page orientations based on page index rotating landscape pages to portrait dimensions |
| [automate-the-creation-of-a-table-of-contents-page-that-lists-all-page-titles-with-hyperlinks.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/automate-the-creation-of-a-table-of-contents-page-that-lists-all-page-titles-with-hyperlinks.cs) | `Diagram`, `Page`, `Pages` | Automate the creation of a table of contents page that lists all page titles with hyperlinks |
| [batch-process-a-folder-of-vdx-files-set-each-page-orientation-to-portrait-and-save-as-pdfs.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/batch-process-a-folder-of-vdx-files-set-each-page-orientation-to-portrait-and-save-as-pdfs.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Batch process a folder of vdx files set each page orientation to portrait and save as pdfs |
| [batch-process-a-folder-of-visio-files-setting-each-first-page-height-to-14-inches.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/batch-process-a-folder-of-visio-files-setting-each-first-page-height-to-14-inches.cs) | `Diagram`, `Pages`, `Save` | Batch process a folder of visio files setting each first page height to 14 inches |
| [benchmark-auto-spacing-performance-on-diagrams-containing-thousands-of-shapes-to-identify-bottlenecks.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/benchmark-auto-spacing-performance-on-diagrams-containing-thousands-of-shapes-to-identify-bottlenecks.cs) | `AddShape`, `AutoSpaceOptions`, `AutoSpaceShapes` | Benchmark auto spacing performance on diagrams containing thousands of shapes to identify bottlenecks |
| [benchmark-the-time-required-to-add-100-rectangle-shapes-to-a-single-page-and-log-the-duration.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/benchmark-the-time-required-to-add-100-rectangle-shapes-to-a-single-page-and-log-the-duration.cs) | `Diagram`, `Pages`, `Save` | Benchmark the time required to add 100 rectangle shapes to a single page and log the duration |
| [cache-autospaceoptions-instances-for-reuse-across-multiple-diagrams-to-improve-performance-significantly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/cache-autospaceoptions-instances-for-reuse-across-multiple-diagrams-to-improve-performance-significantly.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Cache autospaceoptions instances for reuse across multiple diagrams to improve performance significantly |
| [call-the-diagram-s-validate-method-after-spacing-to-confirm-diagram-integrity-and-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/call-the-diagram-s-validate-method-after-spacing-to-confirm-diagram-integrity-and-consistency.cs) | `Diagram`, `Layout`, `LayoutOptions` | Call the diagram s validate method after spacing to confirm diagram integrity and consistency |
| [catch-exceptions-thrown-during-auto-spacing-and-log-error-messages-in-the-application-for-troubleshooting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/catch-exceptions-thrown-during-auto-spacing-and-log-error-messages-in-the-application-for-troubleshooting.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Catch exceptions thrown during auto spacing and log error messages in the application for troubleshooting |
| [change-the-background-color-of-a-specific-page-to-light-gray-using-page-formatting-options.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/change-the-background-color-of-a-specific-page-to-light-gray-using-page-formatting-options.cs) | `Diagram`, `Page`, `Pages` | Change the background color of a specific page to light gray using page formatting options |
| [check-whether-auto-expand-is-enabled-on-a-page-before-applying-custom-page-size-adjustments.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/check-whether-auto-expand-is-enabled-on-a-page-before-applying-custom-page-size-adjustments.cs) | `Diagram`, `Pages`, `Save` | Check whether auto expand is enabled on a page before applying custom page size adjustments |
| [clone-page-two-into-a-new-page-rename-it-and-apply-a-different-stylesheet-to-the-clone.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/clone-page-two-into-a-new-page-rename-it-and-apply-a-different-stylesheet-to-the-clone.cs) | `Diagram`, `Page`, `Pages` | Clone page two into a new page rename it and apply a different stylesheet to the clone |
| [compare-performance-of-resizing-pages-individually-versus-applying-a-uniform-size-to-all-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/compare-performance-of-resizing-pages-individually-versus-applying-a-uniform-size-to-all-pages.cs) | `Diagram`, `Pages`, `Save` | Compare performance of resizing pages individually versus applying a uniform size to all pages |
| [compare-two-pages-for-visual-differences-and-generate-a-diff-report-highlighting-changed-elements.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/compare-two-pages-for-visual-differences-and-generate-a-diff-report-highlighting-changed-elements.cs) | `Diagram`, `Pages`, `diagram` | Compare two pages for visual differences and generate a diff report highlighting changed elements |
| [configure-a-page-to-follow-printer-defaults-by-setting-printpageorientation-sameasprinter.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/configure-a-page-to-follow-printer-defaults-by-setting-printpageorientation-sameasprinter.cs) | `Diagram`, `Page`, `Pages` | Configure a page to follow printer defaults by setting printpageorientation sameasprinter |
| [configure-page-margins-to-0-5-inches-on-all-sides-for-page-zero-before-adding-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/configure-page-margins-to-0-5-inches-on-all-sides-for-page-zero-before-adding-shapes.cs) | `AddShape`, `Diagram`, `Page` | Configure page margins to 0 5 inches on all sides for page zero before adding shapes |
| [configure-the-horizontaldistance-property-on-autospaceoptions-to-define-custom-horizontal-spacing-gaps.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/configure-the-horizontaldistance-property-on-autospaceoptions-to-define-custom-horizontal-spacing-gaps.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Configure the horizontaldistance property on autospaceoptions to define custom horizontal spacing gaps |
| [configure-the-verticaldistance-property-on-autospaceoptions-to-define-custom-vertical-spacing-gaps.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/configure-the-verticaldistance-property-on-autospaceoptions-to-define-custom-vertical-spacing-gaps.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Configure the verticaldistance property on autospaceoptions to define custom vertical spacing gaps |
| [connect-the-two-rectangle-shapes-with-the-connector-using-page-connectshapesviaconnector-specifying-source-and-target-id.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/connect-the-two-rectangle-shapes-with-the-connector-using-page-connectshapesviaconnector-specifying-source-and-target-id.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Connect the two rectangle shapes with the connector using page connectshapesviaconnector specifying source and target id |
| [convert-a-page-to-svg-format-and-embed-custom-css-styles-for-shapes-and-connectors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/convert-a-page-to-svg-format-and-embed-custom-css-styles-for-shapes-and-connectors.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Convert a page to svg format and embed custom css styles for shapes and connectors |
| [copy-a-page-from-a-source-diagram-modify-its-background-flag-and-add-to-destination-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/copy-a-page-from-a-source-diagram-modify-its-background-flag-and-add-to-destination-diagram.cs) | `Diagram`, `Page`, `page` | Copy a page from a source diagram modify its background flag and add to destination diagram |
| [copy-a-page-from-one-diagram-and-insert-it-into-another-diagram-at-a-specified-index.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/copy-a-page-from-one-diagram-and-insert-it-into-another-diagram-at-a-specified-index.cs) | `Diagram`, `Page`, `page` | Copy a page from one diagram and insert it into another diagram at a specified index |
| [copy-a-source-page-from-one-diagram-and-add-it-to-the-target-diagram-s-end.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/copy-a-source-page-from-one-diagram-and-add-it-to-the-target-diagram-s-end.cs) | `Diagram`, `Page` | Copy a source page from one diagram and add it to the target diagram s end |
| [copy-first-pages-from-multiple-diagrams-into-a-master-diagram-while-preserving-original-page-ids.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/copy-first-pages-from-multiple-diagrams-into-a-master-diagram-while-preserving-original-page-ids.cs) | `Diagram`, `Page`, `page` | Copy first pages from multiple diagrams into a master diagram while preserving original page ids |
| [create-a-command-line-tool-that-accepts-width-and-height-arguments-to-resize-a-specified-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-command-line-tool-that-accepts-width-and-height-arguments-to-resize-a-specified-page.cs) | `Diagram`, `Pages`, `Save` | Create a command line tool that accepts width and height arguments to resize a specified page |
| [create-a-gradient-fill-stylesheet-and-apply-it-to-page-five-for-visual-testing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-gradient-fill-stylesheet-and-apply-it-to-page-five-for-visual-testing.cs) | `Diagram`, `Page`, `Pages` | Create a gradient fill stylesheet and apply it to page five for visual testing |
| [create-a-macro-that-iterates-pages-setting-height-to-match-width-for-square-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-macro-that-iterates-pages-setting-height-to-match-width-for-square-layout.cs) | `Diagram`, `Pages`, `Save` | Create a macro that iterates pages setting height to match width for square layout |
| [create-a-new-blank-page-instance-and-insert-it-at-index-two-within-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-new-blank-page-instance-and-insert-it-at-index-two-within-the-diagram.cs) | `Diagram`, `Page`, `Pages` | Create a new blank page instance and insert it at index two within the diagram |
| [create-a-new-diagram-add-a-page-with-specific-dimensions-and-save-as-vsdx.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-new-diagram-add-a-page-with-specific-dimensions-and-save-as-vsdx.cs) | `Diagram`, `Page`, `Pages` | Create a new diagram add a page with specific dimensions and save as vsdx |
| [create-a-new-stylesheet-set-fill-color-to-light-gray-and-add-it-to-the-diagram-collection.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-new-stylesheet-set-fill-color-to-light-gray-and-add-it-to-the-diagram-collection.cs) | `Diagram`, `Save`, `StyleSheet` | Create a new stylesheet set fill color to light gray and add it to the diagram collection |
| [create-a-script-that-copies-pages-from-multiple-source-diagrams-into-a-single-target-diagram-based-on-configuration.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-script-that-copies-pages-from-multiple-source-diagrams-into-a-single-target-diagram-based-on-configuration.cs) | `Diagram` | Create a script that copies pages from multiple source diagrams into a single target diagram based on configuration |
| [create-a-utility-that-duplicates-every-page-renames-copies-with-a-copy-suffix-and-saves-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-utility-that-duplicates-every-page-renames-copies-with-a-copy-suffix-and-saves-diagram.cs) | `Diagram`, `Page`, `Pages` | Create a utility that duplicates every page renames copies with a copy suffix and saves diagram |
| [create-a-utility-that-reads-page-size-from-a-configuration-file-and-applies-it-to-each-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-a-utility-that-reads-page-size-from-a-configuration-file-and-applies-it-to-each-page.cs) | `Diagram`, `Pages`, `Save` | Create a utility that reads page size from a configuration file and applies it to each page |
| [create-an-autospaceoptions-instance-with-default-spacing-values-for-quick-layout-improvement.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-an-autospaceoptions-instance-with-default-spacing-values-for-quick-layout-improvement.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Create an autospaceoptions instance with default spacing values for quick layout improvement |
| [create-pdf-save-options-that-compress-images-while-still-exporting-hidden-pages-for-archival-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-pdf-save-options-that-compress-images-while-still-exporting-hidden-pages-for-archival-purposes.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Create pdf save options that compress images while still exporting hidden pages for archival purposes |
| [create-pdf-save-options-that-embed-all-fonts-enable-hidden-page-export-and-set-a-custom-document-title.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-pdf-save-options-that-embed-all-fonts-enable-hidden-page-export-and-set-a-custom-document-title.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Create pdf save options that embed all fonts enable hidden page export and set a custom document title |
| [create-xps-output-from-the-diagram-while-excluding-hidden-pages-by-leaving-exporthiddenpage-false.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/create-xps-output-from-the-diagram-while-excluding-hidden-pages-by-leaving-exporthiddenpage-false.cs) | `Diagram`, `Save`, `XPSSaveOptions` | Create xps output from the diagram while excluding hidden pages by leaving exporthiddenpage false |
| [delete-page-three-after-confirming-it-contains-no-shapes-to-preserve-data-integrity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/delete-page-three-after-confirming-it-contains-no-shapes-to-preserve-data-integrity.cs) | `Diagram`, `Pages`, `Save` | Delete page three after confirming it contains no shapes to preserve data integrity |
| [delete-the-third-page-from-a-diagram-and-renumber-the-remaining-pages-sequentially.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/delete-the-third-page-from-a-diagram-and-renumber-the-remaining-pages-sequentially.cs) | `Pages`, `Save`, `diagram` | Delete the third page from a diagram and renumber the remaining pages sequentially |
| [detect-and-list-any-orphan-shapes-on-a-page-that-are-not-connected-to-any-other-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/detect-and-list-any-orphan-shapes-on-a-page-that-are-not-connected-to-any-other-shape.cs) | `Diagram`, `Pages`, `Shapes` | Detect and list any orphan shapes on a page that are not connected to any other shape |
| [detect-pages-with-auto-expand-enabled-and-log-their-original-dimensions-before-resizing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/detect-pages-with-auto-expand-enabled-and-log-their-original-dimensions-before-resizing.cs) | `Diagram`, `Pages`, `Save` | Detect pages with auto expand enabled and log their original dimensions before resizing |
| [document-auto-spacing-usage-examples-in-generated-api-documentation-for-developer-reference-clearly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/document-auto-spacing-usage-examples-in-generated-api-documentation-for-developer-reference-clearly.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Document auto spacing usage examples in generated api documentation for developer reference clearly |
| [enable-inclusion-of-hidden-pages-when-saving-to-pdf-by-setting-exporthiddenpage-true.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/enable-inclusion-of-hidden-pages-when-saving-to-pdf-by-setting-exporthiddenpage-true.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Enable inclusion of hidden pages when saving to pdf by setting exporthiddenpage true |
| [enable-page-grid-visibility-on-page-two-to-assist-with-manual-shape-alignment.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/enable-page-grid-visibility-on-page-two-to-assist-with-manual-shape-alignment.cs) | `Diagram`, `Pages`, `Save` | Enable page grid visibility on page two to assist with manual shape alignment |
| [ensure-custom-data-fields-attached-to-shapes-are-preserved-during-auto-spacing-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/ensure-custom-data-fields-attached-to-shapes-are-preserved-during-auto-spacing-processing.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Ensure custom data fields attached to shapes are preserved during auto spacing processing |
| [export-a-diagram-after-auto-spacing-to-svg-format-to-preserve-vector-quality-for-web-display.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-a-diagram-after-auto-spacing-to-svg-format-to-preserve-vector-quality-for-web-display.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Export a diagram after auto spacing to svg format to preserve vector quality for web display |
| [export-a-diagram-with-modified-page-dimensions-to-pdf-while-preserving-original-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-a-diagram-with-modified-page-dimensions-to-pdf-while-preserving-original-layout.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Export a diagram with modified page dimensions to pdf while preserving original layout |
| [export-a-selected-page-as-a-high-resolution-png-image-with-custom-dpi-settings.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-a-selected-page-as-a-high-resolution-png-image-with-custom-dpi-settings.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Export a selected page as a high resolution png image with custom dpi settings |
| [export-page-comments-and-annotations-to-a-separate-xml-document-for-external-review.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-page-comments-and-annotations-to-a-separate-xml-document-for-external-review.cs) | `Diagram`, `Pages`, `diagram` | Export page comments and annotations to a separate xml document for external review |
| [export-resized-pages-to-separate-vsdx-files-naming-each-file-after-its-page-index.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-resized-pages-to-separate-vsdx-files-naming-each-file-after-its-page-index.cs) | `Diagram` | Export resized pages to separate vsdx files naming each file after its page index |
| [export-the-auto-spaced-diagram-to-pdf-format-for-sharing-with-non-visio-users.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-the-auto-spaced-diagram-to-pdf-format-for-sharing-with-non-visio-users.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export the auto spaced diagram to pdf format for sharing with non visio users |
| [export-the-auto-spaced-diagram-to-xps-format-to-enable-high-quality-printing-capabilities.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-the-auto-spaced-diagram-to-xps-format-to-enable-high-quality-printing-capabilities.cs) | `Diagram`, `Save`, `XPSSaveOptions` | Export the auto spaced diagram to xps format to enable high quality printing capabilities |
| [export-the-diagram-to-png-format-ensuring-hidden-pages-are-omitted-by-default-settings.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/export-the-diagram-to-png-format-ensuring-hidden-pages-are-omitted-by-default-settings.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Export the diagram to png format ensuring hidden pages are omitted by default settings |
| [extract-all-shape-names-and-their-positions-from-a-given-page-and-write-them-to-a-csv-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/extract-all-shape-names-and-their-positions-from-a-given-page-and-write-them-to-a-csv-file.cs) | `Diagram`, `Pages`, `Shapes` | Extract all shape names and their positions from a given page and write them to a csv file |
| [filter-shapes-on-a-page-by-type-and-remove-all-connector-shapes-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/filter-shapes-on-a-page-by-type-and-remove-all-connector-shapes-programmatically.cs) | `Diagram`, `Pages`, `Save` | Filter shapes on a page by type and remove all connector shapes programmatically |
| [find-a-page-using-its-textual-name-via-getpage-method-and-set-uivisibility-to-false.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/find-a-page-using-its-textual-name-via-getpage-method-and-set-uivisibility-to-false.cs) | `Diagram`, `Pages`, `Save` | Find a page using its textual name via getpage method and set uivisibility to false |
| [generate-a-pdf-report-listing-each-page-s-metadata-and-include-thumbnails-from-hidden-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/generate-a-pdf-report-listing-each-page-s-metadata-and-include-thumbnails-from-hidden-pages.cs) | `Diagram`, `Page`, `PdfSaveOptions` | Generate a pdf report listing each page s metadata and include thumbnails from hidden pages |
| [generate-a-report-listing-each-page-s-original-and-new-dimensions-after-size-modification.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/generate-a-report-listing-each-page-s-original-and-new-dimensions-after-size-modification.cs) | `Diagram`, `Pages`, `Save` | Generate a report listing each page s original and new dimensions after size modification |
| [generate-a-summary-report-listing-each-page-name-shape-count-and-connector-count.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/generate-a-summary-report-listing-each-page-name-shape-count-and-connector-count.cs) | `Diagram`, `Pages`, `Shapes` | Generate a summary report listing each page name shape count and connector count |
| [generate-a-thumbnail-image-for-each-page-in-a-diagram-and-store-them-in-a-zip-archive.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/generate-a-thumbnail-image-for-each-page-in-a-diagram-and-store-them-in-a-zip-archive.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Generate a thumbnail image for each page in a diagram and store them in a zip archive |
| [generate-an-svg-file-from-the-diagram-and-explicitly-include-hidden-pages-by-enabling-exporthiddenpage.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/generate-an-svg-file-from-the-diagram-and-explicitly-include-hidden-pages-by-enabling-exporthiddenpage.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Generate an svg file from the diagram and explicitly include hidden pages by enabling exporthiddenpage |
| [hide-a-background-page-by-setting-its-uivisibility-property-to-false-preventing-export.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/hide-a-background-page-by-setting-its-uivisibility-property-to-false-preventing-export.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Hide a background page by setting its uivisibility property to false preventing export |
| [implement-a-function-that-returns-true-if-any-page-in-a-diagram-is-hidden-using-uivisibility-checks.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/implement-a-function-that-returns-true-if-any-page-in-a-diagram-is-hidden-using-uivisibility-checks.cs) | `Diagram`, `Pages`, `diagram` | Implement a function that returns true if any page in a diagram is hidden using uivisibility checks |
| [implement-an-asynchronous-method-to-perform-auto-spacing-without-blocking-the-ui-thread.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/implement-an-asynchronous-method-to-perform-auto-spacing-without-blocking-the-ui-thread.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Pages` | Implement an asynchronous method to perform auto spacing without blocking the ui thread |
| [implement-error-handling-for-invalid-page-size-values-when-assigning-to-pageprops.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/implement-error-handling-for-invalid-page-size-values-when-assigning-to-pageprops.cs) | `Diagram`, `Pages`, `Save` | Implement error handling for invalid page size values when assigning to pageprops |
| [implement-error-handling-to-catch-exceptions-when-applying-a-stylesheet-to-a-non-existent-page-index.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/implement-error-handling-to-catch-exceptions-when-applying-a-stylesheet-to-a-non-existent-page-index.cs) | `Diagram`, `Pages`, `Save` | Implement error handling to catch exceptions when applying a stylesheet to a non existent page index |
| [implement-unit-tests-verifying-that-pageprops-width-setter-throws-exception-for-negative-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/implement-unit-tests-verifying-that-pageprops-width-setter-throws-exception-for-negative-values.cs) | `Diagram`, `Pages`, `diagram` | Implement unit tests verifying that pageprops width setter throws exception for negative values |
| [insert-a-header-footer-on-each-page-containing-page-number-and-diagram-title-dynamically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/insert-a-header-footer-on-each-page-containing-page-number-and-diagram-title-dynamically.cs) | `Diagram`, `Save`, `diagram` | Insert a header footer on each page containing page number and diagram title dynamically |
| [insert-a-second-rectangle-shape-on-page-zero-assign-a-unique-id-and-position-at-coordinates-2-3.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/insert-a-second-rectangle-shape-on-page-zero-assign-a-unique-id-and-position-at-coordinates-2-3.cs) | `Diagram`, `Pages`, `Save` | Insert a second rectangle shape on page zero assign a unique id and position at coordinates 2 3 |
| [insert-three-blank-pages-at-the-beginning-of-a-diagram-to-serve-as-placeholders.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/insert-three-blank-pages-at-the-beginning-of-a-diagram-to-serve-as-placeholders.cs) | `Diagram`, `Page`, `Pages` | Insert three blank pages at the beginning of a diagram to serve as placeholders |
| [iterate-over-all-pages-and-apply-a-common-stylesheet-to-each-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/iterate-over-all-pages-and-apply-a-common-stylesheet-to-each-page.cs) | `Diagram`, `Pages`, `Save` | Iterate over all pages and apply a common stylesheet to each page |
| [iterate-through-all-pages-increase-each-page-width-by-10-percent-and-overwrite-original-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/iterate-through-all-pages-increase-each-page-width-by-10-percent-and-overwrite-original-file.cs) | `Diagram`, `Pages`, `Save` | Iterate through all pages increase each page width by 10 percent and overwrite original file |
| [iterate-through-diagram-pages-to-log-each-page-s-id-name-and-background-flag.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/iterate-through-diagram-pages-to-log-each-page-s-id-name-and-background-flag.cs) | `Diagram`, `Pages`, `diagram` | Iterate through diagram pages to log each page s id name and background flag |
| [load-a-diagram-change-orientation-of-every-non-background-page-to-landscape-and-export-to-xps.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-diagram-change-orientation-of-every-non-background-page-to-landscape-and-export-to-xps.cs) | `Diagram`, `Pages`, `Save` | Load a diagram change orientation of every non background page to landscape and export to xps |
| [load-a-diagram-clone-the-third-page-modify-its-height-and-insert-into-the-same-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-diagram-clone-the-third-page-modify-its-height-and-insert-into-the-same-file.cs) | `Diagram`, `Page`, `Pages` | Load a diagram clone the third page modify its height and insert into the same file |
| [load-a-diagram-from-a-byte-array-apply-changes-to-a-page-and-write-to-a-byte-array.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-diagram-from-a-byte-array-apply-changes-to-a-page-and-write-to-a-byte-array.cs) | `Diagram`, `Save`, `diagram` | Load a diagram from a byte array apply changes to a page and write to a byte array |
| [load-a-diagram-from-a-network-stream-modify-a-specific-page-and-save-back-to-the-same-stream.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-diagram-from-a-network-stream-modify-a-specific-page-and-save-back-to-the-same-stream.cs) | `Diagram`, `Pages`, `Save` | Load a diagram from a network stream modify a specific page and save back to the same stream |
| [load-a-diagram-set-page-height-based-on-number-of-shapes-then-save-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-diagram-set-page-height-based-on-number-of-shapes-then-save-changes.cs) | `Diagram`, `Pages`, `Save` | Load a diagram set page height based on number of shapes then save changes |
| [load-a-diagram-temporarily-disable-page-auto-expand-modify-size-then-re-enable-auto-expand.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-diagram-temporarily-disable-page-auto-expand-modify-size-then-re-enable-auto-expand.cs) | `Diagram`, `Pages`, `Save` | Load a diagram temporarily disable page auto expand modify size then re enable auto expand |
| [load-a-vdx-diagram-file-into-a-diagram-object-and-verify-successful-initialization.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-vdx-diagram-file-into-a-diagram-object-and-verify-successful-initialization.cs) | `Diagram`, `Pages`, `diagram` | Load a vdx diagram file into a diagram object and verify successful initialization |
| [load-a-visio-diagram-change-the-first-page-width-to-8-5-inches-and-save.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-visio-diagram-change-the-first-page-width-to-8-5-inches-and-save.cs) | `Diagram`, `Pages`, `Save` | Load a visio diagram change the first page width to 8 5 inches and save |
| [load-a-visio-diagram-from-a-file-and-obtain-its-diagram-object-for-further-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-visio-diagram-from-a-file-and-obtain-its-diagram-object-for-further-processing.cs) | `Diagram`, `Pages`, `diagram` | Load a visio diagram from a file and obtain its diagram object for further processing |
| [load-a-visio-diagram-from-a-file-stream-and-verify-at-least-one-page-exists.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-visio-diagram-from-a-file-stream-and-verify-at-least-one-page-exists.cs) | `Diagram`, `Pages`, `diagram` | Load a visio diagram from a file stream and verify at least one page exists |
| [load-a-visio-diagram-from-a-memory-stream-and-create-a-diagram-instance-for-manipulation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-visio-diagram-from-a-memory-stream-and-create-a-diagram-instance-for-manipulation.cs) | `Diagram`, `Pages`, `diagram` | Load a visio diagram from a memory stream and create a diagram instance for manipulation |
| [load-a-visio-vsdx-file-and-retrieve-the-total-number-of-pages-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-visio-vsdx-file-and-retrieve-the-total-number-of-pages-in-the-diagram.cs) | `Diagram`, `Pages`, `diagram` | Load a visio vsdx file and retrieve the total number of pages in the diagram |
| [load-a-vsdx-diagram-using-the-diagram-constructor-and-ensure-all-pages-are-accessible.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-a-vsdx-diagram-using-the-diagram-constructor-and-ensure-all-pages-are-accessible.cs) | `Diagram`, `Pages`, `diagram` | Load a vsdx diagram using the diagram constructor and ensure all pages are accessible |
| [load-autospaceoptions-settings-from-a-json-file-before-applying-spacing-to-customize-behavior.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-autospaceoptions-settings-from-a-json-file-before-applying-spacing-to-customize-behavior.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Load autospaceoptions settings from a json file before applying spacing to customize behavior |
| [load-diagrams-from-both-vdx-and-vsdx-formats-in-a-single-routine-with-automatic-detection.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/load-diagrams-from-both-vdx-and-vsdx-formats-in-a-single-routine-with-automatic-detection.cs) | `Diagram` | Load diagrams from both vdx and vsdx formats in a single routine with automatic detection |
| [log-details-of-each-shape-s-new-position-to-the-console-after-auto-spacing-completes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/log-details-of-each-shape-s-new-position-to-the-console-after-auto-spacing-completes.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Log details of each shape s new position to the console after auto spacing completes |
| [measure-content-bounding-box-then-adjust-page-width-to-tightly-fit-all-shapes-on-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/measure-content-bounding-box-then-adjust-page-width-to-tightly-fit-all-shapes-on-the-page.cs) | `Diagram`, `Pages`, `Save` | Measure content bounding box then adjust page width to tightly fit all shapes on the page |
| [measure-the-total-number-of-connectors-on-each-page-and-store-the-counts-in-a-dictionary.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/measure-the-total-number-of-connectors-on-each-page-and-store-the-counts-in-a-dictionary.cs) | `Diagram`, `Pages`, `diagram` | Measure the total number of connectors on each page and store the counts in a dictionary |
| [merge-multiple-diagrams-by-appending-all-pages-from-each-source-into-a-single-target-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/merge-multiple-diagrams-by-appending-all-pages-from-each-source-into-a-single-target-diagram.cs) | `Diagram` | Merge multiple diagrams by appending all pages from each source into a single target diagram |
| [mock-diagram-and-page-objects-to-isolate-and-test-auto-spacing-logic-without-loading-actual-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/mock-diagram-and-page-objects-to-isolate-and-test-auto-spacing-logic-without-loading-actual-files.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Shapes` | Mock diagram and page objects to isolate and test auto spacing logic without loading actual files |
| [move-a-hidden-page-to-the-end-of-the-page-collection-and-verify-uivisibility-remains-false.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/move-a-hidden-page-to-the-end-of-the-page-collection-and-verify-uivisibility-remains-false.cs) | `Diagram`, `Pages`, `Save` | Move a hidden page to the end of the page collection and verify uivisibility remains false |
| [move-the-third-page-to-the-first-position-using-pages-move-to-reorder-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/move-the-third-page-to-the-first-position-using-pages-move-to-reorder-layout.cs) | `Diagram`, `Pages`, `Save` | Move the third page to the first position using pages move to reorder layout |
| [optimize-page-content-by-flattening-groups-and-simplifying-complex-shapes-for-faster-rendering.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/optimize-page-content-by-flattening-groups-and-simplifying-complex-shapes-for-faster-rendering.cs) | `Diagram`, `Pages`, `Save` | Optimize page content by flattening groups and simplifying complex shapes for faster rendering |
| [perform-batch-conversion-of-all-pages-in-a-diagram-to-individual-pdf-files-using-parallel-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/perform-batch-conversion-of-all-pages-in-a-diagram-to-individual-pdf-files-using-parallel-processing.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Perform batch conversion of all pages in a diagram to individual pdf files using parallel processing |
| [programmatically-disable-auto-expand-then-assign-a-fixed-page-size-to-prevent-automatic-scaling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/programmatically-disable-auto-expand-then-assign-a-fixed-page-size-to-prevent-automatic-scaling.cs) | `Diagram`, `Pages`, `Save` | Programmatically disable auto expand then assign a fixed page size to prevent automatic scaling |
| [programmatically-hide-all-background-pages-in-the-diagram-then-save-to-pdf-with-hidden-pages-excluded.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/programmatically-hide-all-background-pages-in-the-diagram-then-save-to-pdf-with-hidden-pages-excluded.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Programmatically hide all background pages in the diagram then save to pdf with hidden pages excluded |
| [programmatically-set-uivisibility-to-true-for-all-pages-then-export-to-pdf-ensuring-no-hidden-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/programmatically-set-uivisibility-to-true-for-all-pages-then-export-to-pdf-ensuring-no-hidden-pages.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Programmatically set uivisibility to true for all pages then export to pdf ensuring no hidden pages |
| [provide-a-sample-code-snippet-demonstrating-auto-spacing-with-custom-horizontal-and-vertical-distances.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/provide-a-sample-code-snippet-demonstrating-auto-spacing-with-custom-horizontal-and-vertical-distances.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Provide a sample code snippet demonstrating auto spacing with custom horizontal and vertical distances |
| [recalculate-connector-routing-after-auto-spacing-to-avoid-overlapping-or-broken-connections-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/recalculate-connector-routing-after-auto-spacing-to-avoid-overlapping-or-broken-connections-in-the-diagram.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Recalculate connector routing after auto spacing to avoid overlapping or broken connections in the diagram |
| [reconnect-the-same-shapes-using-page-connectshapesviaconn-method-and-compare-routing-results.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/reconnect-the-same-shapes-using-page-connectshapesviaconn-method-and-compare-routing-results.cs) | `Diagram`, `ImageSaveOptions` | Reconnect the same shapes using page connectshapesviaconn method and compare routing results |
| [render-a-page-to-a-bitmap-image-using-anti-aliasing-and-transparent-background-options.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/render-a-page-to-a-bitmap-image-using-anti-aliasing-and-transparent-background-options.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Render a page to a bitmap image using anti aliasing and transparent background options |
| [reorder-pages-by-moving-the-last-page-to-the-second-position-within-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/reorder-pages-by-moving-the-last-page-to-the-second-position-within-the-diagram.cs) | `Pages`, `Save`, `diagram` | Reorder pages by moving the last page to the second position within the diagram |
| [replace-the-background-image-of-a-page-with-a-new-image-file-while-preserving-aspect-ratio.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/replace-the-background-image-of-a-page-with-a-new-image-file-while-preserving-aspect-ratio.cs) | `AddShape`, `Diagram`, `Pages` | Replace the background image of a page with a new image file while preserving aspect ratio |
| [resize-all-shapes-on-a-page-proportionally-to-fit-within-a-new-page-margin-configuration.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/resize-all-shapes-on-a-page-proportionally-to-fit-within-a-new-page-margin-configuration.cs) | `Diagram`, `Pages`, `Save` | Resize all shapes on a page proportionally to fit within a new page margin configuration |
| [retrieve-a-specific-page-by-its-name-from-the-loaded-diagram-to-target-shape-operations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/retrieve-a-specific-page-by-its-name-from-the-loaded-diagram-to-target-shape-operations.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve a specific page by its name from the loaded diagram to target shape operations |
| [retrieve-a-specific-page-by-its-numeric-id-and-store-the-reference-for-later-modifications.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/retrieve-a-specific-page-by-its-numeric-id-and-store-the-reference-for-later-modifications.cs) | `Diagram`, `Pages`, `diagram` | Retrieve a specific page by its numeric id and store the reference for later modifications |
| [retrieve-all-pages-in-the-diagram-and-iterate-through-them-to-apply-batch-auto-spacing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/retrieve-all-pages-in-the-diagram-and-iterate-through-them-to-apply-batch-auto-spacing.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Retrieve all pages in the diagram and iterate through them to apply batch auto spacing |
| [retrieve-each-page-s-name-and-log-it-to-the-console-for-audit-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/retrieve-each-page-s-name-and-log-it-to-the-console-for-audit-purposes.cs) | `Diagram`, `Pages`, `diagram` | Retrieve each page s name and log it to the console for audit purposes |
| [retrieve-page-properties-for-the-second-page-set-height-to-11-inches-then-export-to-vdx.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/retrieve-page-properties-for-the-second-page-set-height-to-11-inches-then-export-to-vdx.cs) | `Diagram`, `Pages`, `Save` | Retrieve page properties for the second page set height to 11 inches then export to vdx |
| [save-a-particular-page-as-a-pdf-document-while-preserving-vector-graphics-and-text-quality.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/save-a-particular-page-as-a-pdf-document-while-preserving-vector-graphics-and-text-quality.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Save a particular page as a pdf document while preserving vector graphics and text quality |
| [save-the-diagram-containing-hidden-pages-to-html-while-preserving-their-visibility-using-htmlsaveoptions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/save-the-diagram-containing-hidden-pages-to-html-while-preserving-their-visibility-using-htmlsaveoptions.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Save the diagram containing hidden pages to html while preserving their visibility using htmlsaveoptions |
| [save-the-modified-diagram-to-a-memory-stream-for-further-processing-or-transmission.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/save-the-modified-diagram-to-a-memory-stream-for-further-processing-or-transmission.cs) | `Diagram`, `Save`, `diagram` | Save the modified diagram to a memory stream for further processing or transmission |
| [save-the-updated-diagram-to-a-new-file-while-preserving-the-original-visio-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/save-the-updated-diagram-to-a-new-file-while-preserving-the-original-visio-format.cs) | `Diagram`, `Save`, `diagram` | Save the updated diagram to a new file while preserving the original visio format |
| [select-a-shape-collection-by-shape-ids-to-prepare-for-custom-spacing-adjustments.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/select-a-shape-collection-by-shape-ids-to-prepare-for-custom-spacing-adjustments.cs) | `Diagram`, `Pages`, `Save` | Select a shape collection by shape ids to prepare for custom spacing adjustments |
| [select-shapes-based-on-their-master-name-to-apply-targeted-auto-spacing-on-similar-objects.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/select-shapes-based-on-their-master-name-to-apply-targeted-auto-spacing-on-similar-objects.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Select shapes based on their master name to apply targeted auto spacing on similar objects |
| [serialize-autospaceoptions-configuration-to-json-for-external-configuration-management-and-version-control.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/serialize-autospaceoptions-configuration-to-json-for-external-configuration-management-and-version-control.cs) | `AutoSpaceOptions` | Serialize autospaceoptions configuration to json for external configuration management and version control |
| [set-exporthiddenpage-to-true-for-html-png-and-svg-saves-in-a-loop-to-generate-outputs.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-exporthiddenpage-to-true-for-html-png-and-svg-saves-in-a-loop-to-generate-outputs.cs) | `Diagram`, `HTMLSaveOptions`, `ImageSaveOptions` | Set exporthiddenpage to true for html png and svg saves in a loop to generate outputs |
| [set-horizontaldistance-to-zero-in-autospaceoptions-to-align-shapes-vertically-without-horizontal-gaps.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-horizontaldistance-to-zero-in-autospaceoptions-to-align-shapes-vertically-without-horizontal-gaps.cs) | `AutoSpaceShapes`, `Pages`, `Save` | Set horizontaldistance to zero in autospaceoptions to align shapes vertically without horizontal gaps |
| [set-page-orientation-to-landscape-for-page-four-using-the-page-orientation-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-page-orientation-to-landscape-for-page-four-using-the-page-orientation-property.cs) | `Diagram`, `Pages`, `Save` | Set page orientation to landscape for page four using the page orientation property |
| [set-page-protection-to-read-only-mode-and-assign-a-password-for-editing-restrictions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-page-protection-to-read-only-mode-and-assign-a-password-for-editing-restrictions.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Set page protection to read only mode and assign a password for editing restrictions |
| [set-the-connector-s-line-weight-to-0-5-points-and-change-its-line-color-to-dark-blue.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-the-connector-s-line-weight-to-0-5-points-and-change-its-line-color-to-dark-blue.cs) | `Diagram`, `Pages`, `Save` | Set the connector s line weight to 0 5 points and change its line color to dark blue |
| [set-the-orientation-of-a-page-to-landscape-and-adjust-its-width-and-height-accordingly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-the-orientation-of-a-page-to-landscape-and-adjust-its-width-and-height-accordingly.cs) | `Diagram`, `Pages`, `Save` | Set the orientation of a page to landscape and adjust its width and height accordingly |
| [set-the-orientation-of-a-selected-page-to-landscape-by-assigning-printpageorientation-landscape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-the-orientation-of-a-selected-page-to-landscape-by-assigning-printpageorientation-landscape.cs) | `Diagram`, `Pages`, `Save` | Set the orientation of a selected page to landscape by assigning printpageorientation landscape |
| [set-the-orientation-of-all-pages-to-landscape-then-generate-a-pdf-and-verify-page-dimensions-reflect-rotation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-the-orientation-of-all-pages-to-landscape-then-generate-a-pdf-and-verify-page-dimensions-reflect-rotation.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Set the orientation of all pages to landscape then generate a pdf and verify page dimensions reflect rotation |
| [set-the-page-background-color-to-white-for-all-pages-using-a-loop-and-a-single-stylesheet.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-the-page-background-color-to-white-for-all-pages-using-a-loop-and-a-single-stylesheet.cs) | `Diagram`, `Page`, `Pages` | Set the page background color to white for all pages using a loop and a single stylesheet |
| [set-verticaldistance-to-zero-in-autospaceoptions-to-align-shapes-horizontally-without-vertical-gaps.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/set-verticaldistance-to-zero-in-autospaceoptions-to-align-shapes-horizontally-without-vertical-gaps.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Set verticaldistance to zero in autospaceoptions to align shapes horizontally without vertical gaps |
| [translate-page-metadata-into-a-localized-language-using-a-resource-dictionary-before-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/translate-page-metadata-into-a-localized-language-using-a-resource-dictionary-before-saving.cs) | `Diagram`, `Pages`, `Save` | Translate page metadata into a localized language using a resource dictionary before saving |
| [update-the-page-title-property-based-on-external-metadata-retrieved-from-a-json-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/update-the-page-title-property-based-on-external-metadata-retrieved-from-a-json-file.cs) | `Diagram`, `Pages`, `Save` | Update the page title property based on external metadata retrieved from a json file |
| [use-a-cancellation-token-to-abort-the-auto-spacing-operation-if-the-user-requests-cancellation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-a-cancellation-token-to-abort-the-auto-spacing-operation-if-the-user-requests-cancellation.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Use a cancellation token to abort the auto spacing operation if the user requests cancellation |
| [use-a-lambda-expression-to-set-page-width-based-on-page-index-parity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-a-lambda-expression-to-set-page-width-based-on-page-index-parity.cs) | `Diagram`, `Pages`, `Save` | Use a lambda expression to set page width based on page index parity |
| [use-a-try-catch-block-around-the-auto-spacing-call-to-gracefully-handle-runtime-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-a-try-catch-block-around-the-auto-spacing-call-to-gracefully-handle-runtime-errors.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Use a try catch block around the auto spacing call to gracefully handle runtime errors |
| [use-a-try-finally-block-to-ensure-the-diagram-object-is-disposed-even-if-processing-fails.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-a-try-finally-block-to-ensure-the-diagram-object-is-disposed-even-if-processing-fails.cs) | `Diagram`, `Save`, `diagram` | Use a try finally block to ensure the diagram object is disposed even if processing fails |
| [use-asynchronous-loading-of-diagrams-while-preparing-page-size-adjustments-in-parallel-threads.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-asynchronous-loading-of-diagrams-while-preparing-page-size-adjustments-in-parallel-threads.cs) | `Diagram`, `Pages`, `Save` | Use asynchronous loading of diagrams while preparing page size adjustments in parallel threads |
| [use-negative-spacing-values-in-autospaceoptions-to-intentionally-overlap-shapes-for-compact-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-negative-spacing-values-in-autospaceoptions-to-intentionally-overlap-shapes-for-compact-layout.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Use negative spacing values in autospaceoptions to intentionally overlap shapes for compact layout |
| [use-pageprops-to-copy-size-from-a-template-page-to-multiple-target-pages-within-a-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/use-pageprops-to-copy-size-from-a-template-page-to-multiple-target-pages-within-a-diagram.cs) | `Diagram`, `Pages`, `Save` | Use pageprops to copy size from a template page to multiple target pages within a diagram |
| [validate-page-dimensions-against-a-predefined-template-and-report-any-mismatches-in-a-log.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-page-dimensions-against-a-predefined-template-and-report-any-mismatches-in-a-log.cs) | `Diagram`, `Pages`, `diagram` | Validate page dimensions against a predefined template and report any mismatches in a log |
| [validate-shape-positions-after-auto-spacing-by-comparing-their-x-and-y-coordinates-to-expected-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-shape-positions-after-auto-spacing-by-comparing-their-x-and-y-coordinates-to-expected-values.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Validate shape positions after auto spacing by comparing their x and y coordinates to expected values |
| [validate-that-each-page-contains-at-least-one-shape-after-batch-adding-shapes-to-all-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-that-each-page-contains-at-least-one-shape-after-batch-adding-shapes-to-all-pages.cs) | `AddShape`, `Diagram`, `Page` | Validate that each page contains at least one shape after batch adding shapes to all pages |
| [validate-that-every-page-in-the-loaded-diagram-has-a-unique-name-raising-an-exception-on-duplicates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-that-every-page-in-the-loaded-diagram-has-a-unique-name-raising-an-exception-on-duplicates.cs) | `Diagram`, `Pages`, `diagram` | Validate that every page in the loaded diagram has a unique name raising an exception on duplicates |
| [validate-that-page-names-are-unique-across-the-diagram-after-renaming-pages-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-that-page-names-are-unique-across-the-diagram-after-renaming-pages-programmatically.cs) | `Diagram`, `Pages`, `Save` | Validate that page names are unique across the diagram after renaming pages programmatically |
| [validate-that-page-size-changes-persist-after-closing-and-reopening-the-visio-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-that-page-size-changes-persist-after-closing-and-reopening-the-visio-document.cs) | `Diagram`, `Page`, `Pages` | Validate that page size changes persist after closing and reopening the visio document |
| [validate-that-saved-diagram-retains-custom-page-size-when-opened-in-microsoft-visio.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/validate-that-saved-diagram-retains-custom-page-size-when-opened-in-microsoft-visio.cs) | `Diagram`, `Pages`, `Save` | Validate that saved diagram retains custom page size when opened in microsoft visio |
| [verify-that-all-connectors-stay-attached-to-their-source-and-target-shapes-after-spacing-operation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/verify-that-all-connectors-stay-attached-to-their-source-and-target-shapes-after-spacing-operation.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Verify that all connectors stay attached to their source and target shapes after spacing operation |
| [write-a-csv-file-summarizing-shape-ids-and-their-new-coordinates-following-auto-spacing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/write-a-csv-file-summarizing-shape-ids-and-their-new-coordinates-following-auto-spacing.cs) | `AutoSpaceOptions`, `AutoSpaceShapes`, `Diagram` | Write a csv file summarizing shape ids and their new coordinates following auto spacing |
| [write-unit-tests-asserting-that-shape-positions-match-expected-coordinates-after-applying-auto-spacing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages/write-unit-tests-asserting-that-shape-positions-match-expected-coordinates-after-applying-auto-spacing.cs) | `AddShape`, `AutoSpaceOptions`, `Diagram` | Write unit tests asserting that shape positions match expected coordinates after applying auto spacing |

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
- `AutoSpaceOptions`
- `AutoSpaceShapes`
- `ConnectShapesViaConnector`
- `Diagram`
- `HTMLSaveOptions`
- `ImageSaveOptions`
- `Layout`
- `LayoutOptions`
- `Masters`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `SVGSaveOptions`
- `Save`
- `Shapes`
- `StyleSheet`
- `StyleSheets`
- `XPSSaveOptions`
- `diagram`
- `page`
- `stylesheet`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with pages capabilities are applied in production applications:

- Managing multi-page Visio documents in enterprise content management systems
- Reordering pages in automated report generation workflows
- Adding background pages for watermarks and corporate branding
- Copying pages between diagrams in document merge operations

## Developer Q&A

Frequently asked questions about **Working With Pages** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Pages in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.5.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

**Q: How do I access a page by name?**

A: Use `diagram.Pages.GetPage("PageName")`. To access by index use `diagram.Pages[0]`. Never use `diagram.ActivePage` — it does not exist.

**Q: How do I add a new blank page to a diagram?**

A: Create `Page newPage = new Page(); newPage.Name = "NewPage"; newPage.ID = maxId + 1; diagram.Pages.Add(newPage);` where `maxId` is the maximum existing page ID.

## Related Categories

- [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) — loading, saving, and basic diagram operations
- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Page Setup Features](https://github.com/aspose-diagram/agentic-net-examples/tree/main/page-setup-features) — page size, margins, and orientation
- [Working With Layers](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-layers) — layer management and visibility

## Category Statistics

- Total examples: 168
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-06-23 | Examples: 168 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
