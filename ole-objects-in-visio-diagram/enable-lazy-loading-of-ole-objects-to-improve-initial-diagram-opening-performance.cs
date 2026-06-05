using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram. Lazy loading of OLE objects is the default behavior.
            Diagram diagram = new Diagram(inputPath);

            // Access OLE objects only when needed.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData?.ForeignType == ForeignType.Object)
                    {
                        byte[] oleData = shape.ForeignData.ObjectData;
                        Console.WriteLine($"Shape ID {shape.ID} OLE size: {oleData?.Length ?? 0} bytes");
                    }
                }
            }

            // Save the diagram without modifications.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}