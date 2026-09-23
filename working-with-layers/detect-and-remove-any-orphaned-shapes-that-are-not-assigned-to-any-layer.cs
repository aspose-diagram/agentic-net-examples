using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: OrphanedShapeRemoval <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes to be removed to avoid modifying the collection while iterating.
                var shapesToDelete = new System.Collections.Generic.List<Shape>();

                // Examine each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the LayerMem and its LayerMember are available.
                    if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                    {
                        // An empty LayerMember string means the shape is not assigned to any layer.
                        string layerMembership = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrWhiteSpace(layerMembership))
                        {
                            // Mark the shape for deletion.
                            shape.Del = BOOL.True;
                            // Optionally keep a reference if further processing is needed.
                            shapesToDelete.Add(shape);
                        }
                    }
                }

                // Remove the marked shapes from the collection.
                foreach (Shape s in shapesToDelete)
                {
                    // The ShapeCollection supports removal by shape reference.
                    page.Shapes.Remove(s);
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Orphaned shapes removed and diagram saved to '{outputPath}'.");
        }
    }