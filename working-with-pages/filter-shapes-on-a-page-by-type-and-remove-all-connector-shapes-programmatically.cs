using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of connector (1‑D) shapes on the current page.
                    List<long> connectorIds = new List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes using the OneD boolean property.
                        if (shape.OneD)
                        {
                            // Shape.ID is a long primitive; add it directly.
                            connectorIds.Add(shape.ID);
                        }
                    }

                    // Remove the identified connector shapes from the page.
                    foreach (long id in connectorIds)
                    {
                        // Retrieve the shape object by its ID before removal.
                        Shape connectorShape = page.Shapes.GetShape(id);
                        if (connectorShape != null)
                        {
                            // The Remove method accepts a Shape instance.
                            page.Shapes.Remove(connectorShape);
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
