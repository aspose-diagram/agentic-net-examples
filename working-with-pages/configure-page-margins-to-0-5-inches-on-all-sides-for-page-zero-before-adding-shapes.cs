using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (page index 0).
                Page page = diagram.Pages[0];

                // Set all four margins to 0.5 inches.
                // Values are in inches; assign via the .Value property.
                page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;

                // Example: add a rectangle shape after configuring margins.
                // Parameters: page ID, master ID (1 = rectangle), shape name, and flags (0).
                long shapeId = diagram.AddShape(page.ID, 1, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Optionally set shape position and size (in inches).
                shape.XForm.PinX.Value = 2.0;   // X coordinate of the shape center.
                shape.XForm.PinY.Value = 2.0;   // Y coordinate of the shape center.
                shape.XForm.Width.Value = 1.0;  // Width of the shape.
                shape.XForm.Height.Value = 0.5; // Height of the shape.

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}