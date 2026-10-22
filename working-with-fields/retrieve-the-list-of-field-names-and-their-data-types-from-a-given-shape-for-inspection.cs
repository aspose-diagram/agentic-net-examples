using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard against missing diagram file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse the shape ID.
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape with the specified ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == targetShapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {targetShapeId} was found in the diagram.");
                return;
            }

            // Ensure the shape has user-defined cells (fields) to inspect.
            if (targetShape.Users == null || targetShape.Users.Count == 0)
            {
                Console.WriteLine($"Shape ID {targetShapeId} contains no user-defined fields.");
                return;
            }

            Console.WriteLine($"User-defined fields for shape ID {targetShapeId}:");

            // Iterate over each user-defined cell and infer its data type.
            foreach (User userField in targetShape.Users)
            {
                // Retrieve the field name (NameU) and its raw string value.
                string fieldName = userField.NameU ?? string.Empty;
                string rawValue = userField.Value?.Val ?? string.Empty;

                // Infer the data type based on the value content.
                string inferredType;
                if (int.TryParse(rawValue, out _))
                {
                    inferredType = "int";
                }
                else if (double.TryParse(rawValue, out _))
                {
                    inferredType = "double";
                }
                else if (DateTime.TryParse(rawValue, out _))
                {
                    inferredType = "DateTime";
                }
                else
                {
                    inferredType = "string";
                }

                // Output the field information.
                Console.WriteLine($"  Name: {fieldName}, Type: {inferredType}, Value: {rawValue}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}