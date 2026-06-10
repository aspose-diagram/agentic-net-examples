using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the original Visio file
                string inputPath = "original.vsdx";

                // Path for the new file that will contain the saved diagram
                string outputPath = "copy.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Save the diagram to a new file, preserving the Visio format (VSDX)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }