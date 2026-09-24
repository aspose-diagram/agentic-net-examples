using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output SVG file path
        string outputPath = "shape.svg";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.Error.WriteLine("No shape found on the first page.");
                return;
            }

            // Prepare SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Temporary file to let Aspose write the SVG content
            string tempSvgPath = Path.GetTempFileName();

            // Export the shape to the temporary SVG file
            shape.ToSvg(tempSvgPath, svgOptions);

            // Read the generated SVG markup as a string
            string svgContent = File.ReadAllText(tempSvgPath);

            // Write the SVG string to the desired output file
            File.WriteAllText(outputPath, svgContent);

            // Clean up the temporary file
            File.Delete(tempSvgPath);

            Console.WriteLine($"Shape exported to SVG file: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}