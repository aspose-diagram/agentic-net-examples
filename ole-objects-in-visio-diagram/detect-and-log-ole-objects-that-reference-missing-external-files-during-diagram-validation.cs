using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument.
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Retrieve the source path of the OLE object, if any.
                        string sourcePath = shape.ForeignData.ObjectSourceFullName;

                        // If a source path is defined but the file does not exist, log it.
                        if (!string.IsNullOrEmpty(sourcePath) && !File.Exists(sourcePath))
                        {
                            Console.WriteLine($"Missing OLE external file detected:");
                            Console.WriteLine($"  Page ID   : {page.ID}");
                            Console.WriteLine($"  Shape ID  : {shape.ID}");
                            Console.WriteLine($"  Source    : {sourcePath}");
                        }
                    }
                }
            }

            // Dispose the diagram to release resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}