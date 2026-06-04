# Aspose.Diagram for .NET — Agentic Examples

> AI-generated, compiler-validated C# examples for the [Aspose.Diagram for .NET](https://products.aspose.com/diagram/net/) API.

## Statistics

| Metric | Value |
|--------|-------|
| Total Examples | 191 |
| Categories | 4 |
| Overall Pass Rate | 100.0% |
| Aspose.Diagram Version | 26.5.0 |
| Target Framework | net8.0 |
| Last Updated | 2026-06-04 |

## Repository Structure

```
agents.md       ← AI agent instructions (root)
README.md       ← This file
index.json      ← Machine-readable catalogue
LICENSE         ← MIT licence
.github/
  workflows/
    validate-pr.yml  ← CI validation
basic-operations/
  agents.md    ← Category AI instructions
  index.json   ← Category catalogue
  *.cs         ← Example files
convert-visio-document/
  agents.md    ← Category AI instructions
  index.json   ← Category catalogue
  *.cs         ← Example files
diagram-conversions/
  agents.md    ← Category AI instructions
  index.json   ← Category catalogue
  *.cs         ← Example files
diagram-vba/
  agents.md    ← Category AI instructions
  index.json   ← Category catalogue
  *.cs         ← Example files
```

## Categories

| Category | Examples | Pass Rate | agents.md |
|----------|----------|-----------|-----------|
| [Basic Operations](https://github.com/aspose-diagram/agentic-net-examples/tree/main/basic-operations) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/basic-operations/agents.md) |
| [Convert Visio Document](https://github.com/aspose-diagram/agentic-net-examples/tree/main/convert-visio-document) | 30 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/convert-visio-document/agents.md) |
| [Diagram Conversions](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-conversions) | 96 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/diagram-conversions/agents.md) |
| [Diagram Vba](https://github.com/aspose-diagram/agentic-net-examples/tree/main/diagram-vba) | 35 | 100.0% | [agents.md](https://github.com/aspose-diagram/agentic-net-examples/blob/main/diagram-vba/agents.md) |

## How to Use

```bash
git clone https://github.com/aspose-diagram/agentic-net-examples.git
cd <category-folder>
# Copy any .cs file content into your project
# Ensure Aspose.Diagram.dll is referenced
dotnet build && dotnet run
```

## Prerequisites

- .NET SDK (net8.0 or later)
- [Aspose.Diagram for .NET 26.5.0](https://releases.aspose.com/diagram/net/)
- DLL referenced in your `.csproj`

## Agent Pipeline

| Attempt | Strategy | Trigger |
|---------|----------|---------|
| 1 | MCP direct retrieval + code assembly | Always |
| 2 | MCP + injected API correction rules | Attempt 1 fails |
| 3 | LLM repair with compiler errors + rules | Attempt 2 fails |

## Validation

Every PR is automatically validated by GitHub Actions:

- `dotnet build` — required, blocks merge on failure
- `dotnet run` — informational only

## Versioning

Each Aspose.Diagram release gets its own set of PRs validated against that version's DLL. Examples are tagged by version in `index.json`.

## Contributing

Examples are generated automatically by the [Aspose.Diagram Examples Generator Agent](https://github.com/aspose-diagram/agentic-net-examples). To contribute rules or fixes see `rules/rules.json` in the generator repo.

---

*Maintained by [agent-aspose-diagram-examples](https://github.com/agent-aspose-diagram-examples) · 2026-06-04*
