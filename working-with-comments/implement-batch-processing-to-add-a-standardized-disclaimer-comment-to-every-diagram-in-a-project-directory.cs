using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramDisclaimerBatchProcessor
{
    // Standardized disclaimer text to be added to each diagram page
    private const string DisclaimerText = "Disclaimer: This diagram is confidential and intended for internal use only.";

    // Entry point
    static void Main(string[] args)
    {
        // Expect the first argument to be the root directory containing Visio files
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the project directory.");
            return;
        }

        string projectDirectory = args[0];
        if (!Directory.Exists(projectDirectory))
        {
            Console.WriteLine($"Directory does not exist: {projectDirectory}");
            return;
        }

        ProcessDirectory(projectDirectory);
        Console.WriteLine("Processing completed.");
    }

    // Processes all Visio files in the given directory (recursively)
    private static void ProcessDirectory(string rootPath)
    {
        // Visio file extensions to consider
        string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vssx", ".vss", ".vstx", ".vst" };

        // Get all files matching the extensions
        var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            if (Array.Exists(visioExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                AddDisclaimerToDiagram(file);
            }
        }
    }

    // Loads a diagram, adds the disclaimer comment to each page, and saves it back
    private static void AddDisclaimerToDiagram(string filePath)
    {
        // Load the diagram using the appropriate constructor
        using (var diagram = new Diagram(filePath))
        {
            // Add disclaimer comment to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // PinX and PinY coordinates (center of the page). Adjust as needed.
                double pinX = 0.5;
                double pinY = 0.5;

                page.AddComment(pinX, pinY, DisclaimerText);
            }

            // Determine save format based on file extension
            SaveFileFormat saveFormat = GetSaveFormatFromExtension(Path.GetExtension(filePath));

            // Save the diagram back to the original file using the appropriate overload
            diagram.Save(filePath, saveFormat);
        }
    }

    // Maps file extensions to Aspose.Diagram.SaveFileFormat values
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension.ToLowerInvariant())
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
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to VDX if unknown
                return SaveFileFormat.Vdx;
        }
    }
}
