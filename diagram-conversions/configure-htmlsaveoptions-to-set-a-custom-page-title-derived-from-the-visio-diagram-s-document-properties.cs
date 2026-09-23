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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Derive a custom title from existing document properties
            // Example: combine the original title with the creation date
            string originalTitle = diagram.DocumentProps.Title ?? "Untitled";
            DateTime created = diagram.DocumentProps.TimeCreated;
            string customTitle = $"{originalTitle} (Created on {created:yyyy-MM-dd})";

            // Set the new title; HTML export uses this title for the page
            diagram.DocumentProps.Title = customTitle;

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false,
                IsExportComments = false
            };

            // Save the diagram as HTML with the custom title
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
