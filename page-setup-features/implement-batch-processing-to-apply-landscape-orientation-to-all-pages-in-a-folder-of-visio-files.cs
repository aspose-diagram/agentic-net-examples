using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: use first argument or ask the user.
        string folderPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path of the folder containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Define supported Visio extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx" };

        // Retrieve all files with supported extensions.
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Process only files with the supported extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(extensions, ext) < 0)
                continue;

            // Guard: ensure the file still exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found (skipping): " + filePath);
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and set orientation to Landscape.
                foreach (Page page in diagram.Pages)
                {
                    // Set the print page orientation to Landscape.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Determine the appropriate SaveFileFormat based on the original extension.
                SaveFileFormat format;
                switch (ext)
                {
                    case ".vsdx":
                        format = SaveFileFormat.Vsdx;
                        break;
                    case ".vsd":
                        format = SaveFileFormat.Vsd;
                        break;
                    case ".vdx":
                        format = SaveFileFormat.Vdx;
                        break;
                    case ".vsx":
                        format = SaveFileFormat.Vsx;
                        break;
                    case ".vtx":
                        format = SaveFileFormat.Vtx;
                        break;
                    default:
                        // Fallback to Vsdx if somehow an unexpected extension appears.
                        format = SaveFileFormat.Vsdx;
                        break;
                }

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, format);
                Console.WriteLine("Processed and saved: " + filePath);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}