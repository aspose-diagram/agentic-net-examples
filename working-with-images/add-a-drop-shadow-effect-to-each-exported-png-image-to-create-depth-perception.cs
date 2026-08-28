using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file.
                // Adjust the path as needed.
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Apply a simple drop shadow to every non‑deleted shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Enable simple shadow.
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                        // Shadow color (gray).
                        shape.Fill.ShdwForegnd.Value = "#808080";

                        // Shadow transparency (30% transparent).
                        shape.Fill.ShdwForegndTrans.Value = 0.3;

                        // Shadow offset (0.1 inch right and down).
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }

                    // Configure PNG export options for the current page.
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page.
                        PageIndex = pageIndex,
                        PageCount = 1,

                        // Optional: set resolution (dots per inch).
                        Resolution = 300f
                    };

                    // Build output file name, e.g., "Page_0.png", "Page_1.png", etc.
                    string outputPath = $"Page_{pageIndex}.png";

                    // Save the diagram (single page) as PNG.
                    diagram.Save(outputPath, pngOptions);
                }

                // Clean up resources.
                diagram.Dispose();

                Console.WriteLine("Export completed with drop shadows applied.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }