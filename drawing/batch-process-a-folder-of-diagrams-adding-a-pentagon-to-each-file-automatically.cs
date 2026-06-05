using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Specify the folder containing Visio diagram files.
            // You can pass the folder path as a command‑line argument; otherwise use a default path.
            string folderPath = args.Length > 0 ? args[0] : @"C:\VisioDiagrams";

            // Process each Visio file in the folder (supports common extensions).
            foreach (string filePath in Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsx")
                    continue; // Skip non‑Visio files.

                // Load the diagram using the constructor that accepts a file name.
                Diagram diagram = new Diagram(filePath);

                // Add a pentagon shape to the active page.
                // PinX and PinY are set to 2.0 inches; adjust as needed.
                // The built‑in master name for a pentagon is "Pentagon".
                diagram.ActivePage.AddShape(2.0, 2.0, "Pentagon");

                // Save the modified diagram back to the same file.
                // Using VDX format for simplicity; change if you need to preserve the original format.
                diagram.Save(filePath, SaveFileFormat.Vdx);

                // Release resources.
                diagram.Dispose();
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
