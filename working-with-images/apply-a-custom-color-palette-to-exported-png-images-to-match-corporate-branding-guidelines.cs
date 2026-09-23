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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Corporate color palette (hex strings)
            string primaryColor = "#003366";   // Dark blue
            string secondaryColor = "#FFCC00"; // Gold

            // Apply the palette to all shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Example rule: rectangles get the primary color, others get secondary
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        shape.Fill.FillPattern.Value = 1;               // Solid fill
                        shape.Fill.FillForegnd.Value = primaryColor;    // Primary color
                    }
                    else
                    {
                        shape.Fill.FillPattern.Value = 1;               // Solid fill
                        shape.Fill.FillForegnd.Value = secondaryColor;  // Secondary color
                    }
                }
            }

            // Export each page as a PNG image using the corporate palette
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i;      // Export current page
                pngOptions.PageCount = 1;      // Single page per file
                pngOptions.Resolution = 300f;  // Optional: higher resolution

                string outputPath = $"output_page_{i + 1}.png";
                diagram.Save(outputPath, pngOptions);
            }

            Console.WriteLine("PNG export with corporate color palette completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
