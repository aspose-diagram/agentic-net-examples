using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files. Change as needed or pass as first argument.
        string folderPath = args.Length > 0 ? args[0] : @"C:\VisioFiles";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Process common Visio extensions
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vssx", "*.vss", "*.vstx", "*.vst" };
        foreach (string ext in extensions)
        {
            foreach (string filePath in Directory.GetFiles(folderPath, ext))
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    // Load the diagram from file
                    Diagram diagram = new Diagram(filePath);

                    // Ensure at least one window exists; if not, create a default drawing window
                    if (diagram.Windows.Count == 0)
                    {
                        Window newWindow = new Window
                        {
                            WindowType = WindowTypeValue.Drawing,
                            WindowState = WindowStateValue.Maximized,
                            WindowWidth = 800,
                            WindowHeight = 600
                        };
                        diagram.Windows.Add(newWindow);
                    }

                    // Toggle ShowRulers for each window in the document
                    foreach (Window win in diagram.Windows)
                    {
                        win.ShowRulers = win.ShowRulers == BOOL.True ? BOOL.False : BOOL.True;
                    }

                    // Save back to the original file, preserving the original format when possible
                    SaveFileFormat format = filePath.EndsWith(".vsdx", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vsdx :
                                            filePath.EndsWith(".vsd", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vsd :
                                            filePath.EndsWith(".vdx", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vdx :
                                            filePath.EndsWith(".vssx", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vssx :
                                            filePath.EndsWith(".vss", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vss :
                                            filePath.EndsWith(".vstx", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vstx :
                                            filePath.EndsWith(".vst", StringComparison.OrdinalIgnoreCase) ? SaveFileFormat.Vst :
                                            SaveFileFormat.Vsdx; // fallback

                    diagram.Save(filePath, format);
                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }
    }
}