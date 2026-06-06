using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // ISO 216 A‑series sizes in inches (width x height)
        private static readonly List<(double Width, double Height)> Iso216Sizes = new()
        {
            (33.11, 46.81), // A0
            (23.39, 33.11), // A1
            (16.54, 23.39), // A2
            (11.69, 16.54), // A3
            (8.27, 11.69),  // A4
            (5.83, 8.27),   // A5
            (4.13, 5.83)    // A6 (optional)
        };

        private const double Tolerance = 0.01; // inches

        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";

                // Load the diagram
                using Diagram diagram = new Diagram(inputPath);

                // Validate each page size against ISO 216 standards
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    if (!IsIso216Size(width, height))
                    {
                        string msg = $"Page \"{page.Name}\" has non‑ISO dimensions: {width:F2}\" x {height:F2}\".";
                        throw new Exception(msg);
                    }
                }

                // All pages are valid – proceed with batch export (example: PDF)
                const string outputPath = "exported.pdf";
                diagram.Save(outputPath, SaveFileFormat.Pdf);
                Console.WriteLine("Export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Checks whether the given dimensions match any ISO 216 size (allowing portrait or landscape)
        private static bool IsIso216Size(double width, double height)
        {
            foreach (var (w, h) in Iso216Sizes)
            {
                if (Math.Abs(width - w) <= Tolerance && Math.Abs(height - h) <= Tolerance)
                    return true;
                if (Math.Abs(width - h) <= Tolerance && Math.Abs(height - w) <= Tolerance)
                    return true;
            }
            return false;
        }
    }