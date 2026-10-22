using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Define the deprecated font name and its replacement.
    private const string DeprecatedFontName = "OldFont";
    private const string ReplacementFontName = "NewFont";

    static void Main(string[] args)
    {
        // Expect a single argument: the path to the folder containing VSD/VSDX files.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Gather all Visio files with supported extensions.
        string[] vsdFiles = Directory.GetFiles(folderPath, "*.vsd");
        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx");
        string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx");
        string[] allFiles = vsdFiles.Concat(vsdxFiles).Concat(vdxFiles).ToArray();

        if (allFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        foreach (string filePath in allFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate over all fonts used in the diagram.
                foreach (Font font in diagram.Fonts)
                {
                    // If the font matches the deprecated name, replace it.
                    if (string.Equals(font.Name, DeprecatedFontName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Substitute with the modern equivalent.
                        font.Name = ReplacementFontName;
                        Console.WriteLine($"Replaced font in '{Path.GetFileName(filePath)}': '{DeprecatedFontName}' -> '{ReplacementFontName}'");
                    }
                }

                // Determine output file path (save as VSDX with a suffix to avoid overwriting original).
                string directory = Path.GetDirectoryName(filePath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_updated.vsdx");

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Saved updated diagram to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}