using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - input Visio file path
            // args[1] - page index (0-based)
            // args[2] - new page width (in inches)
            // args[3] - new page height (in inches)
            // args[4] - output Visio file path

            if (args.Length != 5)
            {
                Console.WriteLine("Usage: DiagramResizeTool <inputFile> <pageIndex> <widthInches> <heightInches> <outputFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[4];

            if (!int.TryParse(args[1], out int pageIndex))
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            if (!double.TryParse(args[2], out double newWidth))
            {
                Console.WriteLine("Invalid width value.");
                return;
            }

            if (!double.TryParse(args[3], out double newHeight))
            {
                Console.WriteLine("Invalid height value.");
                return;
            }

            try
            {
                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Validate page index
                    if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                    {
                        Console.WriteLine($"Page index out of range. Diagram has {diagram.Pages.Count} pages.");
                        return;
                    }

                    // Access the specified page
                    Page page = diagram.Pages[pageIndex];

                    // Set new dimensions (values are in inches)
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page {pageIndex} resized to {newWidth}x{newHeight} inches and saved to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }