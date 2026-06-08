---
category: visio-shape-gradient
display_name: Visio Shape Gradient
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 30
pass_rate: 100.0
generated: 2026-06-08
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Visio Shape Gradient

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Visio Shape Gradient** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 30 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-08 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Visio Shape Gradient** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for visio shape gradient operations.
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
| `System` | 30 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 29 | Core diagram API |
| `System.IO` | 23 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 7 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 6 | List, Dictionary, HashSet |

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
| [access-gradient-stop-at-index-0-and-read-its-color-value-for-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/access-gradient-stop-at-index-0-and-read-its-color-value-for-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Access gradient stop at index 0 and read its color value for analysis |
| [access-gradient-stop-at-index-0-and-read-its-position-value-for-verification.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/access-gradient-stop-at-index-0-and-read-its-position-value-for-verification.cs) | `Diagram`, `Pages`, `diagram` | Access gradient stop at index 0 and read its position value for verification |
| [add-a-new-gradient-stop-at-position-0-25-with-yellow-color-rgb-255-255-0.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/add-a-new-gradient-stop-at-position-0-25-with-yellow-color-rgb-255-255-0.cs) | `Diagram`, `Pages`, `Save` | Add a new gradient stop at position 0 25 with yellow color rgb 255 255 0 |
| [add-a-new-gradient-stop-at-position-0-75-with-blue-color-rgb-0-0-255.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/add-a-new-gradient-stop-at-position-0-75-with-blue-color-rgb-0-0-255.cs) | `Diagram`, `Pages`, `Save` | Add a new gradient stop at position 0 75 with blue color rgb 0 0 255 |
| [apply-the-modified-gradientfill-back-to-the-shape-s-fill-property-to-update-appearance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/apply-the-modified-gradientfill-back-to-the-shape-s-fill-property-to-update-appearance.cs) | `Diagram`, `Pages`, `Save` | Apply the modified gradientfill back to the shape s fill property to update appearance |
| [change-the-color-of-gradient-stop-at-index-0-to-pure-red-rgb-255-0-0.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/change-the-color-of-gradient-stop-at-index-0-to-pure-red-rgb-255-0-0.cs) | `Diagram`, `Pages`, `Save` | Change the color of gradient stop at index 0 to pure red rgb 255 0 0 |
| [change-the-color-of-gradient-stop-at-index-1-to-pure-green-rgb-0-255-0.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/change-the-color-of-gradient-stop-at-index-1-to-pure-green-rgb-0-255-0.cs) | `Diagram`, `Pages`, `Save` | Change the color of gradient stop at index 1 to pure green rgb 0 255 0 |
| [change-the-position-of-gradient-stop-at-index-0-to-0-0-representing-the-start.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/change-the-position-of-gradient-stop-at-index-0-to-0-0-representing-the-start.cs) | `Diagram`, `Pages`, `Save` | Change the position of gradient stop at index 0 to 0 0 representing the start |
| [change-the-position-of-gradient-stop-at-index-1-to-0-5-representing-the-midpoint.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/change-the-position-of-gradient-stop-at-index-1-to-0-5-representing-the-midpoint.cs) | `Diagram`, `Pages`, `Save` | Change the position of gradient stop at index 1 to 0 5 representing the midpoint |
| [compare-the-gradient-angles-of-two-shapes-and-record-any-variations-in-orientation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/compare-the-gradient-angles-of-two-shapes-and-record-any-variations-in-orientation.cs) | `Diagram`, `Pages`, `Shapes` | Compare the gradient angles of two shapes and record any variations in orientation |
| [compare-the-gradient-direction-of-two-shapes-and-note-any-differences-for-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/compare-the-gradient-direction-of-two-shapes-and-note-any-differences-for-analysis.cs) | `Diagram`, `Pages`, `Shapes` | Compare the gradient direction of two shapes and note any differences for analysis |
| [iterate-through-all-gradient-stops-and-log-each-stop-s-color-and-position-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/iterate-through-all-gradient-stops-and-log-each-stop-s-color-and-position-values.cs) | `Diagram`, `Pages`, `Save` | Iterate through all gradient stops and log each stop s color and position values |
| [load-a-visio-diagram-and-locate-a-shape-by-its-identifier-for-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/load-a-visio-diagram-and-locate-a-shape-by-its-identifier-for-processing.cs) | `Diagram`, `Pages`, `Shapes` | Load a visio diagram and locate a shape by its identifier for processing |
| [read-the-current-gradient-angle-of-the-selected-shape-to-understand-its-orientation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/read-the-current-gradient-angle-of-the-selected-shape-to-understand-its-orientation.cs) | `Diagram`, `Pages`, `Shapes` | Read the current gradient angle of the selected shape to understand its orientation |
| [read-the-current-gradient-direction-of-the-selected-shape-for-verification-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/read-the-current-gradient-direction-of-the-selected-shape-for-verification-purposes.cs) | `Diagram`, `Pages`, `diagram` | Read the current gradient direction of the selected shape for verification purposes |
| [remove-the-gradient-stop-at-index-2-from-the-collection-to-simplify-the-gradient.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/remove-the-gradient-stop-at-index-2-from-the-collection-to-simplify-the-gradient.cs) | `Diagram`, `Pages`, `Save` | Remove the gradient stop at index 2 from the collection to simplify the gradient |
| [remove-the-gradient-stop-at-index-3-from-the-collection-to-adjust-stop-count.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/remove-the-gradient-stop-at-index-3-from-the-collection-to-adjust-stop-count.cs) | `Diagram`, `Pages`, `Save` | Remove the gradient stop at index 3 from the collection to adjust stop count |
| [reset-the-gradient-angle-to-0-degrees-to-achieve-a-left-to-right-fill-orientation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/reset-the-gradient-angle-to-0-degrees-to-achieve-a-left-to-right-fill-orientation.cs) | `Diagram`, `Pages`, `Save` | Reset the gradient angle to 0 degrees to achieve a left to right fill orientation |
| [reset-the-gradient-direction-to-the-shape-s-original-default-value-to-restore-initial-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/reset-the-gradient-direction-to-the-shape-s-original-default-value-to-restore-initial-layout.cs) | `Diagram`, `Pages`, `Save` | Reset the gradient direction to the shape s original default value to restore initial layout |
| [retrieve-the-gradient-stop-count-and-ensure-it-meets-a-minimum-of-three-stops.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/retrieve-the-gradient-stop-count-and-ensure-it-meets-a-minimum-of-three-stops.cs) | `Diagram`, `Pages`, `Save` | Retrieve the gradient stop count and ensure it meets a minimum of three stops |
| [retrieve-the-shape-s-fill-property-and-obtain-the-associated-gradientfill-object.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/retrieve-the-shape-s-fill-property-and-obtain-the-associated-gradientfill-object.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve the shape s fill property and obtain the associated gradientfill object |
| [set-the-gradient-angle-of-the-shape-to-0-degrees-for-a-horizontal-fill.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/set-the-gradient-angle-of-the-shape-to-0-degrees-for-a-horizontal-fill.cs) | `Pages`, `Save`, `diagram` | Set the gradient angle of the shape to 0 degrees for a horizontal fill |
| [set-the-gradient-angle-of-the-shape-to-45-degrees-for-a-diagonal-effect.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/set-the-gradient-angle-of-the-shape-to-45-degrees-for-a-diagonal-effect.cs) | `Diagram`, `Pages`, `Save` | Set the gradient angle of the shape to 45 degrees for a diagonal effect |
| [set-the-gradient-angle-of-the-shape-to-90-degrees-to-create-a-vertical-fill.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/set-the-gradient-angle-of-the-shape-to-90-degrees-to-create-a-vertical-fill.cs) | `Diagram`, `Pages`, `Save` | Set the gradient angle of the shape to 90 degrees to create a vertical fill |
| [set-the-gradient-direction-of-the-shape-to-diagonal-top-left-to-bottom-right.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/set-the-gradient-direction-of-the-shape-to-diagonal-top-left-to-bottom-right.cs) | `Diagram`, `Pages`, `Save` | Set the gradient direction of the shape to diagonal top left to bottom right |
| [set-the-gradient-direction-of-the-shape-to-horizontal-to-achieve-left-to-right-fill.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/set-the-gradient-direction-of-the-shape-to-horizontal-to-achieve-left-to-right-fill.cs) | `AddShape`, `Diagram`, `Pages` | Set the gradient direction of the shape to horizontal to achieve left to right fill |
| [set-the-gradient-direction-of-the-shape-to-vertical-for-top-to-bottom-fill-effect.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/set-the-gradient-direction-of-the-shape-to-vertical-for-top-to-bottom-fill-effect.cs) | `Diagram`, `Pages`, `Save` | Set the gradient direction of the shape to vertical for top to bottom fill effect |
| [shift-all-gradient-stop-positions-by-0-1-while-keeping-each-value-within-the-0-1-range.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/shift-all-gradient-stop-positions-by-0-1-while-keeping-each-value-within-the-0-1-range.cs) | `Diagram`, `Pages`, `Save` | Shift all gradient stop positions by 0 1 while keeping each value within the 0 1 range |
| [update-the-color-of-all-gradient-stops-to-a-uniform-gray-shade-rgb-128-128-128.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/update-the-color-of-all-gradient-stops-to-a-uniform-gray-shade-rgb-128-128-128.cs) | `Diagram`, `Pages`, `Save` | Update the color of all gradient stops to a uniform gray shade rgb 128 128 128 |
| [verify-that-the-shape-now-contains-exactly-four-gradient-stops-after-modifications.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient/verify-that-the-shape-now-contains-exactly-four-gradient-stops-after-modifications.cs) | `Diagram`, `Pages`, `Save` | Verify that the shape now contains exactly four gradient stops after modifications |

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

*Auto-generated by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) · 2026-06-08*
