---
category: working-with-hyperlinks
display_name: Working With Hyperlinks
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 35
pass_rate: 100.0
generated: 2026-07-28
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Hyperlinks

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Hyperlinks** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 35 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-07-28 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Hyperlinks** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with hyperlinks operations.
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
| `System.IO` | 24 | File, Stream, Path, Directory operations |
| `System.Collections.Generic` | 6 | List, Dictionary, HashSet |
| `Aspose.Diagram.Saving` | 5 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Text.Json` | 2 | JSON serialization |
| `System.Threading.Tasks` | 2 | Supporting utilities |
| `Aspose.Diagram.Manipulation` | 1 | Supporting utilities |
| `System.Net.Http` | 1 | Supporting utilities |

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
| [add-a-hyperlink-using-a-mailto-address-to-enable-sending-email-directly-from-the-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/add-a-hyperlink-using-a-mailto-address-to-enable-sending-email-directly-from-the-shape.cs) | `AddShape`, `Diagram`, `Pages` | Add a hyperlink using a mailto address to enable sending email directly from the shape |
| [add-a-new-hyperlink-to-the-selected-shape-pointing-to-an-external-website-with-a-descriptive-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/add-a-new-hyperlink-to-the-selected-shape-pointing-to-an-external-website-with-a-descriptive-name.cs) | `Diagram`, `Pages`, `Save` | Add a new hyperlink to the selected shape pointing to an external website with a descriptive name |
| [apply-a-filter-to-select-only-shapes-whose-hyperlinks-target-external-domains-and-list-their-identifiers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/apply-a-filter-to-select-only-shapes-whose-hyperlinks-target-external-domains-and-list-their-identifiers.cs) | `Diagram`, `Pages`, `Shapes` | Apply a filter to select only shapes whose hyperlinks target external domains and list their identifiers |
| [apply-conditional-logic-to-add-hyperlinks-only-to-shapes-that-meet-a-specific-custom-property-value.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/apply-conditional-logic-to-add-hyperlinks-only-to-shapes-that-meet-a-specific-custom-property-value.cs) | `AddShape`, `Diagram`, `Pages` | Apply conditional logic to add hyperlinks only to shapes that meet a specific custom property value |
| [check-for-duplicate-hyperlink-names-within-a-shape-and-consolidate-them-into-a-single-entry.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/check-for-duplicate-hyperlink-names-within-a-shape-and-consolidate-them-into-a-single-entry.cs) | `Diagram`, `Pages`, `Save` | Check for duplicate hyperlink names within a shape and consolidate them into a single entry |
| [clone-an-existing-hyperlink-and-attach-it-to-another-shape-with-a-modified-description.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/clone-an-existing-hyperlink-and-attach-it-to-another-shape-with-a-modified-description.cs) | `Diagram`, `Pages`, `Save` | Clone an existing hyperlink and attach it to another shape with a modified description |
| [compare-two-diagrams-and-list-shapes-where-hyperlink-targets-differ-outputting-differences-to-the-console.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/compare-two-diagrams-and-list-shapes-where-hyperlink-targets-differ-outputting-differences-to-the-console.cs) | `Diagram`, `Shapes`, `page` | Compare two diagrams and list shapes where hyperlink targets differ outputting differences to the console |
| [configure-the-hyperlink-s-subaddress-property-to-navigate-to-a-specific-page-within-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/configure-the-hyperlink-s-subaddress-property-to-navigate-to-a-specific-page-within-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Configure the hyperlink s subaddress property to navigate to a specific page within the diagram |
| [create-a-batch-process-adding-identical-navigation-hyperlinks-to-multiple-shapes-across-all-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/create-a-batch-process-adding-identical-navigation-hyperlinks-to-multiple-shapes-across-all-pages.cs) | `Diagram`, `Pages`, `Save` | Create a batch process adding identical navigation hyperlinks to multiple shapes across all pages |
| [create-a-configuration-file-defining-default-hyperlink-properties-and-apply-them-programmatically-to-new-hyperlinks.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/create-a-configuration-file-defining-default-hyperlink-properties-and-apply-them-programmatically-to-new-hyperlinks.cs) | `AddShape`, `Diagram`, `Save` | Create a configuration file defining default hyperlink properties and apply them programmatically to new hyperlinks |
| [create-a-hyperlink-that-opens-a-pdf-document-in-a-new-browser-window.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/create-a-hyperlink-that-opens-a-pdf-document-in-a-new-browser-window.cs) | `AddShape`, `Diagram`, `Pages` | Create a hyperlink that opens a pdf document in a new browser window |
| [create-a-unit-test-verifying-hyperlink-addition-correctly-updates-the-hyperlinkcollection-count.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/create-a-unit-test-verifying-hyperlink-addition-correctly-updates-the-hyperlinkcollection-count.cs) | `AddShape`, `Diagram`, `Pages` | Create a unit test verifying hyperlink addition correctly updates the hyperlinkcollection count |
| [develop-a-console-application-that-scans-the-diagram-and-prints-each-shape-s-name-with-its-hyperlink-urls.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/develop-a-console-application-that-scans-the-diagram-and-prints-each-shape-s-name-with-its-hyperlink-urls.cs) | `Diagram`, `Pages`, `Shapes` | Develop a console application that scans the diagram and prints each shape s name with its hyperlink urls |
| [export-shape-identifiers-and-their-associated-hyperlink-urls-to-a-csv-file-for-reporting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/export-shape-identifiers-and-their-associated-hyperlink-urls-to-a-csv-file-for-reporting.cs) | `Diagram`, `Pages`, `Shapes` | Export shape identifiers and their associated hyperlink urls to a csv file for reporting |
| [generate-a-summary-report-showing-the-number-of-hyperlinks-per-page-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/generate-a-summary-report-showing-the-number-of-hyperlinks-per-page-in-the-diagram.cs) | `Diagram`, `Pages`, `diagram` | Generate a summary report showing the number of hyperlinks per page in the diagram |
| [generate-a-visual-map-of-hyperlink-connections-between-shapes-and-export-the-map-as-an-image-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/generate-a-visual-map-of-hyperlink-connections-between-shapes-and-export-the-map-as-an-image-file.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Generate a visual map of hyperlink connections between shapes and export the map as an image file |
| [implement-a-rollback-mechanism-that-restores-original-hyperlink-settings-if-an-update-error-occurs.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/implement-a-rollback-mechanism-that-restores-original-hyperlink-settings-if-an-update-error-occurs.cs) | `Diagram`, `Pages`, `Save` | Implement a rollback mechanism that restores original hyperlink settings if an update error occurs |
| [implement-a-routine-to-remove-all-hyperlinks-from-shapes-on-a-specified-page-before-exporting.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/implement-a-routine-to-remove-all-hyperlinks-from-shapes-on-a-specified-page-before-exporting.cs) | `Diagram`, `Pages`, `Save` | Implement a routine to remove all hyperlinks from shapes on a specified page before exporting |
| [implement-error-handling-for-malformed-or-unreachable-hyperlink-addresses-during-addition.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/implement-error-handling-for-malformed-or-unreachable-hyperlink-addresses-during-addition.cs) | `Diagram`, `Pages`, `Shapes` | Implement error handling for malformed or unreachable hyperlink addresses during addition |
| [implement-pagination-logic-that-adds-page-specific-hyperlinks-to-navigate-sequentially-through-diagram-pages.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/implement-pagination-logic-that-adds-page-specific-hyperlinks-to-navigate-sequentially-through-diagram-pages.cs) | `AddShape`, `Diagram`, `Pages` | Implement pagination logic that adds page specific hyperlinks to navigate sequentially through diagram pages |
| [iterate-through-a-shape-s-hyperlinks-and-log-each-hyperlink-s-name-and-target-address.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/iterate-through-a-shape-s-hyperlinks-and-log-each-hyperlink-s-name-and-target-address.cs) | `Diagram`, `Pages`, `Shapes` | Iterate through a shape s hyperlinks and log each hyperlink s name and target address |
| [load-a-diagram-from-a-memory-stream-modify-hyperlink-targets-and-write-the-updated-diagram-back-to-the-stream.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/load-a-diagram-from-a-memory-stream-modify-hyperlink-targets-and-write-the-updated-diagram-back-to-the-stream.cs) | `Diagram`, `Pages`, `Save` | Load a diagram from a memory stream modify hyperlink targets and write the updated diagram back to the stream |
| [load-the-visio-diagram-from-file-and-access-a-shape-using-its-unique-identifier.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/load-the-visio-diagram-from-file-and-access-a-shape-using-its-unique-identifier.cs) | `Diagram`, `Pages`, `diagram` | Load the visio diagram from file and access a shape using its unique identifier |
| [locate-shapes-by-their-names-within-the-diagram-and-assign-page-link-hyperlinks-for-navigation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/locate-shapes-by-their-names-within-the-diagram-and-assign-page-link-hyperlinks-for-navigation.cs) | `Diagram`, `Pages`, `Save` | Locate shapes by their names within the diagram and assign page link hyperlinks for navigation |
| [programmatically-set-the-hyperlink-s-target-frame-to-open-in-the-same-window.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/programmatically-set-the-hyperlink-s-target-frame-to-open-in-the-same-window.cs) | `Diagram`, `Pages`, `Save` | Programmatically set the hyperlink s target frame to open in the same window |
| [remove-a-hyperlink-from-a-shape-by-matching-its-name-and-save-the-updated-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/remove-a-hyperlink-from-a-shape-by-matching-its-name-and-save-the-updated-diagram.cs) | `Diagram`, `Pages`, `Save` | Remove a hyperlink from a shape by matching its name and save the updated diagram |
| [replace-an-existing-hyperlink-s-target-url-with-a-new-address-while-preserving-its-description.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/replace-an-existing-hyperlink-s-target-url-with-a-new-address-while-preserving-its-description.cs) | `Diagram`, `Pages`, `Save` | Replace an existing hyperlink s target url with a new address while preserving its description |
| [retrieve-hyperlink-properties-convert-them-to-a-dictionary-and-pass-the-dictionary-to-a-logging-framework.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/retrieve-hyperlink-properties-convert-them-to-a-dictionary-and-pass-the-dictionary-to-a-logging-framework.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve hyperlink properties convert them to a dictionary and pass the dictionary to a logging framework |
| [retrieve-the-hyperlink-collection-from-a-shape-and-serialize-each-hyperlink-s-properties-into-json-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/retrieve-the-hyperlink-collection-from-a-shape-and-serialize-each-hyperlink-s-properties-into-json-format.cs) | `Diagram`, `Pages`, `Shapes` | Retrieve the hyperlink collection from a shape and serialize each hyperlink s properties into json format |
| [set-the-hyperlink-s-description-property-to-provide-tooltip-text-on-mouse-hover.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/set-the-hyperlink-s-description-property-to-provide-tooltip-text-on-mouse-hover.cs) | `Diagram`, `Pages`, `Save` | Set the hyperlink s description property to provide tooltip text on mouse hover |
| [update-all-hyperlinks-referencing-an-old-domain-to-a-new-domain-via-string-replacement.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/update-all-hyperlinks-referencing-an-old-domain-to-a-new-domain-via-string-replacement.cs) | `Diagram`, `Pages`, `Save` | Update all hyperlinks referencing an old domain to a new domain via string replacement |
| [use-asynchronous-i-o-to-load-the-diagram-modify-hyperlinks-and-save-without-blocking-the-ui-thread.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/use-asynchronous-i-o-to-load-the-diagram-modify-hyperlinks-and-save-without-blocking-the-ui-thread.cs) | `Diagram`, `Pages`, `Save` | Use asynchronous i o to load the diagram modify hyperlinks and save without blocking the ui thread |
| [use-hyperlinkcollection-add-overload-to-set-name-address-and-subaddress-simultaneously.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/use-hyperlinkcollection-add-overload-to-set-name-address-and-subaddress-simultaneously.cs) | `AddShape`, `Diagram`, `Pages` | Use hyperlinkcollection add overload to set name address and subaddress simultaneously |
| [use-the-hyperlinkcollection-class-to-enumerate-all-hyperlinks-associated-with-a-specific-shape.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/use-the-hyperlinkcollection-class-to-enumerate-all-hyperlinks-associated-with-a-specific-shape.cs) | `Diagram`, `Pages`, `Save` | Use the hyperlinkcollection class to enumerate all hyperlinks associated with a specific shape |
| [validate-that-each-shape-with-a-hyperlink-has-a-non-empty-description-property.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks/validate-that-each-shape-with-a-hyperlink-has-a-non-empty-description-property.cs) | `Diagram`, `Pages`, `Shapes` | Validate that each shape with a hyperlink has a non empty description property |

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
- `ImageSaveOptions`
- `Pages`
- `Prop`
- `Save`
- `Shapes`
- `diagram`
- `page`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with hyperlinks capabilities are applied in production applications:

- Adding navigation links between pages in large multi-page process maps
- Linking diagram shapes to external documentation or URLs
- Generating clickable diagrams for interactive web-based presentations

## Developer Q&A

Frequently asked questions about **Working With Hyperlinks** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Hyperlinks in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) — shape creation, modification, and styling
- [Working With Pages](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages) — page management and navigation
- [Working With Text](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text) — text content and formatting

## Category Statistics

- Total examples: 35
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-07-28 | Examples: 35 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
