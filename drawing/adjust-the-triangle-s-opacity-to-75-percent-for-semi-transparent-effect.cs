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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page.
                Page page = diagram.Pages[0];

                // Attempt to retrieve a shape that is expected to be a triangle.
                // Here we use shape ID 1 as an example; adjust the ID as needed.
                Shape triangleShape = page.Shapes.GetShape(1);

                // Verify the shape is not logically deleted.
                if (triangleShape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("The target shape is deleted.");
                    return;
                }

                // Set the fill foreground transparency to 75 percent (semi‑transparent).
                // Transparency is expressed as a percentage value (0‑100).
                triangleShape.Fill.FillForegndTrans.Value = 75;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Triangle opacity adjusted and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}