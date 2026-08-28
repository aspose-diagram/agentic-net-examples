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
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Create HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Example of setting available options
                htmlOptions.ExportHiddenPage = false;          // Do not export hidden pages
                htmlOptions.SaveAsSingleFile = true;           // Save all pages into a single HTML file
                htmlOptions.Title = "Custom Diagram Export";   // Set a custom title for the HTML page

                // NOTE: Aspose.Diagram does not provide a property to set a custom CSS class prefix.
                // The HTML output uses internal class naming conventions that cannot be overridden via the API.

                // Save the diagram as HTML using the configured options
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }