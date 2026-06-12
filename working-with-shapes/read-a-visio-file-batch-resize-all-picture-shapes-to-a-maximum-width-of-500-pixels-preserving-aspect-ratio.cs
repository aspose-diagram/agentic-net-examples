using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (provide via command‑line or use defaults)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output_resized.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Maximum width in pixels, converted to inches (assuming 96 DPI)
            const double maxWidthPixels = 500.0;
            const double dpi = 96.0;
            double maxWidthInches = maxWidthPixels / dpi;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only picture (foreign) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        double currentWidth = shape.XForm.Width.Value;

                        // Resize if the width exceeds the maximum
                        if (currentWidth > maxWidthInches)
                        {
                            double scale = maxWidthInches / currentWidth;

                            // Set new width and adjust height to keep aspect ratio
                            shape.XForm.Width.Value = maxWidthInches;
                            shape.XForm.Height.Value = shape.XForm.Height.Value * scale;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Resizing completed. Saved to: {outputPath}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
