using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the folder containing Visio files (adjust as needed)
        string folderPath = @"C:\VisioFiles";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all Visio files (you can add other extensions if required)
        string[] files = Directory.GetFiles(folderPath, "*.vsdx");

        foreach (string filePath in files)
        {
            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure there is at least one window; if not, create a default one
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window
                    {
                        WindowType = WindowTypeValue.Drawing,
                        ShowGuides = BOOL.False
                    };
                    diagram.Windows.Add(newWindow);
                }
                else
                {
                    // Set ShowGuides to false for each existing window
                    foreach (Window win in diagram.Windows)
                    {
                        win.ShowGuides = BOOL.False;
                    }
                }

                // Save the diagram back to the same file (overwrites original)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }
}
