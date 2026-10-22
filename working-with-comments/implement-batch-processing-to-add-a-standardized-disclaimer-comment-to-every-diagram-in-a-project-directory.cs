using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the target directory either from command‑line arguments or user input.
        string targetDirectory;
        if (args.Length > 0)
        {
            targetDirectory = args[0];
        }
        else
        {
            Console.Write("Enter the path to the project directory: ");
            targetDirectory = Console.ReadLine();
        }

        // Guard: ensure the directory exists.
        if (string.IsNullOrWhiteSpace(targetDirectory) || !Directory.Exists(targetDirectory))
        {
            Console.Error.WriteLine("The specified directory does not exist.");
            return;
        }

        // Define the file extensions that represent Visio diagrams.
        string[] diagramExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx" };

        // Retrieve all files with the supported extensions (search recursively).
        string[] allFiles = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);
        foreach (string filePath in allFiles)
        {
            // Process only files with a supported Visio extension.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(diagramExtensions, ext) < 0)
                continue;

            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Add the standardized disclaimer comment to every page at coordinates (0,0).
                    foreach (Page page in diagram.Pages)
                    {
                        // The AddComment method places a comment at the given X/Y location.
                        page.AddComment(0, 0, "Disclaimer: This diagram is confidential and intended for authorized personnel only.");
                    }

                    // Determine the appropriate SaveFileFormat based on the original file extension.
                    SaveFileFormat saveFormat;
                    switch (ext)
                    {
                        case ".vsdx":
                            saveFormat = SaveFileFormat.Vsdx;
                            break;
                        case ".vsd":
                            saveFormat = SaveFileFormat.Vsd;
                            break;
                        case ".vdx":
                            saveFormat = SaveFileFormat.Vdx;
                            break;
                        case ".vssx":
                            saveFormat = SaveFileFormat.Vssx;
                            break;
                        case ".vstx":
                            saveFormat = SaveFileFormat.Vstx;
                            break;
                        default:
                            // Fallback to Vsdx if the extension is unexpected.
                            saveFormat = SaveFileFormat.Vsdx;
                            break;
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, saveFormat);
                }

                Console.WriteLine($"Processed: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}