using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the resized output file
            string outputPath = "output_resized.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // New page margins in inches
            double topMargin = 0.5;    // 0.5 inch from the top edge
            double bottomMargin = 0.5; // 0.5 inch from the bottom edge
            double leftMargin = 0.5;   // 0.5 inch from the left edge
            double rightMargin = 0.5;  // 0.5 inch from the right edge

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply the new margin settings
                page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;

                // Original page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute the drawable area after applying margins
                double availableWidth = pageWidth - leftMargin - rightMargin;
                double availableHeight = pageHeight - topMargin - bottomMargin;

                // Determine a uniform scaling factor to keep aspect ratio
                double scaleX = availableWidth / pageWidth;
                double scaleY = availableHeight / pageHeight;
                double uniformScale = Math.Min(scaleX, scaleY);

                // Resize and reposition each shape proportionally
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Scale position (PinX, PinY) relative to the new margins
                    shape.XForm.PinX.Value = leftMargin + shape.XForm.PinX.Value * uniformScale;
                    shape.XForm.PinY.Value = bottomMargin + shape.XForm.PinY.Value * uniformScale;

                    // Scale size (Width, Height)
                    shape.XForm.Width.Value = shape.XForm.Width.Value * uniformScale;
                    shape.XForm.Height.Value = shape.XForm.Height.Value * uniformScale;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
