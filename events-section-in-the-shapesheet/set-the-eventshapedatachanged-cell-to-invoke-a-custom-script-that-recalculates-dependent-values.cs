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

        Page page = diagram.Pages[0];

        if (page.Shapes.Count == 0)
        {
            Console.WriteLine("The diagram contains no shapes.");
            return;
        }

        long firstShapeId = page.Shapes[0].ID;
        Shape shape = page.Shapes.GetShape(firstShapeId);

        // Set the EventXFMod cell to invoke a custom script when shape data changes
        shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"RecalcScript\")";

        string outputPath = "output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("EventXFMod cell configured and diagram saved successfully.");
    }
}