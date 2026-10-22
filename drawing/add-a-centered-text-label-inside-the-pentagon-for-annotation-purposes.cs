using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "PentagonWithLabel.vsdx";

        // Guard: ensure we can write to the output directory
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());

            // Use the first (and only) page as the active page
            Page page = diagram.ActivePage;

            // Define position for the pentagon (center of the page can be adjusted as needed)
            double pinX = 5.0;
            double pinY = 5.0;

            // Add a pentagon shape to the page; the fourth argument isCalculate is set to false
            long pentagonId = page.AddShape(pinX, pinY, "Pentagon", false);

            // Retrieve the newly added pentagon shape
            Shape pentagon = page.Shapes.GetShape(pentagonId);

            // Clear any existing text (if any) and add a centered label
            pentagon.Text.Value.Clear();
            pentagon.Text.Value.Add(new Txt("Annotation"));

            // Set vertical alignment to middle to center the text vertically within the shape
            pentagon.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}