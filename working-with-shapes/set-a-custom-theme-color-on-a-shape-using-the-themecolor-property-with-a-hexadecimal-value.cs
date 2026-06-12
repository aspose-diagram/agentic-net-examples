using System;
using System.IO;
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

        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("No pages found in the diagram.");
            return;
        }

        Page page = diagram.Pages[0];

        if (page.Shapes.Count == 0)
        {
            Console.Error.WriteLine("No shapes found on the page.");
            return;
        }

        long shapeId = page.Shapes[0].ID;
        Shape shape = page.Shapes.GetShape(shapeId);

        // Set a custom fill color using a hexadecimal value
        shape.Fill.FillForegnd.Value = "#FF5733";

        string outputPath = "output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}