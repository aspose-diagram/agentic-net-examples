using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: diagramPath csvPath outputPath
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramCsvBinding <diagramPath> <csvPath> <outputPath>");
                return;
            }

            string diagramPath = args[0];
            string csvPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Read CSV file
                if (!File.Exists(csvPath))
                {
                    Console.WriteLine($"CSV file not found: {csvPath}");
                    return;
                }

                using (StreamReader reader = new StreamReader(csvPath))
                {
                    // Read header (optional, ignored)
                    string headerLine = reader.ReadLine();
                    if (headerLine == null)
                    {
                        Console.WriteLine("CSV file is empty.");
                        return;
                    }

                    // Process each data row
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Simple CSV split by comma; does not handle quoted commas
                        string[] fields = line.Split(',');

                        if (fields.Length < 2)
                        {
                            Console.WriteLine($"Skipping malformed line: {line}");
                            continue;
                        }

                        string shapeName = fields[0].Trim();
                        string data1 = fields.Length > 1 ? fields[1].Trim() : string.Empty;
                        string data2 = fields.Length > 2 ? fields[2].Trim() : string.Empty;
                        string data3 = fields.Length > 3 ? fields[3].Trim() : string.Empty;

                        bool shapeFound = false;

                        // Iterate pages and shapes to find the matching shape by universal name
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                if (shape.NameU != null && shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Bind CSV values to shape data fields
                                    shape.Data1 = data1;
                                    shape.Data2 = data2;
                                    shape.Data3 = data3;

                                    shapeFound = true;
                                    break;
                                }
                            }

                            if (shapeFound)
                                break;
                        }

                        if (!shapeFound)
                        {
                            Console.WriteLine($"Shape not found for name: {shapeName}");
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
    }