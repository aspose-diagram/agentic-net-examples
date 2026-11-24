using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Verify that at least one file path is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <relative-or-absolute-path-to-visio-file> [additional files...]");
            return;
        }

        foreach (string inputPath in args)
        {
            // Resolve the path to an absolute path.
            string absolutePath = Path.GetFullPath(inputPath);

            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(absolutePath))
            {
                Console.Error.WriteLine($"File not found: {absolutePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(absolutePath))
                {
                    // Example operation: output basic information about the diagram.
                    Console.WriteLine($"Loaded diagram: {Path.GetFileName(absolutePath)}");
                    Console.WriteLine($"  Pages count: {diagram.Pages.Count}");
                    Console.WriteLine($"  Shapes count (first page): {(diagram.Pages.Count > 0 ? diagram.Pages[0].Shapes.Count : 0)}");

                    // Prepare an output path – same directory, with "_copy" suffix.
                    string directory = Path.GetDirectoryName(absolutePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(absolutePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_copy.vsdx");

                    // Save the diagram in VSDX format using the required overload.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"  Saved copy to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Write any Aspose or IO errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{absolutePath}': {ex.Message}");
            }
        }
    }
}