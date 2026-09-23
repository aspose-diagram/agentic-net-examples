using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file; can be passed as a command‑line argument
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold comment counts per shape type
                Dictionary<TypeValue, int> commentCounts = new Dictionary<TypeValue, int>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the shape associated with the comment
                        int shapeId = annotation.ShapeID;
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape == null)
                        {
                            // Skip if the shape cannot be found (should not happen)
                            continue;
                        }

                        // Determine the shape type
                        TypeValue shapeType = shape.Type;

                        // Increment the count for this shape type
                        if (commentCounts.ContainsKey(shapeType))
                        {
                            commentCounts[shapeType]++;
                        }
                        else
                        {
                            commentCounts[shapeType] = 1;
                        }
                    }
                }

                // Output the report
                Console.WriteLine("Comment Count by Shape Type:");
                foreach (KeyValuePair<TypeValue, int> entry in commentCounts)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }