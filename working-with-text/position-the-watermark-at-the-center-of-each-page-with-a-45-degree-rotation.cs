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

            // Load the Visio diagram (replace with your actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a text shape that will serve as the watermark.
                    // Width and height are set to the full page size.
                    // Font size is specified in inches (e.g., 0.25 inches ≈ 18 points).
                    Shape watermark = page.AddText(
                        pinX,
                        pinY,
                        pageWidth,
                        pageHeight,
                        "Watermark",
                        "Calibri",
                        "#a5a5a5",
                        0.25);

                    // Rotate the watermark text by 45 degrees (value in radians)
                    watermark.TextXForm.TxtAngle.Value = (Math.PI / 180.0) * 45.0;
                }

                // Save the modified diagram (replace with desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
