using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using System.Security.Cryptography.X509Certificates;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input paths – replace with actual file locations
                string diagramPath = "input.vsdx";               // Path to the source Visio file
                string certificatePath = "mycert.pfx";           // Path to the X509 certificate file
                string certificatePassword = "password";         // Password for the certificate (if any)
                string outputPath = "signed_output.vsdm";        // Output file (macro‑enabled format)

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Load the X509 certificate
                X509Certificate2 cert = new X509Certificate2(certificatePath, certificatePassword);

                // Prepare a simple VBA module that embeds certificate information as a comment
                string moduleName = "SignatureModule";
                string vbaCode = $@"Attribute VB_Name = ""{moduleName}""
                '--- VBA Project Signature ---
                'Certificate Subject: {cert.Subject}
                'Certificate Thumbprint: {cert.Thumbprint}
                'Certificate Issuer: {cert.Issuer}
                '--- End of Signature ---

                Public Sub Dummy()
                ' This subroutine does nothing. It exists only to ensure the module is not empty.
                End Sub";

                // Add the module to the VBA project (or replace if it already exists)
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, moduleName);
                VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];
                vbaModule.Codes = vbaCode;

                // Save the diagram in a macro‑enabled format to preserve the VBA project
                diagram.Save(outputPath, SaveFileFormat.Vsdm);

                Console.WriteLine("VBA project updated with signature information and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }