using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the XPS output
                string outputPath = "output.xps";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure XPS save options to exclude hidden pages
                    XPSSaveOptions xpsOptions = new XPSSaveOptions
                    {
                        ExportHiddenPage = false
                    };

                    // Save the diagram as XPS using the configured options
                    diagram.Save(outputPath, xpsOptions);
                }

                Console.WriteLine("Diagram exported to XPS successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }