using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output SVG file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            int trianglePageIndex = -1;

            // Iterate through pages to find the first page that contains a triangle shape.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Iterate through shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and a name.
                    if (shape.Master != null && shape.Master.Name != null)
                    {
                        // Check if the master name indicates a triangle.
                        if (shape.Master.Name.IndexOf("Triangle", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            trianglePageIndex = i;
                            break;
                        }
                    }
                }

                if (trianglePageIndex != -1)
                    break;
            }

            if (trianglePageIndex == -1)
            {
                Console.Error.WriteLine("No triangle shape found in any page of the diagram.");
                return;
            }

            // Configure SVG save options to export only the identified page.
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.PageIndex = trianglePageIndex; // zero‑based page index.
            // The SVGSaveOptions class in this version does not expose a PageCount property,
            // so we rely on PageIndex alone to export a single page.
            svgOptions.SVGFitToViewPort = true;       // fit SVG to viewport.

            // Save the selected page as an SVG file.
            diagram.Save(outputPath, svgOptions);
            Console.WriteLine($"Triangle page saved to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
