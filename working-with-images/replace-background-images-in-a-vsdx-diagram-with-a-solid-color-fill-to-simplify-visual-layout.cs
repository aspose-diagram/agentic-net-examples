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

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Process only background pages (where Background == BOOL.True).
                    if (page.Background == BOOL.True)
                    {
                        // Iterate over all shapes on the background page.
                        for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                        {
                            Shape shape = page.Shapes[shapeIdx];

                            // Identify image shapes by their Type being Foreign.
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // Replace the image fill with a solid white color.
                                shape.Fill.FillPattern.Value = 1;               // Solid fill pattern.
                                shape.Fill.FillForegnd.Value = "#FFFFFF";       // White foreground color.

                                // Ensure the shape is sent to the back so it behaves like a background.
                                shape.SendToBack();
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}