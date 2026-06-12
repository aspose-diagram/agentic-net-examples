using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first connector shape (1‑D shape)
            Shape connector = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.OneD) // Connectors are 1‑D shapes
                {
                    connector = shape;
                    break;
                }
            }

            if (connector == null)
            {
                throw new Exception("No connector shape found in the diagram.");
            }

            // Change the connector style to curved lines
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Save the modified diagram as an image to observe the rendering
            string outputPath = "output.png";
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine("Connector style changed to curved and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
