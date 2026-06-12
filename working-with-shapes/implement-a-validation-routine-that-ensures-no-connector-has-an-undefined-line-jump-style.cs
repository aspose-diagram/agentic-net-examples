using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                bool hasUndefinedJump = false;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connectors (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Retrieve the line jump style for the connector
                            ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                            // Check for undefined style
                            if (jumpStyle == ConLineJumpStyleValue.Undefined)
                            {
                                Console.WriteLine($"Connector ID {shape.ID} on page \"{page.Name}\" has undefined line jump style.");

                                // Optionally fix the undefined style by setting it to the page default
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                                Console.WriteLine(" -> Line jump style set to PageDefault.");

                                hasUndefinedJump = true;
                            }
                        }
                    }
                }

                if (hasUndefinedJump)
                {
                    // Save the corrected diagram
                    string outputPath = "validated_output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved with corrections to \"{outputPath}\".");
                }
                else
                {
                    Console.WriteLine("All connectors have defined line jump styles.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }