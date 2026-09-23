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

        // Validate that the environment variables are set
        if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destDir))
        {
            Console.WriteLine("Please set SOURCE_DIR and DEST_DIR environment variables.");
            return;
        }

        // Define the diagram file name (adjust as needed)
        string diagramFileName = "example.vdx";

        // Build full paths
        string sourcePath = Path.Combine(sourceDir, diagramFileName);
        string destPath   = Path.Combine(destDir, diagramFileName);

        // Load the diagram from the source directory (using Aspose.Diagram load rule)
        Diagram diagram = new Diagram(sourcePath);

        // Perform any processing on the diagram here (if required)

        // Save the diagram to the destination directory (using Aspose.Diagram save rule)
        diagram.Save(destPath, SaveFileFormat.Vdx);

        Console.WriteLine($"Diagram successfully copied from '{sourcePath}' to '{destPath}'.");
    }
}
