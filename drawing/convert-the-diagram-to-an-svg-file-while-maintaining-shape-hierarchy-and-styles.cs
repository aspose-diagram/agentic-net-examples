using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram file path (can be .vsdx, .vsd, etc.)
                string inputPath = @"C:\Diagrams\sample.vsdx";

                // Output SVG file path
                string outputPath = @"C:\Diagrams\sample.svg";

                // Load the Visio diagram using Aspose.Diagram
                // This utilizes the standard load rule for Diagram objects.
                Diagram diagram = new Diagram(inputPath);

                // Save the diagram as SVG.
                // Aspose.Diagram preserves shape hierarchy and styles during SVG export.
                diagram.Save(outputPath, SaveFileFormat.Svg);

                Console.WriteLine("Diagram successfully converted to SVG.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }