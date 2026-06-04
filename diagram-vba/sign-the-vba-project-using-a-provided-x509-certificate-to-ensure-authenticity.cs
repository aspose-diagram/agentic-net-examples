using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (must be a macro-enabled format to contain VBA)
            string diagramPath = "input.vsdm";
            Diagram diagram = new Diagram(diagramPath);

            // Load the X509 certificate that would be used for signing
            string certPath = "mycert.pfx";
            string certPassword = "password";
            X509Certificate2 certificate = new X509Certificate2(certPath, certPassword);

            // Verify that a VBA project exists in the diagram
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Display current signing status (read‑only)
            Console.WriteLine($"VBA project signed: {diagram.VbaProject.IsSigned}");

            // NOTE: Aspose.Diagram does not provide an API to sign a VBA project.
            // The VbaProject.Sign method does not exist, and IsSigned is read‑only.
            // Therefore, signing cannot be performed programmatically with this library.
            Console.WriteLine("Signing the VBA project with a certificate is not supported by Aspose.Diagram.");

            // Save the diagram (macro‑enabled) to preserve any modifications made to VBA modules, if any.
            diagram.Save("output.vsdm", SaveFileFormat.Vsdm);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
