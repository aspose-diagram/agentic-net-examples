using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            // args[0] = input Visio file path
            // args[1] = output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramExportWithMetadata <input.vsdx> <output.pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                ExportDiagramWithPageMetadata(inputPath, outputPath);
                Console.WriteLine($"Diagram exported successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during export: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Loads a Visio diagram, embeds page dimension metadata into the document header,
        /// and saves the diagram to the specified output file.
        /// </summary>
        /// <param name="inputFile">Path to the source Visio file.</param>
        /// <param name="outputFile">Path where the exported file will be saved.</param>
        static void ExportDiagramWithPageMetadata(string inputFile, string outputFile)
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputFile);

            // Retrieve the first page (index 0) to obtain its dimensions
            // Use explicit typing as required by the rules
            Page firstPage = diagram.Pages[0];

            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;   // inches
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value; // inches

            // Embed dimensions into the global header (left side)
            // Header/Footer strings support Visio wildcards; we use plain text here
            diagram.HeaderFooter.HeaderLeft = $"Page Size: {pageWidth:F2} x {pageHeight:F2} inches";

            // Optionally, also store dimensions as custom document properties
            // This demonstrates another way to embed metadata
            var customProps = diagram.DocumentProps.CustomProps;
            // Remove existing properties with the same names if they exist
            for (int i = customProps.Count - 1; i >= 0; i--)
            {
                var prop = customProps[i];
                if (prop.Name == "PageWidth" || prop.Name == "PageHeight")
                {
                    customProps.Remove(prop);
                }
            }

            // Add new custom properties for width and height
            var widthProp = new CustomProp();
            widthProp.Name = "PageWidth";
            widthProp.PropType = PropType.String;
            widthProp.CustomValue.ValueString = pageWidth.ToString("F2");
            customProps.Add(widthProp);

            var heightProp = new CustomProp();
            heightProp.Name = "PageHeight";
            heightProp.PropType = PropType.String;
            heightProp.CustomValue.ValueString = pageHeight.ToString("F2");
            customProps.Add(heightProp);

            // Prepare PDF save options (you can choose other formats if needed)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Ensure the format is explicitly set (required by some overloads)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Set a default font to avoid missing font warnings
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram with the header/footer and custom properties embedded
            diagram.Save(outputFile, pdfOptions);
        }
    }