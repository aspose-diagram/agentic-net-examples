using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, background color (hex, e.g., "#FF0000")
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioFile> <outputPngFile> <backgroundColorHex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string backgroundColor = args[2];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (assumes at least one page exists)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (width and height) from the page sheet
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate shape position to cover the entire page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                double shapeWidth = pageWidth;
                double shapeHeight = pageHeight;

                // Add a rectangle shape that will serve as the background.
                // The overload uses a master name ("Rectangle") to create a standard shape.
                long shapeId = page.AddShape(pinX, pinY, shapeWidth, shapeHeight, "Rectangle");

                // Retrieve the newly added shape object
                Shape backgroundShape = page.Shapes.GetShape(shapeId);

                // Set the fill pattern to solid (value = 1) and apply the desired background color
                backgroundShape.Fill.FillPattern.Value = 1;
                backgroundShape.Fill.FillForegnd.Value = backgroundColor;

                // Send the background shape to the back so other content appears on top
                backgroundShape.SendToBack();

                // Configure PNG export options
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export only the first page (optional, can be omitted to export all pages)
                saveOptions.PageIndex = 0;
                saveOptions.PageCount = 1;

                // Save the diagram as a PNG image with the configured options
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram exported successfully to PNG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}