using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define the new width (in inches) to apply to matching connectors
            double newWidth = 2.0;

            // Select shapes whose universal name starts with "Connector"
            var connectorShapes = diagram.Pages[0].Shapes
                .Cast<Shape>()
                .Where(s => !string.IsNullOrEmpty(s.NameU) && s.NameU.StartsWith("Connector"));

            // Adjust the width of each selected shape
            foreach (var shape in connectorShapes)
            {
                shape.XForm.Width.Value = newWidth;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
