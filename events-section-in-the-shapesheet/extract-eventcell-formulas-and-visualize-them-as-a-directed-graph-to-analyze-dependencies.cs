using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDotFile]");
            return;
        }

        string inputPath = args[0];
        string outputDotPath = args.Length >= 2 ? args[1] : "event_graph.dot";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold adjacency list: source shape ID -> set of target shape IDs
            Dictionary<long, HashSet<long>> adjacency = new Dictionary<long, HashSet<long>>();

            // Set to keep track of all shape IDs (for node creation)
            HashSet<long> allShapeIds = new HashSet<long>();

            // Regular expression to extract numeric IDs from formulas (simple heuristic)
            Regex idRegex = new Regex(@"\b\d+\b", RegexOptions.Compiled);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    allShapeIds.Add(shapeId);

                    // Ensure the shape has an Event section
                    if (shape.Event == null)
                        continue;

                    // Helper local function to process a single event formula
                    void ProcessFormula(string formula)
                    {
                        if (string.IsNullOrWhiteSpace(formula))
                            return;

                        // Find all numeric IDs in the formula
                        foreach (Match match in idRegex.Matches(formula))
                        {
                            if (long.TryParse(match.Value, out long targetId))
                            {
                                // Skip self-references
                                if (targetId == shapeId)
                                    continue;

                                if (!adjacency.ContainsKey(shapeId))
                                    adjacency[shapeId] = new HashSet<long>();

                                adjacency[shapeId].Add(targetId);
                                allShapeIds.Add(targetId);
                            }
                        }
                    }

                    // Extract formulas from each supported event cell
                    // TheText
                    if (shape.Event.TheText != null && shape.Event.TheText.Ufe != null)
                        ProcessFormula(shape.Event.TheText.Ufe.F);

                    // EventXFMod
                    if (shape.Event.EventXFMod != null && shape.Event.EventXFMod.Ufe != null)
                        ProcessFormula(shape.Event.EventXFMod.Ufe.F);

                    // EventDblClick
                    if (shape.Event.EventDblClick != null && shape.Event.EventDblClick.Ufe != null)
                        ProcessFormula(shape.Event.EventDblClick.Ufe.F);

                    // EventDrop
                    if (shape.Event.EventDrop != null && shape.Event.EventDrop.Ufe != null)
                        ProcessFormula(shape.Event.EventDrop.Ufe.F);

                    // EventMultiDrop
                    if (shape.Event.EventMultiDrop != null && shape.Event.EventMultiDrop.Ufe != null)
                        ProcessFormula(shape.Event.EventMultiDrop.Ufe.F);

                    // TheData
                    if (shape.Event.TheData != null && shape.Event.TheData.Ufe != null)
                        ProcessFormula(shape.Event.TheData.Ufe.F);
                }
            }

            // Build DOT representation
            using (StreamWriter writer = new StreamWriter(outputDotPath))
            {
                writer.WriteLine("digraph EventDependencyGraph {");
                writer.WriteLine("    rankdir=LR;");

                // Declare all nodes
                foreach (long nodeId in allShapeIds)
                {
                    writer.WriteLine($"    \"{nodeId}\" [label=\"Shape {nodeId}\"];");
                }

                // Declare edges
                foreach (var kvp in adjacency)
                {
                    long sourceId = kvp.Key;
                    foreach (long targetId in kvp.Value)
                    {
                        writer.WriteLine($"    \"{sourceId}\" -> \"{targetId}\";");
                    }
                }

                writer.WriteLine("}");
            }

            Console.WriteLine($"Event dependency graph saved to \"{outputDotPath}\".");

            // Optional: Save a copy of the diagram in VSDX format for reference
            string outputVsdxPath = Path.ChangeExtension(inputPath, ".output.vsdx");
            diagram.Save(outputVsdxPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved as VSDX to \"{outputVsdxPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}