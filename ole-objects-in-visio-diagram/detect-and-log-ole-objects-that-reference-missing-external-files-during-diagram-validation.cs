using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null)
                    {
                        // Determine if the OLE object is a linked object
                        if ((shape.ForeignData.ObjectType & ObjectType.LinkedObject) == ObjectType.LinkedObject)
                        {
                            // Get the external file path referenced by the OLE object
                            string sourcePath = shape.ForeignData.ObjectSourceFullName;

                            // If the path is set and the file does not exist, log the issue
                            if (!string.IsNullOrEmpty(sourcePath) && !File.Exists(sourcePath))
                            {
                                Console.WriteLine($"Missing OLE reference - Page: '{page.Name}', Shape ID: {shape.ID}, File: '{sourcePath}'");
                            }
                        }
                    }
                }
            }

            // Save the diagram (optional, here just to demonstrate lifecycle compliance)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
