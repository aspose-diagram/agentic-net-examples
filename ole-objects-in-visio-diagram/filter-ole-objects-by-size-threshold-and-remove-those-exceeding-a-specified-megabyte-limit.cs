using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file, output file, size limit in megabytes
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <inputVisioFile> <outputVisioFile> <sizeLimitMB>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        if (!double.TryParse(args[2], out double sizeLimitMb) || sizeLimitMb <= 0)
        {
            Console.WriteLine("Invalid size limit. Provide a positive number for megabytes.");
            return;
        }

        // Convert megabytes to bytes for comparison
        long sizeLimitBytes = (long)(sizeLimitMb * 1024 * 1024);

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes to remove to avoid modifying collection during iteration
                    var shapesToRemove = new System.Collections.Generic.List<Shape>();

                    // Iterate through shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is a foreign (OLE) shape and has foreign data
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            // Verify the foreign type is an embedded object
                            if (shape.ForeignData.ForeignType == ForeignType.Object)
                            {
                                byte[] oleData = shape.ForeignData.ObjectData;
                                // Check that OLE data exists
                                if (oleData != null && oleData.Length > sizeLimitBytes)
                                {
                                    // Mark this shape for removal
                                    shapesToRemove.Add(shape);
                                    Console.WriteLine($"Removing OLE shape ID {shape.ID} (size {oleData.Length / (1024.0 * 1024.0):F2} MB) from page {page.NameU}");
                                }
                            }
                        }
                    }

                    // Remove the identified shapes
                    foreach (Shape shapeToRemove in shapesToRemove)
                    {
                        page.Shapes.Remove(shapeToRemove);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
