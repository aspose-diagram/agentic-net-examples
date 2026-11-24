using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a diagram file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool hasUndefinedJumpStyle = false;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD && shape.Type == TypeValue.Shape)
                        {
                            // Ensure the Layout object and ConLineJumpStyle cell exist
                            if (shape.Layout != null && shape.Layout.ConLineJumpStyle != null)
                            {
                                // Check if the line jump style is undefined
                                if (shape.Layout.ConLineJumpStyle.Value == ConLineJumpStyleValue.Undefined)
                                {
                                    hasUndefinedJumpStyle = true;
                                    Console.WriteLine($"Connector ID {shape.ID} on page \"{page.Name}\" has undefined line jump style.");
                                }
                            }
                        }
                    }
                }

                if (!hasUndefinedJumpStyle)
                {
                    Console.WriteLine("Validation passed: All connectors have defined line jump styles.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}