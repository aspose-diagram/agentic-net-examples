using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Create a timestamp prefix (e.g., 20230820153045)
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // OLE objects are represented as shapes with ForeignData
                    if (shape.ForeignData != null)
                    {
                        // Preserve the existing display name (using the Shape's Name property)
                        string originalName = shape.Name;

                        // Update the display name to include the timestamp prefix
                        shape.Name = $"{timestamp}_{originalName}";
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
