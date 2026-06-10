using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path to the output Visio file
                const string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Desired fixed page size (A4 in inches)
                    const double pageWidthInches = 8.27;
                    const double pageHeightInches = 11.69;

                    // Iterate through all pages and apply settings
                    foreach (Page page in diagram.Pages)
                    {
                        // Disable automatic page expansion (auto‑expand)
                        page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                        // Set a fixed page width and height
                        page.PageSheet.PageProps.PageWidth.Value = pageWidthInches;
                        page.PageSheet.PageProps.PageHeight.Value = pageHeightInches;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }