using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output_lazy_loaded.vsdx";

        try
        {
            // Load the diagram with default options.
            // Lazy loading of OLE objects is enabled by default in the library,
            // so no explicit property needs to be set.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to trigger lazy loading when needed.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check that the shape is a foreign (OLE) shape and contains embedded object data.
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Accessing ObjectData forces the lazy load of the OLE binary.
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Output basic information about the loaded OLE object.
                        Console.WriteLine($"Loaded OLE object from shape ID {shape.ID}, size {oleData?.Length ?? 0} bytes.");
                    }
                }
            }

            // Save the diagram; no special save options are required.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading, processing, or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}