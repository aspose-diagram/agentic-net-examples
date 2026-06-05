using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the folder containing Visio files.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input folder path as the first argument.");
            return;
        }

        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder does not exist: {inputFolder}");
            return;
        }

        // Process all supported Visio files in the folder.
        string[] supportedExtensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vssx", "*.vstx", "*.vsdm", "*.vssm", "*.vstm" };
        foreach (string ext in supportedExtensions)
        {
            foreach (string filePath in Directory.GetFiles(inputFolder, ext, SearchOption.TopDirectoryOnly))
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Determine if the shape is marked as read‑only.
                            // Here we treat a shape as read‑only if its Delete protection is locked.
                            if (shape.Protection.LockDelete.Value == BOOL.True)
                            {
                                // Disable an event cell (using EventDrop as a representative example).
                                shape.Event.EventDrop.Ufe.F = "";
                            }
                        }
                    }

                    // Save the modified diagram to a new file (suffix "_updated").
                    string directory = Path.GetDirectoryName(filePath);
                    string filenameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_updated.vsdx");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Saved updated file to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}