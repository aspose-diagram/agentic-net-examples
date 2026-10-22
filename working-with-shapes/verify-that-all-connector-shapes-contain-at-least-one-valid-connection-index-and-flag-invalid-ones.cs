using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            bool hasConnection = false;

                            // Examine the page's Connects collection to see if this connector participates in any connection.
                            foreach (Connect conn in page.Connects)
                            {
                                if (conn.FromSheet == shape.ID || conn.ToSheet == shape.ID)
                                {
                                    hasConnection = true;
                                    break;
                                }
                            }

                            // Flag connectors without any connections.
                            if (!hasConnection)
                            {
                                Console.WriteLine($"Invalid connector found: Shape ID {shape.ID}, NameU \"{shape.NameU}\", on Page ID {page.ID}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}