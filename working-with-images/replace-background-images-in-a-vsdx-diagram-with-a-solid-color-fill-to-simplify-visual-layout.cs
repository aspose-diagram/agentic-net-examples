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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Process only background pages.
                    if (page.Background == BOOL.True)
                    {
                        // Iterate over all shapes on the background page.
                        for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                        {
                            Shape shape = page.Shapes[shapeIndex];

                            // Identify foreign (image) shapes by their Type cell.
                            // TypeValue.Foreign corresponds to an embedded image.
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // Replace the image fill with a solid color (white in this example).
                                shape.Fill.FillPattern.Value = 1;               // Solid fill pattern.
                                shape.Fill.FillForegnd.Value = "#FFFFFF";       // Solid white color.

                                // Ensure the shape is sent to the back so it does not obscure other content.
                                shape.SendToBack();
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}