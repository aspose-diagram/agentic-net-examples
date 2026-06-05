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

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is a foreign OLE object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Retrieve the embedded OLE binary data
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Ensure the data is present
                        if (oleData != null && oleData.Length > 0)
                        {
                            // Wrap the binary data in a MemoryStream as per guidelines
                            using (MemoryStream ms = new MemoryStream(oleData))
                            {
                                // Output basic information about the OLE object
                                Console.WriteLine($"Found OLE object on Page '{page.Name}' (ID: {page.ID})");
                                Console.WriteLine($"  Shape ID: {shape.ID}");
                                Console.WriteLine($"  Data size: {ms.Length} bytes");
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
