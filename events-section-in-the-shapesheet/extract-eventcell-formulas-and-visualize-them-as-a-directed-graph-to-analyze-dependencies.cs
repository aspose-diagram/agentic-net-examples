using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram using Aspose.Diagram constructor
                Diagram diagram = new Diagram(inputPath);

                // Prepare lines for GraphViz DOT format
                List<string> dotLines = new List<string>();
                dotLines.Add("digraph EventDependencies {");

                // Regular expression to find numeric shape IDs inside formulas
                Regex idRegex = new Regex(@"\b\d+\b", RegexOptions.Compiled);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        string shapeId = shape.ID.ToString();

                        // Add node for the shape
                        dotLines.Add($"  \"{shapeId}\" [label=\"Shape {shapeId}\"];");

                        // Collect all event formulas for this shape
                        List<string> formulas = new List<string>();

                        // Helper to add formula if present
                        void AddFormula(string formula)
                        {
                            if (!string.IsNullOrEmpty(formula))
                            {
                                formulas.Add(formula);
                            }
                        }

                        // Event cells (use Ufe.F to get the formula string)
                        if (shape.Event.EventDblClick?.Ufe != null) AddFormula(shape.Event.EventDblClick.Ufe.F);
                        if (shape.Event.EventDrop?.Ufe != null) AddFormula(shape.Event.EventDrop.Ufe.F);
                        if (shape.Event.EventMultiDrop?.Ufe != null) AddFormula(shape.Event.EventMultiDrop.Ufe.F);
                        if (shape.Event.EventXFMod?.Ufe != null) AddFormula(shape.Event.EventXFMod.Ufe.F);
                        if (shape.Event.TheText?.Ufe != null) AddFormula(shape.Event.TheText.Ufe.F);
                        if (shape.Event.TheData?.Ufe != null) AddFormula(shape.Event.TheData.Ufe.F);

                        // Parse each formula for referenced shape IDs and create edges
                        foreach (string formula in formulas)
                        {
                            foreach (Match match in idRegex.Matches(formula))
                            {
                                string targetId = match.Value;
                                // Avoid self‑reference edges
                                if (targetId != shapeId)
                                {
                                    dotLines.Add($"  \"{shapeId}\" -> \"{targetId}\";");
                                }
                            }
                        }
                    }
                }

                dotLines.Add("}");

                // Output DOT file
                string outputPath = "event_graph.dot";
                File.WriteAllLines(outputPath, dotLines);

                Console.WriteLine($"Event dependency graph written to: {Path.GetFullPath(outputPath)}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }