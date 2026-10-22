using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing Visio diagrams: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Define supported Visio file extensions.
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx", "*.vtx", "*.vssx", "*.vstx" };

        // Collect all diagram files matching the extensions.
        var diagramFiles = new System.Collections.Generic.List<string>();
        foreach (var ext in extensions)
        {
            diagramFiles.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.TopDirectoryOnly));
        }

        if (diagramFiles.Count == 0)
        {
            Console.WriteLine("No Visio diagram files found in the specified folder.");
            return;
        }

        // Process each diagram file.
        foreach (var filePath in diagramFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found (skipping): " + filePath);
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Disable line inheritance by assigning the current line color to itself.
                        // This breaks the inheritance chain for the line color.
                        shape.Line.LineColor.Value = shape.Line.LineColor.Value;
                    }
                }

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                // Release resources.
                diagram.Dispose();

                Console.WriteLine("Processed and saved: " + filePath);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}