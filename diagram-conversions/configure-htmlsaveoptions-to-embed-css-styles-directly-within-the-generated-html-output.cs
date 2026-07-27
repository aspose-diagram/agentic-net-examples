using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options to embed CSS and other resources into a single HTML file
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // When true, all CSS, images, and scripts are embedded, producing a self‑contained HTML file
                    SaveAsSingleFile = true,

                    // Optional: set a title for the generated HTML
                    Title = "Embedded CSS HTML Export"
                };

                // Save the diagram as HTML with the configured options
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram successfully exported to {outputPath} with embedded CSS.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }