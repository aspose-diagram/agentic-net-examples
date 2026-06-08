---
category: working-with-external-data-sources
display_name: Working With External Data Sources
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 29
pass_rate: 100.0
generated: 2026-06-08
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With External Data Sources

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With External Data Sources** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 29 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-08 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With External Data Sources** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with external data sources operations.
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
| `Aspose.Diagram` | 29 | Core diagram API |
| `System` | 29 | Console, Math, DateTime, Exception |
| `System.IO` | 20 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 17 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 12 | List, Dictionary, HashSet |
| `System.Text.Json` | 5 | JSON serialization |
| `System.Xml` | 3 | Supporting utilities |
| `System.Data.SqlClient` | 3 | Supporting utilities |
| `System.Net.Http` | 3 | Supporting utilities |
| `System.Threading.Tasks` | 3 | Supporting utilities |
| `System.Data` | 2 | Supporting utilities |
| `System.Text` | 2 | StringBuilder |
| `System.Xml.Xsl` | 1 | Supporting utilities |
| `Aspose.Cells` | 1 | Supporting utilities |
| `System.Linq` | 1 | LINQ queries on collections |
| `System.Threading` | 1 | Supporting utilities |
| `System.Xml.Linq` | 1 | Supporting utilities |
| `System.Diagnostics` | 1 | Supporting utilities |
| `System.Security` | 1 | Supporting utilities |
| `System.Xml.Schema` | 1 | Supporting utilities |

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
| [apply-conditional-formatting-to-shapes-based-on-thresholds-defined-in-external-data.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/apply-conditional-formatting-to-shapes-based-on-thresholds-defined-in-external-data.cs) | `Diagram`, `Pages`, `Save` | Apply conditional formatting to shapes based on thresholds defined in external data |
| [apply-transformations-to-imported-external-data-values-before-updating-diagram-shape-text.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/apply-transformations-to-imported-external-data-values-before-updating-diagram-shape-text.cs) | `Diagram`, `Pages`, `Save` | Apply transformations to imported external data values before updating diagram shape text |
| [apply-xslt-transformations-to-xml-data-before-importing-into-diagram-shape-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/apply-xslt-transformations-to-xml-data-before-importing-into-diagram-shape-properties.cs) | `Diagram`, `Pages`, `Save` | Apply xslt transformations to xml data before importing into diagram shape properties |
| [connect-to-a-sql-server-database-and-populate-diagram-shapes-with-query-results.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/connect-to-a-sql-server-database-and-populate-diagram-shapes-with-query-results.cs) | `AddShape`, `Diagram`, `Pages` | Connect to a sql server database and populate diagram shapes with query results |
| [create-new-diagram-pages-dynamically-for-each-distinct-record-in-an-external-dataset.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/create-new-diagram-pages-dynamically-for-each-distinct-record-in-an-external-dataset.cs) | `AddMaster`, `AddShape`, `Diagram` | Create new diagram pages dynamically for each distinct record in an external dataset |
| [create-swimlane-diagrams-where-lane-definitions-are-driven-by-external-csv-rows.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/create-swimlane-diagrams-where-lane-definitions-are-driven-by-external-csv-rows.cs) | `AddMaster`, `AddShape`, `Diagram` | Create swimlane diagrams where lane definitions are driven by external csv rows |
| [define-custom-mapping-rules-to-translate-external-field-names-to-diagram-shape-data-fields.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/define-custom-mapping-rules-to-translate-external-field-names-to-diagram-shape-data-fields.cs) | `Diagram`, `Pages`, `Save` | Define custom mapping rules to translate external field names to diagram shape data fields |
| [export-diagram-shape-data-to-a-csv-file-for-external-reporting-and-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/export-diagram-shape-data-to-a-csv-file-for-external-reporting-and-analysis.cs) | `Diagram`, `Save`, `diagram` | Export diagram shape data to a csv file for external reporting and analysis |
| [export-diagram-shape-data-to-an-excel-workbook-for-further-manipulation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/export-diagram-shape-data-to-an-excel-workbook-for-further-manipulation.cs) | `Diagram`, `Pages`, `Shapes` | Export diagram shape data to an excel workbook for further manipulation |
| [filter-external-data-using-linq-before-binding-to-diagram-shapes-to-reduce-clutter.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/filter-external-data-using-linq-before-binding-to-diagram-shapes-to-reduce-clutter.cs) | `Diagram`, `Pages`, `Save` | Filter external data using linq before binding to diagram shapes to reduce clutter |
| [generate-a-diagram-legend-automatically-from-distinct-external-data-categories.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/generate-a-diagram-legend-automatically-from-distinct-external-data-categories.cs) | `Diagram`, `Pages`, `Save` | Generate a diagram legend automatically from distinct external data categories |
| [implement-retry-logic-when-loading-data-from-an-unstable-network-source-for-diagram-updates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/implement-retry-logic-when-loading-data-from-an-unstable-network-source-for-diagram-updates.cs) | `Diagram` | Implement retry logic when loading data from an unstable network source for diagram updates |
| [import-shape-data-from-a-csv-file-and-bind-each-row-to-corresponding-diagram-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/import-shape-data-from-a-csv-file-and-bind-each-row-to-corresponding-diagram-shapes.cs) | `Diagram`, `Pages`, `Save` | Import shape data from a csv file and bind each row to corresponding diagram shapes |
| [integrate-sharepoint-list-items-as-external-data-to-drive-diagram-shape-content.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/integrate-sharepoint-list-items-as-external-data-to-drive-diagram-shape-content.cs) | `Diagram`, `Pages`, `Save` | Integrate sharepoint list items as external data to drive diagram shape content |
| [load-a-visio-diagram-from-an-xml-data-source-and-map-elements-to-shape-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/load-a-visio-diagram-from-an-xml-data-source-and-map-elements-to-shape-properties.cs) | `Diagram`, `Pages`, `Save` | Load a visio diagram from an xml data source and map elements to shape properties |
| [log-each-step-of-the-external-data-import-process-for-audit-and-debugging-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/log-each-step-of-the-external-data-import-process-for-audit-and-debugging-purposes.cs) | `Diagram`, `Save`, `diagram` | Log each step of the external data import process for audit and debugging purposes |
| [map-hierarchical-json-arrays-to-nested-shape-groups-within-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/map-hierarchical-json-arrays-to-nested-shape-groups-within-the-diagram.cs) | `AddShape`, `Diagram`, `Save` | Map hierarchical json arrays to nested shape groups within the diagram |
| [merge-hierarchical-xml-data-into-grouped-shapes-within-the-visio-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/merge-hierarchical-xml-data-into-grouped-shapes-within-the-visio-diagram.cs) | `AddShape`, `Diagram`, `Pages` | Merge hierarchical xml data into grouped shapes within the visio diagram |
| [perform-batch-processing-of-multiple-diagrams-each-loading-data-from-separate-csv-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/perform-batch-processing-of-multiple-diagrams-each-loading-data-from-separate-csv-files.cs) | `Diagram`, `Pages`, `Save` | Perform batch processing of multiple diagrams each loading data from separate csv files |
| [populate-shape-hyperlinks-using-urls-retrieved-from-an-external-data-feed.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/populate-shape-hyperlinks-using-urls-retrieved-from-an-external-data-feed.cs) | `Diagram`, `Pages`, `Save` | Populate shape hyperlinks using urls retrieved from an external data feed |
| [populate-shape-tooltips-with-descriptive-text-sourced-from-an-external-database.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/populate-shape-tooltips-with-descriptive-text-sourced-from-an-external-database.cs) | `Diagram`, `Pages`, `Save` | Populate shape tooltips with descriptive text sourced from an external database |
| [preserve-unicode-characters-when-importing-text-data-from-external-csv-files-into-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/preserve-unicode-characters-when-importing-text-data-from-external-csv-files-into-shapes.cs) | `Diagram`, `Save`, `diagram` | Preserve unicode characters when importing text data from external csv files into shapes |
| [profile-memory-usage-while-loading-large-external-datasets-into-a-visio-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/profile-memory-usage-while-loading-large-external-datasets-into-a-visio-diagram.cs) | `Diagram`, `Save`, `diagram` | Profile memory usage while loading large external datasets into a visio diagram |
| [retrieve-json-data-from-a-rest-endpoint-and-assign-values-to-shape-custom-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/retrieve-json-data-from-a-rest-endpoint-and-assign-values-to-shape-custom-properties.cs) | `Diagram`, `Pages`, `Save` | Retrieve json data from a rest endpoint and assign values to shape custom properties |
| [set-shape-visibility-dynamically-based-on-boolean-values-from-an-external-data-source.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/set-shape-visibility-dynamically-based-on-boolean-values-from-an-external-data-source.cs) | `Diagram`, `Pages`, `Save` | Set shape visibility dynamically based on boolean values from an external data source |
| [use-ado-net-to-fetch-data-from-an-oracle-database-and-populate-diagram-shapes-accordingly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/use-ado-net-to-fetch-data-from-an-oracle-database-and-populate-diagram-shapes-accordingly.cs) | `AddShape`, `Diagram`, `Pages` | Use ado net to fetch data from an oracle database and populate diagram shapes accordingly |
| [use-an-odata-feed-to-synchronize-external-data-with-diagram-shape-data-fields.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/use-an-odata-feed-to-synchronize-external-data-with-diagram-shape-data-fields.cs) | `Diagram`, `Pages`, `Save` | Use an odata feed to synchronize external data with diagram shape data fields |
| [use-external-data-to-set-shape-fill-colors-according-to-categorical-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/use-external-data-to-set-shape-fill-colors-according-to-categorical-values.cs) | `Diagram`, `Pages`, `Save` | Use external data to set shape fill colors according to categorical values |
| [validate-external-data-against-a-predefined-schema-before-mapping-to-diagram-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources/validate-external-data-against-a-predefined-schema-before-mapping-to-diagram-shapes.cs) | `Diagram`, `Save`, `diagram` | Validate external data against a predefined schema before mapping to diagram shapes |

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
