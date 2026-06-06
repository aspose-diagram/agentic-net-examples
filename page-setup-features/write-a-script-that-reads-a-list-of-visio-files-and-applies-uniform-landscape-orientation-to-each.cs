using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing Visio files.
            // If a folder path is provided as a command‑line argument, use it; otherwise use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Supported Visio extensions and their corresponding SaveFileFormat values.
            var formatMap = new Dictionary<string, SaveFileFormat>(StringComparer.OrdinalIgnoreCase)
            {
                { ".vsdx", SaveFileFormat.Vsdx },
                { ".vsd",  SaveFileFormat.Vsd },
                { ".vdx",  SaveFileFormat.Vdx },
                { ".vsx",  SaveFileFormat.Vsx },
                { ".vtx",  SaveFileFormat.Vtx },
                { ".vssx", SaveFileFormat.Vssx },
                { ".vstx", SaveFileFormat.Vstx },
                { ".vsdm", SaveFileFormat.Vsdm },
                { ".vssm", SaveFileFormat.Vssm },
                { ".vstm", SaveFileFormat.Vstm },
                { ".vss",  SaveFileFormat.Vss },
                { ".vst",  SaveFileFormat.Vst }
            };

            // Gather all files with the supported extensions.
            var visioFiles = new List<string>();
            foreach (var ext in formatMap.Keys)
            {
                visioFiles.AddRange(Directory.GetFiles(folderPath, $"*{ext}", SearchOption.TopDirectoryOnly));
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
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply Landscape orientation to every page.
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Determine the appropriate SaveFileFormat based on the file extension.
                    string ext = Path.GetExtension(filePath);
                    if (!formatMap.TryGetValue(ext, out SaveFileFormat saveFormat))
                    {
                        Console.WriteLine($"Unsupported file format for file: {filePath}");
                        continue;
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, saveFormat);
                    Console.WriteLine($"Processed and saved: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
    }