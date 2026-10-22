using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Open a StreamWriter for the CSV output.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("ShapeID,ShapeNameU,UserNameU,UserValue,UserPrompt");

                // Local function to escape CSV fields.
                string Escape(string s)
                {
                    if (s == null) return "";
                    if (s.Contains("\"") || s.Contains(",") || s.Contains("\n") || s.Contains("\r"))
                    {
                        s = s.Replace("\"", "\"\"");
                        return $"\"{s}\"";
                    }
                    return s;
                }

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape identifier and name.
                        long shapeId = shape.ID;
                        string shapeNameU = shape.NameU ?? "";

                        // Iterate over user‑defined cells.
                        foreach (User user in shape.Users)
                        {
                            string userNameU = user.NameU ?? "";
                            string userValue = user.Value?.Val ?? "";
                            string userPrompt = user.Prompt?.Value ?? "";

                            // Write a CSV line for each user‑defined cell.
                            writer.WriteLine($"{shapeId},{Escape(shapeNameU)},{Escape(userNameU)},{Escape(userValue)},{Escape(userPrompt)}");
                        }
                    }
                }
            }

            Console.WriteLine($"Export completed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}