using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Retrieve source and destination directories from environment variables
        string sourceDir = Environment.GetEnvironmentVariable("SOURCE_DIR");
        string destDir = Environment.GetEnvironmentVariable("DEST_DIR");

        // Validate that the required environment variables are set
        if (string.IsNullOrWhiteSpace(sourceDir) || string.IsNullOrWhiteSpace(destDir))
        {
            Console.WriteLine("Please set both SOURCE_DIR and DEST_DIR environment variables.");
            return;
        }

        // Build full file paths (adjust file names as needed)
        string sourceFile = Path.Combine(sourceDir, "input.vsdx");
        string destFile   = Path.Combine(destDir,   "output.vsdx");

        // Load the diagram from the source file
        Diagram diagram = new Diagram(sourceFile);

        // Optional: configure a custom fonts folder if FONT_DIR is provided
        string fontDir = Environment.GetEnvironmentVariable("FONT_DIR");
        if (!string.IsNullOrWhiteSpace(fontDir))
        {
            // Set the fonts folder for the diagram (non‑recursive scan)
            diagram.FontDirs = new[] { fontDir };
        }

        // Save the diagram to the destination path
        diagram.Save(destFile, SaveFileFormat.Vsdx);
    }
}
