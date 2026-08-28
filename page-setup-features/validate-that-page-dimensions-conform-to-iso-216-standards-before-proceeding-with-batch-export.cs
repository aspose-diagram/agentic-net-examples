using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // ISO 216 A‑series sizes in inches (width x height)
        private static readonly (double Width, double Height)[] ASeriesInches = new (double, double)[]
        {
            (33.11, 46.81), // A0
            (23.39, 33.11), // A1
            (16.54, 23.39), // A2
            (11.69, 16.54), // A3
            (8.27, 11.69),  // A4
            (5.83, 8.27)    // A5
        };

        // Tolerance for floating‑point comparison (in inches)
        private const double Tolerance = 0.01;

        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";

                // Load the diagram (using a using block to ensure disposal)
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Validate each page size against ISO 216 A‑series dimensions
                    ValidatePageSizes(diagram);

                    // After successful validation, export the diagram to PDF
                    ExportToPdf(diagram, "output.pdf");
                }

                Console.WriteLine("Batch export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Checks that every page in the diagram matches one of the ISO 216 A‑series sizes
        /// (allowing portrait or landscape orientation). Throws an exception if a page
        /// does not conform.
        /// </summary>
        /// <param name="diagram">The loaded Diagram instance.</param>
        private static void ValidatePageSizes(Diagram diagram)
        {
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                bool matches = false;
                foreach (var (stdWidth, stdHeight) in ASeriesInches)
                {
                    // Check portrait orientation
                    if (Math.Abs(width - stdWidth) <= Tolerance && Math.Abs(height - stdHeight) <= Tolerance)
                    {
                        matches = true;
                        break;
                    }
                    // Check landscape orientation (swap width/height)
                    if (Math.Abs(width - stdHeight) <= Tolerance && Math.Abs(height - stdWidth) <= Tolerance)
                    {
                        matches = true;
                        break;
                    }
                }

                if (!matches)
                {
                    string message = $"Page {pageIndex} size ({width:F2}\" x {height:F2}\") does not conform to ISO 216 A‑series dimensions.";
                    throw new Exception(message);
                }

                pageIndex++;
            }
        }

        /// <summary>
        /// Exports the entire diagram to a PDF file using default PDF save options.
        /// </summary>
        /// <param name="diagram">The Diagram to export.</param>
        /// <param name="outputPath">The file path for the exported PDF.</param>
        private static void ExportToPdf(Diagram diagram, string outputPath)
        {
            // Configure PDF save options (e.g., set a default font to avoid missing‑font issues)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
            };

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);
        }
    }