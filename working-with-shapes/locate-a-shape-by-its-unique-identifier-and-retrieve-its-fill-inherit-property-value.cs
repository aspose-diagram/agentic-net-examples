using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and the shape ID (unique identifier)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.WriteLine("Invalid shape ID.");
            return;
        }

        // Load the Visio diagram
        Diagram diagram = new Diagram(diagramPath);

        // Assume the shape is on the first page (index 0)
        Page page = diagram.Pages[0];

        // Retrieve the shape by its unique identifier
        Shape shape = page.Shapes.GetShape(shapeId);
        if (shape == null)
        {
            Console.WriteLine($"Shape with ID {shapeId} not found.");
            return;
        }

        // Access the inherited fill information.
        // For demonstration, we output the inherited foreground color and fill pattern.
        string inheritedForeColor = shape.InheritFill.FillForegnd.Value;
        int inheritedPattern = (int)shape.InheritFill.FillPattern.Value;

        Console.WriteLine($"Shape ID: {shapeId}");
        Console.WriteLine($"Inherited Fill Foreground Color: {inheritedForeColor}");
        Console.WriteLine($"Inherited Fill Pattern: {inheritedPattern}");
    }
}
