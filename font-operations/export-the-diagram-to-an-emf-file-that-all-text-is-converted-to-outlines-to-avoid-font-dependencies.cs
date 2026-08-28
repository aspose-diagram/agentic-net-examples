using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "input.vsdx";

            // Output EMF file
            string outputPath = "output.emf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Set a default font to ensure any missing fonts are handled gracefully
            FontConfigs.DefaultFontName = "Arial";

            // Configure EMF export options
            PrintSaveOptions saveOptions = new PrintSaveOptions();
            saveOptions.SaveFormat = SaveFileFormat.Emf;

            // Save the diagram as EMF; text will be rendered as outlines by default for this format
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
