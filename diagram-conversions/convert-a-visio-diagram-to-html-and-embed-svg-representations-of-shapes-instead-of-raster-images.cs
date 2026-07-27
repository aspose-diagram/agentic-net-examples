using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate input arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioToHtmlSvg <inputVisioFile> <outputHtmlFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Export all pages (default), do not export hidden pages
                ExportHiddenPage = false,
                // Do not include comments in the HTML output
                IsExportComments = false,
                // Generate a single HTML file with embedded resources
                SaveAsSingleFile = true
                // The HTML exporter will embed vector graphics (SVG) for shapes automatically
            };

            // Save the diagram as HTML with embedded SVG representations
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
        }
    }