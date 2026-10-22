using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over pages by index to obtain a valid PageIndex value
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Apply a simple drop shadow to every shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Enable simple shadow type
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        // 30% transparency (0.0 = opaque, 1.0 = fully transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        // Small offset to make the shadow visible
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }

                    // Configure PNG export options for the current page
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.PageIndex = pageIdx; // Export only this page
                    pngOptions.PageCount = 1;

                    // Build the output file path (e.g., Page_1.png)
                    string outputPath = Path.Combine(outputFolder, $"Page_{pageIdx + 1}.png");

                    // Export the page as PNG with the configured shadow effect
                    diagram.Save(outputPath, pngOptions);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}