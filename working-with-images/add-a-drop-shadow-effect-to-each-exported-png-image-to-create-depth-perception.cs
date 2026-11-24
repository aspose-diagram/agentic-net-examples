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

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Load the diagram and process each page
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Apply a simple drop shadow to every non‑deleted shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Fill object before configuring shadow properties
                        if (shape.Fill != null)
                        {
                            // Set shadow type to Simple
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                            // Set shadow color to black
                            shape.Fill.ShdwForegnd.Value = "#000000";

                            // Set shadow transparency (30% transparent)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;

                            // Set shadow offsets (0.1 inch horizontally and vertically)
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                        }
                    }

                    // Configure PNG export options for the current page
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.PageIndex = pageIndex; // Export only this page
                    pngOptions.PageCount = 1;         // Single‑page image

                    // Build the output file path
                    string outputPath = Path.Combine(outputFolder, $"page_{pageIndex + 1}.png");

                    // Save the page as PNG with the shadow effect applied
                    diagram.Save(outputPath, pngOptions);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}