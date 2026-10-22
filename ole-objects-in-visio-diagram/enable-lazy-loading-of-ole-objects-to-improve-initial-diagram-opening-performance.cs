using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio diagram path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // TODO: Enable lazy loading of OLE objects if supported by Aspose.Diagram API.
            // Currently, Aspose.Diagram does not expose a direct setting for lazy loading OLE data.
            // As a workaround, load the diagram normally and defer accessing ForeignData.ObjectData
            // until the OLE content is actually needed.

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Example of lazy OLE data access (commented out; uncomment when needed)
            // foreach (Aspose.Diagram.Page page in diagram.Pages)
            // {
            //     foreach (Aspose.Diagram.Shape shape in page.Shapes)
            //     {
            //         if (shape.ForeignData != null)
            //         {
            //             // OLE data is accessed only when required
            //             byte[] oleData = shape.ForeignData.ObjectData;
            //             // Process oleData here...
            //         }
            //     }
            // }

            // Optional: save the diagram after any modifications
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}