using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Translation offsets (in inches). Adjust as needed.
        const double offsetX = 1.0;
        const double offsetY = 1.0;

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Iterate through each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply translation to the shape's PinX and PinY.
                    shape.XForm.PinX.Value += offsetX;
                    shape.XForm.PinY.Value += offsetY;

                    // Refresh shape data after modification.
                    shape.RefreshData();

                    // Calculate the shape's bounding box.
                    double left   = shape.XForm.PinX.Value - shape.XForm.LocPinX.Value;
                    double top    = shape.XForm.PinY.Value - shape.XForm.LocPinY.Value;
                    double right  = left + shape.XForm.Width.Value;
                    double bottom = top + shape.XForm.Height.Value;

                    // Validate that the shape stays within page boundaries.
                    bool withinHorizontal = left >= 0 && right <= pageWidth;
                    bool withinVertical   = top  >= 0 && bottom <= pageHeight;

                    if (!withinHorizontal || !withinVertical)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page {page.Name} is out of bounds after translation.");
                        Console.WriteLine($"  Left: {left}, Right: {right} (Page Width: {pageWidth})");
                        Console.WriteLine($"  Top: {top}, Bottom: {bottom} (Page Height: {pageHeight})");
                    }
                }
            }

            // Optionally, save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", 
                                             Path.GetFileNameWithoutExtension(inputPath) + "_translated.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Translated diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}