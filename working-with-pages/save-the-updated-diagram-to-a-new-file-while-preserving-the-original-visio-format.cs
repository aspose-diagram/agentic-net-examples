using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine the appropriate SaveFileFormat based on the output file extension.
        SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(outputPath));
        if (format == 0) // enum default (invalid) value
        {
            Console.Error.WriteLine("Unsupported output file extension. Supported extensions: .vsdx, .vsd, .vdx, .vsx, .vssx, .vstx, .vss, .vst, .vtx");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save the diagram preserving the Visio format.
                diagram.Save(outputPath, format);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}' using format {format}.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding SaveFileFormat enum value.
    /// Returns 0 (invalid) if the extension is not recognized.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return 0;

        switch (extension.Trim().ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".vtx":
                return SaveFileFormat.Vtx;
            default:
                return 0; // Unknown format
        }
    }
}