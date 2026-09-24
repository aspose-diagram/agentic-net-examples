using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram instance.
        Diagram diagram = new Diagram();

        // Ensure the diagram contains at least one page.
        if (diagram.Pages.Count == 0)
        {
            // Add a blank page when none exist.
            diagram.Pages.Add(new Page());
        }

        // Prepare the current date string for the header.
        string currentDate = DateTime.Now.ToString("d");

        // Iterate over each page to add a header shape.
        foreach (Page page in diagram.Pages)
        {
            // Retrieve page dimensions (in inches) for positioning.
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define header dimensions and calculate its center position.
            double headerHeight = 0.5;               // Header height in inches.
            double headerWidth = pageWidth;          // Full page width.
            double pinX = pageWidth / 2.0;           // Horizontal center.
            double pinY = pageHeight - (headerHeight / 2.0); // Near top edge.

            // Add a text shape containing the current date.
            // AddText returns a Shape object.
            Shape headerShape = page.AddText(pinX, pinY, headerWidth, headerHeight, currentDate);

            // Optional: set vertical alignment to middle (horizontal alignment is default centered by pin position).
            headerShape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
        }

        // Define the output file path.
        string outputPath = "DiagramWithHeaders.vsdx";

        // Guard to ensure the output directory exists (no input file to check).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory not found: {outputDir}");
            return;
        }

        // Save the modified diagram inside a try/catch to capture any Aspose errors.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}