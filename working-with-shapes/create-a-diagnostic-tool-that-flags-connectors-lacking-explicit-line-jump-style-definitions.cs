using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get diagram file path from command line or prompt the user
            string diagramPath;
            if (args.Length > 0)
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
                Console.WriteLine("No file path provided. Exiting.");
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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1-D shapes)
                    if (shape.OneD)
                    {
                        // Retrieve the line jump style for the connector
                        var jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                        // Flag if the jump style is undefined or uses the page default (i.e., not explicitly set)
                        if (jumpStyle == ConLineJumpStyleValue.Undefined ||
                            jumpStyle == ConLineJumpStyleValue.PageDefault)
                        {
                            anyFlagged = true;
                            Console.WriteLine($"Connector ID {shape.ID} on page '{page.Name}' lacks explicit line jump style. Current value: {jumpStyle}");
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