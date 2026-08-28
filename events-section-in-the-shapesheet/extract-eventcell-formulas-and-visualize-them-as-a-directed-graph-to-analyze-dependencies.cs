using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Aspose.Diagram;

class EventDependencyGraphGenerator
{
    // Entry point
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: EventDependencyGraphGenerator <inputVisioFile> <outputDotFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram (using the provided load rule)
        Diagram diagram = new Diagram(inputPath);

        // Build a directed graph in DOT format
        StringBuilder dotBuilder = new StringBuilder();
        dotBuilder.AppendLine("digraph EventDependencies {");
        dotBuilder.AppendLine("    rankdir=LR;"); // left‑to‑right layout

        // Regular expression to capture shape IDs referenced in formulas (e.g., Sheet.5!Prop.Row)
        Regex sheetIdRegex = new Regex(@"Sheet\.([0-9]+)", RegexOptions.Compiled);

        // Iterate through all pages and shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                long sourceId = shape.ID;

                // Access the Event object of the shape
                Event shapeEvent = shape.Event;
                if (shapeEvent == null) continue;

                // Use reflection to enumerate all event‑related properties (EventDblClick, EventDrop, etc.)
                PropertyInfo[] eventProps = typeof(Event).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo propInfo in eventProps)
                {
                    // Skip properties that are not event cells (e.g., Del)
                    if (propInfo.Name == "Del") continue;

                    object eventCell = propInfo.GetValue(shapeEvent);
                    if (eventCell == null) continue;

                    // Many event cells are of type RuleValue which contains a Formula property
                    PropertyInfo formulaProp = eventCell.GetType().GetProperty("Formula", BindingFlags.Public | BindingFlags.Instance);
                    if (formulaProp == null) continue; // Not a formula‑holding cell

                    string formula = formulaProp.GetValue(eventCell) as string;
                    if (string.IsNullOrWhiteSpace(formula)) continue;

                    // Find all referenced shape IDs within the formula
                    MatchCollection matches = sheetIdRegex.Matches(formula);
                    foreach (Match match in matches)
                    {
                        if (long.TryParse(match.Groups[1].Value, out long targetId))
                        {
                            // Add an edge from the source shape to the target shape
                            dotBuilder.AppendLine($"    \"{sourceId}\" -> \"{targetId}\" [label=\"{propInfo.Name}\"];");
                        }
                    }
                }
            }
        }

        dotBuilder.AppendLine("}"); // close digraph

        // Write the DOT representation to the specified output file
        File.WriteAllText(outputPath, dotBuilder.ToString());

        Console.WriteLine($"Event dependency graph generated at: {outputPath}");
    }
}
