using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains foreign data (possible OLE object)
                    if (shape.ForeignData != null)
                    {
                        // Check if the foreign data represents a linked OLE object
                        if ((shape.ForeignData.ObjectType & ObjectType.LinkedObject) == ObjectType.LinkedObject)
                        {
                            // Get the external file path referenced by the OLE object
                            string sourcePath = shape.ForeignData.ObjectSourceFullName;

                            // If the path is set and the file does not exist, log the issue
                            if (!string.IsNullOrEmpty(sourcePath) && !File.Exists(sourcePath))
                            {
                                Console.WriteLine($"Missing OLE file on page '{page.Name}', shape ID {shape.ID}: {sourcePath}");
                            }
                        }
                    }
                }
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
