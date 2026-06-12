using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            var connectorShapes = page.Shapes
                .Cast<Shape>()
                .Where(s => !string.IsNullOrEmpty(s.NameU) && s.NameU.StartsWith("Connector"))
                .ToList();

            foreach (var shape in connectorShapes)
            {
                shape.XForm.Width.Value = shape.XForm.Width.Value + 0.5;
            }

            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}