using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputVisioFile> <outputVisioFile> <certificatePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string certPath = args[2];

        // Load the X509 certificate (assumes it has no password)
        X509Certificate2 certificate;
        try
        {
            certificate = new X509Certificate2(certPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load certificate: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Access the VBA project
        VbaProject vbaProject = diagram.VbaProject;

        // Check if the VBA project is already signed
        if (vbaProject.IsSigned)
        {
            Console.WriteLine("The VBA project is already signed.");
        }
        else
        {
            // NOTE: Aspose.Diagram does not provide a direct API to sign a VBA project.
            // The certificate can be stored or used for custom validation, but signing
            // must be performed outside of Aspose.Diagram (e.g., using Visio automation).
            Console.WriteLine("Signing the VBA project is not supported via Aspose.Diagram API.");
            Console.WriteLine($"Certificate Subject: {certificate.Subject}");
        }

        // Save the diagram in a macro-enabled format to preserve VBA
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
