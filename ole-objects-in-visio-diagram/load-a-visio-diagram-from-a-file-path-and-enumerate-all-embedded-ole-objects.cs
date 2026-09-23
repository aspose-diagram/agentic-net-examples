using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages and shapes to find embedded OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is a foreign (OLE) shape and contains an embedded object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Retrieve the binary OLE data
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Ensure the OLE data exists before processing
                        if (oleData != null && oleData.Length > 0)
                        {
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, OLE size: {oleData.Length} bytes");
                        }
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
