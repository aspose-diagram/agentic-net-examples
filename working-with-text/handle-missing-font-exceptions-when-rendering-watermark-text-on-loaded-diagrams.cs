using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Ensure the system font folder is added for Aspose.Diagram font lookup
        string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        // The SetFontFolder method requires two parameters: path and recursive flag
        FontConfigs.SetFontFolder(systemFontFolder, true);

        // Set a default fallback font to avoid missing‑font rendering issues
        FontConfigs.DefaultFontName = "Arial";

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram from '{inputPath}': {ex.Message}");
            return;
        }

        // Validate fonts used in the diagram against installed system fonts
        // Collect installed font names (case‑insensitive)
        InstalledFontCollection installedFonts = new InstalledFontCollection();
        var installedFontNames = installedFonts.Families
            .Select(f => f.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Iterate over diagram fonts using explicit type (no var)
        foreach (Aspose.Diagram.Font diagramFont in diagram.Fonts)
        {
            string fontName = diagramFont.Name;
            if (!installedFontNames.Contains(fontName))
            {
                Console.WriteLine($"Warning: Font '{fontName}' used in diagram is not installed on the system.");
            }
        }

        // Add a watermark text shape to the first page
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram contains no pages.");
            return;
        }

        Aspose.Diagram.Page page = diagram.Pages[0];

        // Retrieve page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Center position for the watermark
        double pinX = pageWidth / 2.0;
        double pinY = pageHeight / 2.0;

        // Watermark text properties
        string watermarkText = "CONFIDENTIAL";
        string watermarkFont = "Arial"; // Fallback font guaranteed to exist
        string watermarkColor = "#808080"; // Light gray
        double watermarkSizeInPoints = 36; // 36 pt
        double watermarkSizeInInches = watermarkSizeInPoints / 72.0; // Convert points to inches

        // Verify the chosen watermark font is installed; otherwise use the default fallback
        if (!installedFontNames.Contains(watermarkFont))
        {
            Console.WriteLine($"Warning: Watermark font '{watermarkFont}' not installed. Using default font '{FontConfigs.DefaultFontName}'.");
            watermarkFont = FontConfigs.DefaultFontName;
        }

        // Attempt to add the watermark shape; handle any font‑related exceptions gracefully
        Shape watermarkShape;
        try
        {
            watermarkShape = page.AddText(
                pinX,
                pinY,
                pageWidth,
                pageHeight,
                watermarkText,
                watermarkFont,
                watermarkColor,
                watermarkSizeInInches);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add watermark shape: {ex.Message}");
            return;
        }

        // Optional: rotate the watermark (e.g., 45 degrees). Text rotation uses radians.
        double rotationDegrees = 45.0;
        double rotationRadians = (Math.PI / 180.0) * rotationDegrees;
        watermarkShape.TextXForm.TxtAngle.Value = rotationRadians;

        // Save the diagram to PDF with a default font fallback
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";

        try
        {
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram to '{outputPath}': {ex.Message}");
        }
    }
}