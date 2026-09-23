using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a timestamp prefix (e.g., 20230921153045)
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE objects: must be a foreign shape with Object foreign type
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Preserve the original display name
                        string originalName = shape.Name;

                        // Update the display name with the timestamp prefix
                        shape.Name = $"{timestamp}_{originalName}";
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
