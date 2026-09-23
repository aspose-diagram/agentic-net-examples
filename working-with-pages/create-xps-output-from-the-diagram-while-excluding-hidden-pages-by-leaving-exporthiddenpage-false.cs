using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure XPS save options to exclude hidden pages
                XPSSaveOptions xpsOptions = new XPSSaveOptions();
                xpsOptions.ExportHiddenPage = false;

                // Save the diagram as XPS
                string outputPath = "output.xps";
                diagram.Save(outputPath, xpsOptions);

                Console.WriteLine($"Diagram exported to XPS successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }