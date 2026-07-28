using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load a Visio file into a memory stream (replace with your own source stream as needed)
            string filePath = "sample.vsdx"; // Example file path; adjust as necessary
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    memoryStream.Position = 0; // Reset stream position before loading

                    // Load the diagram from the memory stream
                    using (Diagram diagram = new Diagram(memoryStream))
                    {
                        EnumerateOleObjects(diagram);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void EnumerateOleObjects(Diagram diagram)
    {
        // Iterate through all pages and shapes to find OLE objects
        foreach (Aspose.Diagram.Page page in diagram.Pages)
        {
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                // Verify the shape is a foreign OLE object
                if (shape.Type == TypeValue.Foreign &&
                    shape.ForeignData != null &&
                    shape.ForeignData.ForeignType == ForeignType.Object)
                {
                    byte[] oleData = shape.ForeignData.ObjectData;

                    if (oleData != null && oleData.Length > 0)
                    {
                        Console.WriteLine($"Page \"{page.Name}\" - Shape ID {shape.ID} contains OLE data ({oleData.Length} bytes).");
                    }
                    else
                    {
                        Console.WriteLine($"Page \"{page.Name}\" - Shape ID {shape.ID} is an OLE placeholder with no data.");
                    }
                }
            }
        }
    }
}
