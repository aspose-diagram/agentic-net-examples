using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputEmfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare image save options for EMF format.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);

                // NOTE: Aspose.Diagram does not provide a direct API to convert
                // text to outlines for EMF export. The EMF will be saved with
                // regular text; font fallback is handled by the library.
                // This is the closest supported alternative.

                // Export the diagram to EMF.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram successfully exported to EMF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}