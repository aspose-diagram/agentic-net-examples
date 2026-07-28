using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Access the first page (adjust index if needed)
                    Page page = diagram.Pages[0];

                    // Store the current auto‑expand setting
                    var originalResize = page.PageSheet.PageProps.DrawingResizeType.Value;

                    // Temporarily disable auto‑expand
                    page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                    // Modify page dimensions (values are in inches)
                    page.PageSheet.PageProps.PageWidth.Value = 11.0;   // Width
                    page.PageSheet.PageProps.PageHeight.Value = 8.5;   // Height

                    // Re‑enable (restore) the original auto‑expand setting
                    page.PageSheet.PageProps.DrawingResizeType.Value = originalResize;

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }