---
category: drawing
display_name: Drawing
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 125
pass_rate: 100.0
generated: 2026-08-03
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Drawing

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Drawing** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 125 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Drawing** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for drawing operations.
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
| `System` | 125 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 122 | Core diagram API |
| `System.IO` | 90 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 69 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `Aspose.Diagram.Manipulation` | 4 | Supporting utilities |
| `System.Text` | 2 | StringBuilder |
| `System.Linq` | 1 | LINQ queries on collections |
| `System.Drawing` | 1 | Supporting utilities |

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
| [add-a-centered-text-label-inside-a-diamond-shape-using-a-custom-font.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-centered-text-label-inside-a-diamond-shape-using-a-custom-font.cs) | `Diagram`, `Pages`, `Save` | Add a centered text label inside a diamond shape using a custom font |
| [add-a-centered-text-label-inside-the-pentagon-for-annotation-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-centered-text-label-inside-the-pentagon-for-annotation-purposes.cs) | `Diagram`, `Pages`, `Save` | Add a centered text label inside the pentagon for annotation purposes |
| [add-a-connector-line-between-two-rectangles-using-an-elbow-routing-style.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-connector-line-between-two-rectangles-using-an-elbow-routing-style.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Add a connector line between two rectangles using an elbow routing style |
| [add-a-diamond-shape-to-the-diagram-and-set-its-size-to-2-inches-by-2-inches.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-diamond-shape-to-the-diagram-and-set-its-size-to-2-inches-by-2-inches.cs) | `AddShape`, `Diagram`, `Pages` | Add a diamond shape to the diagram and set its size to 2 inches by 2 inches |
| [add-a-drop-shadow-effect-to-the-rectangle-with-five-pixel-offset-and-thirty-percent-opacity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-drop-shadow-effect-to-the-rectangle-with-five-pixel-offset-and-thirty-percent-opacity.cs) | `Diagram`, `Pages`, `Save` | Add a drop shadow effect to the rectangle with five pixel offset and thirty percent opacity |
| [add-a-page-header-containing-the-document-title-using-a-twelve-point-arial-font.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-page-header-containing-the-document-title-using-a-twelve-point-arial-font.cs) | `Diagram`, `Save`, `diagram` | Add a page header containing the document title using a twelve point arial font |
| [add-a-shadow-effect-to-the-triangle-with-default-parameters.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-shadow-effect-to-the-triangle-with-default-parameters.cs) | `Diagram`, `Pages`, `Save` | Add a shadow effect to the triangle with default parameters |
| [add-a-tiled-background-image-to-the-page-covering-the-full-page-dimensions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/add-a-tiled-background-image-to-the-page-covering-the-full-page-dimensions.cs) | `AddShape`, `Diagram`, `Pages` | Add a tiled background image to the page covering the full page dimensions |
| [adjust-line-thickness-of-a-circle-based-on-a-user-provided-numeric-input.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/adjust-line-thickness-of-a-circle-based-on-a-user-provided-numeric-input.cs) | `Diagram`, `Pages`, `Save` | Adjust line thickness of a circle based on a user provided numeric input |
| [adjust-the-triangle-s-opacity-to-75-percent-for-semi-transparent-effect.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/adjust-the-triangle-s-opacity-to-75-percent-for-semi-transparent-effect.cs) | `Diagram`, `Pages`, `Save` | Adjust the triangle s opacity to 75 percent for semi transparent effect |
| [adjust-z-order-to-bring-the-circle-shape-to-the-front-of-overlapping-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/adjust-z-order-to-bring-the-circle-shape-to-the-front-of-overlapping-shapes.cs) | `Diagram`, `Pages`, `Save` | Adjust z order to bring the circle shape to the front of overlapping shapes |
| [align-the-pentagon-shape-to-the-exact-center-of-the-diagram-page-for-symmetry.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/align-the-pentagon-shape-to-the-exact-center-of-the-diagram-page-for-symmetry.cs) | `Diagram`, `Pages`, `Save` | Align the pentagon shape to the exact center of the diagram page for symmetry |
| [align-the-rectangle-to-the-top-left-corner-of-the-page-with-a-ten-pixel-margin.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/align-the-rectangle-to-the-top-left-corner-of-the-page-with-a-ten-pixel-margin.cs) | `Diagram`, `Pages`, `Save` | Align the rectangle to the top left corner of the page with a ten pixel margin |
| [apply-a-45-degree-rotation-transform-to-the-entire-diagram-page-for-artistic-effect.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-45-degree-rotation-transform-to-the-entire-diagram-page-for-artistic-effect.cs) | `Diagram`, `Save`, `Shapes` | Apply a 45 degree rotation transform to the entire diagram page for artistic effect |
| [apply-a-curved-connector-between-two-shapes-with-a-smooth-spline-curve.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-curved-connector-between-two-shapes-with-a-smooth-spline-curve.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Apply a curved connector between two shapes with a smooth spline curve |
| [apply-a-linear-gradient-fill-from-red-to-yellow-on-the-triangle.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-linear-gradient-fill-from-red-to-yellow-on-the-triangle.cs) | `Diagram`, `Pages`, `Save` | Apply a linear gradient fill from red to yellow on the triangle |
| [apply-a-linear-gradient-fill-to-the-rectangle-transitioning-from-blue-to-green-colors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-linear-gradient-fill-to-the-rectangle-transitioning-from-blue-to-green-colors.cs) | `Diagram`, `Pages`, `Save` | Apply a linear gradient fill to the rectangle transitioning from blue to green colors |
| [apply-a-rotation-of-30-degrees-to-the-triangle-before-exporting-to-png.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-rotation-of-30-degrees-to-the-triangle-before-exporting-to-png.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Apply a rotation of 30 degrees to the triangle before exporting to png |
| [apply-a-solid-red-fill-color-to-the-triangle-shape-for-visual-emphasis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-solid-red-fill-color-to-the-triangle-shape-for-visual-emphasis.cs) | `Diagram`, `Pages`, `Save` | Apply a solid red fill color to the triangle shape for visual emphasis |
| [apply-a-text-outline-effect-with-a-one-pixel-black-border-around-white-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/apply-a-text-outline-effect-with-a-one-pixel-black-border-around-white-text.cs) | `AddShape`, `Diagram`, `Pages` | Apply a text outline effect with a one pixel black border around white text |
| [arrange-shapes-in-a-grid-layout-and-export-the-diagram-as-pdf.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/arrange-shapes-in-a-grid-layout-and-export-the-diagram-as-pdf.cs) | `AddShape`, `Diagram`, `PdfSaveOptions` | Arrange shapes in a grid layout and export the diagram as pdf |
| [batch-process-a-folder-of-diagrams-adding-a-pentagon-to-each-file-automatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/batch-process-a-folder-of-diagrams-adding-a-pentagon-to-each-file-automatically.cs) | `AddShape`, `Diagram`, `Pages` | Batch process a folder of diagrams adding a pentagon to each file automatically |
| [calculate-the-bounding-box-dimensions-of-the-pentagon-using-the-api-geometry-methods.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/calculate-the-bounding-box-dimensions-of-the-pentagon-using-the-api-geometry-methods.cs) | `Diagram`, `Pages`, `Save` | Calculate the bounding box dimensions of the pentagon using the api geometry methods |
| [center-the-triangle-on-the-page-using-page-dimensions-for-alignment.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/center-the-triangle-on-the-page-using-page-dimensions-for-alignment.cs) | `Diagram`, `Pages`, `Save` | Center the triangle on the page using page dimensions for alignment |
| [change-the-connector-s-arrowhead-style-to-a-filled-triangle-at-the-target-end.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/change-the-connector-s-arrowhead-style-to-a-filled-triangle-at-the-target-end.cs) | `Diagram`, `Pages`, `Save` | Change the connector s arrowhead style to a filled triangle at the target end |
| [change-the-triangle-s-fill-color-to-blue-for-visual-distinction.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/change-the-triangle-s-fill-color-to-blue-for-visual-distinction.cs) | `Diagram`, `Pages`, `Save` | Change the triangle s fill color to blue for visual distinction |
| [clone-the-triangle-shape-and-place-the-copy-on-the-second-page-at-offset-coordinates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/clone-the-triangle-shape-and-place-the-copy-on-the-second-page-at-offset-coordinates.cs) | `Diagram`, `Page`, `Pages` | Clone the triangle shape and place the copy on the second page at offset coordinates |
| [connect-the-pentagon-and-square-using-a-dynamic-connector-line-with-arrowheads.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/connect-the-pentagon-and-square-using-a-dynamic-connector-line-with-arrowheads.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Connect the pentagon and square using a dynamic connector line with arrowheads |
| [convert-the-diagram-to-an-svg-file-while-maintaining-shape-hierarchy-and-styles.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/convert-the-diagram-to-an-svg-file-while-maintaining-shape-hierarchy-and-styles.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Convert the diagram to an svg file while maintaining shape hierarchy and styles |
| [create-a-batch-script-that-draws-diamonds-on-ten-pages-and-saves-each-as-png.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/create-a-batch-script-that-draws-diamonds-on-ten-pages-and-saves-each-as-png.cs) | `Diagram`, `ImageSaveOptions`, `Page` | Create a batch script that draws diamonds on ten pages and saves each as png |
| [create-a-diagram-with-two-pages-each-containing-a-triangle-of-different-colors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/create-a-diagram-with-two-pages-each-containing-a-triangle-of-different-colors.cs) | `Diagram`, `Page`, `Pages` | Create a diagram with two pages each containing a triangle of different colors |
| [create-a-new-diagram-object-and-add-a-triangle-shape-to-the-first-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/create-a-new-diagram-object-and-add-a-triangle-shape-to-the-first-page.cs) | `AddShape`, `Diagram`, `diagram` | Create a new diagram object and add a triangle shape to the first page |
| [create-a-new-visio-diagram-add-a-page-and-draw-a-centered-circle-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/create-a-new-visio-diagram-add-a-page-and-draw-a-centered-circle-shape.cs) | `Diagram`, `Save`, `diagram` | Create a new visio diagram add a page and draw a centered circle shape |
| [create-a-thumbnail-png-of-the-diagram-page-sized-to-one-hundred-pixels-wide.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/create-a-thumbnail-png-of-the-diagram-page-sized-to-one-hundred-pixels-wide.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Create a thumbnail png of the diagram page sized to one hundred pixels wide |
| [create-guide-lines-on-the-page-and-align-shapes-precisely-to-those-guides.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/create-guide-lines-on-the-page-and-align-shapes-precisely-to-those-guides.cs) | `AddShape`, `Diagram`, `Page` | Create guide lines on the page and align shapes precisely to those guides |
| [define-the-triangle-s-three-geometry-points-using-absolute-coordinates-on-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/define-the-triangle-s-three-geometry-points-using-absolute-coordinates-on-the-page.cs) | `Diagram`, `Pages`, `Save` | Define the triangle s three geometry points using absolute coordinates on the page |
| [distribute-three-identical-rectangles-evenly-across-the-page-horizontally-with-equal-spacing-between-each-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/distribute-three-identical-rectangles-evenly-across-the-page-horizontally-with-equal-spacing-between-each-shape.cs) | `AddShape`, `Diagram`, `Pages` | Distribute three identical rectangles evenly across the page horizontally with equal spacing between each shape |
| [draw-a-diamond-shape-with-a-thick-border-and-export-it-as-svg.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/draw-a-diamond-shape-with-a-thick-border-and-export-it-as-svg.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Draw a diamond shape with a thick border and export it as svg |
| [draw-a-pentagon-shape-on-the-page-with-custom-size-and-position.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/draw-a-pentagon-shape-on-the-page-with-custom-size-and-position.cs) | `Diagram`, `Pages`, `Save` | Draw a pentagon shape on the page with custom size and position |
| [draw-an-oval-with-a-transparent-fill-and-export-the-image-as-png.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/draw-an-oval-with-a-transparent-fill-and-export-the-image-as-png.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Draw an oval with a transparent fill and export the image as png |
| [draw-multiple-circles-with-varying-colors-based-on-their-index-position-and-export-as-png.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/draw-multiple-circles-with-varying-colors-based-on-their-index-position-and-export-as-png.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Draw multiple circles with varying colors based on their index position and export as png |
| [duplicate-a-circle-shape-across-five-pages-each-with-unique-position-offsets.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/duplicate-a-circle-shape-across-five-pages-each-with-unique-position-offsets.cs) | `Diagram`, `Page`, `Pages` | Duplicate a circle shape across five pages each with unique position offsets |
| [duplicate-a-diamond-shape-and-offset-its-position-by-0-5-inches-to-the-right.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/duplicate-a-diamond-shape-and-offset-its-position-by-0-5-inches-to-the-right.cs) | `AddShape`, `Diagram`, `Pages` | Duplicate a diamond shape and offset its position by 0 5 inches to the right |
| [duplicate-the-pentagon-multiple-times-in-a-grid-layout-across-the-diagram-canvas.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/duplicate-the-pentagon-multiple-times-in-a-grid-layout-across-the-diagram-canvas.cs) | `Diagram`, `Pages`, `Save` | Duplicate the pentagon multiple times in a grid layout across the diagram canvas |
| [duplicate-the-triangle-shape-three-times-and-arrange-them-vertically-on-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/duplicate-the-triangle-shape-three-times-and-arrange-them-vertically-on-the-page.cs) | `Diagram`, `Pages`, `Save` | Duplicate the triangle shape three times and arrange them vertically on the page |
| [embed-a-hyperlink-in-the-rectangle-s-text-that-opens-a-specified-website-url.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/embed-a-hyperlink-in-the-rectangle-s-text-that-opens-a-specified-website-url.cs) | `AddShape`, `Diagram`, `Pages` | Embed a hyperlink in the rectangle s text that opens a specified website url |
| [enable-text-wrapping-inside-the-rectangle-to-automatically-break-long-sentences-for-readability.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/enable-text-wrapping-inside-the-rectangle-to-automatically-break-long-sentences-for-readability.cs) | `AddShape`, `Diagram`, `Page` | Enable text wrapping inside the rectangle to automatically break long sentences for readability |
| [export-each-page-of-the-diagram-as-separate-png-files-with-transparent-background.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-each-page-of-the-diagram-as-separate-png-files-with-transparent-background.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Export each page of the diagram as separate png files with transparent background |
| [export-only-the-second-page-of-a-multi-page-diagram-to-a-pdf-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-only-the-second-page-of-a-multi-page-diagram-to-a-pdf-document.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export only the second page of a multi page diagram to a pdf document |
| [export-only-the-triangle-shape-as-a-standalone-svg-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-only-the-triangle-shape-as-a-standalone-svg-file.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Export only the triangle shape as a standalone svg file |
| [export-the-diagram-containing-the-triangle-to-an-svg-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-containing-the-triangle-to-an-svg-file.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Export the diagram containing the triangle to an svg file |
| [export-the-diagram-to-html-format-including-interactive-zoom-controls.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-html-format-including-interactive-zoom-controls.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Export the diagram to html format including interactive zoom controls |
| [export-the-diagram-to-html-with-embedded-svg-resources-for-all-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-html-with-embedded-svg-resources-for-all-shapes.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Export the diagram to html with embedded svg resources for all shapes |
| [export-the-diagram-to-html-with-inline-svg-markup-for-all-drawn-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-html-with-inline-svg-markup-for-all-drawn-shapes.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Export the diagram to html with inline svg markup for all drawn shapes |
| [export-the-diagram-to-pdf-and-include-the-triangle-on-a-separate-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-pdf-and-include-the-triangle-on-a-separate-page.cs) | `Diagram`, `Page`, `Pages` | Export the diagram to pdf and include the triangle on a separate page |
| [export-the-diagram-to-pdf-format-using-high-resolution-settings-for-print-quality.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-pdf-format-using-high-resolution-settings-for-print-quality.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export the diagram to pdf format using high resolution settings for print quality |
| [export-the-diagram-to-pdf-with-high-resolution-suitable-for-print-quality.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-pdf-with-high-resolution-suitable-for-print-quality.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export the diagram to pdf with high resolution suitable for print quality |
| [export-the-diagram-to-png-format-with-a-transparent-background-layer.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-png-format-with-a-transparent-background-layer.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Export the diagram to png format with a transparent background layer |
| [export-the-diagram-to-png-with-a-specified-solid-background-color.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-png-with-a-specified-solid-background-color.cs) | `Diagram`, `ImageSaveOptions`, `Page` | Export the diagram to png with a specified solid background color |
| [export-the-diagram-to-svg-format-embedding-required-font-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-svg-format-embedding-required-font-resources.cs) | `Diagram`, `SVGSaveOptions`, `Save` | Export the diagram to svg format embedding required font resources |
| [export-the-diagram-to-svg-while-assigning-css-classes-to-each-shape-for-styling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-diagram-to-svg-while-assigning-css-classes-to-each-shape-for-styling.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Export the diagram to svg while assigning css classes to each shape for styling |
| [export-the-modified-diagram-to-a-high-resolution-png-image-with-three-hundred-dpi.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-modified-diagram-to-a-high-resolution-png-image-with-three-hundred-dpi.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Export the modified diagram to a high resolution png image with three hundred dpi |
| [export-the-pdf-using-compression-settings-to-reduce-file-size-while-maintaining-quality.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-pdf-using-compression-settings-to-reduce-file-size-while-maintaining-quality.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Export the pdf using compression settings to reduce file size while maintaining quality |
| [export-the-png-and-override-the-default-background-color-with-white.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-png-and-override-the-default-background-color-with-white.cs) | `Diagram`, `ImageSaveOptions`, `Page` | Export the png and override the default background color with white |
| [export-the-triangle-diagram-to-a-png-image-with-default-resolution.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/export-the-triangle-diagram-to-a-png-image-with-default-resolution.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Export the triangle diagram to a png image with default resolution |
| [generate-a-diagram-containing-fifty-circles-with-incremental-radii-and-export-as-pdf.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/generate-a-diagram-containing-fifty-circles-with-incremental-radii-and-export-as-pdf.cs) | `Diagram`, `Save`, `diagram` | Generate a diagram containing fifty circles with incremental radii and export as pdf |
| [generate-an-html-file-that-embeds-javascript-event-handlers-for-shape-interaction.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/generate-an-html-file-that-embeds-javascript-event-handlers-for-shape-interaction.cs) | `AddShape`, `Diagram`, `HTMLSaveOptions` | Generate an html file that embeds javascript event handlers for shape interaction |
| [generate-an-html-page-that-displays-the-triangle-diagram-using-inline-svg-markup.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/generate-an-html-page-that-displays-the-triangle-diagram-using-inline-svg-markup.cs) |  | Generate an html page that displays the triangle diagram using inline svg markup |
| [generate-ten-identical-triangles-arranged-in-a-grid-pattern-across-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/generate-ten-identical-triangles-arranged-in-a-grid-pattern-across-the-page.cs) | `Pages`, `Save`, `diagram` | Generate ten identical triangles arranged in a grid pattern across the page |
| [group-a-circle-and-an-oval-together-and-export-the-group-as-a-single-svg-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/group-a-circle-and-an-oval-together-and-export-the-group-as-a-single-svg-file.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Group a circle and an oval together and export the group as a single svg file |
| [group-multiple-diamonds-and-export-the-group-as-a-single-svg-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/group-multiple-diamonds-and-export-the-group-as-a-single-svg-document.cs) | `Diagram`, `SVGSaveOptions`, `Shape` | Group multiple diamonds and export the group as a single svg document |
| [group-the-pentagon-and-square-together-to-form-a-composite-shape-collection.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/group-the-pentagon-and-square-together-to-form-a-composite-shape-collection.cs) | `Diagram`, `Pages`, `Save` | Group the pentagon and square together to form a composite shape collection |
| [group-the-rectangle-with-a-newly-drawn-ellipse-and-assign-a-common-group-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/group-the-rectangle-with-a-newly-drawn-ellipse-and-assign-a-common-group-name.cs) | `Pages`, `Shape`, `Shapes` | Group the rectangle with a newly drawn ellipse and assign a common group name |
| [group-the-triangle-with-the-inserted-image-to-move-them-together.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/group-the-triangle-with-the-inserted-image-to-move-them-together.cs) | `AddShape`, `Diagram`, `Pages` | Group the triangle with the inserted image to move them together |
| [include-clickable-hyperlinks-in-the-pdf-export-that-navigate-to-external-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/include-clickable-hyperlinks-in-the-pdf-export-that-navigate-to-external-resources.cs) | `AddShape`, `Diagram`, `Pages` | Include clickable hyperlinks in the pdf export that navigate to external resources |
| [insert-a-page-footer-with-automatic-page-numbering-aligned-to-the-right-margin.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-a-page-footer-with-automatic-page-numbering-aligned-to-the-right-margin.cs) | `Diagram`, `Save`, `diagram` | Insert a page footer with automatic page numbering aligned to the right margin |
| [insert-a-square-shape-on-the-same-page-and-apply-a-solid-fill-color.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-a-square-shape-on-the-same-page-and-apply-a-solid-fill-color.cs) | `Diagram`, `Pages`, `Save` | Insert a square shape on the same page and apply a solid fill color |
| [insert-a-text-box-above-the-rectangle-with-centered-alignment-and-bold-formatting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-a-text-box-above-the-rectangle-with-centered-alignment-and-bold-formatting.cs) | `Diagram`, `Pages`, `Save` | Insert a text box above the rectangle with centered alignment and bold formatting |
| [insert-an-external-image-behind-the-triangle-on-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-an-external-image-behind-the-triangle-on-the-page.cs) | `AddShape`, `Diagram`, `Pages` | Insert an external image behind the triangle on the page |
| [insert-an-image-onto-the-page-and-position-the-triangle-above-it.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-an-image-onto-the-page-and-position-the-triangle-above-it.cs) | `AddShape`, `Diagram`, `Pages` | Insert an image onto the page and position the triangle above it |
| [insert-an-oval-shape-onto-a-newly-added-page-and-position-it-at-specific-coordinates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-an-oval-shape-onto-a-newly-added-page-and-position-it-at-specific-coordinates.cs) | `Diagram`, `Page`, `Pages` | Insert an oval shape onto a newly added page and position it at specific coordinates |
| [insert-multiline-text-inside-the-rectangle-using-a-custom-truetype-font-at-fourteen-point-size.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/insert-multiline-text-inside-the-rectangle-using-a-custom-truetype-font-at-fourteen-point-size.cs) | `Diagram`, `Pages`, `Save` | Insert multiline text inside the rectangle using a custom truetype font at fourteen point size |
| [instantiate-a-diagram-object-and-add-a-new-page-to-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/instantiate-a-diagram-object-and-add-a-new-page-to-the-diagram.cs) | `Diagram`, `Page`, `Pages` | Instantiate a diagram object and add a new page to the diagram |
| [load-a-vsdx-diagram-file-and-add-a-rectangle-with-a-solid-red-border.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/load-a-vsdx-diagram-file-and-add-a-rectangle-with-a-solid-red-border.cs) | `Diagram`, `Pages`, `Save` | Load a vsdx diagram file and add a rectangle with a solid red border |
| [load-an-existing-visio-file-insert-a-pentagon-shape-and-re-save-the-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/load-an-existing-visio-file-insert-a-pentagon-shape-and-re-save-the-document.cs) | `Diagram`, `Save`, `diagram` | Load an existing visio file insert a pentagon shape and re save the document |
| [lock-aspect-ratio-of-a-diamond-shape-while-resizing-to-preserve-proportions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/lock-aspect-ratio-of-a-diamond-shape-while-resizing-to-preserve-proportions.cs) | `Diagram`, `Pages`, `Save` | Lock aspect ratio of a diamond shape while resizing to preserve proportions |
| [lock-the-position-of-an-oval-shape-after-drawing-to-prevent-further-movement.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/lock-the-position-of-an-oval-shape-after-drawing-to-prevent-further-movement.cs) | `Diagram`, `Pages`, `Save` | Lock the position of an oval shape after drawing to prevent further movement |
| [lock-the-rectangle-to-prevent-modifications-during-batch-processing-until-all-drawing-steps-are-completed.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/lock-the-rectangle-to-prevent-modifications-during-batch-processing-until-all-drawing-steps-are-completed.cs) | `Diagram`, `Save`, `diagram` | Lock the rectangle to prevent modifications during batch processing until all drawing steps are completed |
| [mirror-the-rectangle-horizontally-keeping-its-original-fill-and-border-styles-unchanged.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/mirror-the-rectangle-horizontally-keeping-its-original-fill-and-border-styles-unchanged.cs) | `Diagram`, `Pages`, `Save` | Mirror the rectangle horizontally keeping its original fill and border styles unchanged |
| [move-the-triangle-to-coordinates-200-150-on-the-page-for-precise-placement.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/move-the-triangle-to-coordinates-200-150-on-the-page-for-precise-placement.cs) | `Pages`, `Save`, `Shapes` | Move the triangle to coordinates 200 150 on the page for precise placement |
| [position-a-circle-shape-at-coordinates-measured-in-centimeters-on-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/position-a-circle-shape-at-coordinates-measured-in-centimeters-on-the-page.cs) | `Diagram`, `Pages`, `Save` | Position a circle shape at coordinates measured in centimeters on the page |
| [rotate-the-pentagon-thirty-degrees-around-its-geometric-center-on-the-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/rotate-the-pentagon-thirty-degrees-around-its-geometric-center-on-the-page.cs) | `Diagram`, `Pages`, `Save` | Rotate the pentagon thirty degrees around its geometric center on the page |
| [rotate-the-rectangle-forty-five-degrees-around-its-center-point-while-preserving-its-position.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/rotate-the-rectangle-forty-five-degrees-around-its-center-point-while-preserving-its-position.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Rotate the rectangle forty five degrees around its center point while preserving its position |
| [rotate-the-triangle-shape-45-degrees-clockwise-around-its-center-point-for-alignment.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/rotate-the-triangle-shape-45-degrees-clockwise-around-its-center-point-for-alignment.cs) | `Diagram`, `Pages`, `Save` | Rotate the triangle shape 45 degrees clockwise around its center point for alignment |
| [save-only-the-first-page-containing-the-triangle-as-a-separate-svg-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/save-only-the-first-page-containing-the-triangle-as-a-separate-svg-file.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Save only the first page containing the triangle as a separate svg file |
| [save-the-complete-diagram-as-a-visio-vsdx-file-for-later-editing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/save-the-complete-diagram-as-a-visio-vsdx-file-for-later-editing.cs) | `Diagram`, `Save`, `diagram` | Save the complete diagram as a visio vsdx file for later editing |
| [save-the-diagram-as-a-pdf-document-using-default-export-settings.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/save-the-diagram-as-a-pdf-document-using-default-export-settings.cs) | `Diagram`, `Save`, `diagram` | Save the diagram as a pdf document using default export settings |
| [save-the-diagram-as-a-pdf-file-preserving-vector-shapes-and-text-quality.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/save-the-diagram-as-a-pdf-file-preserving-vector-shapes-and-text-quality.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Save the diagram as a pdf file preserving vector shapes and text quality |
| [save-the-diagram-as-an-html-file-embedding-the-svg-inline.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/save-the-diagram-as-an-html-file-embedding-the-svg-inline.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Save the diagram as an html file embedding the svg inline |
| [scale-the-pentagon-uniformly-to-double-its-original-dimensions-while-preserving-proportions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/scale-the-pentagon-uniformly-to-double-its-original-dimensions-while-preserving-proportions.cs) | `AddShape`, `Diagram`, `Pages` | Scale the pentagon uniformly to double its original dimensions while preserving proportions |
| [scale-the-rectangle-proportionally-to-double-its-original-width-and-height-maintaining-aspect-ratio.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/scale-the-rectangle-proportionally-to-double-its-original-width-and-height-maintaining-aspect-ratio.cs) | `Diagram`, `Pages`, `Save` | Scale the rectangle proportionally to double its original width and height maintaining aspect ratio |
| [scale-the-triangle-by-0-5-factor-to-create-a-smaller-version-on-the-same-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/scale-the-triangle-by-0-5-factor-to-create-a-smaller-version-on-the-same-page.cs) | `Pages`, `Save`, `Shapes` | Scale the triangle by 0 5 factor to create a smaller version on the same page |
| [scale-the-triangle-uniformly-by-a-factor-of-1-5-to-increase-its-size.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/scale-the-triangle-uniformly-by-a-factor-of-1-5-to-increase-its-size.cs) | `Diagram`, `Pages`, `Save` | Scale the triangle uniformly by a factor of 1 5 to increase its size |
| [set-custom-page-margins-of-twenty-points-on-all-sides-before-drawing-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-custom-page-margins-of-twenty-points-on-all-sides-before-drawing-shapes.cs) | `Diagram`, `Pages`, `Save` | Set custom page margins of twenty points on all sides before drawing shapes |
| [set-fill-opacity-of-an-oval-shape-to-70-percent-for-semi-transparent-appearance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-fill-opacity-of-an-oval-shape-to-70-percent-for-semi-transparent-appearance.cs) | `Diagram`, `Pages`, `Save` | Set fill opacity of an oval shape to 70 percent for semi transparent appearance |
| [set-line-cap-style-of-an-oval-shape-to-round-for-smoother-line-ends.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-line-cap-style-of-an-oval-shape-to-round-for-smoother-line-ends.cs) | `AddShape`, `Diagram`, `Pages` | Set line cap style of an oval shape to round for smoother line ends |
| [set-line-join-style-of-a-circle-shape-to-bevel-for-sharper-corners.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-line-join-style-of-a-circle-shape-to-bevel-for-sharper-corners.cs) | `Diagram`, `Pages`, `Save` | Set line join style of a circle shape to bevel for sharper corners |
| [set-rotation-pivot-point-of-a-circle-shape-to-its-geometric-center-before-rotating.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-rotation-pivot-point-of-a-circle-shape-to-its-geometric-center-before-rotating.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Set rotation pivot point of a circle shape to its geometric center before rotating |
| [set-text-alignment-inside-a-diamond-shape-to-centered-horizontally-and-vertically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-text-alignment-inside-a-diamond-shape-to-centered-horizontally-and-vertically.cs) | `AddShape`, `Diagram`, `Pages` | Set text alignment inside a diamond shape to centered horizontally and vertically |
| [set-the-circle-shape-s-fill-color-to-blue-and-its-line-weight-to-0-5-points.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-circle-shape-s-fill-color-to-blue-and-its-line-weight-to-0-5-points.cs) | `Diagram`, `Pages`, `Save` | Set the circle shape s fill color to blue and its line weight to 0 5 points |
| [set-the-connector-line-color-to-dark-gray-and-line-thickness-to-one-point-five.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-connector-line-color-to-dark-gray-and-line-thickness-to-one-point-five.cs) | `Diagram`, `Pages`, `Save` | Set the connector line color to dark gray and line thickness to one point five |
| [set-the-diagram-page-background-to-a-light-gray-color-before-adding-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-diagram-page-background-to-a-light-gray-color-before-adding-shapes.cs) | `Diagram`, `Page`, `Pages` | Set the diagram page background to a light gray color before adding shapes |
| [set-the-page-background-color-to-light-gray-and-draw-shapes-on-top.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-page-background-color-to-light-gray-and-draw-shapes-on-top.cs) | `Diagram`, `Page`, `Pages` | Set the page background color to light gray and draw shapes on top |
| [set-the-page-background-color-to-light-gray-for-the-entire-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-page-background-color-to-light-gray-for-the-entire-diagram.cs) | `Diagram`, `Pages`, `Save` | Set the page background color to light gray for the entire diagram |
| [set-the-pentagon-s-line-style-to-a-dashed-pattern-with-specific-thickness.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-pentagon-s-line-style-to-a-dashed-pattern-with-specific-thickness.cs) | `Diagram`, `Pages`, `Save` | Set the pentagon s line style to a dashed pattern with specific thickness |
| [set-the-rectangle-s-line-dash-pattern-to-dash-dot-and-increase-line-thickness-to-two-points.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-rectangle-s-line-dash-pattern-to-dash-dot-and-increase-line-thickness-to-two-points.cs) | `Diagram`, `Save`, `Shapes` | Set the rectangle s line dash pattern to dash dot and increase line thickness to two points |
| [set-the-rotation-angle-of-an-oval-shape-to-30-degrees-using-api-parameters.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-rotation-angle-of-an-oval-shape-to-30-degrees-using-api-parameters.cs) | `Diagram`, `Pages`, `Save` | Set the rotation angle of an oval shape to 30 degrees using api parameters |
| [set-the-text-direction-inside-the-rectangle-to-vertical-for-column-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-text-direction-inside-the-rectangle-to-vertical-for-column-layout.cs) | `AddShape`, `Diagram`, `Pages` | Set the text direction inside the rectangle to vertical for column layout |
| [set-the-triangle-s-line-dash-pattern-to-dashed-for-stylized-border.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-triangle-s-line-dash-pattern-to-dashed-for-stylized-border.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Set the triangle s line dash pattern to dashed for stylized border |
| [set-the-triangle-s-line-weight-to-2-points-and-change-line-color-to-navy.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/set-the-triangle-s-line-weight-to-2-points-and-change-line-color-to-navy.cs) | `Diagram`, `Pages`, `Save` | Set the triangle s line weight to 2 points and change line color to navy |
| [size-shapes-proportionally-to-page-dimensions-to-maintain-layout-across-page-sizes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/size-shapes-proportionally-to-page-dimensions-to-maintain-layout-across-page-sizes.cs) | `Diagram`, `Pages`, `Save` | Size shapes proportionally to page dimensions to maintain layout across page sizes |
| [snap-the-rectangle-to-the-nearest-grid-line-using-a-five-pixel-grid-spacing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/snap-the-rectangle-to-the-nearest-grid-line-using-a-five-pixel-grid-spacing.cs) | `Diagram`, `Page`, `Pages` | Snap the rectangle to the nearest grid line using a five pixel grid spacing |
| [unlock-the-rectangle-after-completing-all-drawing-operations-to-allow-further-editing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/unlock-the-rectangle-after-completing-all-drawing-operations-to-allow-further-editing.cs) | `Diagram`, `Pages`, `Save` | Unlock the rectangle after completing all drawing operations to allow further editing |
| [validate-that-all-pentagon-vertices-align-with-the-expected-coordinate-grid-positions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/validate-that-all-pentagon-vertices-align-with-the-expected-coordinate-grid-positions.cs) | `Diagram`, `Pages`, `Shapes` | Validate that all pentagon vertices align with the expected coordinate grid positions |
| [verify-that-the-exported-svg-contains-a-correct-ellipse-element-for-drawn-circles.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing/verify-that-the-exported-svg-contains-a-correct-ellipse-element-for-drawn-circles.cs) | `Diagram`, `Pages`, `SVGSaveOptions` | Verify that the exported svg contains a correct ellipse element for drawn circles |

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
- `ConnectShapesViaConnector`
- `Diagram`
- `HTMLSaveOptions`
- `ImageSaveOptions`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `SVGSaveOptions`
- `Save`
- `Shape`
- `Shapes`
- `diagram`
- `page`
- `shape`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** drawing capabilities are applied in production applications:

- Drawing custom shapes and annotations programmatically
- Generating technical drawings from coordinate data
- Creating geometric diagrams for educational or engineering use

## Developer Q&A

Frequently asked questions about **Drawing** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Drawing in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Working With Geometry Section](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-geometry-section) — custom shape geometry
- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure

## Category Statistics

- Total examples: 125
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-08-03 | Examples: 125 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
