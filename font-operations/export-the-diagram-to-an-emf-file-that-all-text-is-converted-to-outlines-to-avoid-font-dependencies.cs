using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram file.
                string inputPath = "input.vsdx";

                // Path where the EMF file will be saved.
                string outputPath = "output.emf";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure EMF export options.
                // PrintSaveOptions is used for EMF rendering.
                PrintSaveOptions saveOptions = new PrintSaveOptions();
                saveOptions.SaveFormat = SaveFileFormat.Emf;

                // Export the diagram to EMF.
                diagram.Save(outputPath, saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }