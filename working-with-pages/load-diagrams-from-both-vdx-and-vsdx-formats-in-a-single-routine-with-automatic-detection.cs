using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect one or more file paths as command‑line arguments
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide at least one diagram file path (VDX or VSDX).");
            return;
        }

        foreach (var filePath in args)
        {
            try
            {
                // Load the diagram; the constructor detects VDX, VSDX, etc. automatically
                Diagram diagram = LoadDiagram(filePath);
                Console.WriteLine($"Successfully loaded '{filePath}'. Page count: {diagram.Pages.Count}");

                // Example: save the loaded diagram as PDF to verify the load succeeded
                string pdfPath = Path.ChangeExtension(filePath, ".pdf");
                diagram.Save(pdfPath, SaveFileFormat.Pdf);
                Console.WriteLine($"Saved PDF to '{pdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }

    // Loads a Visio diagram from the given path; format detection is handled internally
    static Diagram LoadDiagram(string path)
    {
        return new Diagram(path);
    }
}
