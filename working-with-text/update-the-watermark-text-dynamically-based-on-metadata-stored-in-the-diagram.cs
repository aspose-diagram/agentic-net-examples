using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve watermark text from a custom document property named "WatermarkText"
                string watermarkText = "Default Watermark";
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (prop.Name == "WatermarkText")
                    {
                        // CustomValue.ValueString holds the string value of the custom property
                        watermarkText = prop.CustomValue.ValueString;
                        break;
                    }
                }

                // Iterate through all pages and add/update the watermark
                foreach (Page page in diagram.Pages)
                {
                    // Page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark text
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Font size in inches (e.g., 0.25 inches ≈ 18 points)
                    double fontSizeInInches = 0.25;

                    // Add the watermark text covering the full page
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                    page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, "Calibri", "#A5A5A5", fontSizeInInches);
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }