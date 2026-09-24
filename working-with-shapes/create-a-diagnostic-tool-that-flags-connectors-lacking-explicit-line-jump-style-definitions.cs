using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the diagram file path
            string diagramPath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                diagramPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio diagram file: ");
                diagramPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(diagramPath))
            {
                Console.WriteLine("No diagram path provided. Exiting.");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            bool anyFlagged = false;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1-D shapes)
                    if (shape.OneD)
                    {
                        // Retrieve the line jump style value
                        var jumpStyle = shape.Layout?.ConLineJumpStyle?.Value ?? ConLineJumpStyleValue.Undefined;

                        // Flag if the jump style is not explicitly set (PageDefault or Undefined)
                        if (jumpStyle == ConLineJumpStyleValue.PageDefault || jumpStyle == ConLineJumpStyleValue.Undefined)
                        {
                            anyFlagged = true;
                            Console.WriteLine($"Connector ID {shape.ID} on page '{page.Name}' lacks explicit line jump style (Current value: {jumpStyle}).");
                        }
                    }
                }
            }

            if (!anyFlagged)
            {
                Console.WriteLine("All connectors have explicit line jump style definitions.");
            }
        }
    }