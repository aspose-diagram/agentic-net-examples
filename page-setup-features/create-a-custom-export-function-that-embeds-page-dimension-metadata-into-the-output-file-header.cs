using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Example usage:
                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_with_metadata.vsdx";

                ExportDiagramWithPageMetadata(inputPath, outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Loads a Visio diagram, embeds page dimension metadata into custom document properties
        /// and the footer, then saves the diagram.
        /// </summary>
        /// <param name="inputFile">Path to the source Visio file.</param>
        /// <param name="outputFile">Path where the modified Visio file will be saved.</param>
        static void ExportDiagramWithPageMetadata(string inputFile, string outputFile)
        {
            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputFile))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Retrieve the first page (you can adjust to target a specific page)
                Page page = diagram.Pages[0];

                // Get page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Create custom document properties for width and height
                CustomProp widthProp = new CustomProp
                {
                    Name = "PageWidth",
                    PropType = PropType.String,
                    CustomValue = { ValueString = pageWidth.ToString() }
                };

                CustomProp heightProp = new CustomProp
                {
                    Name = "PageHeight",
                    PropType = PropType.String,
                    CustomValue = { ValueString = pageHeight.ToString() }
                };

                // Add custom properties to the document
                diagram.DocumentProps.CustomProps.Add(widthProp);
                diagram.DocumentProps.CustomProps.Add(heightProp);

                // Embed the dimensions into the footer (right side)
                diagram.HeaderFooter.FooterRight = $"Page Size: {pageWidth} x {pageHeight} inches";

                // Save the diagram with metadata
                diagram.Save(outputFile, SaveFileFormat.Vsdx);
            }
        }
    }