using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Name of the user-defined cell to update
        private const string TargetUserCellName = "MyUserCell";

        // New value to assign to the cell
        private const string NewCellValue = "12345";

        static void Main(string[] args)
        {
            // Expect the folder path as the first argument
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: VisioBatchUpdater <folderPath>");
                return;
            }

            string folderPath = args[0];

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Process supported Visio file extensions
            string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vss", ".vstx", ".vst", ".vsdm", ".vssm", ".vstm" };

            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(supportedExtensions, ext) < 0)
                {
                    // Skip unsupported files
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    bool anyChange = false;

                    // Iterate all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Iterate user-defined cells
                            foreach (User userCell in shape.Users)
                            {
                                // Match by Name or universal NameU
                                if (string.Equals(userCell.Name, TargetUserCellName, StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(userCell.NameU, TargetUserCellName, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Update the cell value
                                    userCell.Value.Val = NewCellValue;
                                    anyChange = true;
                                    Console.WriteLine($"Updated cell '{TargetUserCellName}' in shape ID {shape.ID} on page '{page.Name}' of file '{Path.GetFileName(filePath)}'.");
                                }
                            }
                        }
                    }

                    if (anyChange)
                    {
                        // Determine the appropriate SaveFileFormat based on the original extension
                        SaveFileFormat format = GetSaveFormatFromExtension(ext);
                        // Overwrite the original file
                        diagram.Save(filePath, format);
                        Console.WriteLine($"File saved: {filePath}");
                    }
                    else
                    {
                        Console.WriteLine($"No matching user-defined cell found in file: {Path.GetFileName(filePath)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }
        }

        // Maps file extensions to the corresponding SaveFileFormat enum value
        private static SaveFileFormat GetSaveFormatFromExtension(string extension)
        {
            return extension switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd"  => SaveFileFormat.Vsd,
                ".vdx"  => SaveFileFormat.Vdx,
                ".vsx"  => SaveFileFormat.Vsx,
                ".vtx"  => SaveFileFormat.Vtx,
                ".vssx" => SaveFileFormat.Vssx,
                ".vss"  => SaveFileFormat.Vss,
                ".vstx" => SaveFileFormat.Vstx,
                ".vst"  => SaveFileFormat.Vst,
                ".vsdm" => SaveFileFormat.Vsdm,
                ".vssm" => SaveFileFormat.Vssm,
                ".vstm" => SaveFileFormat.Vstm,
                _ => SaveFileFormat.Vsdx // Default fallback
            };
        }
    }