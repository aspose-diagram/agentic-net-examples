using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: inputPath outputPath newWidthInInches newHeightInInches
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: DiagramPageThumbnailRefresh <inputPath> <outputPath> <widthInInches> <heightInInches>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
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
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and set the new dimensions
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;
                }

                // Refresh the diagram to update internal representations (including thumbnails)
                diagram.Refresh();

                // Save the updated diagram back to a Visio file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram page size updated and thumbnails refreshed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }