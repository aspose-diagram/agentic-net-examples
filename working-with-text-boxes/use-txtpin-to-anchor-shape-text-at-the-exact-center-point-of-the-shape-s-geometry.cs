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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that have no geometry (e.g., connectors without width/height).
                    if (shape.XForm == null || shape.TextXForm == null)
                        continue;

                    // Calculate the center of the shape's geometry.
                    double shapeCenterX = shape.XForm.Width.Value / 2.0;
                    double shapeCenterY = shape.XForm.Height.Value / 2.0;

                    // Set the text block's pin to the shape's center.
                    shape.TextXForm.TxtPinX.Value = shapeCenterX;
                    shape.TextXForm.TxtPinY.Value = shapeCenterY;

                    // Optionally, center the text within its own text block.
                    // This uses half of the text block's width/height as the local pin.
                    double txtHalfWidth = shape.TextXForm.TxtWidth.Value / 2.0;
                    double txtHalfHeight = shape.TextXForm.TxtHeight.Value / 2.0;

                    shape.TextXForm.TxtLocPinX.Value = txtHalfWidth;
                    shape.TextXForm.TxtLocPinY.Value = txtHalfHeight;

                    // Refresh shape data so the changes take effect.
                    shape.RefreshData();
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}