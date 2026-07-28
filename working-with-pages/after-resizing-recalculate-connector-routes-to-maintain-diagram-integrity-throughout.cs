using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;   // required for LayoutOptions

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path
        string inputPath = "input.vsdx";
        // Guard: ensure the file exists before loading
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output diagram path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and resize non‑connector shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip 1‑D connector shapes
                    if (shape.OneD)
                        continue;

                    // Increase width and height by 20%
                    double newWidth = shape.XForm.Width.Value * 1.2;
                    double newHeight = shape.XForm.Height.Value * 1.2;
                    shape.XForm.Width.Value = newWidth;
                    shape.XForm.Height.Value = newHeight;
                }
            }

            // Recalculate connector routes after resizing
            LayoutOptions layoutOptions = new LayoutOptions
            {
                EnlargePage = false   // keep page size unchanged
            };
            diagram.Layout(layoutOptions);   // apply layout to the whole diagram

            // Save the updated diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}