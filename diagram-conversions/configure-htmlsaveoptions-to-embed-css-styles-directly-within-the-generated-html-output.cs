using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the generated HTML file
            string outputPath = "output.html";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options to embed CSS (and other resources) into a single HTML file
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // When true, all CSS styles and images are embedded directly in the HTML,
                // producing a self‑contained file without external resources.
                SaveAsSingleFile = true
            };

            // Save the diagram as HTML with the configured options
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
