using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout; // required for LayoutOptions

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Example resizing: increase width and height of all shapes by 10%
            foreach (Page page in diagram.Pages) // iterate each page
            {
                foreach (Shape shape in page.Shapes) // iterate each shape on the page
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Skip 1‑D connector shapes; only resize 2‑D shapes
                    if (shape.OneD)
                        continue;

                    // Calculate new dimensions (10% increase)
                    double newWidth = shape.XForm.Width.Value * 1.10;
                    double newHeight = shape.XForm.Height.Value * 1.10;

                    // Apply the new dimensions to the shape
                    shape.XForm.Width.Value = newWidth;
                    shape.XForm.Height.Value = newHeight;
                }
            }

            // Recalculate connector routes after resizing to maintain diagram integrity
            LayoutOptions layoutOpts = new LayoutOptions(); // default layout options
            diagram.Layout(layoutOpts); // apply layout recalculation

            // Save the updated diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Simple error reporting
            Console.Error.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}