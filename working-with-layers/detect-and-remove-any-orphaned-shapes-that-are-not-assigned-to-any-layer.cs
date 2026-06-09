using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs to delete (cannot modify collection while iterating)
                    var shapesToDelete = new System.Collections.Generic.List<long>();

                    // Iterate through shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure LayerMem and LayerMember are available
                        if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                        {
                            string layerMembership = shape.LayerMem.LayerMember.Value;

                            // If the shape is not assigned to any layer (empty string), mark for deletion
                            if (string.IsNullOrEmpty(layerMembership))
                            {
                                shapesToDelete.Add(shape.ID);
                            }
                        }
                    }

                    // Delete the identified orphaned shapes
                    foreach (long shapeId in shapesToDelete)
                    {
                        Shape orphanShape = page.Shapes.GetShape(shapeId);
                        if (orphanShape != null)
                        {
                            // Mark the shape as deleted
                            orphanShape.Del = BOOL.True;
                            Console.WriteLine($"Deleted orphaned shape ID {shapeId} on page '{page.Name}'.");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Processing complete. Diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }