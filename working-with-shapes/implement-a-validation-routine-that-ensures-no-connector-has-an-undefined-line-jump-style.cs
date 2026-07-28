using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
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

            try
            {
                ValidateConnectorJumpStyles(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Validation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads a Visio diagram and checks every connector (1‑D shape) to ensure its
        /// line jump style is not undefined. Throws an exception if an undefined style is found.
        /// </summary>
        /// <param name="filePath">Path to the Visio file.</param>
        static void ValidateConnectorJumpStyles(string filePath)
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes.
                    if (shape.OneD)
                    {
                        // Retrieve the connector's line jump style.
                        ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                        // Undefined indicates the style is not set.
                        if (jumpStyle == ConLineJumpStyleValue.Undefined)
                        {
                            string message = $"Connector shape ID {shape.ID} on page \"{page.Name}\" has an undefined line jump style.";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }
                    }
                }
            }

            Console.WriteLine("All connectors have defined line jump styles.");
        }
    }