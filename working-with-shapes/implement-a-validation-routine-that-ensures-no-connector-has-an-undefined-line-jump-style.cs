using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the Visio file path as the first argument.
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: ConnectorJumpStyleValidator <VisioFilePath>");
                    return;
                }

                string visioPath = args[0];

                // Load the diagram.
                Diagram diagram = new Diagram(visioPath);

                bool validationFailed = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            // Retrieve the line jump style.
                            ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                            // Undefined jump style is not allowed.
                            if (jumpStyle == ConLineJumpStyleValue.Undefined)
                            {
                                validationFailed = true;
                                Console.WriteLine($"Connector (ID: {shape.ID}) on page '{page.Name}' has undefined line jump style.");
                            }
                        }
                    }
                }

                if (validationFailed)
                {
                    throw new Exception("Validation failed: One or more connectors have undefined line jump styles.");
                }
                else
                {
                    Console.WriteLine("Validation succeeded: All connectors have defined line jump styles.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }