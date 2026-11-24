using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the folder containing diagram files.");
            return;
        }

        string folderPath = args[0];

        // Verify the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define supported diagram extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };

        // Get all files in the folder with supported extensions.
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            // Skip files that do not have a supported extension.
            if (Array.IndexOf(supportedExtensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check if the shape's NameU matches "Arrow" (case-insensitive).
                            if (!string.IsNullOrEmpty(shape.NameU) &&
                                shape.NameU.Equals("Arrow", StringComparison.OrdinalIgnoreCase))
                            {
                                // Rotate the shape by 90 degrees.
                                shape.XForm.Angle.Value = 90.0;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}