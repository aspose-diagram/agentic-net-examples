using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToSvgBatch
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Root folder where SVG files will be stored
            string outputRoot = "output_svgs";

            // Load the Visio diagram from file
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Ensure the root output directory exists
                Directory.CreateDirectory(outputRoot);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Create a subfolder for the current page
                    string pageFolder = Path.Combine(outputRoot, page.Name);
                    Directory.CreateDirectory(pageFolder);

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a safe file name using shape ID and name (if available)
                        string shapeName = string.IsNullOrEmpty(shape.NameU) ? $"Shape{shape.ID}" : shape.NameU;
                        foreach (char c in Path.GetInvalidFileNameChars())
                            shapeName = shapeName.Replace(c, '_');

                        string svgPath = Path.Combine(pageFolder, $"{shape.ID}_{shapeName}.svg");

                        // Save the shape as SVG using the ToSvg method
                        SVGSaveOptions options = new SVGSaveOptions();
                        shape.ToSvg(svgPath, options);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
