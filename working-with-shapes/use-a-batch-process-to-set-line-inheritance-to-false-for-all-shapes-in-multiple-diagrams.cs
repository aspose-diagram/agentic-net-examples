using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: check if the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Supported Visio file extensions
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vss", ".vstm", ".vst" };

        // Get all files with supported extensions
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath);
            if (Array.IndexOf(extensions, ext, 0, extensions.Length) < 0)
                continue; // Skip unsupported files

            // Guard: ensure the file exists
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Disable line inheritance by assigning current line color to itself
                        // This breaks the inheritance chain as per Aspose guidelines
                        shape.Line.LineColor.Value = shape.Line.LineColor.Value;
                    }
                }

                // Determine appropriate save format based on original extension
                SaveFileFormat format = GetSaveFormat(ext);

                // Save the modified diagram, overwriting the original file
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extension to SaveFileFormat enum
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstm":
                return SaveFileFormat.Vstm;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if unknown extension
                return SaveFileFormat.Vsdx;
        }
    }
}