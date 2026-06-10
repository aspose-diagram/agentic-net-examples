using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output_resized.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Define a scaling factor for resizing shapes
            double scaleFactor = 1.2;

            // Iterate through each page and resize non‑connector shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip 1‑D connector shapes; only resize regular shapes
                    if (shape.OneD) continue;

                    // Scale width, height, and position to keep the shape centered
                    shape.XForm.Width.Value *= scaleFactor;
                    shape.XForm.Height.Value *= scaleFactor;
                    shape.XForm.PinX.Value *= scaleFactor;
                    shape.XForm.PinY.Value *= scaleFactor;
                }

                // Recalculate connector routes on the current page
                LayoutOptions layoutOpts = new LayoutOptions
                {
                    // Use a layout style that does not move shapes but reroutes connectors
                    LayoutStyle = LayoutStyle.FlowChart,
                    Direction = LayoutDirection.TopToBottom,
                    EnlargePage = false
                };
                page.Layout(layoutOpts);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
