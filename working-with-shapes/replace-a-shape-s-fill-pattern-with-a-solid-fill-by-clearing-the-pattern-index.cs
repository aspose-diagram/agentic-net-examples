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

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page of the diagram.
                // If the document has no pages, report an error.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Get the first page (index 0).
                Page page = diagram.Pages[0];

                // Define the ID of the shape whose fill pattern will be changed.
                // Here we use shape ID 1 as an example; adjust as needed.
                long shapeId = 1;

                // Retrieve the shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page \"{page.Name}\".");
                    return;
                }

                // Replace the existing fill pattern with a solid fill.
                // Setting FillPattern.Value to 1 corresponds to a solid fill.
                shape.Fill.FillPattern.Value = 1;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully saved modified diagram to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}