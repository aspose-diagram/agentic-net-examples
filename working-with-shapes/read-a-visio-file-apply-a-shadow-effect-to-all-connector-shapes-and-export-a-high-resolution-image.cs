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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes (1‑D shapes). OneD is a native bool.
                        if (shape.OneD)
                        {
                            // Apply a simple drop shadow to the connector
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Shadow type
                            shape.Fill.ShdwForegnd.Value = "#000000";                    // Shadow color (black)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;                    // 30% transparency
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;                    // Horizontal offset
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;                    // Vertical offset

                            // Send the connector to the back to keep visual order consistent
                            shape.SendToBack();
                        }
                    }
                }

                // Configure high‑resolution image export options
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.Resolution = 300; // DPI for high‑resolution output

                // Save the diagram as a high‑resolution PNG image
                diagram.Save(outputPath, imgOptions);
                Console.WriteLine($"Image saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}