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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare image save options for EMF format.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);

            // EmfRenderSetting exists, but ConvertTextToOutlines is not supported.
            // We instantiate it without setting unsupported properties.
            saveOptions.EmfRenderSetting = new EmfRenderSetting();

            // Save the diagram as EMF. Text will be rendered using available fonts;
            // conversion to outlines is not available via the API.
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}