using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, X509 certificate path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <certificatePath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string certificatePath = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the certificate file exists (though we cannot use it directly)
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;

            // Output basic VBA project information
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is VBA Project Signed? {vbaProject.IsSigned}");

            // List all VBA modules in the project
            Console.WriteLine("VBA Modules:");
            foreach (VbaModule module in vbaProject.Modules)
            {
                string codePreview = string.IsNullOrEmpty(module.Codes) ? "(empty)" : $"(Length: {module.Codes.Length})";
                Console.WriteLine($" - Module Name: {module.Name}, Code: {codePreview}");
            }

            // NOTE: Aspose.Diagram does not provide an API to sign a VBA project with an X509 certificate.
            // The IsSigned property is read‑only and reflects whether the project is already signed.
            // To apply a digital signature, an external process using the certificate would be required.
            Console.WriteLine("Signing the VBA project via Aspose.Diagram is not supported.");
            Console.WriteLine("If a signature is required, apply it using external tools after saving the diagram.");

            // Save the diagram in a macro‑enabled format to preserve VBA content
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}