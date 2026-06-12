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
            string visioFilePath = @"C:\VisioFiles\sample.vsdx";

            // Root folder where SVG files will be stored
            string outputRootFolder = @"C:\VisioFiles\SVG_Output";

            // Ensure the root output folder exists
            Directory.CreateDirectory(outputRootFolder);

            // Load the Visio diagram using the provided constructor
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Create a subfolder for the current page (hierarchical structure)
                    string pageFolder = Path.Combine(outputRootFolder, page.Name);
                    Directory.CreateDirectory(pageFolder);

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for the shape SVG
                        string shapeFileName = $"shape_{shape.ID}.svg";
                        string shapeFilePath = Path.Combine(pageFolder, shapeFileName);

                        // Create SVG save options (default options are sufficient for basic export)
                        SVGSaveOptions svgOptions = new SVGSaveOptions();

                        // Export the shape to SVG using the provided ToSvg method
                        shape.ToSvg(shapeFilePath, svgOptions);
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
