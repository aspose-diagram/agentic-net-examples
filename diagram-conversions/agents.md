---
category: diagram-conversions
display_name: Diagram Conversions
language: csharp
framework: net8.0
package: Aspose.Diagram
version: 26.7.0
examples: 96
pass_rate: 100.0
generated: 2026-07-28
parent: https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md
---

# Diagram Conversions

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API — **Diagram Conversions** category.

## Statistics

| Metric | Value |
|--------|-------|
| Examples | 96 |
| Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.7.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-07-28 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You are working in the **Diagram Conversions** category.
Your task is to write clean, compilable C# console examples that demonstrate Aspose.Diagram API usage for diagram conversions operations.
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
| `Aspose.Diagram` | 96 | Core diagram API |
| `System` | 96 | Console, Math, DateTime, Exception |
| `Aspose.Diagram.Saving` | 91 | Save options (PDF, PNG, HTML, SVG, XPS) |
| `System.IO` | 73 | File, Stream, Path, Directory operations |
| `System.Collections.Generic` | 19 | List, Dictionary, HashSet |
| `System.Text` | 5 | StringBuilder |
| `System.Threading` | 5 | Supporting utilities |
| `System.Text.Json` | 4 | JSON serialization |
| `System.Text.RegularExpressions` | 4 | Supporting utilities |
| `System.Threading.Tasks` | 3 | Supporting utilities |
| `Aspose.Diagram.Properties` | 2 | Supporting utilities |
| `System.IO.Compression` | 2 | Supporting utilities |
| `System.Xml` | 2 | Supporting utilities |
| `System.Linq` | 2 | LINQ queries on collections |
| `System.Net` | 2 | Supporting utilities |
| `System.Data` | 2 | Supporting utilities |
| `System.Data.SqlClient` | 2 | Supporting utilities |
| `System.Diagnostics` | 2 | Supporting utilities |
| `System.Net.Http` | 1 | Supporting utilities |
| `System.Net.Http.Headers` | 1 | Supporting utilities |
| `System.Net.Mail` | 1 | Supporting utilities |
| `System.Xml.Schema` | 1 | Supporting utilities |
| `System.Collections.Concurrent` | 1 | Supporting utilities |

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
| [add-configuration-to-enable-or-disable-progress-logging-via-appsettings-json-without-code-changes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/add-configuration-to-enable-or-disable-progress-logging-via-appsettings-json-without-code-changes.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Add configuration to enable or disable progress logging via appsettings json without code changes |
| [add-support-for-custom-page-naming-by-modifying-page-titles-during-the-onpagesaving-callback.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/add-support-for-custom-page-naming-by-modifying-page-titles-during-the-onpagesaving-callback.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Add support for custom page naming by modifying page titles during the onpagesaving callback |
| [apply-a-filter-to-exclude-hidden-layers-from-the-html-conversion-using-diagram-saveoptions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/apply-a-filter-to-exclude-hidden-layers-from-the-html-conversion-using-diagram-saveoptions.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Apply a filter to exclude hidden layers from the html conversion using diagram saveoptions |
| [apply-a-transformation-to-replace-all-absolute-file-paths-in-html-with-relative-paths-using-a-post-process-step.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/apply-a-transformation-to-replace-all-absolute-file-paths-in-html-with-relative-paths-using-a-post-process-step.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Apply a transformation to replace all absolute file paths in html with relative paths using a post process step |
| [assign-the-custom-ipagesavingcallback-instance-to-diagramsavingoptions-before-invoking-save.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/assign-the-custom-ipagesavingcallback-instance-to-diagramsavingoptions-before-invoking-save.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Assign the custom ipagesavingcallback instance to diagramsavingoptions before invoking save |
| [combine-page-progress-callbacks-with-image-extraction-to-save-each-page-as-separate-png-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/combine-page-progress-callbacks-with-image-extraction-to-save-each-page-as-separate-png-files.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Combine page progress callbacks with image extraction to save each page as separate png files |
| [configure-diagramsavingoptions-to-embed-metadata-indicating-conversion-timestamp-and-source-file-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-diagramsavingoptions-to-embed-metadata-indicating-conversion-timestamp-and-source-file-name.cs) | `Diagram`, `Save`, `diagram` | Configure diagramsavingoptions to embed metadata indicating conversion timestamp and source file name |
| [configure-htmlsaveoptions-to-embed-css-styles-directly-within-the-generated-html-output.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-htmlsaveoptions-to-embed-css-styles-directly-within-the-generated-html-output.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Configure htmlsaveoptions to embed css styles directly within the generated html output |
| [configure-htmlsaveoptions-to-generate-separate-html-files-for-each-page-of-a-multi-page-visio-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-htmlsaveoptions-to-generate-separate-html-files-for-each-page-of-a-multi-page-visio-diagram.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Configure htmlsaveoptions to generate separate html files for each page of a multi page visio diagram |
| [configure-htmlsaveoptions-to-limit-the-maximum-image-dimensions-in-the-generated-html.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-htmlsaveoptions-to-limit-the-maximum-image-dimensions-in-the-generated-html.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Configure htmlsaveoptions to limit the maximum image dimensions in the generated html |
| [configure-htmlsaveoptions-to-set-a-custom-css-class-prefix-for-all-generated-html-elements.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-htmlsaveoptions-to-set-a-custom-css-class-prefix-for-all-generated-html-elements.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Configure htmlsaveoptions to set a custom css class prefix for all generated html elements |
| [configure-htmlsaveoptions-to-set-a-custom-image-format-such-as-png-for-all-exported-diagram-images.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-htmlsaveoptions-to-set-a-custom-image-format-such-as-png-for-all-exported-diagram-images.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Configure htmlsaveoptions to set a custom image format such as png for all exported diagram images |
| [configure-htmlsaveoptions-to-set-a-custom-page-title-derived-from-the-visio-diagram-s-document-properties.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/configure-htmlsaveoptions-to-set-a-custom-page-title-derived-from-the-visio-diagram-s-document-properties.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Configure htmlsaveoptions to set a custom page title derived from the visio diagram s document properties |
| [convert-a-diagram-to-html-and-then-compress-the-entire-output-folder-into-a-zip-archive-programmatically.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-compress-the-entire-output-folder-into-a-zip-archive-programmatically.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Convert a diagram to html and then compress the entire output folder into a zip archive programmatically |
| [convert-a-diagram-to-html-and-then-embed-the-html-into-a-sharepoint-page-via-rest-api.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-embed-the-html-into-a-sharepoint-page-via-rest-api.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a diagram to html and then embed the html into a sharepoint page via rest api |
| [convert-a-diagram-to-html-and-then-embed-the-resulting-html-into-an-email-body-using-mime-multipart-format.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-embed-the-resulting-html-into-an-email-body-using-mime-multipart-format.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Convert a diagram to html and then embed the resulting html into an email body using mime multipart format |
| [convert-a-diagram-to-html-and-then-generate-a-pdf-snapshot-of-the-html-using-a-headless-browser-library.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-generate-a-pdf-snapshot-of-the-html-using-a-headless-browser-library.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a diagram to html and then generate a pdf snapshot of the html using a headless browser library |
| [convert-a-diagram-to-html-and-then-generate-a-sitemap-xml-file-listing-all-generated-html-pages-for-crawling.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-generate-a-sitemap-xml-file-listing-all-generated-html-pages-for-crawling.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Convert a diagram to html and then generate a sitemap xml file listing all generated html pages for crawling |
| [convert-a-diagram-to-html-and-then-programmatically-replace-placeholder-urls-with-cdn-links-using-string-manipulation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-programmatically-replace-placeholder-urls-with-cdn-links-using-string-manipulation.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a diagram to html and then programmatically replace placeholder urls with cdn links using string manipulation |
| [convert-a-diagram-to-html-and-then-validate-the-resulting-markup-against-an-html5-schema.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-diagram-to-html-and-then-validate-the-resulting-markup-against-an-html5-schema.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a diagram to html and then validate the resulting markup against an html5 schema |
| [convert-a-visio-diagram-to-html-and-embed-svg-representations-of-shapes-instead-of-raster-images.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-visio-diagram-to-html-and-embed-svg-representations-of-shapes-instead-of-raster-images.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a visio diagram to html and embed svg representations of shapes instead of raster images |
| [convert-a-visio-diagram-to-html-while-compressing-embedded-images-using-a-jpeg-encoder.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-visio-diagram-to-html-while-compressing-embedded-images-using-a-jpeg-encoder.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a visio diagram to html while compressing embedded images using a jpeg encoder |
| [convert-a-visio-diagram-to-html-while-preserving-original-page-layout-by-setting-appropriate-htmlsaveoptions-flags.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-a-visio-diagram-to-html-while-preserving-original-page-layout-by-setting-appropriate-htmlsaveoptions-flags.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert a visio diagram to html while preserving original page layout by setting appropriate htmlsaveoptions flags |
| [convert-diagrams-in-parallel-threads-each-with-its-own-istreamprovider-to-improve-batch-processing-throughput.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/convert-diagrams-in-parallel-threads-each-with-its-own-istreamprovider-to-improve-batch-processing-throughput.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Convert diagrams in parallel threads each with its own istreamprovider to improve batch processing throughput |
| [create-a-batch-process-converting-multiple-visio-files-to-html-using-a-shared-istreamprovider-instance.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-batch-process-converting-multiple-visio-files-to-html-using-a-shared-istreamprovider-instance.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Create a batch process converting multiple visio files to html using a shared istreamprovider instance |
| [create-a-batch-processing-loop-that-loads-multiple-visio-files-and-tracks-progress-for-each-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-batch-processing-loop-that-loads-multiple-visio-files-and-tracks-progress-for-each-conversion.cs) | `Diagram`, `Save`, `diagram` | Create a batch processing loop that loads multiple visio files and tracks progress for each conversion |
| [create-a-console-app-that-accepts-a-directory-path-and-converts-all-visio-files-to-html-using-streams.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-console-app-that-accepts-a-directory-path-and-converts-all-visio-files-to-html-using-streams.cs) | `Diagram`, `Save`, `diagram` | Create a console app that accepts a directory path and converts all visio files to html using streams |
| [create-a-diagnostic-report-listing-all-resources-created-by-istreamprovider-during-a-conversion-session.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-diagnostic-report-listing-all-resources-created-by-istreamprovider-during-a-conversion-session.cs) | `AddShape`, `Diagram`, `HTMLSaveOptions` | Create a diagnostic report listing all resources created by istreamprovider during a conversion session |
| [create-a-powershell-script-that-invokes-a-net-assembly-to-perform-html-conversion-with-a-custom-stream-provider.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-powershell-script-that-invokes-a-net-assembly-to-perform-html-conversion-with-a-custom-stream-provider.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Create a powershell script that invokes a net assembly to perform html conversion with a custom stream provider |
| [create-a-unit-test-verifying-onpagesaving-is-called-for-each-page-in-a-multi-page-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-unit-test-verifying-onpagesaving-is-called-for-each-page-in-a-multi-page-diagram.cs) | `Diagram`, `Page`, `Pages` | Create a unit test verifying onpagesaving is called for each page in a multi page diagram |
| [create-a-wpf-application-that-visualizes-conversion-progress-using-animated-progress-circles-per-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-a-wpf-application-that-visualizes-conversion-progress-using-animated-progress-circles-per-page.cs) | `Diagram`, `ImageSaveOptions`, `Page` | Create a wpf application that visualizes conversion progress using animated progress circles per page |
| [create-an-event-driven-architecture-where-onpagesaved-triggers-downstream-processing-of-saved-page-files.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/create-an-event-driven-architecture-where-onpagesaved-triggers-downstream-processing-of-saved-page-files.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Create an event driven architecture where onpagesaved triggers downstream processing of saved page files |
| [customize-htmlsaveoptions-to-generate-a-single-html-file-with-base64-encoded-resources-embedded.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/customize-htmlsaveoptions-to-generate-a-single-html-file-with-base64-encoded-resources-embedded.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Customize htmlsaveoptions to generate a single html file with base64 encoded resources embedded |
| [develop-a-console-application-that-accepts-input-and-output-paths-and-displays-real-time-conversion-progress.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/develop-a-console-application-that-accepts-input-and-output-paths-and-displays-real-time-conversion-progress.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Develop a console application that accepts input and output paths and displays real time conversion progress |
| [develop-a-powershell-script-that-invokes-the-net-conversion-library-and-outputs-progress-messages-to-the-console.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/develop-a-powershell-script-that-invokes-the-net-conversion-library-and-outputs-progress-messages-to-the-console.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Develop a powershell script that invokes the net conversion library and outputs progress messages to the console |
| [dispose-of-all-streams-returned-by-istreamprovider-correctly-to-prevent-memory-leaks-during-batch-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/dispose-of-all-streams-returned-by-istreamprovider-correctly-to-prevent-memory-leaks-during-batch-conversion.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Dispose of all streams returned by istreamprovider correctly to prevent memory leaks during batch conversion |
| [dispose-the-diagram-object-after-conversion-completes-to-promptly-release-unmanaged-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/dispose-the-diagram-object-after-conversion-completes-to-promptly-release-unmanaged-resources.cs) | `Diagram`, `Save`, `diagram` | Dispose the diagram object after conversion completes to promptly release unmanaged resources |
| [expose-conversion-progress-through-a-rest-api-endpoint-by-invoking-the-callback-and-returning-json-status.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/expose-conversion-progress-through-a-rest-api-endpoint-by-invoking-the-callback-and-returning-json-status.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Expose conversion progress through a rest api endpoint by invoking the callback and returning json status |
| [extend-the-callback-to-capture-timestamps-for-each-page-save-and-calculate-average-processing-time.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/extend-the-callback-to-capture-timestamps-for-each-page-save-and-calculate-average-processing-time.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Extend the callback to capture timestamps for each page save and calculate average processing time |
| [extract-all-shape-ids-from-a-diagram-before-conversion-and-write-them-to-a-json-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/extract-all-shape-ids-from-a-diagram-before-conversion-and-write-them-to-a-json-file.cs) | `Diagram`, `Pages`, `Shapes` | Extract all shape ids from a diagram before conversion and write them to a json file |
| [generate-html-from-a-diagram-and-capture-warning-messages-using-the-diagram-get-warnings-collection.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/generate-html-from-a-diagram-and-capture-warning-messages-using-the-diagram-get-warnings-collection.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Generate html from a diagram and capture warning messages using the diagram get warnings collection |
| [generate-html-output-then-extract-all-hyperlink-urls-from-the-generated-markup-for-analysis.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/generate-html-output-then-extract-all-hyperlink-urls-from-the-generated-markup-for-analysis.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Generate html output then extract all hyperlink urls from the generated markup for analysis |
| [handle-exceptions-during-diagram-save-by-wrapping-the-call-in-try-catch-and-logging-errors.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/handle-exceptions-during-diagram-save-by-wrapping-the-call-in-try-catch-and-logging-errors.cs) | `Diagram`, `Save`, `diagram` | Handle exceptions during diagram save by wrapping the call in try catch and logging errors |
| [implement-a-cancellation-token-in-the-callback-to-abort-conversion-when-the-user-requests-stop.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-cancellation-token-in-the-callback-to-abort-conversion-when-the-user-requests-stop.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Implement a cancellation token in the callback to abort conversion when the user requests stop |
| [implement-a-custom-class-inheriting-ipagesavingcallback-to-receive-page-level-conversion-notifications.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-custom-class-inheriting-ipagesavingcallback-to-receive-page-level-conversion-notifications.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Implement a custom class inheriting ipagesavingcallback to receive page level conversion notifications |
| [implement-a-custom-ipagesavingcallback-that-throttles-conversion-speed-to-limit-cpu-usage-on-low-end-devices.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-custom-ipagesavingcallback-that-throttles-conversion-speed-to-limit-cpu-usage-on-low-end-devices.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Implement a custom ipagesavingcallback that throttles conversion speed to limit cpu usage on low end devices |
| [implement-a-custom-istreamprovider-that-caches-streams-in-memory-to-avoid-repeated-disk-writes-for-identical-resources.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-custom-istreamprovider-that-caches-streams-in-memory-to-avoid-repeated-disk-writes-for-identical-resources.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a custom istreamprovider that caches streams in memory to avoid repeated disk writes for identical resources |
| [implement-a-custom-istreamprovider-that-returns-a-read-only-stream-for-existing-resources-to-avoid-duplication.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-custom-istreamprovider-that-returns-a-read-only-stream-for-existing-resources-to-avoid-duplication.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a custom istreamprovider that returns a read only stream for existing resources to avoid duplication |
| [implement-a-custom-istreamprovider-that-streams-resources-directly-to-an-http-response-for-web-api-delivery.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-custom-istreamprovider-that-streams-resources-directly-to-an-http-response-for-web-api-delivery.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a custom istreamprovider that streams resources directly to an http response for web api delivery |
| [implement-a-custom-istreamprovider-that-writes-image-resources-to-a-memory-stream-for-later-processing.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-custom-istreamprovider-that-writes-image-resources-to-a-memory-stream-for-later-processing.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a custom istreamprovider that writes image resources to a memory stream for later processing |
| [implement-a-logging-mechanism-inside-istreamprovider-getstream-to-record-each-resource-creation-event.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-logging-mechanism-inside-istreamprovider-getstream-to-record-each-resource-creation-event.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a logging mechanism inside istreamprovider getstream to record each resource creation event |
| [implement-a-progress-reporter-updating-a-console-progress-bar-each-time-istreamprovider-getstream-is-called.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-progress-reporter-updating-a-console-progress-bar-each-time-istreamprovider-getstream-is-called.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a progress reporter updating a console progress bar each time istreamprovider getstream is called |
| [implement-a-retry-mechanism-for-transient-i-o-errors-when-writing-resource-streams-in-istreamprovider.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-retry-mechanism-for-transient-i-o-errors-when-writing-resource-streams-in-istreamprovider.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a retry mechanism for transient i o errors when writing resource streams in istreamprovider |
| [implement-a-retry-mechanism-that-re-attempts-saving-a-page-if-onpagesaved-reports-failure.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-retry-mechanism-that-re-attempts-saving-a-page-if-onpagesaved-reports-failure.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Implement a retry mechanism that re attempts saving a page if onpagesaved reports failure |
| [implement-a-unit-test-verifying-istreamprovider-receives-correct-resource-type-identifiers-for-images-and-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-unit-test-verifying-istreamprovider-receives-correct-resource-type-identifiers-for-images-and-shapes.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement a unit test verifying istreamprovider receives correct resource type identifiers for images and shapes |
| [implement-a-unit-test-verifying-the-custom-istreamprovider-receives-correct-resource-names-during-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-a-unit-test-verifying-the-custom-istreamprovider-receives-correct-resource-names-during-conversion.cs) | `AddShape`, `Diagram`, `HTMLSaveOptions` | Implement a unit test verifying the custom istreamprovider receives correct resource names during conversion |
| [implement-error-handling-for-missing-resource-streams-when-istreamprovider-returns-null-for-a-requested-name.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-error-handling-for-missing-resource-streams-when-istreamprovider-returns-null-for-a-requested-name.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement error handling for missing resource streams when istreamprovider returns null for a requested name |
| [implement-istreamprovider-getstream-to-route-shape-resources-to-separate-subfolders-based-on-shape-type.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-istreamprovider-getstream-to-route-shape-resources-to-separate-subfolders-based-on-shape-type.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement istreamprovider getstream to route shape resources to separate subfolders based on shape type |
| [implement-istreamprovider-to-log-the-size-of-each-resource-stream-after-it-is-closed-for-auditing-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-istreamprovider-to-log-the-size-of-each-resource-stream-after-it-is-closed-for-auditing-purposes.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement istreamprovider to log the size of each resource stream after it is closed for auditing purposes |
| [implement-istreamprovider-to-store-resources-in-a-compressed-gzip-stream-before-writing-to-disk.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-istreamprovider-to-store-resources-in-a-compressed-gzip-stream-before-writing-to-disk.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement istreamprovider to store resources in a compressed gzip stream before writing to disk |
| [implement-istreamprovider-to-store-resources-in-azure-blob-storage-and-reference-them-via-public-urls.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-istreamprovider-to-store-resources-in-azure-blob-storage-and-reference-them-via-public-urls.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement istreamprovider to store resources in azure blob storage and reference them via public urls |
| [implement-istreamprovider-to-store-shape-resources-in-a-database-blob-field-instead-of-file-system.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-istreamprovider-to-store-shape-resources-in-a-database-blob-field-instead-of-file-system.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement istreamprovider to store shape resources in a database blob field instead of file system |
| [implement-istreamprovider-to-write-resource-streams-to-a-temporary-folder-that-is-automatically-cleaned-up-after-convers.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-istreamprovider-to-write-resource-streams-to-a-temporary-folder-that-is-automatically-cleaned-up-after-convers.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Implement istreamprovider to write resource streams to a temporary folder that is automatically cleaned up after convers |
| [implement-localization-in-progress-messages-to-support-multiple-languages-during-conversion-ui-display.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/implement-localization-in-progress-messages-to-support-multiple-languages-during-conversion-ui-display.cs) | `Diagram`, `ImageSaveOptions`, `Save` | Implement localization in progress messages to support multiple languages during conversion ui display |
| [integrate-page-progress-messages-into-a-windows-forms-progress-bar-to-reflect-conversion-status.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/integrate-page-progress-messages-into-a-windows-forms-progress-bar-to-reflect-conversion-status.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Integrate page progress messages into a windows forms progress bar to reflect conversion status |
| [load-a-visio-diagram-from-a-file-path-and-initialize-a-diagram-object.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/load-a-visio-diagram-from-a-file-path-and-initialize-a-diagram-object.cs) | `Diagram` | Load a visio diagram from a file path and initialize a diagram object |
| [load-a-visio-diagram-from-a-file-path-and-validate-its-structure-before-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/load-a-visio-diagram-from-a-file-path-and-validate-its-structure-before-conversion.cs) | `Diagram`, `Pages`, `diagram` | Load a visio diagram from a file path and validate its structure before conversion |
| [log-page-index-and-total-page-count-to-a-database-table-for-audit-trail-purposes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/log-page-index-and-total-page-count-to-a-database-table-for-audit-trail-purposes.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Log page index and total page count to a database table for audit trail purposes |
| [measure-performance-of-html-conversion-with-and-without-a-custom-istreamprovider-to-compare-speed.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/measure-performance-of-html-conversion-with-and-without-a-custom-istreamprovider-to-compare-speed.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Measure performance of html conversion with and without a custom istreamprovider to compare speed |
| [measure-total-conversion-time-by-recording-timestamps-before-and-after-diagram-save-and-logging-duration.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/measure-total-conversion-time-by-recording-timestamps-before-and-after-diagram-save-and-logging-duration.cs) | `Diagram`, `Save`, `diagram` | Measure total conversion time by recording timestamps before and after diagram save and logging duration |
| [mock-ipagesavingcallback-in-integration-tests-to-simulate-progress-events-without-performing-actual-file-saves.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/mock-ipagesavingcallback-in-integration-tests-to-simulate-progress-events-without-performing-actual-file-saves.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Mock ipagesavingcallback in integration tests to simulate progress events without performing actual file saves |
| [override-onpagesaved-to-log-completion-of-each-page-save-and-total-pages-processed.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/override-onpagesaved-to-log-completion-of-each-page-save-and-total-pages-processed.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Override onpagesaved to log completion of each page save and total pages processed |
| [override-onpagesaving-to-log-the-start-of-each-page-save-operation-with-its-index.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/override-onpagesaving-to-log-the-start-of-each-page-save-operation-with-its-index.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Override onpagesaving to log the start of each page save operation with its index |
| [profile-memory-usage-during-large-diagram-conversion-to-ensure-callback-implementation-does-not-cause-leaks.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/profile-memory-usage-during-large-diagram-conversion-to-ensure-callback-implementation-does-not-cause-leaks.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Profile memory usage during large diagram conversion to ensure callback implementation does not cause leaks |
| [save-html-conversion-output-to-a-network-share-by-providing-a-stream-that-writes-to-a-unc-path.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/save-html-conversion-output-to-a-network-share-by-providing-a-stream-that-writes-to-a-unc-path.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Save html conversion output to a network share by providing a stream that writes to a unc path |
| [save-the-loaded-diagram-to-pdf-format-while-receiving-progress-callbacks-for-each-page.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/save-the-loaded-diagram-to-pdf-format-while-receiving-progress-callbacks-for-each-page.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Save the loaded diagram to pdf format while receiving progress callbacks for each page |
| [serialize-progress-callback-data-to-json-and-write-to-a-cloud-storage-bucket-for-remote-monitoring.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/serialize-progress-callback-data-to-json-and-write-to-a-cloud-storage-bucket-for-remote-monitoring.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Serialize progress callback data to json and write to a cloud storage bucket for remote monitoring |
| [use-a-custom-istreamprovider-that-prefixes-each-resource-file-name-with-a-timestamp-to-ensure-uniqueness.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-a-custom-istreamprovider-that-prefixes-each-resource-file-name-with-a-timestamp-to-ensure-uniqueness.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use a custom istreamprovider that prefixes each resource file name with a timestamp to ensure uniqueness |
| [use-a-custom-istreamprovider-that-writes-resource-streams-to-a-networked-file-system-with-retry-logic.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-a-custom-istreamprovider-that-writes-resource-streams-to-a-networked-file-system-with-retry-logic.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use a custom istreamprovider that writes resource streams to a networked file system with retry logic |
| [use-a-custom-istreamprovider-to-encrypt-image-streams-before-writing-them-to-disk.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-a-custom-istreamprovider-to-encrypt-image-streams-before-writing-them-to-disk.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use a custom istreamprovider to encrypt image streams before writing them to disk |
| [use-a-custom-istreamprovider-to-generate-thumbnail-images-for-each-page-and-embed-them-in-the-html-index.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-a-custom-istreamprovider-to-generate-thumbnail-images-for-each-page-and-embed-them-in-the-html-index.cs) | `Diagram`, `HTMLSaveOptions`, `ImageSaveOptions` | Use a custom istreamprovider to generate thumbnail images for each page and embed them in the html index |
| [use-dependency-injection-to-provide-the-ipagesavingcallback-implementation-to-the-conversion-service.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-dependency-injection-to-provide-the-ipagesavingcallback-implementation-to-the-conversion-service.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Use dependency injection to provide the ipagesavingcallback implementation to the conversion service |
| [use-diagram-load-with-a-stream-containing-a-visio-file-encrypted-with-a-password-and-then-convert.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-diagram-load-with-a-stream-containing-a-visio-file-encrypted-with-a-password-and-then-convert.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Use diagram load with a stream containing a visio file encrypted with a password and then convert |
| [use-diagram-loadoptions-to-specify-password-for-encrypted-visio-files-before-tracking-conversion-progress.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-diagram-loadoptions-to-specify-password-for-encrypted-visio-files-before-tracking-conversion-progress.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Use diagram loadoptions to specify password for encrypted visio files before tracking conversion progress |
| [use-diagram-save-with-a-filestream-and-htmlsaveoptions-to-write-html-directly-to-a-pre-created-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-diagram-save-with-a-filestream-and-htmlsaveoptions-to-write-html-directly-to-a-pre-created-file.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use diagram save with a filestream and htmlsaveoptions to write html directly to a pre created file |
| [use-diagram-save-with-a-memorystream-and-htmlsaveoptions-to-generate-html-without-touching-the-file-system.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-diagram-save-with-a-memorystream-and-htmlsaveoptions-to-generate-html-without-touching-the-file-system.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use diagram save with a memorystream and htmlsaveoptions to generate html without touching the file system |
| [use-htmlsaveoptions-to-control-whether-shape-tooltips-are-included-in-the-generated-html-output.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-htmlsaveoptions-to-control-whether-shape-tooltips-are-included-in-the-generated-html-output.cs) | `Diagram`, `HTMLSaveOptions`, `Pages` | Use htmlsaveoptions to control whether shape tooltips are included in the generated html output |
| [use-htmlsaveoptions-to-disable-generation-of-external-css-files-and-produce-inline-style-definitions.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-htmlsaveoptions-to-disable-generation-of-external-css-files-and-produce-inline-style-definitions.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use htmlsaveoptions to disable generation of external css files and produce inline style definitions |
| [use-htmlsaveoptions-to-enable-or-disable-inclusion-of-comments-from-the-original-visio-file-in-html-output.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-htmlsaveoptions-to-enable-or-disable-inclusion-of-comments-from-the-original-visio-file-in-html-output.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use htmlsaveoptions to enable or disable inclusion of comments from the original visio file in html output |
| [use-htmlsaveoptions-to-set-a-custom-base-url-for-all-linked-resources-in-the-html-file.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-htmlsaveoptions-to-set-a-custom-base-url-for-all-linked-resources-in-the-html-file.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Use htmlsaveoptions to set a custom base url for all linked resources in the html file |
| [use-parallel-foreach-to-convert-visio-diagrams-concurrently-while-ensuring-thread-safe-progress-logging.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/use-parallel-foreach-to-convert-visio-diagrams-concurrently-while-ensuring-thread-safe-progress-logging.cs) | `Diagram`, `Save`, `diagram` | Use parallel foreach to convert visio diagrams concurrently while ensuring thread safe progress logging |
| [validate-all-external-resource-references-in-generated-html-point-to-existing-files-after-conversion.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/validate-all-external-resource-references-in-generated-html-point-to-existing-files-after-conversion.cs) | `Diagram`, `HTMLSaveOptions`, `Save` | Validate all external resource references in generated html point to existing files after conversion |
| [validate-no-duplicate-resource-names-are-produced-when-converting-diagrams-containing-identical-shapes.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/validate-no-duplicate-resource-names-are-produced-when-converting-diagrams-containing-identical-shapes.cs) | `Diagram`, `Pages`, `Shapes` | Validate no duplicate resource names are produced when converting diagrams containing identical shapes |
| [validate-that-total-page-count-reported-in-callbacks-matches-diagram-pages-collection-size-after-loading.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/validate-that-total-page-count-reported-in-callbacks-matches-diagram-pages-collection-size-after-loading.cs) | `Diagram`, `Pages`, `PdfSaveOptions` | Validate that total page count reported in callbacks matches diagram pages collection size after loading |
| [validate-the-number-of-generated-image-streams-matches-the-number-of-pages-in-the-source-diagram.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/validate-the-number-of-generated-image-streams-matches-the-number-of-pages-in-the-source-diagram.cs) | `Diagram`, `ImageSaveOptions`, `Pages` | Validate the number of generated image streams matches the number of pages in the source diagram |
| [write-page-start-and-end-notifications-to-a-text-file-using-a-custom-logger-implementation.cs](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions/write-page-start-and-end-notifications-to-a-text-file-using-a-custom-logger-implementation.cs) | `Diagram`, `PdfSaveOptions`, `Save` | Write page start and end notifications to a text file using a custom logger implementation |

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
- `HTMLSaveOptions`
- `ImageSaveOptions`
- `Page`
- `Pages`
- `PdfSaveOptions`
- `Save`
- `Shapes`
- `diagram`
- `page`

## Real-World Use Cases

Common scenarios where **Aspose.Diagram for .NET** diagram conversions capabilities are applied in production applications:

- Converting Visio diagrams to PDF for distribution and printing
- Exporting diagrams to PNG/SVG for embedding in web applications
- Batch-converting legacy VSD files to modern VSDX format

## Developer Q&A

Frequently asked questions about **Diagram Conversions** in **Aspose.Diagram for .NET**:

**Q: How do I get started with Diagram Conversions in Aspose.Diagram for .NET?**

A: Add a reference to `Aspose.Diagram.dll` (v26.7.0), include `using Aspose.Diagram;` and `using Aspose.Diagram.Saving;`, then use `Diagram diagram = new Diagram("input.vsdx");` to load a file. All examples in this category target `net8.0`.

**Q: Why do I get CS1674 when using `using (Diagram diagram = ...)`?**

A: `Diagram` does not implement `IDisposable` in Aspose.Diagram for .NET. Remove the `using` statement and declare the variable normally: `Diagram diagram = new Diagram("input.vsdx");`

**Q: Why does my code fail with CS0117 on `SaveFileFormat.VSDX`?**

A: `SaveFileFormat` enum members use PascalCase in Aspose.Diagram for .NET. Use `SaveFileFormat.Vsdx` (not `VSDX`). The same applies to all formats: `SaveFileFormat.Pdf`, `SaveFileFormat.Png`, `SaveFileFormat.Svg`.

**Q: Why do I get CS0029 when assigning `true` to a BOOL property?**

A: Aspose.Diagram uses its own `BOOL` enumeration type, not C# `bool`. Use `BOOL.True` and `BOOL.False` instead of plain `true`/`false` for all Aspose.Diagram BOOL properties.

**Q: How do I convert a Visio diagram to PDF?**

A: Use `PdfSaveOptions opts = new PdfSaveOptions(); diagram.Save("output.pdf", opts);` or the shorthand `diagram.Save("output.pdf", SaveFileFormat.Pdf);`

## Related Categories

- [Convert Visio Document](https://github.com/aspose-diagram/agentic-net-examples/tree/main/convert-visio-document) — format conversion operations
- [Working With Images](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images) — image embedding and extraction
- [Page Setup Features](https://github.com/aspose-diagram/agentic-net-examples/tree/main/page-setup-features) — page size, margins, and orientation

## Category Statistics

- Total examples: 96
- Failed: 0
- Pass rate: 100.0%

## Failed Tasks

All tasks passed ✅

---

Updated: 2026-07-28 | Examples: 96 | Pass Rate: 100.0% | [↑ Root agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/agents.md) | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/)
