using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing Visio files
            string folderPath;
            if (args.Length > 0)
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path to the folder with Visio files: ");
                folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Supported Visio extensions
            string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx", "*.vtx",
                                          "*.vssx", "*.vss", "*.vstx", "*.vst",
                                          "*.vssm", "*.vstm", "*.vssm", "*.vstm" };

            // Collect all matching files
            var visioFiles = new System.Collections.Generic.List<string>();
            foreach (var ext in extensions)
            {
                visioFiles.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.TopDirectoryOnly));
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

                    // Ensure at least one window exists; if not, create a default drawing window
                    if (diagram.Windows.Count == 0)
                    {
                        Window defaultWindow = new Window
                        {
                            WindowType = WindowTypeValue.Drawing,
                            WindowState = WindowStateValue.Maximized,
                            WindowWidth = 1100,
                            WindowHeight = 700
                        };
                        diagram.Windows.Add(defaultWindow);
                    }

                    // Toggle ShowRulers for each window
                    foreach (Window win in diagram.Windows)
                    {
                        win.ShowRulers = (win.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;
                    }

                    // Determine appropriate save format based on file extension
                    SaveFileFormat format = GetSaveFormat(Path.GetExtension(filePath));

                    // Overwrite the original file with the updated settings
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Operation completed.");
        }

        // Maps file extensions to the corresponding SaveFileFormat enum values
        private static SaveFileFormat GetSaveFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case ".vsdx": return SaveFileFormat.Vsdx;
                case ".vsd":  return SaveFileFormat.Vsd;
                case ".vdx":  return SaveFileFormat.Vdx;
                case ".vsx":  return SaveFileFormat.Vsx;
                case ".vtx":  return SaveFileFormat.Vtx;
                case ".vssx": return SaveFileFormat.Vssx;
                case ".vss":  return SaveFileFormat.Vss;
                case ".vstx": return SaveFileFormat.Vstx;
                case ".vst":  return SaveFileFormat.Vst;
                case ".vssm": return SaveFileFormat.Vssm;
                case ".vstm": return SaveFileFormat.Vstm;
                default:      return SaveFileFormat.Vsdx; // Fallback to VSDX
            }
        }
    }