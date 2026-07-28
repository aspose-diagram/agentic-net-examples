using System.IO;
using System;
using Aspose.Diagram;

class UpdateOleDisplayNames
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Generate timestamp prefix (e.g., 20230721153045)
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE objects by checking for foreign data
                    if (shape.ForeignData != null)
                    {
                        // Prefix the display name (NameU) with the timestamp
                        shape.NameU = $"{timestamp}_{shape.NameU}";
                    }
                }
            }

            // Save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
