using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // LINQ: select pages where the horizontal scaling factor (ScaleX) exceeds 1.0.
            var pagesToTransform = diagram.Pages
                .Cast<Page>()
                .Where(p => p.PageSheet.PrintProps.ScaleX.Value > 1.0)
                .ToList();

            // Apply additional transformations to each filtered page.
            foreach (var page in pagesToTransform)
            {
                // Example transformation: reset vertical scaling to 1.0.
                page.PageSheet.PrintProps.ScaleY.Value = 1.0;

                // Example additional transformation:
                // Add a full‑page rectangle shape with a light‑gray fill as a background.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle shape (master name "Rectangle").
                long shapeId = page.AddShape(0, 0, "Rectangle", false);
                var rectShape = page.Shapes.GetShape(shapeId);

                // Position the rectangle to cover the entire page.
                rectShape.XForm.PinX.Value = pageWidth / 2.0;
                rectShape.XForm.PinY.Value = pageHeight / 2.0;
                rectShape.XForm.Width.Value = pageWidth;
                rectShape.XForm.Height.Value = pageHeight;

                // Set solid fill with a light‑gray color.
                rectShape.Fill.FillPattern.Value = 1; // Solid fill.
                rectShape.Fill.FillForegnd.Value = "#D3D3D3"; // Light gray.
            }

            // Save the modified diagram to the specified output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}