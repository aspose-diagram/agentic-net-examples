using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path for the exported SVG file
                const string outputPath = "first_triangle_page.svg";

                // Load the diagram inside a using block to ensure resources are released
                using (Diagram diagram = new Diagram(inputPath))
                {
                    int trianglePageIndex = -1;

                    // Iterate through pages to locate the first page that contains a triangle shape
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        Page page = diagram.Pages[i];
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has a master and compare its name to "Triangle"
                            if (shape.Master != null && string.Equals(shape.Master.Name, "Triangle", StringComparison.OrdinalIgnoreCase))
                            {
                                trianglePageIndex = i;
                                break;
                            }
                        }

                        if (trianglePageIndex != -1)
                            break;
                    }

                    // If no triangle was found, abort with an informative message
                    if (trianglePageIndex == -1)
                    {
                        Console.WriteLine("No triangle shape was found in any page of the diagram.");
                        return;
                    }

                    // Configure SVG export options to render only the identified page
                    SVGSaveOptions svgOptions = new SVGSaveOptions
                    {
                        PageIndex = trianglePageIndex,
                        ExportHiddenPage = false,
                        DefaultFont = "Arial"
                    };

                    // Save the selected page as an SVG file
                    diagram.Save(outputPath, svgOptions);
                    Console.WriteLine($"Page {trianglePageIndex + 1} containing a triangle was saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }