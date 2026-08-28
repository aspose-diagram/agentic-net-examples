using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output HTML file path
            string outputPath = "output.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Set custom title based on document properties (fallback if empty)
            string title = diagram.DocumentProps.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Untitled Diagram";
            }
            htmlOptions.Title = title;

            // Save the diagram as HTML with the custom title
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
