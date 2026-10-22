using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (you can adjust the page name or index as needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID (replace 1 with the actual ID of the circle shape)
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape is not deleted
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("The specified shape is marked as deleted.");
                return;
            }

            // Set fill pattern to solid (value 1) and fill foreground color to blue
            shape.Fill.FillPattern.Value = 1;               // Solid fill
            shape.Fill.FillForegnd.Value = "#0000FF";       // Blue color in hex

            // Set line weight to 0.5 points (Visio uses inches; 1 point = 1/72 inch)
            double lineWeightInInches = 0.5 / 72.0;
            shape.Line.LineWeight.Value = lineWeightInInches;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}