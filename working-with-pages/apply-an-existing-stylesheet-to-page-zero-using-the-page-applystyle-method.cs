using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (page zero)
            Page page = diagram.Pages[0];

            // Verify that the diagram contains at least one stylesheet
            if (diagram.StyleSheets.Count == 0)
            {
                Console.WriteLine("The diagram contains no stylesheets.");
                return;
            }

            // Use the first stylesheet in the collection
            StyleSheet styleSheet = diagram.StyleSheets[0];
            // Cast the stylesheet ID from long to int as required by ApplyStyle
            int styleId = (int)styleSheet.ID;

            // Apply the stylesheet to the page (line, fill, and text styles)
            page.ApplyStyle(styleId, styleId, styleId);

            // Output Visio file path
            string outputPath = "output.vsdx";

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Stylesheet applied to page zero and diagram saved.");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}