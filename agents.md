---
product: Aspose.Diagram for .NET
language: csharp
framework: net8.0
version: 26.7.0
total_examples: 1840
categories: 31
generated: 2026-08-03
---

# Aspose.Diagram for .NET — Agentic Examples

> AI-generated, compiler-validated C# examples for [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/).

## Agent Identity

| Field | Value |
|-------|-------|
| Agent | agent-aspose-diagram-examples |
| Repository | [aspose-diagram/agentic-net-examples](https://github.com/aspose-diagram/agentic-net-examples) |
| Product | Aspose.Diagram for .NET |
| Version | 26.7.0 |
| Framework | net8.0 |
| Total Examples | 1840 |
| Categories | 31 |
| Last Updated | 2026-08-03 |

## Persona

You are a C# developer specializing in Visio diagram processing using Aspose.Diagram for .NET. You write clean, compilable C# console examples that demonstrate real API usage. You never use placeholder comments — every line of code is functional and validated.

## Boundaries

### Always

- Use explicit types — never `var`
- Include all required `using` directives
- Use `SaveFileFormat` enum in PascalCase: `SaveFileFormat.Vsdx` not `SaveFileFormat.VSDX`
- Use `BOOL.True` / `BOOL.False` for Aspose BOOL properties
- Wrap all code in `static void Main()` inside `class Program`
- Use `page.Shapes.GetShape(id)` — never `GetShapeByID` or `GetShapeByName`

### Ask First

- Multi-file or multi-project solutions
- External NuGet packages beyond Aspose.Diagram
- UI frameworks (WinForms, WPF, Blazor)

### Never

- Use `var` for any variable
- Use `using (Diagram diagram = ...)` — Diagram is not IDisposable
- Use ALL_CAPS SaveFileFormat values (`VSDX`, `PDF`, `PNG`)
- Use `System.Windows.Forms` — not available in net8.0 console
- Use NUnit `Assert` — not available, use manual checks
- Generate PowerShell syntax inside C# files

## Domain Knowledge

Cross-cutting API rules that apply to all categories:

- `Diagram` constructor: `new Diagram(filePath)` or `new Diagram()` for empty
- `diagram.Save(path, SaveFileFormat.Vsdx)` — always PascalCase format enum
- `page.Shapes.GetShape(long id)` — returns Shape by ID
- `diagram.Pages.GetPage(string name)` — returns Page by name
- `shape.Text.Value.ToString()` — get plain text content
- `shape.TextBlock.VerticalAlign.Value` — text alignment (not `shape.TextBlock.Align`)
- `shape.Fill.FillForegnd.Value` — foreground color (not `shape.Fill.Color`)
- `shape.Line.LineColor.Value` — line color (not `shape.Line.Color`)
- `BOOL.True` / `BOOL.False` — never plain `true`/`false` for Aspose BOOL type
- `shape.ThreeDFormat.RotationType.Value` — read-only property, never assign

## Command Reference

```bash
# Build
cd compiler/CSharpRunner/_warmup && dotnet build

# Run
dotnet run

# Verbose build
dotnet build --verbosity detailed
```

## Testing Guide

| Result | Meaning |
|--------|---------|
| `rc=0` build + run | ✅ Full pass |
| `rc=0` build only | ✅ Build-only pass (no Main) |
| TIMEOUT | ✅ Pass (Console.ReadLine guard) |
| `rc=1` CS errors | ❌ Fail — fix API usage |
| `rc!=0` runtime | ❌ Fail — unhandled exception |

## Categories

| Category | Examples | Pass Rate | Details |
|----------|----------|-----------|---------|
| [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/basic-operations/agents.md) |
| [Convert Visio Document](https://github.com/aspose-diagram/agentic-net-examples/tree/main/convert-visio-document) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/convert-visio-document/agents.md) |
| [Diagram Conversions](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions) | 96 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/diagram-conversions/agents.md) |
| [Diagram Vba](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba) | 35 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/diagram-vba/agents.md) |
| [Document Properties](https://github.com/aspose-diagram/agentic-net-examples/tree/main/document-properties) | 34 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/document-properties/agents.md) |
| [Drawing](https://github.com/aspose-diagram/agentic-net-examples/tree/main/drawing) | 125 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/drawing/agents.md) |
| [Events Section In The Shapesheet](https://github.com/aspose-diagram/agentic-net-examples/tree/main/events-section-in-the-shapesheet) | 33 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/events-section-in-the-shapesheet/agents.md) |
| [Font Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/font-operations) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/font-operations/agents.md) |
| [Ole Objects In Visio Diagram](https://github.com/aspose-diagram/agentic-net-examples/tree/main/ole-objects-in-visio-diagram) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/ole-objects-in-visio-diagram/agents.md) |
| [Page Setup Features](https://github.com/aspose-diagram/agentic-net-examples/tree/main/page-setup-features) | 82 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/page-setup-features/agents.md) |
| [Visio Activex Controls](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-activex-controls) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/visio-activex-controls/agents.md) |
| [Visio Shape Gradient](https://github.com/aspose-diagram/agentic-net-examples/tree/main/visio-shape-gradient) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/visio-shape-gradient/agents.md) |
| [Working With Comments](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-comments) | 35 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-comments/agents.md) |
| [Working With Diagrams](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-diagrams) | 40 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-diagrams/agents.md) |
| [Working With External Data Sources](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-external-data-sources) | 29 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-external-data-sources/agents.md) |
| [Working With Fields](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-fields) | 33 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-fields/agents.md) |
| [Working With Geometry Section](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-geometry-section) | 36 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-geometry-section/agents.md) |
| [Working With Headers And Footers](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-headers-and-footers) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-headers-and-footers/agents.md) |
| [Working With Hyperlinks](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-hyperlinks) | 35 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-hyperlinks/agents.md) |
| [Working With Images](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-images) | 38 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-images/agents.md) |
| [Working With Layers](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-layers) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-layers/agents.md) |
| [Working With Masters](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-masters) | 29 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-masters/agents.md) |
| [Working With Pages](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-pages) | 168 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-pages/agents.md) |
| [Working With Protection](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-protection) | 35 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-protection/agents.md) |
| [Working With Shapes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-shapes) | 459 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-shapes/agents.md) |
| [Working With Solutionxml Elements](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-solutionxml-elements) | 34 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-solutionxml-elements/agents.md) |
| [Working With Text](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text) | 89 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-text/agents.md) |
| [Working With Text Boxes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-text-boxes) | 35 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-text-boxes/agents.md) |
| [Working With Themes](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-themes) | 40 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-themes/agents.md) |
| [Working With User Defined Cells](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-user-defined-cells) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-user-defined-cells/agents.md) |
| [Working With Window Elements](https://github.com/aspose-diagram/agentic-net-examples/tree/main/working-with-window-elements) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/working-with-window-elements/agents.md) |

## Pipeline

| Attempt | Strategy | Trigger |
|---------|----------|---------|
| 1 | MCP direct retrieval + code assembly | Always |
| 2 | MCP retrieval with injected rules | Attempt 1 fails |
| 3 | LLM repair with compiler errors + rules | Attempt 2 fails |

Only examples that pass both `dotnet build` and `dotnet run` are committed.

## Extension Points

- Add rules to `rules/rules.json` to correct known API patterns
- Add categories via the Task Generator API integration
- Trigger PR creation via the web UI **Create PR** button
- Use **Final PR** button to regenerate all agents.md files in one PR

## Agent Capabilities

### What This Agent Does

The Aspose.Diagram Examples Generator Agent produces compiler-validated C# console application examples for **Aspose.Diagram for .NET**. It accepts natural-language task descriptions and returns ready-to-run C# code that has passed both `dotnet build` and `dotnet run` validation.

### Accepted Inputs

- **Natural language task description** specifying the Aspose.Diagram operation
- **Category context** — used for output routing and rule selection
- **Batch task list** — plain text file with one task per line

### Produced Outputs

- **C# source file** (`.cs`) — passes `dotnet build` and `dotnet run`
- **`agents.md`** per category — AI-friendly instructions with Q&A and use cases
- **`README.md`** per category — SEO-optimized landing page
- **`index.json`** per category — machine-readable metadata
- **Root `agents.md`**, **`README.md`**, **`index.json`**, **`llms.txt`**, **`/.well-known/agent.json`**, **`openapi.json`** at repository root

### Supported Operations

Covers all 31 Aspose.Diagram for .NET task categories: basic operations, shapes, pages, connectors, text, themes, format conversion, VBA, layers, masters, headers/footers, hyperlinks, comments, protection, user-defined cells, window elements, external data, OLE objects, ActiveX controls, drawing, fonts, document properties, SolutionXML, and ShapeSheet events.

## Quick Reference

| Task | Category | Key API |
|------|----------|---------|
| Load VSDX | basic-operations | `new Diagram("input.vsdx")` |
| Save as PDF | diagram-conversions | `diagram.Save("out.pdf", new PdfSaveOptions())` |
| Save as PNG | diagram-conversions | `diagram.Save("out.png", new ImageSaveOptions(SaveFileFormat.Png))` |
| Add shape | working-with-shapes | `long id = diagram.AddShape(x, y, w, h, master, page)` |
| Connect shapes | working-with-diagrams | `page.ConnectShapesViaConnector(id1, place, id2, place, connId)` |
| Set shape text | working-with-text | `shape.Text.Value.Add(new Txt("text"))` |
| Apply theme | working-with-themes | `page.PresetTheme = PresetThemeValue.Bubble` |
| Add page | working-with-pages | `diagram.Pages.Add(new Page())` |
| Set fill color | working-with-shapes | `shape.Fill.FillForegnd.Value = "#FF0000"` |
| Read custom prop | working-with-user-defined-cells | `foreach (Prop p in shape.Props)` |

---

*Maintained by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) | Examples: 1840 | Categories: 31 | [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) | Updated: 2026-08-03*
