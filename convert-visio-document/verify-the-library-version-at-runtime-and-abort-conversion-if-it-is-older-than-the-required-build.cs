using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // --------------------------------------------------------------------
        // Verify Aspose.Diagram library version at runtime.
        // Abort conversion if the library version is older than the required build.
        // --------------------------------------------------------------------
        try
        {
            // Define the minimum required version (major, minor, build, revision).
            Version requiredVersion = new Version(23, 10, 0, 0); // Adjust as needed.

            // Retrieve the version of the Aspose.Diagram assembly.
            Assembly diagramAssembly = typeof(Diagram).Assembly;
            Version currentVersion = diagramAssembly.GetName().Version;

            if (currentVersion == null)
            {
                Console.Error.WriteLine("Error: Unable to determine Aspose.Diagram library version.");
                return;
            }

            // Compare versions.
            if (currentVersion < requiredVersion)
            {
                Console.Error.WriteLine($"Error: Aspose.Diagram version {currentVersion} is older than the required version {requiredVersion}. Conversion aborted.");
                return;
            }

            Console.WriteLine($"Aspose.Diagram version {currentVersion} meets the required version {requiredVersion}.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during version check: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // Perform a simple conversion: load the Visio diagram and save as PDF.
        // --------------------------------------------------------------------
        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options (optional).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Conversion succeeded. PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}