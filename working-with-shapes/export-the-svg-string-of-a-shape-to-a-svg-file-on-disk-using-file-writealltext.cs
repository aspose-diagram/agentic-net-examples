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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputSvgPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("Error: The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes[0];

                // Temporary file to let Aspose write the SVG.
                string tempSvgPath = Path.GetTempFileName();

                // Export the shape to SVG using Aspose's ToSvg method.
                // This overload writes the SVG file directly.
                shape.ToSvg(tempSvgPath, new SVGSaveOptions());

                // Read the generated SVG content.
                string svgContent = File.ReadAllText(tempSvgPath);

                // Write the SVG string to the desired output file.
                File.WriteAllText(outputSvgPath, svgContent);

                // Clean up the temporary file.
                File.Delete(tempSvgPath);

                Console.WriteLine($"Shape exported to SVG successfully: {outputSvgPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}