using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ConnectorCsvExport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            try
            {
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("FromShapeId,ToShapeId,FromCell,ToCell");

                    // Iterate through all pages and their connector collections
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Connect connect in page.Connects)
                        {
                            long fromId = connect.FromSheet;
                            long toId = connect.ToSheet;
                            string fromCell = connect.FromCell ?? string.Empty;
                            string toCell = connect.ToCell ?? string.Empty;

                            // Write connector information as a CSV line
                            writer.WriteLine($"{fromId},{toId},{fromCell},{toCell}");
                        }
                    }
                }

                Console.WriteLine($"Connector information exported successfully to '{outputCsvPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during export: {ex.Message}");
                throw;
            }
        }
    }