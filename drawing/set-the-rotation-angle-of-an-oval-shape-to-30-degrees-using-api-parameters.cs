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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional argument or default)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "RotatedOval.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Add an oval shape at position (2,2) using the master name "Oval"
            // The AddShape method returns the shape ID (long)
            long ovalShapeId = page.AddShape(2.0, 2.0, "Oval");

            // Retrieve the shape object from the page's shape collection
            Shape ovalShape = page.Shapes.GetShape((int)ovalShapeId);
            if (ovalShape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added oval shape.");
                return;
            }

            // Set the rotation angle of the oval shape to 30 degrees
            // Shape rotation is controlled via XForm.Angle.Value (degrees)
            ovalShape.XForm.Angle.Value = 30.0;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Oval shape rotated to 30 degrees and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}