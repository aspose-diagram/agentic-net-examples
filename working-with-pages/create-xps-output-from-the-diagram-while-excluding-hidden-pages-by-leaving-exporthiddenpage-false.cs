using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";

                // Output XPS file path
                string outputPath = "output.xps";

                // Load the Visio diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Configure XPS save options to exclude hidden pages
                XPSSaveOptions xpsOptions = new XPSSaveOptions
                {
                    ExportHiddenPage = false
                };

                // Save the diagram as XPS using the configured options
                diagram.Save(outputPath, xpsOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }