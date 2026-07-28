using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = @"C:\Diagrams\sample.vsdx";

                // Name of the master whose shapes should be exported
                string targetMasterName = "Rectangle";

                // Output folder for HTML files
                string outputFolder = @"C:\Diagrams\ExportedHtml";

                // Ensure the output directory exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses the specified master
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Build a unique file name for each shape
                            string htmlFile = Path.Combine(outputFolder, $"Shape_{shape.ID}.html");

                            // Use default HTML save options (PNG images are embedded by default)
                            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                            // Export the shape to HTML while preserving its formatting
                            shape.ToHTML(htmlFile, htmlOptions);

                            Console.WriteLine($"Exported shape ID {shape.ID} to {htmlFile}");
                        }
                    }
                }

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }