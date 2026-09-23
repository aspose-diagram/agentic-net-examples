using System.IO;
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
            string sourcePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Index of the page that contains a triangle shape
                int trianglePageIndex = -1;

                // Iterate through pages to find the first page with a triangle shape
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and check its name
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            trianglePageIndex = i;
                            break;
                        }
                    }

                    if (trianglePageIndex != -1)
                        break;
                }

                if (trianglePageIndex == -1)
                {
                    Console.WriteLine("No triangle shape found in any page.");
                    return;
                }

                // Create a new diagram containing only the identified page
                using (Diagram singlePageDiagram = new Diagram())
                {
                    // Remove the default empty page
                    if (singlePageDiagram.Pages.Count > 0)
                    {
                        Page defaultPage = singlePageDiagram.Pages[0];
                        singlePageDiagram.Pages.Remove(defaultPage);
                    }

                    // Add the page with the triangle to the new diagram
                    Page trianglePage = diagram.Pages[trianglePageIndex];
                    singlePageDiagram.Pages.Add(trianglePage);

                    // Prepare SVG save options
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    svgOptions.SaveFormat = SaveFileFormat.Svg;

                    // Save the new diagram as an SVG file
                    string outputPath = "triangle_page.svg";
                    singlePageDiagram.Save(outputPath, svgOptions);

                    Console.WriteLine($"Triangle page saved to '{outputPath}'.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
