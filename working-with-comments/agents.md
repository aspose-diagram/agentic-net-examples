---
category: working-with-comments
display_name: Working With Comments
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.5.0
examples: 35
pass_rate: 100.0
generated: 2026-06-23
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Working With Comments

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Working With Comments** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 35 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-23 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Working With Comments** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for working with comments operations.
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
| `System.IO` | 22 | File, Stream, Path, Directory operations |
| `Aspose.Diagram.Saving` | 14 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.Collections.Generic` | 8 | List, Dictionary, HashSet |
| `System.Text` | 3 | StringBuilder |
| `System.Text.Json` | 1 | JSON serialization |
| `System.Data` | 1 | Supporting utilities |
| `System.Data.SqlClient` | 1 | Supporting utilities |
| `Aspose.Diagram.Properties` | 1 | Supporting utilities |
| `Aspose.Cells` | 1 | Supporting utilities |
| `System.Xml.Linq` | 1 | Supporting utilities |
| `Aspose.Diagram.Manipulation` | 1 | Supporting utilities |

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

- To add a page-level comment, use page.AddComment(double x, double y, string text) — this places the comment at specific coordinates.
- To add a shape-level comment, use the overloaded page.AddComment(Shape shape, string text) — this associates the comment directly with that shape.
- To edit existing comments, access them via page.PageSheet.Annotations — DO NOT look for a Comments collection directly on the Page object.
- ALWAYS use the .Value property when updating comment text (e.g., annotation.Comment.Value = "new text") — assigning directly to the Comment object will fail.
- When retrieving a page for comments, use diagram.Pages.GetPage("Page-1") or diagram.Pages[index] to ensure you have a valid Page instance.
- Remember that comments are 'annotations' in the Visio object model; use the Annotation class and AnnotationCollection for iteration.
- Correct Edit Example: foreach (Annotation annotation in page.PageSheet.Annotations) { annotation.Comment.Value = "Updated"; }
- Correct Add Example: page.AddComment(shapeInstance, "Review this shape");
- DO NOT attempt to access Annotations directly from the Page object — page.Annotations does not exist.
- ALWAYS access comments through the PageSheet: use page.PageSheet.Annotations.

## Examples

| File | Key APIs | Task |
|------|----------|------|
| [add-a-reply-to-an-existing-comment-thread-preserving-the-original-hierarchy-and-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/add-a-reply-to-an-existing-comment-thread-preserving-the-original-hierarchy-and-metadata.cs) | `Diagram`, `Pages`, `Save` | Add a reply to an existing comment thread preserving the original hierarchy and metadata |
| [add-timestamps-to-comments-automatically-when-they-are-created-using-the-system-s-current-time-zone.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/add-timestamps-to-comments-automatically-when-they-are-created-using-the-system-s-current-time-zone.cs) | `Diagram`, `Pages`, `Save` | Add timestamps to comments automatically when they are created using the system s current time zone |
| [apply-a-conditional-formatting-rule-that-changes-comment-background-color-based-on-author-role.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/apply-a-conditional-formatting-rule-that-changes-comment-background-color-based-on-author-role.cs) | `Diagram`, `Pages`, `Save` | Apply a conditional formatting rule that changes comment background color based on author role |
| [apply-a-custom-tag-to-comments-that-meet-certain-criteria-enabling-later-filtered-searches.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/apply-a-custom-tag-to-comments-that-meet-certain-criteria-enabling-later-filtered-searches.cs) | `Diagram`, `Pages`, `Save` | Apply a custom tag to comments that meet certain criteria enabling later filtered searches |
| [copy-comments-from-one-diagram-to-another-preserving-their-original-author-and-creation-dates.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/copy-comments-from-one-diagram-to-another-preserving-their-original-author-and-creation-dates.cs) | `Diagram`, `Page`, `page` | Copy comments from one diagram to another preserving their original author and creation dates |
| [create-a-custom-comment-style-with-specific-font-and-background-color-then-apply-it-to-selected-comments.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/create-a-custom-comment-style-with-specific-font-and-background-color-then-apply-it-to-selected-comments.cs) | `Diagram`, `Pages`, `Shapes` | Create a custom comment style with specific font and background color then apply it to selected comments |
| [create-a-macro-that-removes-all-comments-older-than-a-specified-number-of-days-from-a-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/create-a-macro-that-removes-all-comments-older-than-a-specified-number-of-days-from-a-diagram.cs) | `Diagram`, `Pages`, `Save` | Create a macro that removes all comments older than a specified number of days from a diagram |
| [create-a-report-that-groups-comments-by-shape-type-and-counts-occurrences-per-group.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/create-a-report-that-groups-comments-by-shape-type-and-counts-occurrences-per-group.cs) | `Diagram`, `Pages`, `Shapes` | Create a report that groups comments by shape type and counts occurrences per group |
| [create-a-utility-that-lists-comment-authors-alphabetically-and-outputs-the-list-to-a-text-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/create-a-utility-that-lists-comment-authors-alphabetically-and-outputs-the-list-to-a-text-file.cs) | `Diagram`, `Pages`, `diagram` | Create a utility that lists comment authors alphabetically and outputs the list to a text file |
| [delete-comments-containing-a-specific-keyword-and-verify-the-diagram-no-longer-displays-them.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/delete-comments-containing-a-specific-keyword-and-verify-the-diagram-no-longer-displays-them.cs) | `Diagram`, `Pages`, `Save` | Delete comments containing a specific keyword and verify the diagram no longer displays them |
| [detect-overlapping-comment-positions-and-automatically-adjust-their-coordinates-to-avoid-visual-clutter.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/detect-overlapping-comment-positions-and-automatically-adjust-their-coordinates-to-avoid-visual-clutter.cs) | `Diagram`, `Pages`, `Save` | Detect overlapping comment positions and automatically adjust their coordinates to avoid visual clutter |
| [develop-a-script-to-migrate-comments-from-legacy-diagram-files-to-the-latest-file-format-version.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/develop-a-script-to-migrate-comments-from-legacy-diagram-files-to-the-latest-file-format-version.cs) | `Diagram`, `Pages`, `Save` | Develop a script to migrate comments from legacy diagram files to the latest file format version |
| [export-all-diagram-comments-to-a-json-file-including-position-coordinates-and-linked-shape-identifiers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/export-all-diagram-comments-to-a-json-file-including-position-coordinates-and-linked-shape-identifiers.cs) | `Diagram`, `Pages`, `Shapes` | Export all diagram comments to a json file including position coordinates and linked shape identifiers |
| [export-comment-data-to-an-html-file-formatting-each-comment-as-a-collapsible-section-with-metadata.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/export-comment-data-to-an-html-file-formatting-each-comment-as-a-collapsible-section-with-metadata.cs) | `Diagram`, `Pages`, `diagram` | Export comment data to an html file formatting each comment as a collapsible section with metadata |
| [export-comment-metadata-to-a-relational-database-mapping-each-comment-to-its-associated-shape-id.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/export-comment-metadata-to-a-relational-database-mapping-each-comment-to-its-associated-shape-id.cs) | `Diagram`, `Pages`, `diagram` | Export comment metadata to a relational database mapping each comment to its associated shape id |
| [extract-comment-text-and-embed-it-as-hidden-metadata-within-the-diagram-file-for-later-retrieval.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/extract-comment-text-and-embed-it-as-hidden-metadata-within-the-diagram-file-for-later-retrieval.cs) | `Diagram`, `Pages`, `Save` | Extract comment text and embed it as hidden metadata within the diagram file for later retrieval |
| [filter-comments-by-creation-date-range-and-export-the-filtered-set-to-a-plain-text-log-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/filter-comments-by-creation-date-range-and-export-the-filtered-set-to-a-plain-text-log-file.cs) | `Diagram`, `Pages`, `diagram` | Filter comments by creation date range and export the filtered set to a plain text log file |
| [generate-a-pdf-snapshot-of-a-diagram-with-comments-rendered-as-callout-annotations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/generate-a-pdf-snapshot-of-a-diagram-with-comments-rendered-as-callout-annotations.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Generate a pdf snapshot of a diagram with comments rendered as callout annotations |
| [generate-a-summary-table-listing-comment-counts-per-page-and-save-it-as-an-excel-worksheet.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/generate-a-summary-table-listing-comment-counts-per-page-and-save-it-as-an-excel-worksheet.cs) | `Diagram`, `Pages`, `diagram` | Generate a summary table listing comment counts per page and save it as an excel worksheet |
| [implement-a-feature-to-lock-comments-preventing-further-edits-unless-a-specific-unlock-flag-is-set.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/implement-a-feature-to-lock-comments-preventing-further-edits-unless-a-specific-unlock-flag-is-set.cs) | `Diagram`, `Pages`, `Save` | Implement a feature to lock comments preventing further edits unless a specific unlock flag is set |
| [implement-batch-processing-to-add-a-standardized-disclaimer-comment-to-every-diagram-in-a-project-directory.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/implement-batch-processing-to-add-a-standardized-disclaimer-comment-to-every-diagram-in-a-project-directory.cs) | `Diagram`, `Pages`, `Save` | Implement batch processing to add a standardized disclaimer comment to every diagram in a project directory |
| [implement-error-handling-to-gracefully-skip-diagrams-that-lack-comment-support-during-batch-operations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/implement-error-handling-to-gracefully-skip-diagrams-that-lack-comment-support-during-batch-operations.cs) | `Diagram`, `Pages`, `Save` | Implement error handling to gracefully skip diagrams that lack comment support during batch operations |
| [import-comments-from-an-xml-document-assigning-them-to-corresponding-shapes-based-on-matching-ids.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/import-comments-from-an-xml-document-assigning-them-to-corresponding-shapes-based-on-matching-ids.cs) | `Diagram`, `Pages`, `Save` | Import comments from an xml document assigning them to corresponding shapes based on matching ids |
| [iterate-through-multiple-visio-files-in-a-folder-extracting-comment-timestamps-into-a-consolidated-report.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/iterate-through-multiple-visio-files-in-a-folder-extracting-comment-timestamps-into-a-consolidated-report.cs) | `Diagram`, `Pages`, `diagram` | Iterate through multiple visio files in a folder extracting comment timestamps into a consolidated report |
| [load-a-diagram-enumerate-comment-ids-and-use-them-to-retrieve-detailed-comment-objects-via-api.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/load-a-diagram-enumerate-comment-ids-and-use-them-to-retrieve-detailed-comment-objects-via-api.cs) | `Diagram`, `Pages`, `Shapes` | Load a diagram enumerate comment ids and use them to retrieve detailed comment objects via api |
| [load-a-visio-diagram-add-a-new-comment-to-a-specific-shape-then-save-the-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/load-a-visio-diagram-add-a-new-comment-to-a-specific-shape-then-save-the-file.cs) | `Diagram`, `Save`, `diagram` | Load a visio diagram add a new comment to a specific shape then save the file |
| [programmatically-attach-a-comment-to-a-connector-shape-specifying-the-exact-segment-where-it-appears.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/programmatically-attach-a-comment-to-a-connector-shape-specifying-the-exact-segment-where-it-appears.cs) | `AddShape`, `ConnectShapesViaConnector`, `Diagram` | Programmatically attach a comment to a connector shape specifying the exact segment where it appears |
| [read-comments-from-a-diagram-translate-their-text-using-an-external-service-and-update-them-in-place.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/read-comments-from-a-diagram-translate-their-text-using-an-external-service-and-update-them-in-place.cs) | `Diagram`, `Pages`, `Save` | Read comments from a diagram translate their text using an external service and update them in place |
| [retrieve-all-comments-from-a-diagram-and-export-their-text-and-author-information-to-a-csv-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/retrieve-all-comments-from-a-diagram-and-export-their-text-and-author-information-to-a-csv-file.cs) | `Diagram`, `Pages`, `diagram` | Retrieve all comments from a diagram and export their text and author information to a csv file |
| [search-for-comments-authored-by-a-particular-user-and-highlight-the-associated-shapes-in-the-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/search-for-comments-authored-by-a-particular-user-and-highlight-the-associated-shapes-in-the-diagram.cs) | `Diagram`, `Pages`, `Save` | Search for comments authored by a particular user and highlight the associated shapes in the diagram |
| [set-the-visibility-flag-of-all-comments-to-false-then-render-the-diagram-without-comment-overlays.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/set-the-visibility-flag-of-all-comments-to-false-then-render-the-diagram-without-comment-overlays.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Set the visibility flag of all comments to false then render the diagram without comment overlays |
| [synchronize-comment-visibility-settings-across-multiple-diagrams-to-ensure-consistent-presentation-during-stakeholder-re.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/synchronize-comment-visibility-settings-across-multiple-diagrams-to-ensure-consistent-presentation-during-stakeholder-re.cs) | `Diagram`, `Pages`, `Save` | Synchronize comment visibility settings across multiple diagrams to ensure consistent presentation during stakeholder re |
| [update-author-names-of-existing-comments-using-a-provided-mapping-dictionary-then-save-the-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/update-author-names-of-existing-comments-using-a-provided-mapping-dictionary-then-save-the-changes.cs) | `Diagram`, `Pages`, `Save` | Update author names of existing comments using a provided mapping dictionary then save the changes |
| [validate-that-comment-positions-stay-within-page-boundaries-after-diagram-scaling-operations.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/validate-that-comment-positions-stay-within-page-boundaries-after-diagram-scaling-operations.cs) | `Diagram`, `Pages`, `Save` | Validate that comment positions stay within page boundaries after diagram scaling operations |
| [validate-that-each-comment-contains-non-empty-text-and-report-any-violations-as-warnings.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments/validate-that-each-comment-contains-non-empty-text-and-report-any-violations-as-warnings.cs) | `Diagram`, `Pages`, `diagram` | Validate that each comment contains non empty text and report any violations as warnings |

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
- `Page`
- `Pages`
- `PdfSaveOptions`
- `Save`
- `Shapes`
- `diagram`
- `page`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** working with comments capabilities are applied in production applications:

- Adding review comments to diagrams in collaborative review workflows
- Extracting and reporting all comments from a set of Visio files
- Clearing all comments before final diagram distribution

## Developer Q&A

Frequently asked questions about **Working With Comments** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Working With Comments in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.5.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

## Related Categories

- [Document Properties](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties) — document metadata and properties
- [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) — diagram-level operations and structure

## Category Statistics

- Total examples: 35
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-06-23 | Examples: 35 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
