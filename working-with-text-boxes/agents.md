---
category: working-with-text-boxes
display_name: Working With Text Boxes
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 35
pass_rate: 100.0
generated: 2026-06-23
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Text Boxes

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Text Boxes** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 35 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-23 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Text Boxes** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with text boxes operations.
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
| `Aspose.Diagram` | 35 | Core diagram API |
| `System` | 35 | Console, Math, DateTime, Exception |
| `System.IO` | 20 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 16 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Text.Json` | 1 | JSON serialization |

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

- TEXT BLOCK — Access via shape.TextBlock. Contains formatting for the entire text block: margins, direction, alignment, background color, transparency, tab stop.
- TEXT BLOCK MARGINS — Create a DoubleValue with units: DoubleValue margin = new DoubleValue(4, MeasureConst.PT); Then set: shape.TextBlock.LeftMargin = margin; shape.TextBlock.RightMargin = margin; shape.TextBlock.TopMargin = margin; shape.TextBlock.BottomMargin = margin;
- MeasureConst unit values: MeasureConst.PT (points), MeasureConst.IN (inches), MeasureConst.CM (centimeters), MeasureConst.MM (millimeters).
- TEXT DIRECTION — shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical; or TextDirectionValue.Horizontal;
- Valid TextDirectionValue members: Horizontal, Vertical.
- VERTICAL ALIGNMENT — shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle; Valid members: Top, Middle, Bottom.
- TEXT BACKGROUND COLOR — shape.TextBlock.TextBkgnd.Ufe.F = "RGB(95,108,53)"; — uses RGB() string format (this is one of the few places RGB() format IS correct, not hex).
- TEXT BACKGROUND TRANSPARENCY — shape.TextBlock.TextBkgndTrans.Value = 50; — value is a percentage (0 to 100). 0 = opaque, 100 = fully transparent.
- DEFAULT TAB STOP — shape.TextBlock.DefaultTabStop.Value = 2; — sets the distance between default tab stops in inches.
- TEXT POSITION — All text positioning uses shape.TextXForm properties: TxtPinX, TxtPinY (text block origin), TxtLocPinX, TxtLocPinY (text block local pin/pivot), TxtWidth, TxtHeight, TxtAngle.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [adjust-the-text-block-background-transparency-to-50-percent-for-a-semi-transparent-effect.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/adjust-the-text-block-background-transparency-to-50-percent-for-a-semi-transparent-effect.cs) | `Diagram`, `Pages`, `Save` | Adjust the text block background transparency to 50 percent for a semi transparent effect |
| [adjust-txtwidth-proportionally-to-the-shape-s-width-while-keeping-txtheight-constant-for-uniform-scaling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/adjust-txtwidth-proportionally-to-the-shape-s-width-while-keeping-txtheight-constant-for-uniform-scaling.cs) | `Diagram`, `Pages`, `Save` | Adjust txtwidth proportionally to the shape s width while keeping txtheight constant for uniform scaling |
| [align-shape-text-to-the-left-side-of-the-shape-by-setting-orientation-and-horizontal-pin-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/align-shape-text-to-the-left-side-of-the-shape-by-setting-orientation-and-horizontal-pin-values.cs) | `Diagram`, `Pages`, `Save` | Align shape text to the left side of the shape by setting orientation and horizontal pin values |
| [align-shape-text-to-the-right-side-of-the-shape-by-configuring-orientation-and-right-hand-pin.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/align-shape-text-to-the-right-side-of-the-shape-by-configuring-orientation-and-right-hand-pin.cs) | `Diagram`, `Pages`, `Save` | Align shape text to the right side of the shape by configuring orientation and right hand pin |
| [apply-a-90-degree-orientation-angle-to-shape-text-and-reposition-it-to-the-left-side-of-the-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/apply-a-90-degree-orientation-angle-to-shape-text-and-reposition-it-to-the-left-side-of-the-shape.cs) | `Diagram`, `Pages`, `Save` | Apply a 90 degree orientation angle to shape text and reposition it to the left side of the shape |
| [apply-a-different-default-tab-stop-for-shapes-that-contain-multiline-text-to-improve-readability.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/apply-a-different-default-tab-stop-for-shapes-that-contain-multiline-text-to-improve-readability.cs) | `Diagram`, `Pages`, `Save` | Apply a different default tab stop for shapes that contain multiline text to improve readability |
| [apply-a-solid-background-color-to-a-shape-s-text-block-using-the-textbackgroundcolor-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/apply-a-solid-background-color-to-a-shape-s-text-block-using-the-textbackgroundcolor-property.cs) | `Diagram`, `Pages`, `Save` | Apply a solid background color to a shape s text block using the textbackgroundcolor property |
| [batch-process-multiple-visio-files-applying-a-30-degree-text-rotation-to-all-shape-texts.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/batch-process-multiple-visio-files-applying-a-30-degree-text-rotation-to-all-shape-texts.cs) | `Diagram`, `Pages`, `Save` | Batch process multiple visio files applying a 30 degree text rotation to all shape texts |
| [calculate-the-bounding-box-of-a-shape-s-text-block-after-setting-custom-width-and-height-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/calculate-the-bounding-box-of-a-shape-s-text-block-after-setting-custom-width-and-height-values.cs) | `Diagram`, `Pages`, `Save` | Calculate the bounding box of a shape s text block after setting custom width and height values |
| [combine-text-rotation-and-margin-adjustments-to-create-a-diagonal-text-effect-inside-a-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/combine-text-rotation-and-margin-adjustments-to-create-a-diagonal-text-effect-inside-a-shape.cs) | `Diagram`, `Pages`, `Save` | Combine text rotation and margin adjustments to create a diagonal text effect inside a shape |
| [copy-text-block-formatting-from-one-shape-to-another-using-the-shape-textblock-property-clone.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/copy-text-block-formatting-from-one-shape-to-another-using-the-shape-textblock-property-clone.cs) | `Diagram`, `Pages`, `Save` | Copy text block formatting from one shape to another using the shape textblock property clone |
| [define-a-default-tab-stop-of-0-5-inches-to-control-spacing-of-tab-characters-in-shape-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/define-a-default-tab-stop-of-0-5-inches-to-control-spacing-of-tab-characters-in-shape-text.cs) | `Diagram`, `Pages`, `Save` | Define a default tab stop of 0 5 inches to control spacing of tab characters in shape text |
| [detect-shapes-with-empty-text-blocks-and-assign-a-placeholder-background-color-for-visual-cues.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/detect-shapes-with-empty-text-blocks-and-assign-a-placeholder-background-color-for-visual-cues.cs) | `Diagram`, `Pages`, `Save` | Detect shapes with empty text blocks and assign a placeholder background color for visual cues |
| [export-the-text-block-content-of-a-shape-to-a-plain-text-file-for-external-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/export-the-text-block-content-of-a-shape-to-a-plain-text-file-for-external-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Export the text block content of a shape to a plain text file for external analysis |
| [filter-shapes-by-type-and-apply-centered-text-alignment-only-to-rectangle-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/filter-shapes-by-type-and-apply-centered-text-alignment-only-to-rectangle-shapes.cs) | `Diagram`, `Pages`, `Save` | Filter shapes by type and apply centered text alignment only to rectangle shapes |
| [generate-a-report-listing-each-shape-s-text-alignment-margin-settings-and-background-transparency-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/generate-a-report-listing-each-shape-s-text-alignment-margin-settings-and-background-transparency-values.cs) | `Diagram`, `Pages`, `Shapes` | Generate a report listing each shape s text alignment margin settings and background transparency values |
| [implement-error-handling-when-setting-text-direction-on-shapes-that-lack-a-textblock-section.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/implement-error-handling-when-setting-text-direction-on-shapes-that-lack-a-textblock-section.cs) | `Diagram`, `Pages`, `Save` | Implement error handling when setting text direction on shapes that lack a textblock section |
| [iterate-through-all-shapes-in-a-diagram-and-set-their-text-direction-to-vertical-for-consistency.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/iterate-through-all-shapes-in-a-diagram-and-set-their-text-direction-to-vertical-for-consistency.cs) | `Diagram`, `Pages`, `Save` | Iterate through all shapes in a diagram and set their text direction to vertical for consistency |
| [load-a-diagram-modify-text-background-transparency-based-on-shape-fill-opacity-then-save.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/load-a-diagram-modify-text-background-transparency-based-on-shape-fill-opacity-then-save.cs) | `Diagram`, `Pages`, `Save` | Load a diagram modify text background transparency based on shape fill opacity then save |
| [load-a-visio-file-access-a-shape-and-set-its-text-direction-to-right-to-left.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/load-a-visio-file-access-a-shape-and-set-its-text-direction-to-right-to-left.cs) | `Diagram`, `Pages`, `Save` | Load a visio file access a shape and set its text direction to right to left |
| [modify-the-top-bottom-left-and-right-text-margins-of-a-shape-to-specific-pixel-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/modify-the-top-bottom-left-and-right-text-margins-of-a-shape-to-specific-pixel-values.cs) | `Diagram`, `Pages`, `Save` | Modify the top bottom left and right text margins of a shape to specific pixel values |
| [pin-the-text-to-a-custom-location-inside-a-shape-by-setting-the-txtpin-property-coordinates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/pin-the-text-to-a-custom-location-inside-a-shape-by-setting-the-txtpin-property-coordinates.cs) | `Diagram`, `Pages`, `Save` | Pin the text to a custom location inside a shape by setting the txtpin property coordinates |
| [position-shape-text-at-the-bottom-of-the-shape-using-orientation-angle-and-bottom-margin-adjustments.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/position-shape-text-at-the-bottom-of-the-shape-using-orientation-angle-and-bottom-margin-adjustments.cs) | `Diagram`, `Pages`, `Save` | Position shape text at the bottom of the shape using orientation angle and bottom margin adjustments |
| [position-shape-text-at-the-top-of-the-shape-by-configuring-orientation-angle-and-vertical-offset.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/position-shape-text-at-the-top-of-the-shape-by-configuring-orientation-angle-and-vertical-offset.cs) | `Diagram`, `Pages`, `Save` | Position shape text at the top of the shape by configuring orientation angle and vertical offset |
| [programmatically-increase-the-text-block-s-left-margin-by-10-points-for-all-shapes-in-a-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/programmatically-increase-the-text-block-s-left-margin-by-10-points-for-all-shapes-in-a-page.cs) | `Diagram`, `Pages`, `Save` | Programmatically increase the text block s left margin by 10 points for all shapes in a page |
| [read-the-current-text-alignment-of-each-shape-and-log-mismatches-against-a-style-guide.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/read-the-current-text-alignment-of-each-shape-and-log-mismatches-against-a-style-guide.cs) | `Diagram`, `Pages`, `Save` | Read the current text alignment of each shape and log mismatches against a style guide |
| [reset-a-shape-s-text-margins-to-default-values-after-custom-adjustments-have-been-applied.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/reset-a-shape-s-text-margins-to-default-values-after-custom-adjustments-have-been-applied.cs) | `Diagram`, `Pages`, `Save` | Reset a shape s text margins to default values after custom adjustments have been applied |
| [retrieve-a-shape-s-textblock-then-change-the-text-alignment-to-centered-within-the-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/retrieve-a-shape-s-textblock-then-change-the-text-alignment-to-centered-within-the-shape.cs) | `Diagram`, `Pages`, `Save` | Retrieve a shape s textblock then change the text alignment to centered within the shape |
| [rotate-shape-text-by-45-degrees-using-the-txtlocpin-orientation-angle-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/rotate-shape-text-by-45-degrees-using-the-txtlocpin-orientation-angle-property.cs) | `Diagram`, `Pages`, `Save` | Rotate shape text by 45 degrees using the txtlocpin orientation angle property |
| [save-the-modified-visio-diagram-after-applying-all-text-formatting-changes-to-a-new-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/save-the-modified-visio-diagram-after-applying-all-text-formatting-changes-to-a-new-file.cs) | `Diagram`, `Pages`, `Save` | Save the modified visio diagram after applying all text formatting changes to a new file |
| [serialize-a-shape-s-textblock-properties-to-json-for-configuration-backup-and-reuse.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/serialize-a-shape-s-textblock-properties-to-json-for-configuration-backup-and-reuse.cs) | `Diagram`, `Pages`, `Shapes` | Serialize a shape s textblock properties to json for configuration backup and reuse |
| [set-the-text-block-width-and-height-to-match-the-shape-s-dimensions-for-full-coverage.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/set-the-text-block-width-and-height-to-match-the-shape-s-dimensions-for-full-coverage.cs) | `Diagram`, `Pages`, `Save` | Set the text block width and height to match the shape s dimensions for full coverage |
| [use-conditional-logic-to-apply-a-transparent-background-only-to-shapes-containing-warning-messages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/use-conditional-logic-to-apply-a-transparent-background-only-to-shapes-containing-warning-messages.cs) | `Diagram`, `Pages`, `Save` | Use conditional logic to apply a transparent background only to shapes containing warning messages |
| [use-txtpin-to-anchor-shape-text-at-the-exact-center-point-of-the-shape-s-geometry.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/use-txtpin-to-anchor-shape-text-at-the-exact-center-point-of-the-shape-s-geometry.cs) | `Diagram`, `Pages`, `Save` | Use txtpin to anchor shape text at the exact center point of the shape s geometry |
| [validate-that-each-shape-s-text-background-color-meets-a-predefined-corporate-color-palette.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes/validate-that-each-shape-s-text-background-color-meets-a-predefined-corporate-color-palette.cs) | `Diagram`, `Pages`, `Shapes` | Validate that each shape s text background color meets a predefined corporate color palette |

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

- `Diagram`
- `Pages`
- `Save`
- `Shapes`
- `diagram`
- `page`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with text boxes capabilities are applied in production applications:

- Adding annotation text boxes to diagrams programmatically
- Positioning and formatting standalone text elements precisely

## Developer Q&A

Frequently asked questions about **Working With Text Boxes** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Text Boxes in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.5.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Text](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text) — text content and formatting
- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling

## Category Statistics

- Total examples: 35
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-06-23 | Examples: 35 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
