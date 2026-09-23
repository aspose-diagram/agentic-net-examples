using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            // args[0] = input Visio file path
            // args[1] = output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramExportWithMetadata <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                ExportDiagramWithPageDimensions(inputPath, outputPath);
                Console.WriteLine($"Diagram exported successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Loads a Visio diagram, embeds page dimension metadata into the document header,
        /// and saves the diagram to the specified output path.
        /// </summary>
        /// <param name="inputPath">Path to the source Visio file.</param>
        /// <param name="outputPath">Path where the modified file will be saved.</param>
        static void ExportDiagramWithPageDimensions(string inputPath, string outputPath)
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page to obtain its dimensions (in inches)
            // If the document has multiple pages, you could iterate and build a composite header.
            if (diagram.Pages.Count == 0)
                throw new InvalidOperationException("The diagram contains no pages.");

            Page firstPage = diagram.Pages[0];
            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

            // Build a header string that includes page number placeholder (&p) and dimensions
            // The &p field will be replaced by the actual page number during rendering/printing.
            string headerText = $"Page: &p   Size: {pageWidth:F2} x {pageHeight:F2} inches";

            // Assign the header text to the center part of the global header/footer
            diagram.HeaderFooter.HeaderCenter = headerText;

            // Optional: style the header/footer text (font face, size, weight)
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Calibri";
            headerFont.Height = -16; // Approx. 12pt (negative value per API spec)
            headerFont.Weight = 700; // Bold

            // Save the diagram using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }