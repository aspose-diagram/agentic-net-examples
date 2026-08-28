using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect a folder path as the first argument
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the folder path containing Visio files as an argument.");
                return;
            }

            string folderPath = args[0];
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Supported Visio extensions
            var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx",
                ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm",
                ".vss", ".vst"
            };

            // Get all files with supported extensions
            var files = Directory.GetFiles(folderPath);
            var visioFiles = new List<string>();
            foreach (var file in files)
            {
                if (supportedExtensions.Contains(Path.GetExtension(file)))
                {
                    visioFiles.Add(file);
                }
            }

            if (visioFiles.Count == 0)
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            foreach (var filePath in visioFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply Landscape orientation to every page
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Determine appropriate SaveFileFormat based on file extension
                    SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(filePath));

                    // Save back to the same file (overwrites original)
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Processed and saved: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
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
                case ".vsx":
                    return SaveFileFormat.Vsx;
                case ".vtx":
                    return SaveFileFormat.Vtx;
                case ".vssx":
                    return SaveFileFormat.Vssx;
                case ".vstx":
                    return SaveFileFormat.Vstx;
                case ".vsdm":
                    return SaveFileFormat.Vsdm;
                case ".vssm":
                    return SaveFileFormat.Vssm;
                case ".vstm":
                    return SaveFileFormat.Vstm;
                case ".vss":
                    return SaveFileFormat.Vss;
                case ".vst":
                    return SaveFileFormat.Vst;
                default:
                    // Default to Vsdx if unknown
                    return SaveFileFormat.Vsdx;
            }
        }
    }