---
category: visio-activex-controls
display_name: Visio Activex Controls
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 30
pass_rate: 100.0
generated: 2026-08-03
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Visio Activex Controls

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Visio Activex Controls** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 30 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Visio Activex Controls** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for visio activex controls operations.
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
| `Aspose.Diagram.ActiveXControls` | 25 | Supporting utilities |
| `System.IO` | 21 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 5 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 4 | List, Dictionary, HashSet |
| `System.Reflection` | 3 | Supporting utilities |
| `System.Text.Json` | 2 | JSON serialization |
| `System.Diagnostics` | 1 | Supporting utilities |
| `System.Runtime.InteropServices` | 1 | Supporting utilities |
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

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-new-slider-activex-control-to-a-shape-programmatically-and-initialize-its-minimum-maximum-and-value-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/add-a-new-slider-activex-control-to-a-shape-programmatically-and-initialize-its-minimum-maximum-and-value-properties.cs) | `Diagram`, `Pages`, `Save` | Add a new slider activex control to a shape programmatically and initialize its minimum maximum and value properties |
| [benchmark-the-time-required-to-retrieve-and-modify-activex-controls-across-diagrams-of-varying-sizes-and-complexities.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/benchmark-the-time-required-to-retrieve-and-modify-activex-controls-across-diagrams-of-varying-sizes-and-complexities.cs) | `Diagram`, `Pages`, `Save` | Benchmark the time required to retrieve and modify activex controls across diagrams of varying sizes and complexities |
| [cast-the-generic-activexcontrol-instance-to-its-specific-control-class-before-modifying-any-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/cast-the-generic-activexcontrol-instance-to-its-specific-control-class-before-modifying-any-properties.cs) | `Diagram`, `Pages`, `Save` | Cast the generic activexcontrol instance to its specific control class before modifying any properties |
| [check-whether-a-shape-s-activexcontrol-property-is-null-before-accessing-its-members.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/check-whether-a-shape-s-activexcontrol-property-is-null-before-accessing-its-members.cs) | `Diagram`, `Pages`, `Save` | Check whether a shape s activexcontrol property is null before accessing its members |
| [compare-two-visio-diagrams-by-checking-differences-in-activex-control-property-values-across-matching-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/compare-two-visio-diagrams-by-checking-differences-in-activex-control-property-values-across-matching-shapes.cs) | `Diagram`, `Shapes`, `page` | Compare two visio diagrams by checking differences in activex control property values across matching shapes |
| [create-a-reusable-helper-method-that-abstracts-casting-of-activexcontrol-objects-to-their-concrete-types.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/create-a-reusable-helper-method-that-abstracts-casting-of-activexcontrol-objects-to-their-concrete-types.cs) | `Diagram` | Create a reusable helper method that abstracts casting of activexcontrol objects to their concrete types |
| [create-a-script-that-disables-all-activex-controls-on-a-diagram-before-exporting-it-to-pdf-for-security.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/create-a-script-that-disables-all-activex-controls-on-a-diagram-before-exporting-it-to-pdf-for-security.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Create a script that disables all activex controls on a diagram before exporting it to pdf for security |
| [create-a-unit-test-that-verifies-property-changes-on-a-retrieved-checkbox-activex-control-are-persisted-after-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/create-a-unit-test-that-verifies-property-changes-on-a-retrieved-checkbox-activex-control-are-persisted-after-saving.cs) | `Diagram`, `Save`, `diagram` | Create a unit test that verifies property changes on a retrieved checkbox activex control are persisted after saving |
| [deserialize-json-configuration-and-apply-the-values-to-corresponding-activex-control-properties-within-a-visio-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/deserialize-json-configuration-and-apply-the-values-to-corresponding-activex-control-properties-within-a-visio-diagram.cs) | `Diagram`, `Pages`, `Save` | Deserialize json configuration and apply the values to corresponding activex control properties within a visio diagram |
| [design-a-feature-that-disables-activex-controls-when-the-diagram-is-opened-in-read-only-mode-to-prevent-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/design-a-feature-that-disables-activex-controls-when-the-diagram-is-opened-in-read-only-mode-to-prevent-changes.cs) | `Diagram`, `Pages`, `Save` | Design a feature that disables activex controls when the diagram is opened in read only mode to prevent changes |
| [determine-the-concrete-activex-control-type-of-a-retrieved-object-by-inspecting-its-progid-property-value.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/determine-the-concrete-activex-control-type-of-a-retrieved-object-by-inspecting-its-progid-property-value.cs) | `Diagram`, `Pages`, `Shapes` | Determine the concrete activex control type of a retrieved object by inspecting its progid property value |
| [extract-the-helpfile-property-from-a-commandbutton-activex-control-to-locate-associated-documentation-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/extract-the-helpfile-property-from-a-commandbutton-activex-control-to-locate-associated-documentation-resources.cs) | `Diagram`, `Pages`, `Shapes` | Extract the helpfile property from a commandbutton activex control to locate associated documentation resources |
| [handle-invalidcastexception-when-attempting-to-cast-an-activexcontrol-to-an-incompatible-control-class-type.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/handle-invalidcastexception-when-attempting-to-cast-an-activexcontrol-to-an-incompatible-control-class-type.cs) | `Diagram`, `Pages`, `Save` | Handle invalidcastexception when attempting to cast an activexcontrol to an incompatible control class type |
| [implement-a-safeguard-that-prevents-modifying-read-only-properties-of-an-activex-control-and-logs-a-warning.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/implement-a-safeguard-that-prevents-modifying-read-only-properties-of-an-activex-control-and-logs-a-warning.cs) | `Diagram`, `Pages`, `Save` | Implement a safeguard that prevents modifying read only properties of an activex control and logs a warning |
| [implement-error-handling-to-catch-comexception-when-accessing-activex-control-properties-that-require-specific-permissio.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/implement-error-handling-to-catch-comexception-when-accessing-activex-control-properties-that-require-specific-permissio.cs) | `Diagram`, `Pages`, `Save` | Implement error handling to catch comexception when accessing activex control properties that require specific permissio |
| [integrate-activex-control-manipulation-into-an-asp-net-mvc-application-to-dynamically-update-diagram-visuals.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/integrate-activex-control-manipulation-into-an-asp-net-mvc-application-to-dynamically-update-diagram-visuals.cs) | `Diagram`, `Pages`, `Save` | Integrate activex control manipulation into an asp net mvc application to dynamically update diagram visuals |
| [iterate-over-all-shapes-in-a-diagram-processing-only-those-that-contain-an-activexcontrol-object.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/iterate-over-all-shapes-in-a-diagram-processing-only-those-that-contain-an-activexcontrol-object.cs) | `Diagram`, `Pages`, `Save` | Iterate over all shapes in a diagram processing only those that contain an activexcontrol object |
| [load-a-visio-diagram-containing-activex-controls-and-enumerate-all-shapes-that-embed-such-controls.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/load-a-visio-diagram-containing-activex-controls-and-enumerate-all-shapes-that-embed-such-controls.cs) | `Diagram`, `Pages`, `Shapes` | Load a visio diagram containing activex controls and enumerate all shapes that embed such controls |
| [load-a-visio-diagram-from-a-memory-stream-and-update-activex-control-properties-without-writing-to-disk.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/load-a-visio-diagram-from-a-memory-stream-and-update-activex-control-properties-without-writing-to-disk.cs) | `Diagram`, `Pages`, `Save` | Load a visio diagram from a memory stream and update activex control properties without writing to disk |
| [log-each-property-change-made-to-an-activex-control-including-previous-and-new-values-for-audit-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/log-each-property-change-made-to-an-activex-control-including-previous-and-new-values-for-audit-purposes.cs) | `Diagram`, `Pages`, `Save` | Log each property change made to an activex control including previous and new values for audit purposes |
| [read-the-current-caption-property-of-a-button-activex-control-embedded-in-a-visio-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/read-the-current-caption-property-of-a-button-activex-control-embedded-in-a-visio-shape.cs) | `Diagram`, `Pages`, `Shapes` | Read the current caption property of a button activex control embedded in a visio shape |
| [remove-an-activexcontrol-from-a-shape-by-setting-its-activexcontrol-property-to-null-and-saving-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/remove-an-activexcontrol-from-a-shape-by-setting-its-activexcontrol-property-to-null-and-saving-diagram.cs) | `Diagram`, `Pages`, `Save` | Remove an activexcontrol from a shape by setting its activexcontrol property to null and saving diagram |
| [retrieve-the-activexcontrol-object-from-a-specific-shape-using-shape-activexcontrol-property-for-further-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/retrieve-the-activexcontrol-object-from-a-specific-shape-using-shape-activexcontrol-property-for-further-processing.cs) | `Diagram`, `Save`, `diagram` | Retrieve the activexcontrol object from a specific shape using shape activexcontrol property for further processing |
| [save-the-modified-visio-diagram-to-a-new-file-preserving-original-activex-control-configurations-and-layout.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/save-the-modified-visio-diagram-to-a-new-file-preserving-original-activex-control-configurations-and-layout.cs) | `Diagram`, `Save`, `diagram` | Save the modified visio diagram to a new file preserving original activex control configurations and layout |
| [serialize-the-property-values-of-an-activex-control-to-json-for-external-configuration-management.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/serialize-the-property-values-of-an-activex-control-to-json-for-external-configuration-management.cs) | `Diagram`, `Pages`, `Shapes` | Serialize the property values of an activex control to json for external configuration management |
| [set-the-enabled-property-of-a-checkbox-activex-control-to-false-to-disable-user-interaction.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/set-the-enabled-property-of-a-checkbox-activex-control-to-false-to-disable-user-interaction.cs) | `Diagram`, `Pages`, `Save` | Set the enabled property of a checkbox activex control to false to disable user interaction |
| [update-one-or-more-properties-of-the-activex-control-and-persist-the-modified-diagram-to-a-new-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/update-one-or-more-properties-of-the-activex-control-and-persist-the-modified-diagram-to-a-new-file.cs) | `Diagram`, `Pages`, `Save` | Update one or more properties of the activex control and persist the modified diagram to a new file |
| [use-reflection-to-enumerate-all-publicly-settable-properties-of-a-specific-activex-control-class-at-runtime.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/use-reflection-to-enumerate-all-publicly-settable-properties-of-a-specific-activex-control-class-at-runtime.cs) | `Diagram`, `Pages`, `diagram` | Use reflection to enumerate all publicly settable properties of a specific activex control class at runtime |
| [validate-that-the-value-property-of-a-slider-activex-control-remains-within-its-defined-minimum-and-maximum-range.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/validate-that-the-value-property-of-a-slider-activex-control-remains-within-its-defined-minimum-and-maximum-range.cs) | `Diagram`, `Page`, `Pages` | Validate that the value property of a slider activex control remains within its defined minimum and maximum range |
| [write-a-method-that-retrieves-all-activex-controls-on-a-given-page-and-returns-their-count.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls/write-a-method-that-retrieves-all-activex-controls-on-a-given-page-and-returns-their-count.cs) | `Shapes`, `page` | Write a method that retrieves all activex controls on a given page and returns their count |

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
- `Page`
- `Pages`
- `PdfSaveOptions`
- `Save`
- `Shapes`
- `diagram`
- `page`
- `shape`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** visio activex controls capabilities are applied in production applications:

- Reading and modifying ActiveX control properties in legacy Visio diagrams
- Extracting ActiveX control state for migration to modern formats

## Developer Q&A

Frequently asked questions about **Visio Activex Controls** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Visio Activex Controls in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure

## Category Statistics

- Total examples: 30
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-08-03 | Examples: 30 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
