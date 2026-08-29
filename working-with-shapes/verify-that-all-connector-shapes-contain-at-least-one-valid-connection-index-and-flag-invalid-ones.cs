using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file; can be passed as a command‑line argument.
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                int invalidConnectorCount = 0;

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            bool hasConnection = false;

                            // Examine the page's Connect collection to see if the connector participates in any connection.
                            foreach (Connect conn in page.Connects)
                            {
                                if (conn.FromSheet == shape.ID || conn.ToSheet == shape.ID)
                                {
                                    hasConnection = true;
                                    break;
                                }
                            }

                            // If no connection is found, flag the connector as invalid.
                            if (!hasConnection)
                            {
                                invalidConnectorCount++;
                                Console.WriteLine($"Invalid connector found: Shape ID {shape.ID} on page \"{page.Name}\" has no connections.");
                            }
                        }
                    }
                }

                // Summary output.
                if (invalidConnectorCount == 0)
                {
                    Console.WriteLine("All connector shapes have at least one valid connection.");
                }
                else
                {
                    Console.WriteLine($"Total invalid connectors: {invalidConnectorCount}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }