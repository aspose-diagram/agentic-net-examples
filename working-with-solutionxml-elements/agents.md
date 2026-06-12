---
category: working-with-solutionxml-elements
display_name: Working With Solutionxml Elements
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 34
pass_rate: 100.0
generated: 2026-06-12
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Solutionxml Elements

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Solutionxml Elements** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 34 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-12 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Solutionxml Elements** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with solutionxml elements operations.
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
| `System` | 34 | Console, Math, DateTime, Exception |
| `Aspose.Diagram` | 32 | Core diagram API |
| `System.IO` | 27 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 10 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Xml.Linq` | 7 | Supporting utilities |
| `System.Collections.Generic` | 7 | List, Dictionary, HashSet |
| `System.Xml` | 5 | Supporting utilities |
| `System.Linq` | 4 | LINQ queries on collections |
| `System.Text` | 4 | StringBuilder |
| `System.Text.Json` | 2 | JSON serialization |
| `System.Xml.Xsl` | 1 | Supporting utilities |
| `System.IO.Compression` | 1 | Supporting utilities |
| `System.Security.Cryptography` | 1 | Supporting utilities |
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

## Domain Knowledge

Category-specific API rules and gotchas:

- SolutionXML — SolutionXML is well-formed XML stored at the Document level directly in the Visio document. It provides a standardized way to persist solution-specific data.
- ADD SOLUTIONXML — Create a SolutionXML instance, set Name and XmlValue, then add to diagram.SolutionXMLs: SolutionXML solXML = new SolutionXML(); solXML.Name = "Solution XML"; solXML.XmlValue = "XML Value"; diagram.SolutionXMLs.Add(solXML);
- READ SOLUTIONXML — Iterate diagram.SolutionXMLs to read all SolutionXML elements: foreach (SolutionXML solutionXML in diagram.SolutionXMLs) { Console.WriteLine(solutionXML.Name); Console.WriteLine(solutionXML.XmlValue); }
- SolutionXML properties: Name (string) — the identifier name of the SolutionXML element; XmlValue (string) — the XML content stored in the element.
- diagram.SolutionXMLs is the collection property on the Diagram class that holds all SolutionXML elements. Use diagram.SolutionXMLs.Add(solXML) to add and foreach to iterate.
- To find a specific SolutionXML by name: foreach (SolutionXML s in diagram.SolutionXMLs) { if (s.Name == "targetName") { Console.WriteLine(s.XmlValue); } }
- To update an existing SolutionXML value: iterate diagram.SolutionXMLs, find by Name, and set XmlValue directly on the found instance.
- To remove a SolutionXML element: iterate diagram.SolutionXMLs, find the target by Name, then call diagram.SolutionXMLs.Remove(target).
- ALWAYS include using Aspose.Diagram; and using Aspose.Diagram.Saving; for all SolutionXML operations.
- SaveFileFormat enum MUST always use PascalCase: SaveFileFormat.Vsdx — NEVER SaveFileFormat.VSDX.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-custom-data-row-to-a-specific-shape-within-the-solutionxml-and-update-the-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/add-a-custom-data-row-to-a-specific-shape-within-the-solutionxml-and-update-the-file.cs) | `Diagram`, `Pages`, `Save` | Add a custom data row to a specific shape within the solutionxml and update the file |
| [add-a-hyperlink-element-to-a-shape-in-the-solutionxml-and-define-its-target-url.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/add-a-hyperlink-element-to-a-shape-in-the-solutionxml-and-define-its-target-url.cs) | `AddShape`, `Diagram`, `Pages` | Add a hyperlink element to a shape in the solutionxml and define its target url |
| [apply-a-conditional-formatting-rule-in-the-solutionxml-that-highlights-shapes-based-on-data-values.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/apply-a-conditional-formatting-rule-in-the-solutionxml-that-highlights-shapes-based-on-data-values.cs) | `Diagram`, `Save`, `SolutionXMLs` | Apply a conditional formatting rule in the solutionxml that highlights shapes based on data values |
| [apply-a-security-policy-that-removes-all-external-references-from-the-solutionxml-before-distribution.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/apply-a-security-policy-that-removes-all-external-references-from-the-solutionxml-before-distribution.cs) | `SolutionXMLs`, `diagram` | Apply a security policy that removes all external references from the solutionxml before distribution |
| [apply-a-transformation-xslt-to-the-solutionxml-to-generate-a-custom-shape-properties-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/apply-a-transformation-xslt-to-the-solutionxml-to-generate-a-custom-shape-properties-report.cs) | `Diagram`, `SolutionXMLs`, `diagram` | Apply a transformation xslt to the solutionxml to generate a custom shape properties report |
| [batch-process-a-folder-of-visio-files-updating-each-solutionxml-to-include-a-timestamp-attribute.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/batch-process-a-folder-of-visio-files-updating-each-solutionxml-to-include-a-timestamp-attribute.cs) | `Diagram`, `Save`, `SolutionXMLs` | Batch process a folder of visio files updating each solutionxml to include a timestamp attribute |
| [clone-an-existing-shape-node-in-the-solutionxml-and-reposition-the-duplicate-on-the-same-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/clone-an-existing-shape-node-in-the-solutionxml-and-reposition-the-duplicate-on-the-same-page.cs) | `Diagram`, `Save`, `SolutionXMLs` | Clone an existing shape node in the solutionxml and reposition the duplicate on the same page |
| [compare-two-solutionxml-files-to-identify-added-removed-or-modified-shapes-between-versions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/compare-two-solutionxml-files-to-identify-added-removed-or-modified-shapes-between-versions.cs) | `Diagram`, `Pages`, `Shapes` | Compare two solutionxml files to identify added removed or modified shapes between versions |
| [compress-the-solutionxml-content-using-gzip-before-embedding-it-into-a-vsdx-package.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/compress-the-solutionxml-content-using-gzip-before-embedding-it-into-a-vsdx-package.cs) | `Diagram`, `Save`, `SolutionXMLs` | Compress the solutionxml content using gzip before embedding it into a vsdx package |
| [create-a-custom-xml-namespace-mapping-for-solutionxml-to-avoid-naming-collisions-with-other-schemas.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/create-a-custom-xml-namespace-mapping-for-solutionxml-to-avoid-naming-collisions-with-other-schemas.cs) | `Save`, `SolutionXMLs`, `diagram` | Create a custom xml namespace mapping for solutionxml to avoid naming collisions with other schemas |
| [create-a-new-page-node-in-the-solutionxml-and-assign-a-background-style.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/create-a-new-page-node-in-the-solutionxml-and-assign-a-background-style.cs) | `AddShape`, `Diagram`, `Page` | Create a new page node in the solutionxml and assign a background style |
| [create-a-reusable-function-that-injects-a-predefined-style-sheet-into-any-solutionxml-document.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/create-a-reusable-function-that-injects-a-predefined-style-sheet-into-any-solutionxml-document.cs) | `SolutionXMLs`, `diagram` | Create a reusable function that injects a predefined style sheet into any solutionxml document |
| [decrypt-an-encrypted-solutionxml-segment-with-a-provided-key-and-verify-its-integrity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/decrypt-an-encrypted-solutionxml-segment-with-a-provided-key-and-verify-its-integrity.cs) | `Diagram`, `Save`, `SolutionXMLs` | Decrypt an encrypted solutionxml segment with a provided key and verify its integrity |
| [export-the-solutionxml-to-a-formatted-json-file-for-easier-consumption-by-web-services.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/export-the-solutionxml-to-a-formatted-json-file-for-easier-consumption-by-web-services.cs) | `Diagram`, `SolutionXMLs`, `diagram` | Export the solutionxml to a formatted json file for easier consumption by web services |
| [extract-all-custom-data-sections-from-the-solutionxml-and-export-them-to-a-csv-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/extract-all-custom-data-sections-from-the-solutionxml-and-export-them-to-a-csv-file.cs) | `Diagram`, `SolutionXMLs`, `diagram` | Extract all custom data sections from the solutionxml and export them to a csv file |
| [generate-a-summary-report-of-shape-counts-per-page-by-analyzing-the-solutionxml-hierarchy.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/generate-a-summary-report-of-shape-counts-per-page-by-analyzing-the-solutionxml-hierarchy.cs) | `Diagram`, `Pages`, `Shapes` | Generate a summary report of shape counts per page by analyzing the solutionxml hierarchy |
| [generate-a-thumbnail-image-from-the-solutionxml-by-rendering-the-first-page-at-low-resolution.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/generate-a-thumbnail-image-from-the-solutionxml-by-rendering-the-first-page-at-low-resolution.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Generate a thumbnail image from the solutionxml by rendering the first page at low resolution |
| [implement-a-version-control-system-that-stores-incremental-solutionxml-changes-as-separate-diff-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/implement-a-version-control-system-that-stores-incremental-solutionxml-changes-as-separate-diff-files.cs) | `Diagram`, `SolutionXMLs` | Implement a version control system that stores incremental solutionxml changes as separate diff files |
| [implement-error-handling-to-catch-malformed-solutionxml-nodes-and-log-detailed-diagnostics.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/implement-error-handling-to-catch-malformed-solutionxml-nodes-and-log-detailed-diagnostics.cs) | `Diagram`, `Save`, `SolutionXMLs` | Implement error handling to catch malformed solutionxml nodes and log detailed diagnostics |
| [import-a-json-representation-of-diagram-elements-and-merge-it-into-the-existing-solutionxml.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/import-a-json-representation-of-diagram-elements-and-merge-it-into-the-existing-solutionxml.cs) | `Diagram`, `Save`, `SolutionXMLs` | Import a json representation of diagram elements and merge it into the existing solutionxml |
| [integrate-a-third-party-library-to-validate-xml-namespaces-within-the-solutionxml-before-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/integrate-a-third-party-library-to-validate-xml-namespaces-within-the-solutionxml-before-saving.cs) | `Diagram`, `Save`, `SolutionXMLs` | Integrate a third party library to validate xml namespaces within the solutionxml before saving |
| [iterate-through-all-connectors-in-the-solutionxml-and-adjust-their-line-weight-uniformly.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/iterate-through-all-connectors-in-the-solutionxml-and-adjust-their-line-weight-uniformly.cs) | `Diagram`, `Pages`, `Save` | Iterate through all connectors in the solutionxml and adjust their line weight uniformly |
| [load-a-visio-file-and-obtain-its-solutionxml-representation-for-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/load-a-visio-file-and-obtain-its-solutionxml-representation-for-analysis.cs) | `Diagram`, `SolutionXMLs`, `diagram` | Load a visio file and obtain its solutionxml representation for analysis |
| [optimize-the-solutionxml-size-by-removing-redundant-style-definitions-and-consolidating-identical-elements.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/optimize-the-solutionxml-size-by-removing-redundant-style-definitions-and-consolidating-identical-elements.cs) | `Diagram`, `Save`, `SolutionXMLs` | Optimize the solutionxml size by removing redundant style definitions and consolidating identical elements |
| [parse-the-solutionxml-to-extract-each-shape-s-id-and-corresponding-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/parse-the-solutionxml-to-extract-each-shape-s-id-and-corresponding-name.cs) | `Diagram`, `Pages`, `Shapes` | Parse the solutionxml to extract each shape s id and corresponding name |
| [remove-all-comments-from-the-solutionxml-document-to-clean-up-metadata-before-saving.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/remove-all-comments-from-the-solutionxml-document-to-clean-up-metadata-before-saving.cs) | `SolutionXMLs`, `diagram` | Remove all comments from the solutionxml document to clean up metadata before saving |
| [remove-orphaned-shape-references-from-the-solutionxml-to-prevent-rendering-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/remove-orphaned-shape-references-from-the-solutionxml-to-prevent-rendering-errors.cs) | `Diagram`, `Pages`, `Save` | Remove orphaned shape references from the solutionxml to prevent rendering errors |
| [replace-all-instances-of-a-specific-font-name-in-the-solutionxml-with-a-new-font.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/replace-all-instances-of-a-specific-font-name-in-the-solutionxml-with-a-new-font.cs) | `Diagram`, `Save`, `SolutionXMLs` | Replace all instances of a specific font name in the solutionxml with a new font |
| [search-the-solutionxml-for-shapes-containing-a-specific-keyword-and-list-their-ids.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/search-the-solutionxml-for-shapes-containing-a-specific-keyword-and-list-their-ids.cs) | `Diagram`, `Pages`, `Save` | Search the solutionxml for shapes containing a specific keyword and list their ids |
| [serialize-the-modified-solutionxml-back-into-the-original-vsdx-package-while-preserving-all-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/serialize-the-modified-solutionxml-back-into-the-original-vsdx-package-while-preserving-all-resources.cs) | `Diagram`, `Save`, `SolutionXMLs` | Serialize the modified solutionxml back into the original vsdx package while preserving all resources |
| [set-a-custom-document-level-property-in-the-solutionxml-for-version-tracking.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/set-a-custom-document-level-property-in-the-solutionxml-for-version-tracking.cs) | `Save`, `SolutionXMLs`, `diagram` | Set a custom document level property in the solutionxml for version tracking |
| [update-page-dimensions-in-the-solutionxml-to-modify-the-diagram-canvas-size-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/update-page-dimensions-in-the-solutionxml-to-modify-the-diagram-canvas-size-programmatically.cs) | `Diagram`, `Pages`, `Save` | Update page dimensions in the solutionxml to modify the diagram canvas size programmatically |
| [use-linq-to-xml-for-efficiently-querying-shape-attributes-within-the-solutionxml.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/use-linq-to-xml-for-efficiently-querying-shape-attributes-within-the-solutionxml.cs) | `Diagram`, `Save`, `SolutionXMLs` | Use linq to xml for efficiently querying shape attributes within the solutionxml |
| [validate-the-solutionxml-against-the-visio-schema-to-ensure-structural-integrity.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements/validate-the-solutionxml-against-the-visio-schema-to-ensure-structural-integrity.cs) | `Diagram`, `SolutionXMLs`, `diagram` | Validate the solutionxml against the visio schema to ensure structural integrity |

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

*Auto-generated by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) · 2026-06-12*
