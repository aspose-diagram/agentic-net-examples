using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path – use first command‑line argument if provided, otherwise a default path.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (you can adjust the index as needed).
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build the SVG file name using the shape's unique ID.
                    string svgFileName = $"{shape.ID}.svg";

                    // Export the individual shape to SVG.
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    shape.ToSvg(svgFileName, svgOptions);

                    Console.WriteLine($"Exported shape ID {shape.ID} to {svgFileName}");
                }

                Console.WriteLine("Export completed.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }