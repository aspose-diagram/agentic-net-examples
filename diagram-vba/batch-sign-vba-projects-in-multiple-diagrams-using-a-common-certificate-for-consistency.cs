using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files to process
        string folderPath = @"C:\VisioFiles";

        // Certificate path (not used because Aspose.Diagram does not support signing VBA projects)
        string certificatePath = @"C:\Certificates\mycert.pfx";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each Visio file in the folder
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsdm" && ext != ".vsx" && ext != ".vtx")
            {
                continue; // Skip non-Visio files
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Access the VBA project (read‑only)
                VbaProject vba = diagram.VbaProject;

                // Check if the VBA project is already signed
                bool isSigned = vba.IsSigned;
                Console.WriteLine($"File: {Path.GetFileName(filePath)} - VBA Signed: {isSigned}");

                // NOTE: Aspose.Diagram does not provide an API to sign a VBA project.
                // The VbaProject.Sign() method does not exist, and IsSigned is read‑only.
                // Therefore, actual signing cannot be performed with the current library.

                // Save the diagram in a macro‑enabled format to preserve any VBA code
                string outputPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(filePath) + "_processed.vsdm");
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Saved processed file to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
