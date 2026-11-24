using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: folder path containing diagrams and path to a certificate file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramsFolderPath> <CertificateFilePath>");
            return;
        }

        string folderPath = args[0];
        string certificatePath = args[1];

        // Validate folder existence.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Validate certificate file existence (certificate is not used because signing is not supported via Aspose.Diagram).
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Get all Visio files that can contain VBA projects.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Process only supported Visio extensions.
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsdm" && extension != ".vsx" && extension != ".vssx")
            {
                continue;
            }

            // Guard for file existence (redundant after GetFiles but kept per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Access the VBA project (read‑only).
                    VbaProject vbaProject = diagram.VbaProject;

                    // Output basic VBA project information.
                    Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  VBA Project Name : {vbaProject.Name}");
                    Console.WriteLine($"  Is Signed        : {vbaProject.IsSigned}");

                    // NOTE: Aspose.Diagram does not provide an API to sign a VBA project.
                    // The following save operation preserves the existing VBA project.
                    // If signing were supported, this would be the place to apply the certificate.

                    // Save the diagram in a macro‑enabled format to keep VBA content.
                    string outputPath = Path.Combine(folderPath,
                        Path.GetFileNameWithoutExtension(filePath) + "_processed.vsdm");
                    diagram.Save(outputPath, SaveFileFormat.Vsdm);
                    Console.WriteLine($"  Saved processed diagram to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}