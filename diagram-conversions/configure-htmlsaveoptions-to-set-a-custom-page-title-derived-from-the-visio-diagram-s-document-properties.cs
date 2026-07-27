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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Derive a custom title from the built‑in document properties
            string title = diagram.DocumentProps.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                // Fallback to the file name (without extension) if the Title property is empty
                title = System.IO.Path.GetFileNameWithoutExtension(inputPath);
            }

            // Configure HTML save options with the custom title
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.Title = title; // sets the page title in the generated HTML

            // Save the diagram as HTML using the configured options
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
