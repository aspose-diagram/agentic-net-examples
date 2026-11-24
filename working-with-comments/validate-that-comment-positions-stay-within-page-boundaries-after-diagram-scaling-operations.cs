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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example scaling factors (can be adjusted as needed)
                double scaleX = 1.5;
                double scaleY = 1.5;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Apply scaling to the page
                    page.PageSheet.PrintProps.ScaleX.Value = scaleX;
                    page.PageSheet.PrintProps.ScaleY.Value = scaleY;

                    // Retrieve page dimensions
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Retrieve comment coordinates
                        double x = ann.X.Value;
                        double y = ann.Y.Value;

                        // Validate that the comment lies within page boundaries
                        bool isWithinBounds = x >= 0 && x <= pageWidth && y >= 0 && y <= pageHeight;

                        if (!isWithinBounds)
                        {
                            Console.WriteLine($"Comment ID {ann.MarkerIndex.Value} on page '{page.Name ?? "Unnamed"}' is out of bounds: X={x}, Y={y}, PageSize=({pageWidth}, {pageHeight})");
                        }
                        else
                        {
                            Console.WriteLine($"Comment ID {ann.MarkerIndex.Value} on page '{page.Name ?? "Unnamed"}' is within bounds.");
                        }
                    }
                }

                // Optional: Save the scaled diagram (commented out as not required for validation)
                // string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "scaled_output.vsdx");
                // diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}