using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // OLE objects are represented by ForeignData.
                        if (shape.ForeignData != null)
                        {
                            // Retrieve the embedded OLE binary data (if any).
                            byte[] oleData = shape.ForeignData.ObjectData;

                            // If there is no embedded data, the shape may reference an external file.
                            if (oleData == null || oleData.Length == 0)
                            {
                                // Attempt to retrieve the source file path, if available.
                                string sourcePath = shape.ForeignData.ObjectSourceFullName;

                                // If a source path is defined, verify its existence.
                                if (!string.IsNullOrEmpty(sourcePath) && !File.Exists(sourcePath))
                                {
                                    Console.WriteLine("Missing external OLE reference:");
                                    Console.WriteLine($"  Page ID    : {page.ID}");
                                    Console.WriteLine($"  Shape ID   : {shape.ID}");
                                    Console.WriteLine($"  Source Path: {sourcePath}");
                                }
                                else if (string.IsNullOrEmpty(sourcePath))
                                {
                                    // No source path information – still consider it a potentially missing reference.
                                    Console.WriteLine("OLE shape without embedded data and without source path (possible missing file):");
                                    Console.WriteLine($"  Page ID  : {page.ID}");
                                    Console.WriteLine($"  Shape ID : {shape.ID}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}