using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Get folder path from command line or ask the user
            string folderPath;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path of the folder containing Visio files: ");
                folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Folder does not exist. Exiting.");
                    return;
                }
            }

            // Supported Visio extensions
            string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vssx", "*.vss", "*.vstx", "*.vst" };

            // Collect all files matching the extensions
            var visioFiles = new System.Collections.Generic.List<string>();
            foreach (var ext in extensions)
            {
                visioFiles.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.AllDirectories));
            }

            if (visioFiles.Count == 0)
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            Console.WriteLine($"Found {visioFiles.Count} Visio file(s). Processing...");

            foreach (var filePath in visioFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Remove hidden information (including unused masters) and macros to reduce size
                    diagram.RemoveHiddenInformation(0);
                    diagram.RemoveMacro();

                    // Determine the appropriate SaveFileFormat based on original extension
                    SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(filePath));

                    // Overwrite the original file with the cleaned diagram
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Processed: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }

        // Maps file extensions to the corresponding SaveFileFormat enum values
        private static SaveFileFormat GetSaveFileFormat(string extension)
        {
            switch (extension.ToLowerInvariant())
            {
                case ".vsdx":
                    return SaveFileFormat.Vsdx;
                case ".vsd":
                    return SaveFileFormat.Vsd;
                case ".vdx":
                    return SaveFileFormat.Vdx;
                case ".vssx":
                    return SaveFileFormat.Vssx;
                case ".vss":
                    return SaveFileFormat.Vss;
                case ".vstx":
                    return SaveFileFormat.Vstx;
                case ".vst":
                    return SaveFileFormat.Vst;
                default:
                    // Default to Vsdx if unknown
                    return SaveFileFormat.Vsdx;
            }
        }
    }