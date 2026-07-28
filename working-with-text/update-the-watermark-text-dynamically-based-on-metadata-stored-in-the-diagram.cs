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

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Default watermark text if the custom property is not found
                string watermarkText = "Default Watermark";

                // Look for a custom property named "WatermarkText"
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (prop.Name == "WatermarkText")
                    {
                        watermarkText = prop.CustomValue.ValueString;
                        break;
                    }
                }

                // Add or update the watermark on each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a full‑page watermark using the AddText overload
                    // Font size is specified in inches (0.25 ≈ 18 pt)
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                        watermarkText, "Calibri", "#a5a5a5", 0.25);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
