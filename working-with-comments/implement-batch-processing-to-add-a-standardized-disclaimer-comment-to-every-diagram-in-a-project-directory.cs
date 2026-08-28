using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchDisclaimerAdder
{
    // Standardized disclaimer text to be added to each diagram page
    private const string DisclaimerText = "Disclaimer: This diagram is confidential and intended for internal use only.";

    // Position where the comment will be placed on each page (in page units)
    private const double CommentPinX = 1.0;
    private const double CommentPinY = 1.0;

    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the project directory containing diagram files
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the directory containing diagram files.");
            return;
        }

        string projectDirectory = args[0];

        if (!Directory.Exists(projectDirectory))
        {
            Console.WriteLine($"Directory does not exist: {projectDirectory}");
            return;
        }

        // Supported Visio file extensions
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx" };

        // Process each diagram file found in the directory (non‑recursive)
        foreach (string ext in extensions)
        {
            foreach (string filePath in Directory.GetFiles(projectDirectory, ext, SearchOption.TopDirectoryOnly))
            {
                try
                {
                    // Load the diagram using the appropriate constructor
                    Diagram diagram = new Diagram(filePath);

                    // Add the disclaimer comment to every page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        page.AddComment(CommentPinX, CommentPinY, DisclaimerText);
                    }

                    // Save the diagram back to the original file, preserving its original format
                    // Determine the original format based on file extension
                    SaveFileFormat format = GetSaveFormatFromExtension(Path.GetExtension(filePath));
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
    }

    // Helper method to map file extensions to Aspose.Diagram SaveFileFormat values
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
            default:
                // Default to VSDX if unknown
                return SaveFileFormat.Vsdx;
        }
    }
}
