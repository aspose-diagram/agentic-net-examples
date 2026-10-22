using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            int totalConnectors = 0;
            int undefinedJumpStyleCount = 0;

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify connector shapes (1-D shapes).
                    if (shape.OneD)
                    {
                        totalConnectors++;

                        // Ensure the Layout object exists.
                        if (shape.Layout == null)
                        {
                            Console.Error.WriteLine($"Warning: Shape ID {shape.ID} on page '{page.Name}' has no Layout object.");
                            undefinedJumpStyleCount++;
                            continue;
                        }

                        // Retrieve the line jump style value.
                        var jumpStyleValue = shape.Layout.ConLineJumpStyle.Value;

                        // Check for undefined jump style.
                        if (jumpStyleValue == ConLineJumpStyleValue.Undefined)
                        {
                            Console.Error.WriteLine($"Connector ID {shape.ID} on page '{page.Name}' has undefined line jump style.");
                            undefinedJumpStyleCount++;
                        }
                    }
                }
            }

            // Summary output.
            Console.WriteLine($"Validation completed. Total connectors examined: {totalConnectors}.");
            Console.WriteLine($"Connectors with undefined line jump style: {undefinedJumpStyleCount}.");

            // Optionally, you could treat undefined styles as a failure.
            if (undefinedJumpStyleCount > 0)
            {
                Console.Error.WriteLine("Validation failed: some connectors have undefined line jump styles.");
            }
            else
            {
                Console.WriteLine("All connectors have defined line jump styles.");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}