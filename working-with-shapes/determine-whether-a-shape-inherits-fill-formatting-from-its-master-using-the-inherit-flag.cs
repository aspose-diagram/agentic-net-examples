using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (replace with actual file path)
        string diagramPath = "input.vsdx";

        // Guard to ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace with actual shape ID)
            // Here we assume a shape with ID 1 exists
            Shape shape = page.Shapes.GetShape(1);

            if (shape == null)
            {
                Console.WriteLine("Shape not found.");
                return;
            }

            // Ensure InheritFill information is available for the shape
            if (shape.InheritFill == null)
            {
                Console.WriteLine("InheritFill information is not available for this shape.");
                return;
            }

            // Compare fill properties between the shape and its inherited values
            bool inheritsForegnd = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
            bool inheritsBkgnd = shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value;
            bool inheritsPattern = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;
            // Use correct shadow property names (ShdwForegnd and ShdwPattern)
            bool inheritsShadowForegnd = shape.Fill.ShdwForegnd.Value == shape.InheritFill.ShdwForegnd.Value;
            bool inheritsShadowPattern = shape.Fill.ShdwPattern.Value == shape.InheritFill.ShdwPattern.Value;

            // Determine overall inheritance status
            bool inheritsAll = inheritsForegnd && inheritsBkgnd && inheritsPattern && inheritsShadowForegnd && inheritsShadowPattern;

            // Output the result
            if (inheritsAll)
            {
                Console.WriteLine($"Shape ID {shape.ID} inherits its fill formatting from its master.");
            }
            else
            {
                Console.WriteLine($"Shape ID {shape.ID} does NOT fully inherit its fill formatting from its master.");
                Console.WriteLine($" - Foreground color inherited: {inheritsForegnd}");
                Console.WriteLine($" - Background color inherited: {inheritsBkgnd}");
                Console.WriteLine($" - Fill pattern inherited: {inheritsPattern}");
                Console.WriteLine($" - Shadow foreground inherited: {inheritsShadowForegnd}");
                Console.WriteLine($" - Shadow pattern inherited: {inheritsShadowPattern}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}