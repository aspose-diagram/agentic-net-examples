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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output EMF file path
            string outputPath = "output.emf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure EMF export options
            PrintSaveOptions saveOptions = new PrintSaveOptions();
            saveOptions.SaveFormat = SaveFileFormat.Emf;

            // Save the diagram as EMF
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
