using System;
using System.IO;
using Aspose.Diagram;

class OLEMissingFileDetector
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be validated
            string inputPath = "input.vsdx";

            // Load the diagram (lifecycle rule: load)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // OLE objects are represented by ForeignData within a shape
                    if (shape.ForeignData != null)
                    {
                        // Retrieve the external file reference for linked OLE objects
                        string sourceFile = shape.ForeignData.ObjectSourceFullName;

                        // If a source file is specified, check its existence
                        if (!string.IsNullOrEmpty(sourceFile) && !File.Exists(sourceFile))
                        {
                            // Log missing external file information
                            Console.WriteLine($"Missing OLE file: Page='{page.Name}', ShapeID={shape.ID}, Source='{sourceFile}'");
                        }
                    }
                }
            }

            // Save the diagram (no modifications made, but follows lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
