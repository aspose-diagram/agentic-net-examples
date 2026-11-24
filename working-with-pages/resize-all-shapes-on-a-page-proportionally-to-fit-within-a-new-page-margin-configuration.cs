using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [newMarginInInches]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double newMargin = 0.5; // Default margin (in inches).

        if (args.Length >= 3 && !double.TryParse(args[2], out newMargin))
        {
            Console.Error.WriteLine("Invalid margin value.");
            return;
        }

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve original page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Compute the available drawing area after applying the new margins.
                    double availableWidth = pageWidth - 2 * newMargin;
                    double availableHeight = pageHeight - 2 * newMargin;

                    if (availableWidth <= 0 || availableHeight <= 0)
                    {
                        Console.Error.WriteLine("New margin is too large for the page size.");
                        continue;
                    }

                    // Determine uniform scaling factor to keep aspect ratio.
                    double scaleX = availableWidth / pageWidth;
                    double scaleY = availableHeight / pageHeight;
                    double uniformScale = Math.Min(scaleX, scaleY);

                    // Update page margin properties (PrintProps holds the printable margins).
                    page.PageSheet.PrintProps.PageLeftMargin.Value = newMargin;
                    page.PageSheet.PrintProps.PageRightMargin.Value = newMargin;
                    page.PageSheet.PrintProps.PageTopMargin.Value = newMargin;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = newMargin;

                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Original geometry values.
                        double originalPinX = shape.XForm.PinX.Value;
                        double originalPinY = shape.XForm.PinY.Value;
                        double originalWidth = shape.XForm.Width.Value;
                        double originalHeight = shape.XForm.Height.Value;

                        // Apply scaling and shift to respect the new left/top margins.
                        double newPinX = originalPinX * uniformScale + newMargin;
                        double newPinY = originalPinY * uniformScale + newMargin;
                        double newWidth = originalWidth * uniformScale;
                        double newHeight = originalHeight * uniformScale;

                        // Update shape geometry.
                        shape.XForm.PinX.Value = newPinX;
                        shape.XForm.PinY.Value = newPinY;
                        shape.XForm.Width.Value = newWidth;
                        shape.XForm.Height.Value = newHeight;
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}