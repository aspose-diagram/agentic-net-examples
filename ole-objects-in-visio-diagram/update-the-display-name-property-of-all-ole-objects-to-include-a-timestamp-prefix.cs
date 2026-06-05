using System.IO;
using System;
using Aspose.Diagram;

class UpdateOleDisplayNames
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Generate timestamp prefix
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains OLE (ForeignData) information
                    if (shape.ForeignData != null && !string.IsNullOrEmpty(shape.ForeignData.ObjectSourceFullName))
                    {
                        // Prepend timestamp to the shape's display name (Name property)
                        shape.Name = $"{timestamp}_{shape.Name}";
                    }
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
