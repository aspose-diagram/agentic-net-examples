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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx"; // replace with your source file path
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the first page that contains a triangle shape
                int pageIndex = -1;
                int currentIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses a master named "Triangle"
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            pageIndex = currentIndex;
                            break;
                        }
                    }

                    if (pageIndex != -1)
                        break;

                    currentIndex++;
                }

                if (pageIndex == -1)
                {
                    Console.WriteLine("No triangle shape found in any page.");
                    return;
                }

                // Export the identified page as an SVG file
                string outputPath = "triangle_page.svg"; // desired output file
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.PageIndex = pageIndex; // export only the found page
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine($"Page {pageIndex} containing a triangle was saved to '{outputPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
