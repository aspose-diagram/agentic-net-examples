using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Retrieve source and destination directories from environment variables
        string sourceDir = Environment.GetEnvironmentVariable("SOURCE_DIR");
        string destDir   = Environment.GetEnvironmentVariable("DEST_DIR");

        if (string.IsNullOrWhiteSpace(sourceDir) || string.IsNullOrWhiteSpace(destDir))
        {
            Console.WriteLine("Please set both SOURCE_DIR and DEST_DIR environment variables.");
            return;
        }

        // Build full file paths (adjust file names as needed)
        string sourceFile = Path.Combine(sourceDir, "input.vdx");
        string destFile   = Path.Combine(destDir, "output.vdx");

        if (!File.Exists(sourceFile))
        {
            Console.WriteLine($"Source file not found: {sourceFile}");
            return;
        }

        // Load the diagram from the source file
        Diagram diagram = new Diagram(sourceFile);

        // Optionally configure font folders (example: use a FONT_DIR env variable)
        string fontDir = Environment.GetEnvironmentVariable("FONT_DIR");
        if (!string.IsNullOrWhiteSpace(fontDir) && Directory.Exists(fontDir))
        {
            // Set the font folder for the diagram (recursive scan)
            diagram.FontDirs = new[] { fontDir };
        }

        // Ensure the destination directory exists
        Directory.CreateDirectory(destDir);

        // Save the diagram to the destination path in VDX format
        diagram.Save(destFile, SaveFileFormat.Vdx);

        Console.WriteLine($"Diagram successfully saved to: {destFile}");
    }
}
