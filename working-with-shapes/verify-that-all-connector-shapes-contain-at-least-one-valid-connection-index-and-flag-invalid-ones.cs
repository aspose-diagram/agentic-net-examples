using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated.
                // Replace with the actual file path as needed.
                string diagramPath = "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                bool hasInvalidConnectors = false;

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes.
                        // Connectors are 1‑D shapes (OneD == true) and typically use the "Dynamic connector" master.
                        if (shape.OneD && shape.Master != null && shape.Master.Name != null &&
                            shape.Master.Name.IndexOf("Dynamic connector", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            long connectorId = shape.ID;
                            bool hasConnection = false;

                            // Examine the Connect collection for any connection involving this connector.
                            foreach (Connect conn in page.Connects)
                            {
                                if (conn.FromSheet == connectorId || conn.ToSheet == connectorId)
                                {
                                    hasConnection = true;
                                    break;
                                }
                            }

                            // If no connection was found, flag the connector as invalid.
                            if (!hasConnection)
                            {
                                hasInvalidConnectors = true;
                                Console.WriteLine($"Invalid connector found: Shape ID = {connectorId}, Name = {shape.Name}");
                            }
                        }
                    }
                }

                if (hasInvalidConnectors)
                {
                    // Optionally, throw an exception to indicate validation failure.
                    throw new Exception("One or more connector shapes lack valid connection indexes.");
                }
                else
                {
                    Console.WriteLine("All connector shapes contain at least one valid connection index.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }