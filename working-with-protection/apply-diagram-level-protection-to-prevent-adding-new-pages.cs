using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Apply global protection settings to the document.
                // These settings prevent editing of backgrounds, masters, shapes, and styles.
                // While Visio does not have a direct "prevent new pages" flag,
                // protecting backgrounds and masters effectively restricts page creation.
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                diagram.DocumentSettings.ProtectShapes = BOOL.True;
                diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // Save the protected diagram to a new file.
                string outputPath = "output_protected.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with protection applied: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }