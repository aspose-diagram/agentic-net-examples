using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // 0 - input Visio file path
            // 1 - output Visio file path
            // 2 - page index (0‑based)
            // 3 - new page width (in inches)
            // 4 - new page height (in inches)

            if (args.Length != 5)
            {
                Console.WriteLine("Usage: DiagramPageResizer <inputPath> <outputPath> <pageIndex> <widthInches> <heightInches>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!int.TryParse(args[2], out int pageIndex))
            {
                Console.WriteLine("Invalid page index.");
                return;
            }
            if (!double.TryParse(args[3], out double newWidth))
            {
                Console.WriteLine("Invalid width value.");
                return;
            }
            if (!double.TryParse(args[4], out double newHeight))
            {
                Console.WriteLine("Invalid height value.");
                return;
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page index
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.WriteLine($"Page index out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Access the specified page
                Page page = diagram.Pages[pageIndex];

                // Resize the page
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;

                // Save the modified diagram (using VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Page {pageIndex} resized to {newWidth} x {newHeight} inches and saved to '{outputPath}'.");
            }
        }
    }