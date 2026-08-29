using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: use first argument or current directory
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Visio file extensions to process
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vssx", "*.vss", "*.vstx", "*.vst", "*.vtx", "*.vsdm", "*.vssm", "*.vstm" };
        var files = new System.Collections.Generic.List<string>();

        foreach (var ext in extensions)
        {
            files.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.TopDirectoryOnly));
        }

        if (files.Count == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        foreach (var filePath in files)
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Rotate shapes whose universal name is "Arrow"
                        if (shape.NameU == "Arrow")
                        {
                            // Add 90 degrees (π/2 radians) to the current rotation
                            shape.XForm.Angle.Value += Math.PI / 2;
                        }
                    }
                }

                // Save the modified diagram, overwriting the original file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }
}
