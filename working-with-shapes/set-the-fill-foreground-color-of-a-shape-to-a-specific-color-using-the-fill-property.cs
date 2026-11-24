using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0). Adjust as needed for specific pages.
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID. Here we use shape ID 1 as an example.
            // Ensure the shape exists; otherwise, handle the null case.
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Error: Shape with ID 1 was not found on the first page.");
                return;
            }

            // Ensure the shape has a visible fill by setting the fill pattern to solid (value = 1).
            shape.Fill.FillPattern.Value = 1;

            // Set the fill foreground color to a specific hexadecimal color.
            // Example: bright red. Change the hex string to the desired color.
            shape.Fill.FillForegnd.Value = "#FF0000";

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}