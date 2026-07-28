using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the input Visio file path
            string filePath;
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio file: ");
                filePath = Console.ReadLine();
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(filePath);
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
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Access the connector's line jump style
                        var jumpStyleCell = shape.Layout?.ConLineJumpStyle;
                        if (jumpStyleCell != null)
                        {
                            var jumpStyle = jumpStyleCell.Value;
                            // Flag if the style is the default (no explicit definition)
                            if (jumpStyle == ConLineJumpStyleValue.PageDefault ||
                                jumpStyle == ConLineJumpStyleValue.Undefined)
                            {
                                anyFlagged = true;
                                Console.WriteLine(
                                    $"Connector ID {shape.ID} on page '{page.Name}' lacks explicit line jump style (value: {jumpStyle}).");
                            }
                        }
                        else
                        {
                            // If the Layout or ConLineJumpStyle cell is missing, treat as undefined
                            anyFlagged = true;
                            Console.WriteLine(
                                $"Connector ID {shape.ID} on page '{page.Name}' lacks explicit line jump style (Layout/ConLineJumpStyle missing).");
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