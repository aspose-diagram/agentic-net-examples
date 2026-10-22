using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify that the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Optional shape ID (second argument). If not provided, the first non-deleted shape will be used.
        long targetShapeId = 0;
        bool shapeIdProvided = false;
        if (args.Length > 1 && long.TryParse(args[1], out long parsedId))
        {
            targetShapeId = parsedId;
            shapeIdProvided = true;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the target shape
            Shape targetShape = null;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

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
                        // Use the first non-deleted shape as default
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
                    ? $"Error: No shape with ID {targetShapeId} found."
                    : "Error: No suitable shape found in the diagram.");
                return;
            }

            // Enumerate hyperlinks associated with the target shape
            if (targetShape.Hyperlinks != null && targetShape.Hyperlinks.Count > 0)
            {
                Console.WriteLine($"Hyperlinks for shape ID {targetShape.ID}:");
                foreach (Hyperlink link in targetShape.Hyperlinks)
                {
                    // Retrieve hyperlink properties safely
                    string name = link.Name ?? string.Empty;
                    string address = link.Address?.Value ?? string.Empty;
                    string subAddress = link.SubAddress?.Value ?? string.Empty;
                    string description = link.Description?.Value ?? string.Empty;

                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Name       : {name}");
                    Console.WriteLine($"Address    : {address}");
                    Console.WriteLine($"SubAddress : {subAddress}");
                    Console.WriteLine($"Description: {description}");
                }
            }
            else
            {
                Console.WriteLine($"Shape ID {targetShape.ID} has no hyperlinks.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}