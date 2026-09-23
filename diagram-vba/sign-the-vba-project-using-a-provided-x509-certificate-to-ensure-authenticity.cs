using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        // Path to the Visio file (must be macro-enabled to keep VBA)
        string inputPath = "input.vsdm";
        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path to save the signed Visio file
        string outputPath = "signed_output.vsdm";

        // Path to the X509 certificate (PFX) and its password
        string certPath = "mycert.pfx";
        // Guard: ensure the certificate file exists
        if (!File.Exists(certPath)) { Console.Error.WriteLine($"File not found: {certPath}"); return; }
        string certPassword = "password";

        X509Certificate2 certificate;
        try
        {
            // Load the X509 certificate from the provided PFX file
            certificate = new X509Certificate2(certPath, certPassword);
            Console.WriteLine($"Certificate loaded: Subject = {certificate.Subject}");
        }
        catch (Exception ex)
        {
            // Report any errors during certificate loading
            Console.Error.WriteLine($"Error loading certificate: {ex.Message}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram; macro-enabled format preserves VBA
            diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded.");
        }
        catch (Exception ex)
        {
            // Report any errors during diagram loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Access the VBA project to inspect its signed status
            VbaProject vbaProject = diagram.VbaProject;
            Console.WriteLine($"VBA project signed status before: {vbaProject.IsSigned}");
        }
        catch (Exception ex)
        {
            // Report any errors accessing the VBA project
            Console.Error.WriteLine($"Error accessing VBA project: {ex.Message}");
            return;
        }

        // NOTE:
        // Aspose.Diagram does not provide a public API to sign a VBA project.
        // The VbaProject class has a read‑only IsSigned property and no Sign method.
        // Therefore, actual signing with the X509 certificate cannot be performed
        // via the current Aspose.Diagram library. The certificate can be stored
        // or used for other purposes, but signing must be done outside of Aspose.Diagram.

        try
        {
            // Save the diagram in macro‑enabled format to preserve the VBA project
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors during saving
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}