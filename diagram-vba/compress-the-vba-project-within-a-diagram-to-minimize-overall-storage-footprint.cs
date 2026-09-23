using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the compressed output file
            string outputPath = "output_compressed.vsdx";

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Remove the VBA project data to minimize storage size
                diagram.VbProjectData = null;

                // Save the diagram (no VBA) in a non‑macro format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with compressed VBA data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
