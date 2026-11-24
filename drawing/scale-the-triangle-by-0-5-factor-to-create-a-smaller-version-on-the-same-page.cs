using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_scaled.vsdx");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape (the triangle)
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes to scale.");
                    return;
                }

                // Retrieve the first shape (assumed to be the triangle)
                // Note: Shapes are accessed via the Shapes collection iterator
                Shape triangle = null;
                foreach (Shape s in page.Shapes)
                {
                    triangle = s;
                    break;
                }

                if (triangle == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the triangle shape.");
                    return;
                }

                // Scale the triangle by a factor of 0.5 (reduce size to half)
                // Adjust Width and Height; also adjust PinX and PinY to keep the shape centered
                double originalWidth = triangle.XForm.Width.Value;
                double originalHeight = triangle.XForm.Height.Value;
                double originalPinX = triangle.XForm.PinX.Value;
                double originalPinY = triangle.XForm.PinY.Value;

                double scaleFactor = 0.5;

                // Apply scaling to size
                triangle.XForm.Width.Value = originalWidth * scaleFactor;
                triangle.XForm.Height.Value = originalHeight * scaleFactor;

                // Reposition the shape to keep it centered at the original location
                triangle.XForm.PinX.Value = originalPinX;
                triangle.XForm.PinY.Value = originalPinY;

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}