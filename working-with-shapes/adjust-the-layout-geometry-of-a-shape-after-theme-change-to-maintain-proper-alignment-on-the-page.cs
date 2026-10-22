using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first shape on the page (skip master shapes)
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                // Ensure the shape is not a master placeholder
                if (s.Master != null)
                {
                    targetShape = s;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found on the page.");
                return;
            }

            // Store original geometry values before applying the theme
            double originalPinX = targetShape.XForm.PinX.Value;
            double originalPinY = targetShape.XForm.PinY.Value;
            double originalWidth = targetShape.XForm.Width.Value;
            double originalHeight = targetShape.XForm.Height.Value;

            // Apply a preset theme to the page
            // Note: PresetTheme and PresetThemeVariant are write‑only properties
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // After the theme change, re‑apply the original geometry to keep alignment
            targetShape.XForm.PinX.Value = originalPinX;
            targetShape.XForm.PinY.Value = originalPinY;
            targetShape.XForm.Width.Value = originalWidth;
            targetShape.XForm.Height.Value = originalHeight;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}