using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Identifier of the connector shape (replace with actual ID)
                long connectorId = 123; // example ID

                // Iterate through pages to find the connector
                foreach (Page page in diagram.Pages)
                {
                    // Try to get the shape by ID; GetShape returns null if not found on this page
                    Shape connector = page.Shapes.GetShape(connectorId);
                    if (connector != null)
                    {
                        // Ensure the shape is a connector (1‑D shape)
                        if (connector.OneD)
                        {
                            // Read the current line jump style
                            ConLineJumpStyleValue jumpStyle = connector.Layout.ConLineJumpStyle.Value;

                            // Log the value
                            Console.WriteLine($"Connector ID {connectorId} line jump style: {jumpStyle}");
                        }
                        else
                        {
                            Console.WriteLine($"Shape with ID {connectorId} is not a connector.");
                        }

                        // Connector found, exit the loop
                        break;
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }