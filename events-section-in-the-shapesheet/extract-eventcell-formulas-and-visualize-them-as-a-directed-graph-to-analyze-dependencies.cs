using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Dictionary to hold shape ID and its list of event formulas
                Dictionary<long, List<string>> shapeEvents = new Dictionary<long, List<string>>();

                // Iterate through all pages and shapes to collect event formulas
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;
                        List<string> formulas = new List<string>();

                        // Helper local function to add formula if it exists and is not empty
                        void AddFormula(string formula)
                        {
                            if (!string.IsNullOrWhiteSpace(formula))
                            {
                                formulas.Add(formula);
                            }
                        }

                        // Access each supported event cell via the Event property
                        if (shape.Event != null)
                        {
                            if (shape.Event.EventDrop != null) AddFormula(shape.Event.EventDrop.Ufe.F);
                            if (shape.Event.EventDblClick != null) AddFormula(shape.Event.EventDblClick.Ufe.F);
                            if (shape.Event.EventXFMod != null) AddFormula(shape.Event.EventXFMod.Ufe.F);
                            if (shape.Event.EventMultiDrop != null) AddFormula(shape.Event.EventMultiDrop.Ufe.F);
                            if (shape.Event.TheText != null) AddFormula(shape.Event.TheText.Ufe.F);
                            if (shape.Event.TheData != null) AddFormula(shape.Event.TheData.Ufe.F);
                        }

                        if (formulas.Count > 0)
                        {
                            shapeEvents[shapeId] = formulas;
                        }
                    }
                }

                // Output the collected event formulas
                Console.WriteLine("=== Event Cell Formulas ===");
                foreach (var kvp in shapeEvents)
                {
                    Console.WriteLine($"Shape ID: {kvp.Key}");
                    foreach (string formula in kvp.Value)
                    {
                        Console.WriteLine($"  Formula: {formula}");
                    }
                }

                // Simple directed graph representation:
                // For demonstration, we treat any shape name referenced in a formula as a dependency.
                // This example parses tokens that look like shape names (alphanumeric strings) and
                // creates edges from the current shape to the referenced shape if it exists.
                Console.WriteLine("\n=== Dependency Graph (Adjacency List) ===");
                foreach (var kvp in shapeEvents)
                {
                    long fromId = kvp.Key;
                    foreach (string formula in kvp.Value)
                    {
                        // Very naive parsing: split by non-word characters and look for shape IDs
                        string[] tokens = System.Text.RegularExpressions.Regex.Split(formula, @"\W+");
                        foreach (string token in tokens)
                        {
                            if (long.TryParse(token, out long toId))
                            {
                                // Check if the target shape actually exists in the diagram
                                if (shapeEvents.ContainsKey(toId))
                                {
                                    Console.WriteLine($"{fromId} -> {toId}");
                                }
                            }
                        }
                    }
                }

                // OPTIONAL: Save a copy of the diagram (no modifications made)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"\nDiagram saved to: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }