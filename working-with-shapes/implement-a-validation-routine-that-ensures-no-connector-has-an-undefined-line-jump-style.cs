using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to validate. Can be passed as a command‑line argument.
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                bool validationFailed = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connectors are 1‑D shapes.
                        if (shape.OneD)
                        {
                            // Ensure the Layout object exists.
                            if (shape.Layout != null)
                            {
                                // Retrieve the line jump style for the connector.
                                ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                                // Undefined indicates that no explicit jump style is set.
                                if (jumpStyle == ConLineJumpStyleValue.Undefined)
                                {
                                    Console.WriteLine(
                                        $"Connector ID {shape.ID} on page \"{page.Name}\" has an undefined line jump style.");
                                    validationFailed = true;
                                }
                            }
                            else
                            {
                                // If Layout is missing, treat it as undefined.
                                Console.WriteLine(
                                    $"Connector ID {shape.ID} on page \"{page.Name}\" lacks a Layout object (treated as undefined jump style).");
                                validationFailed = true;
                            }
                        }
                    }
                }

                // Report the overall result.
                if (validationFailed)
                {
                    throw new Exception("Validation failed: one or more connectors have undefined line jump styles.");
                }
                else
                {
                    Console.WriteLine("Validation passed: all connectors have defined line jump styles.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }