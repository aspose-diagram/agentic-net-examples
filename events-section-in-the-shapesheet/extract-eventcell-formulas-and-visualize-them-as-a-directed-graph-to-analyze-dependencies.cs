using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDotFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare DOT graph content
            StringBuilder dotBuilder = new StringBuilder();
            dotBuilder.AppendLine("digraph VisioEvents {");
            dotBuilder.AppendLine("    node [shape=box];");

            // Regex to find shape IDs referenced in formulas (simple heuristic)
            Regex shapeIdRegex = new Regex(@"\bShape\.(\d+)\b", RegexOptions.Compiled);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    string shapeNodeName = $"Shape_{shapeId}";
                    // Collect all event formulas for this shape
                    var eventFormulas = new System.Collections.Generic.List<string>();

                    // Helper to add formula if not empty
                    void AddFormula(string formula)
                    {
                        if (!string.IsNullOrEmpty(formula))
                        {
                            eventFormulas.Add(formula);
                        }
                    }

                    // Access each supported event cell
                    AddFormula(shape.Event.EventXFMod?.Ufe?.F);
                    AddFormula(shape.Event.EventDblClick?.Ufe?.F);
                    AddFormula(shape.Event.EventDrop?.Ufe?.F);
                    AddFormula(shape.Event.EventMultiDrop?.Ufe?.F);
                    AddFormula(shape.Event.TheText?.Ufe?.F);
                    AddFormula(shape.Event.TheData?.Ufe?.F);

                    // If there are no event formulas, skip node creation
                    if (eventFormulas.Count == 0)
                        continue;

                    // Create node with label containing shape ID and its formulas
                    dotBuilder.AppendLine($"    {shapeNodeName} [label=\"Shape {shapeId}\\n{string.Join("\\n", eventFormulas)}\"];");

                    // Parse formulas for referenced shape IDs and create edges
                    foreach (string formula in eventFormulas)
                    {
                        foreach (Match match in shapeIdRegex.Matches(formula))
                        {
                            if (long.TryParse(match.Groups[1].Value, out long targetId))
                            {
                                string targetNodeName = $"Shape_{targetId}";
                                dotBuilder.AppendLine($"    {shapeNodeName} -> {targetNodeName};");
                            }
                        }
                    }
                }
            }

            dotBuilder.AppendLine("}");

            // Write DOT content to output file
            File.WriteAllText(outputPath, dotBuilder.ToString());
            Console.WriteLine($"Dependency graph saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}