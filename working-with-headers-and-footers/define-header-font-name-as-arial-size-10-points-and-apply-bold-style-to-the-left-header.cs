using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set the left header text (optional, can be customized)
            diagram.HeaderFooter.HeaderLeft = "Left Header Text";

            // Access the global header/footer font settings
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Define the font face name as Arial
            headerFont.FaceName = "Arial";

            // Set the font size to 10 points.
            // Height uses a negative mapping: Height = round(PointSize * -1.333)
            // 10 * -1.333 = -13.33 => rounded to -13
            headerFont.Height = -13;

            // Apply bold style by setting the weight to 700 (bold)
            headerFont.Weight = 700;

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}