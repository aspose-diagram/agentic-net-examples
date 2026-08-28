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

            // Load the diagram inside a using block to ensure proper disposal
            using Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page width (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                // Calculate a font size that is 10% of the page width.
                // Shape.Char.Size.Value expects a size in inches.
                double titleFontSizeInInches = pageWidth * 0.10;

                // Locate a shape whose universal name is "Title"
                Shape titleShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                    {
                        titleShape = shape;
                        break;
                    }
                }

                // If a title shape exists, update its text and font size
                if (titleShape != null)
                {
                    // Replace any existing text
                    titleShape.Text.Value.Clear();
                    titleShape.Text.Value.Add(new Txt("Page Title"));

                    // Ensure there is at least one Char entry for formatting
                    if (titleShape.Chars.Count == 0)
                    {
                        titleShape.Chars.Add(new Aspose.Diagram.Char());
                    }

                    // Apply the calculated font size (in inches) and a fallback font
                    titleShape.Chars[0].Size.Value = titleFontSizeInInches;
                    titleShape.Chars[0].FontName.Value = "Arial";
                }
            }

            // Configure PDF save options (set a default font for fallback)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as a PDF
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
