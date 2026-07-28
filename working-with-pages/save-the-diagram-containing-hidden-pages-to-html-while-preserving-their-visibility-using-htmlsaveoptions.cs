using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file (replace with actual file path)
                string inputPath = "input.vsdx";

                // Path for the exported HTML file
                string outputPath = "output.html";

                // Load the diagram from the file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure HTML export options
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        // Preserve hidden pages in the exported HTML
                        ExportHiddenPage = true
                    };

                    // Save the diagram as HTML with the specified options
                    diagram.Save(outputPath, htmlOptions);
                }

                Console.WriteLine("Diagram exported to HTML successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }