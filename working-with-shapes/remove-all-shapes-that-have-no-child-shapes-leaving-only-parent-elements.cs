using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the diagram using the provided load rule
                Diagram diagram = LoadDiagram("input.vsdx");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that have no child shapes
                    List<Shape> shapesToRemove = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape.Shapes returns the collection of child shapes
                        if (shape.Shapes.Count == 0)
                        {
                            shapesToRemove.Add(shape);
                        }
                    }

                    // Remove the collected shapes from the page
                    foreach (Shape shape in shapesToRemove)
                    {
                        // Use RemoveDependsOn to also delete any dependent shapes
                        page.Shapes.RemoveDependsOn(shape);
                    }
                }

                // Save the modified diagram using the provided save rule
                SaveDiagram(diagram, "output.vsdx");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Placeholder for the provided load rule
        static Diagram LoadDiagram(string path)
        {
            // Implementation is supplied by the rule set
            return new Diagram(path);
        }

        // Placeholder for the provided save rule
        static void SaveDiagram(Diagram diagram, string path)
        {
            // Implementation is supplied by the rule set
            diagram.Save(path, SaveFileFormat.Vdx);
        }
    }