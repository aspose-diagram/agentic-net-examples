using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Enable global protection for background pages to prevent editing or deletion
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;

                // Optionally, you can also protect masters, shapes, and styles globally
                // diagram.DocumentSettings.ProtectMasters = BOOL.True;
                // diagram.DocumentSettings.ProtectShapes = BOOL.True;
                // diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // Save the protected diagram
                string outputPath = "output_protected.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved with background protection applied.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
