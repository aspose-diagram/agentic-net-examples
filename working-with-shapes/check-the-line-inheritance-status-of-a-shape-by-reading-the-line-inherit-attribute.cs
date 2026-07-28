using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace with the actual ID you want to check)
            long shapeId = 1; // example shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Determine line inheritance by comparing the shape's line color with its inherited line color
            // If the values match, the line properties are inherited from the master/style
            bool isLineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;

            Console.WriteLine($"Shape ID {shapeId} line inheritance status: {(isLineInherited ? "Inherited" : "Not inherited")}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}