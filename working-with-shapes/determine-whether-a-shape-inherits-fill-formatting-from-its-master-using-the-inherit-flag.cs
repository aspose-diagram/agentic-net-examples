using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Assume we work with the active page.
            Page page = diagram.ActivePage;

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the active page.");
                return;
            }

            // Ensure the shape has a master (otherwise inheritance from master is not applicable).
            if (shape.Master == null)
            {
                Console.WriteLine("The shape does not have an associated master; inheritance from master is not applicable.");
                return;
            }

            // Determine if the shape inherits fill formatting from its master.
            // Compare key fill properties with the inherited values.
            bool inheritsFillForegnd = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
            bool inheritsFillBkgnd = shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value;
            bool inheritsFillPattern = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;
            bool inheritsShdwForegnd = shape.Fill.ShdwForegnd.Value == shape.InheritFill.ShdwForegnd.Value;
            bool inheritsShdwPattern = shape.Fill.ShdwPattern.Value == shape.InheritFill.ShdwPattern.Value;

            bool inheritsAll = inheritsFillForegnd && inheritsFillBkgnd && inheritsFillPattern && inheritsShdwForegnd && inheritsShdwPattern;

            // Output the result.
            if (inheritsAll)
            {
                Console.WriteLine($"Shape ID {shapeId} inherits its fill formatting from its master.");
            }
            else
            {
                Console.WriteLine($"Shape ID {shapeId} does NOT fully inherit its fill formatting from its master.");
                Console.WriteLine("Detailed inheritance check:");
                Console.WriteLine($"  FillForegnd inherited: {inheritsFillForegnd}");
                Console.WriteLine($"  FillBkgnd inherited: {inheritsFillBkgnd}");
                Console.WriteLine($"  FillPattern inherited: {inheritsFillPattern}");
                Console.WriteLine($"  ShdwForegnd inherited: {inheritsShdwForegnd}");
                Console.WriteLine($"  ShdwPattern inherited: {inheritsShdwPattern}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}