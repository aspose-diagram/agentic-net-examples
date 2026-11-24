using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header
                writer.WriteLine("ShapeId,ShapeName,UserName,UserValue,UserPrompt");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all user-defined cells of the shape
                        foreach (User user in shape.Users)
                        {
                            // Retrieve required values
                            string shapeId = shape.ID.ToString();
                            string shapeName = shape.NameU ?? string.Empty;
                            string userName = user.NameU ?? string.Empty;
                            string userValue = user.Value?.Val ?? string.Empty;
                            string userPrompt = user.Prompt?.Value ?? string.Empty;

                            // Write a CSV line (escape commas if needed)
                            string line = $"{Escape(shapeId)},{Escape(shapeName)},{Escape(userName)},{Escape(userValue)},{Escape(userPrompt)}";
                            writer.WriteLine(line);
                        }
                    }
                }
            }

            Console.WriteLine($"User-defined cell data exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Simple CSV field escaper to handle commas and quotes
    private static string Escape(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}