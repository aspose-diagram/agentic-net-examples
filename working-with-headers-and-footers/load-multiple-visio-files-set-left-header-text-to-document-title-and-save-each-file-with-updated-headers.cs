using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        // Determine the folder containing Visio files.
        // If a path is provided as a command‑line argument it is used,
        // otherwise the current working directory is processed.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Supported Visio extensions.
        string[] extensions = new[]
        {
            ".vsdx", ".vdx", ".vsd", ".vsx", ".vtx",
            ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm"
        };

        // Collect all files with the supported extensions.
        var files = Directory.GetFiles(folderPath)
                             .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                             .ToArray();

        foreach (var filePath in files)
        {
            // Load the diagram from the file.
            Diagram diagram = new Diagram(filePath);

            // Use the document title for the left header.
            // If the title is empty, fall back to the file name (without extension).
            string title = diagram.DocumentProps.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = Path.GetFileNameWithoutExtension(filePath);
            }

            diagram.HeaderFooter.HeaderLeft = title;

            // Choose the appropriate SaveFileFormat based on the file extension.
            SaveFileFormat format = GetSaveFormat(Path.GetExtension(filePath));

            // Overwrite the original file with the updated header.
            diagram.Save(filePath, format);
        }
    }

    // Maps a file extension to the corresponding SaveFileFormat enum value.
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vstm": return SaveFileFormat.Vstm;
            default:      return SaveFileFormat.Vsdx; // Default fallback.
        }
    }
}
