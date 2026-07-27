using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Create HTML save options.
                // Note: Aspose.Diagram.HTMLSaveOptions does not provide a property to set a custom CSS class prefix.
                // Therefore, we cannot configure a CSS class prefix directly.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Example of setting other available options (optional)
                // htmlOptions.SaveAsSingleFile = true; // Save all pages into a single HTML file
                // htmlOptions.ExportHiddenPage = false; // Do not export hidden pages

                // Save the diagram as HTML using the configured options
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }