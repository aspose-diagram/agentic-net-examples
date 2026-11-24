using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool anyInvalid = false;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            // Determine whether the connector participates in any connection.
                            bool hasConnection = false;

                            // The Connect collection on the page holds all connections.
                            foreach (Connect conn in page.Connects)
                            {
                                // A connector is involved if its ID appears as either end of a connection.
                                if (conn.FromSheet == shape.ID || conn.ToSheet == shape.ID)
                                {
                                    hasConnection = true;
                                    break;
                                }
                            }

                            // Flag connectors with no connections.
                            if (!hasConnection)
                            {
                                anyInvalid = true;
                                Console.WriteLine($"Invalid connector found: Page {page.ID}, Shape ID {shape.ID}, NameU \"{shape.NameU}\" has no connections.");
                            }
                        }
                    }
                }

                if (!anyInvalid)
                {
                    Console.WriteLine("All connector shapes contain at least one valid connection.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}