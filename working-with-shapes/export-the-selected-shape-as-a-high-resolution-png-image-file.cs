using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string diagramPath = "input.vsdx";

                // Output PNG file path
                string outputPngPath = "selectedShape.png";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape selectedShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        selectedShape = shape;
                        break;
                    }
                }

                if (selectedShape == null)
                {
                    throw new Exception("No selectable shape found on the page.");
                }

                // Configure high‑resolution PNG export options
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Set resolution in DPI (e.g., 300 DPI for high quality)
                    Resolution = 300f
                };

                // Export the selected shape to PNG
                selectedShape.ToImage(outputPngPath, pngOptions);

                Console.WriteLine($"Shape ID {selectedShape.ID} exported to '{outputPngPath}' with {pngOptions.Resolution} DPI.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }