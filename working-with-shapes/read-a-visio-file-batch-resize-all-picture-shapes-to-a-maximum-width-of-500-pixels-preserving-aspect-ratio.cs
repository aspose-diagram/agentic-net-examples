using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output_resized.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define maximum width in pixels and convert to inches (assuming 96 DPI)
            const double maxWidthPixels = 500.0;
            const double dpi = 96.0;
            double maxWidthInches = maxWidthPixels / dpi;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify picture (foreign) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        double currentWidth = shape.XForm.Width.Value;
                        // Resize only if the current width exceeds the maximum
                        if (currentWidth > maxWidthInches && currentWidth > 0)
                        {
                            double scale = maxWidthInches / currentWidth;
                            shape.XForm.Width.Value = maxWidthInches;
                            shape.XForm.Height.Value = shape.XForm.Height.Value * scale;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
