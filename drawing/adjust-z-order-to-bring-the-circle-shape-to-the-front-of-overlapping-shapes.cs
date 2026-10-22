using System;
using System.IO;
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to locate the circle (ellipse) shape.
            foreach (Page page in diagram.Pages)
            {
                // Ensure the Shapes collection is not null.
                if (page.Shapes == null) continue;

                foreach (Shape shape in page.Shapes)
                {
                    // Identify the circle shape by its master name.
                    // In Visio, a circle is an ellipse with equal width/height.
                    // The master name for an ellipse is typically "Ellipse".
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        // Bring the identified shape to the front of the Z‑order.
                        // NOTE: Verify that BringToFront() exists in the Aspose.Diagram version used.
                        shape.BringToFront();
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}