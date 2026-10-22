using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Entry point of the console application
    static void Main(string[] args)
    {
        // Determine the folder to process: from first argument or user input
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Define supported Visio extensions
        string[] supportedExtensions = new string[]
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx",
            ".vssx", ".vss", ".vstx", ".vst"
        };

        // Retrieve all files with supported extensions
        string[] allFiles = Directory.GetFiles(folderPath);
        foreach (string filePath in allFiles)
        {
            // Filter by extension
            string ext = Path.GetExtension(filePath);
            bool isSupported = false;
            foreach (string supExt in supportedExtensions)
            {
                if (string.Equals(ext, supExt, StringComparison.OrdinalIgnoreCase))
                {
                    isSupported = true;
                    break;
                }
            }
            if (!isSupported)
                continue; // Skip non‑Visio files

            // Guard: ensure the file exists before processing
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("No pages found in diagram: " + filePath);
                    continue;
                }

                // Access the first page (index 0)
                Page firstPage = diagram.Pages[0];

                // Set the page height to 14 inches
                firstPage.PageSheet.PageProps.PageHeight.Value = 14.0;

                // Determine the appropriate save format based on file extension
                SaveFileFormat saveFormat = GetSaveFormat(ext);

                // Save the modified diagram back to the original file
                diagram.Save(filePath, saveFormat);

                Console.WriteLine("Processed successfully: " + filePath);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if the extension is unrecognized
                return SaveFileFormat.Vsdx;
        }
    }
}