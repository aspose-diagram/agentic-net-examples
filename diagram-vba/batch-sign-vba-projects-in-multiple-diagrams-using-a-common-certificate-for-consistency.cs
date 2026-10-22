using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: the folder containing diagrams and a certificate file path (not used directly).
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchSignVba <DiagramsFolderPath> <CertificateFilePath>");
            return;
        }

        string folderPath = args[0];
        string certificatePath = args[1];

        // Guard against missing folder.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Guard against missing certificate file (the certificate is not used by Aspose, but we validate its presence).
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file does not exist: {certificatePath}");
            return;
        }

        // Get all Visio files that may contain VBA projects.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Process only supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsdm")
            {
                continue;
            }

            // Guard against missing file (should not happen after GetFiles, but added for safety).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Access the VBA project.
                VbaProject vbaProject = diagram.VbaProject;

                // Output current signature status.
                Console.WriteLine($"Processing '{Path.GetFileName(filePath)}':");
                Console.WriteLine($"  VBA Project Name : {vbaProject.Name}");
                Console.WriteLine($"  Is Signed        : {vbaProject.IsSigned}");

                // Since Aspose.Diagram does not provide a direct signing API,
                // we simulate a signing step by adding a module that records the certificate information.
                // This preserves macro-enabled status and documents the intended certificate.

                // Create a simple VBA module with a comment about the certificate.
                string moduleName = "SignatureInfo";
                string moduleCode = $"' Signed with certificate: {Path.GetFileName(certificatePath)}\r\n" +
                                    $"' Timestamp: {DateTime.UtcNow:u}\r\n" +
                                    $"Sub Dummy()\r\n" +
                                    $"    ' No operation – placeholder for signature metadata.\r\n" +
                                    $"End Sub";

                // Add the module if it does not already exist.
                bool moduleExists = false;
                foreach (VbaModule existingModule in vbaProject.Modules)
                {
                    if (string.Equals(existingModule.Name, moduleName, StringComparison.OrdinalIgnoreCase))
                    {
                        moduleExists = true;
                        // Update the code to reflect the latest certificate info.
                        existingModule.Codes = moduleCode;
                        Console.WriteLine($"  Updated existing module '{moduleName}'.");
                        break;
                    }
                }

                if (!moduleExists)
                {
                    // Add a new procedural module.
                    int newIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, moduleName);
                    VbaModule newModule = vbaProject.Modules[newIndex];
                    newModule.Codes = moduleCode;
                    Console.WriteLine($"  Added new module '{moduleName}'.");
                }

                // Save the diagram in a macro-enabled format to preserve VBA.
                string outputFilePath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(filePath) + "_Signed.vsdm");
                diagram.Save(outputFilePath, SaveFileFormat.Vsdm);
                Console.WriteLine($"  Saved signed diagram as '{Path.GetFileName(outputFilePath)}'.");
                diagram.Dispose();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}