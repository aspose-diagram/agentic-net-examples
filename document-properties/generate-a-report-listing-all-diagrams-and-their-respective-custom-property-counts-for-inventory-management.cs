using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing Visio diagram files
        string folderPath = "Diagrams"; // Change this path as needed

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all files in the folder
        string[] allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

        // Define supported Visio file extensions
        string[] supportedExtensions = new string[]
        {
            ".vsdx", ".vsd", ".vdx",
            ".vssx", ".vss", ".vtx",
            ".vstx", ".vst", ".vssm",
            ".vsdm", ".vstm"
        };

        // Process each file
        foreach (string filePath in allFiles)
        {
            string extension = Path.GetExtension(filePath);
            bool isSupported = false;

            // Check if the file has a supported extension
            foreach (string supExt in supportedExtensions)
            {
                if (string.Equals(extension, supExt, StringComparison.OrdinalIgnoreCase))
                {
                    isSupported = true;
                    break;
                }
            }

            if (!isSupported)
                continue; // Skip unsupported files

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Count custom document properties
                int customPropCount = diagram.DocumentProps.CustomProps.Count;

                // Output the result
                Console.WriteLine($"Diagram: {Path.GetFileName(filePath)} - Custom Properties: {customPropCount}");

                // Dispose the diagram to free resources
                diagram.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }
}
