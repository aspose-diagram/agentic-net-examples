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

            // Certificate placeholder – actual signing is not supported by Aspose.Diagram API
            // string certificatePath = @"C:\Certificates\mycert.pfx";
            // string certificatePassword = "password";

            // Get all Visio files (VSDX and VSDM) in the folder
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsdm")
                {
                    continue; // Skip non-Visio files
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Access the VBA project (read‑only)
                    VbaProject vbaProject = diagram.VbaProject;

                    // Check if the VBA project is already signed
                    bool isSigned = vbaProject.IsSigned;
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} – VBA Signed: {isSigned}");

                    // NOTE: Aspose.Diagram does not provide a method to sign a VBA project.
                    // If signing were supported, it would be performed here using the common certificate.

                    // Save the diagram back in a macro‑enabled format to preserve VBA
                    string outputPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(filePath) + "_signed.vsdm");
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