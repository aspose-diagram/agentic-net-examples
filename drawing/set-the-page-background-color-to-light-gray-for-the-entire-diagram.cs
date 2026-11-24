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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions.
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a rectangle shape that covers the entire page.
                    // The master name "Rectangle" is a built‑in Visio shape.
                    long shapeId = page.AddShape(pageWidth / 2, pageHeight / 2, "Rectangle", false);
                    Shape backgroundShape = page.Shapes.GetShape(shapeId);

                    // Set the rectangle size to match the page.
                    backgroundShape.XForm.Width.Value = pageWidth;
                    backgroundShape.XForm.Height.Value = pageHeight;

                    // Configure solid fill with light gray color (#D3D3D3).
                    backgroundShape.Fill.FillPattern.Value = 1; // Solid fill.
                    backgroundShape.Fill.FillForegnd.Value = "#D3D3D3";

                    // Optionally, send the shape to the back by moving it to the first position.
                    // Visio renders shapes in the order they are added; adding first ensures background.
                }

                // Save the modified diagram as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}