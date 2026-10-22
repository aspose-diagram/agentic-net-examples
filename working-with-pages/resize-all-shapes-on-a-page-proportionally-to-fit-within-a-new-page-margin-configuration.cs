using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect arguments: inputPath outputPath newLeftMargin newRightMargin newTopMargin newBottomMargin
        if (args.Length < 6)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <newLeft> <newRight> <newTop> <newBottom>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse new margin values (in inches)
        if (!double.TryParse(args[2], out double newLeft) ||
            !double.TryParse(args[3], out double newRight) ||
            !double.TryParse(args[4], out double newTop) ||
            !double.TryParse(args[5], out double newBottom))
        {
            Console.Error.WriteLine("Invalid margin values. Please provide numeric values for margins.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Process each page (you can change to a specific page if needed)
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve original page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Retrieve original margins from PrintProps (in inches)
                    double oldLeft = page.PageSheet.PrintProps.PageLeftMargin.Value;
                    double oldRight = page.PageSheet.PrintProps.PageRightMargin.Value;
                    double oldTop = page.PageSheet.PrintProps.PageTopMargin.Value;
                    double oldBottom = page.PageSheet.PrintProps.PageBottomMargin.Value;

                    // Compute usable drawing area before and after margin change
                    double oldUsableWidth = pageWidth - oldLeft - oldRight;
                    double oldUsableHeight = pageHeight - oldTop - oldBottom;

                    double newUsableWidth = pageWidth - newLeft - newRight;
                    double newUsableHeight = pageHeight - newTop - newBottom;

                    // Validate usable areas
                    if (oldUsableWidth <= 0 || oldUsableHeight <= 0)
                    {
                        Console.Error.WriteLine("Original margins result in non-positive usable area.");
                        return;
                    }
                    if (newUsableWidth <= 0 || newUsableHeight <= 0)
                    {
                        Console.Error.WriteLine("New margins result in non-positive usable area.");
                        return;
                    }

                    // Determine uniform scaling factor to keep aspect ratio
                    double scaleX = newUsableWidth / oldUsableWidth;
                    double scaleY = newUsableHeight / oldUsableHeight;
                    double uniformScale = Math.Min(scaleX, scaleY);

                    // Iterate over all shapes on the page and resize/reposition them
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve current geometry
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Compute offsets from the original left/top margins
                        double offsetX = pinX - oldLeft;
                        double offsetY = pinY - oldTop;

                        // Apply scaling and new margins
                        double newPinX = newLeft + offsetX * uniformScale;
                        double newPinY = newTop + offsetY * uniformScale;
                        double newWidth = width * uniformScale;
                        double newHeight = height * uniformScale;

                        // Update shape geometry
                        shape.XForm.PinX.Value = newPinX;
                        shape.XForm.PinY.Value = newPinY;
                        shape.XForm.Width.Value = newWidth;
                        shape.XForm.Height.Value = newHeight;
                    }

                    // Update page margins to the new configuration
                    page.PageSheet.PrintProps.PageLeftMargin.Value = newLeft;
                    page.PageSheet.PrintProps.PageRightMargin.Value = newRight;
                    page.PageSheet.PrintProps.PageTopMargin.Value = newTop;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = newBottom;
                }

                // Save the modified diagram preserving VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}