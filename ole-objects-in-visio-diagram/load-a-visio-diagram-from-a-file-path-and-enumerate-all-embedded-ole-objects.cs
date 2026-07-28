using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify OLE (foreign) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                    {
                        // Verify the foreign type is an embedded object
                        if (shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Ensure the OLE binary data exists
                            if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                            {
                                Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, OLE size: {shape.ForeignData.ObjectData.Length} bytes");
                            }
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
