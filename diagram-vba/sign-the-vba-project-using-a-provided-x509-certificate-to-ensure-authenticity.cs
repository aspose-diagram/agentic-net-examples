using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least diagram path and certificate path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <certificatePath> [certificatePassword]");
            return;
        }

        string diagramPath = args[0];
        string certificatePath = args[1];
        string certificatePassword = args.Length >= 3 ? args[2] : null;

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure certificate file exists
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the X509 certificate
        X509Certificate2 certificate;
        try
        {
            certificate = string.IsNullOrEmpty(certificatePassword)
                ? new X509Certificate2(certificatePath)
                : new X509Certificate2(certificatePath, certificatePassword);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load certificate: {ex.Message}");
            return;
        }

        // Process the Visio diagram
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Show current signature status
                Console.WriteLine($"VBA project signed before: {diagram.VbaProject.IsSigned}");

                // Add a new VBA module to embed certificate information
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "SignatureInfo");
                VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];

                // Prepare VBA code containing certificate details as comments
                string vbaCode = $"' Certificate Thumbprint: {certificate.Thumbprint}\n" +
                                 $"' Signed by: {certificate.Subject}\n" +
                                 "' This module is added to indicate signing information.\n";

                vbaModule.Codes = vbaCode;

                // Save the diagram in a macro‑enabled format to preserve VBA content
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_signed.vsdm");

                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved with embedded signing info to: {outputPath}");
            }

            // Re‑open the saved diagram to verify changes
            using (Diagram savedDiagram = new Diagram(Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_signed.vsdm")))
            {
                Console.WriteLine($"VBA project signed after save: {savedDiagram.VbaProject.IsSigned}");

                // Locate the SignatureInfo module and display its code
                foreach (VbaModule module in savedDiagram.VbaProject.Modules)
                {
                    if (module.Name == "SignatureInfo")
                    {
                        Console.WriteLine("SignatureInfo module code:");
                        Console.WriteLine(module.Codes);
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}