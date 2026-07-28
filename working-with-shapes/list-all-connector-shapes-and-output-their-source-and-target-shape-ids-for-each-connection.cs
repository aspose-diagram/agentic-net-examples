using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the Visio file path as the first argument
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide the path to a Visio file as an argument.");
                    return;
                }

                string filePath = args[0];

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                    // Iterate through all connections on the page
                    foreach (Connect connection in page.Connects)
                    {
                        long sourceId = connection.FromSheet;
                        long targetId = connection.ToSheet;

                        Console.WriteLine($"Connector from Shape ID {sourceId} to Shape ID {targetId}");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }