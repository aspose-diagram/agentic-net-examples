using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Get input Visio file path
            string inputPath;
            // Get output root folder path
            string outputRoot;

            if (args.Length >= 2)
            {
                inputPath = args[0];
                outputRoot = args[1];
            }
            else
            {
                Console.Write("Enter the full path to the Visio file: ");
                inputPath = Console.ReadLine()?.Trim() ?? string.Empty;

                Console.Write("Enter the folder where SVG files will be saved: ");
                outputRoot = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            // Validate input file
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Visio file not found at '{inputPath}'.");
                return;
            }

            // Ensure output root exists
            if (!Directory.Exists(outputRoot))
            {
                Directory.CreateDirectory(outputRoot);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Use the page's universal name if available; otherwise fallback to page ID
                string pageName = !string.IsNullOrEmpty(page.NameU) ? page.NameU : $"Page_{page.ID}";
                string pageFolder = Path.Combine(outputRoot, pageName);
                Directory.CreateDirectory(pageFolder);

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build the SVG file name using the shape ID
                    string svgFileName = $"Shape_{shape.ID}.svg";
                    string svgPath = Path.Combine(pageFolder, svgFileName);

                    // Export the shape to SVG
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    shape.ToSvg(svgPath, svgOptions);
                }
            }

            Console.WriteLine("Batch SVG export completed successfully.");
        }
    }