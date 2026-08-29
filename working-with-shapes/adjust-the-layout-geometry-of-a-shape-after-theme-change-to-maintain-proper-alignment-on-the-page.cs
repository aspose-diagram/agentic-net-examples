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

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply a preset theme to the shape (example theme)
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Adjust the shape's horizontal position to keep it centered on the page
                        // PinX represents the X coordinate of the shape's center
                        shape.XForm.PinX.Value = pageWidth / 2.0;

                        // Optionally, you could also adjust vertical alignment or other geometry here
                        // For this example, we keep the original PinY value unchanged
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed and saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
