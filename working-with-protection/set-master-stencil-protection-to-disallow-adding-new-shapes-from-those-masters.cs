using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Set global document protection to prevent adding new shapes from masters
            // This disables the ability to drop shapes from stencils (masters) into the diagram
            diagram.DocumentSettings.ProtectMasters = BOOL.True;

            // Optionally, you can also protect other aspects of the document if needed
            // diagram.DocumentSettings.ProtectShapes = BOOL.True;
            // diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            // diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // Save the protected diagram to a VSDX file
            string outputPath = "ProtectedDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with master protection at: {outputPath}");
        }
    }