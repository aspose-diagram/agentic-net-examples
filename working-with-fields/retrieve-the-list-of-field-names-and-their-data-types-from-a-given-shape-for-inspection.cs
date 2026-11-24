using System;
using System.IO;
using System.Globalization;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> [shapeId]");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Optional shape ID argument.
        long targetShapeId = 0;
        bool shapeIdProvided = false;
        if (args.Length >= 2)
        {
            if (!long.TryParse(args[1], out targetShapeId))
            {
                Console.Error.WriteLine($"Error: Invalid shape ID '{args[1]}'.");
                return;
            }
            shapeIdProvided = true;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            Shape targetShape = null;

            // Locate the shape either by provided ID or take the first shape on the first page.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shapeIdProvided)
                    {
                        if (shape.ID == targetShapeId)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    else
                    {
                        // No ID supplied – pick the first shape we encounter.
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine(shapeIdProvided
                    ? $"Error: No shape found with ID {targetShapeId}."
                    : "Error: No shapes found in the diagram.");
                return;
            }

            // Ensure the shape has user-defined cells (custom fields).
            if (targetShape.Users == null || targetShape.Users.Count == 0)
            {
                Console.WriteLine("The selected shape contains no user-defined fields.");
                return;
            }

            Console.WriteLine($"Inspecting fields of shape ID {targetShape.ID} (NameU: {targetShape.NameU})");
            Console.WriteLine("------------------------------------------------------------");

            // Iterate over each user-defined cell and infer its data type.
            foreach (User userField in targetShape.Users)
            {
                // Retrieve the raw value string; handle possible nulls.
                string rawValue = userField.Value?.Val ?? string.Empty;

                // Determine the data type based on the value content.
                string dataType;
                if (int.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
                {
                    dataType = "Int32";
                }
                else if (double.TryParse(rawValue, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                {
                    dataType = "Double";
                }
                else if (DateTime.TryParse(rawValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    dataType = "DateTime";
                }
                else
                {
                    dataType = "String";
                }

                // Output the field name, inferred type, and its current value.
                Console.WriteLine($"Field Name: {userField.NameU}");
                Console.WriteLine($"  Type : {dataType}");
                Console.WriteLine($"  Value: {rawValue}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}