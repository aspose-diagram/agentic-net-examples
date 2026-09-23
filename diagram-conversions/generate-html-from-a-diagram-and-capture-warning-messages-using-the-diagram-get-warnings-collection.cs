using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "sample.vsdx";

                // Output HTML file path
                string outputPath = "sample.html";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Do not export hidden pages
                    ExportHiddenPage = false,
                    // Set a default font to avoid font‑related warnings
                    DefaultFont = "Arial"
                };

                // Save the diagram as HTML
                diagram.Save(outputPath, htmlOptions);

                // Note: Aspose.Diagram does not expose a warnings collection.
                // Warnings (e.g., missing fonts) are handled internally; they cannot be retrieved programmatically.
                Console.WriteLine($"Diagram exported to HTML successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }