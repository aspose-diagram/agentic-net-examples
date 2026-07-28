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
            string outputRoot = "output";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Create a subfolder for the current page
                    string pageFolder = Path.Combine(outputRoot, SanitizeFileName(page.Name));
                    Directory.CreateDirectory(pageFolder);

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a file name for the shape SVG
                        string shapeFileName = $"Shape_{shape.ID}.svg";
                        string shapePath = Path.Combine(pageFolder, shapeFileName);

                        // Export the shape to SVG using Aspose.Diagram's ToSvg method
                        shape.ToSvg(shapePath, new SVGSaveOptions());
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to ensure folder names are valid for the file system
    static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }
}
