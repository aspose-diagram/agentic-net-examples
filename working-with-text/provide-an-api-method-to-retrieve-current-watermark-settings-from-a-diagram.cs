using System;
using Aspose.Diagram;

public static class DiagramWatermarkHelper
{
    /// <summary>
    /// Retrieves the text of the first shape that appears to be a watermark.
    /// The method looks for a shape that either has a custom property named "Watermark"
    /// or whose universal name contains the word "watermark". If such a shape is found,
    /// its concatenated text is returned; otherwise, null is returned.
    /// </summary>
    /// <param name="diagramPath">Full path to the Visio diagram file.</param>
    /// <returns>Watermark text if found; otherwise, null.</returns>
    public static string GetWatermarkText(string diagramPath)
    {
        // Load the diagram from the specified file.
        using var diagram = new Diagram(diagramPath);

        // Iterate through all pages in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Check for a custom property named "Watermark".
                bool hasWatermarkProp = false;
                foreach (Prop prop in shape.Props)
                {
                    if (prop.NameU != null &&
                        prop.NameU.Equals("Watermark", StringComparison.OrdinalIgnoreCase))
                    {
                        hasWatermarkProp = true;
                        break;
                    }
                }

                // Retrieve the plain text of the shape.
                string shapeText = shape.Text?.Value?.ToString();

                // Determine if this shape qualifies as a watermark.
                bool nameIndicatesWatermark = shape.NameU != null &&
                    shape.NameU.IndexOf("watermark", StringComparison.OrdinalIgnoreCase) >= 0;

                if (!string.IsNullOrWhiteSpace(shapeText) && (hasWatermarkProp || nameIndicatesWatermark))
                {
                    // Return the first matching watermark text.
                    return shapeText;
                }
            }
        }

        // No watermark shape was found.
        return null;
    }
}

// Example usage:
class Program
{
    static void Main()
    {
        try
        {

            string diagramFile = @"C:\Diagrams\sample.vsdx";

            string watermark = DiagramWatermarkHelper.GetWatermarkText(diagramFile);

            if (watermark != null)
            {
                Console.WriteLine($"Watermark found: {watermark}");
            }
            else
            {
                Console.WriteLine("No watermark detected in the diagram.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}