using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        string csvPath = "data.csv";
        string outputPath = "output.vsdx";

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard against missing CSV file
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read all lines from the CSV file using UTF-8 encoding (preserves Unicode)
            string[] csvLines = File.ReadAllLines(csvPath, System.Text.Encoding.UTF8);

            // Process each CSV line (expected format: ShapeId,Text)
            foreach (string line in csvLines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split only on the first comma to allow commas inside the text
                int firstComma = line.IndexOf(',');
                if (firstComma == -1)
                {
                    Console.Error.WriteLine($"Invalid CSV line (no comma found): {line}");
                    continue;
                }

                string idPart = line.Substring(0, firstComma).Trim();
                string textPart = line.Substring(firstComma + 1).Trim();

                // Parse shape ID
                if (!int.TryParse(idPart, out int shapeId))
                {
                    Console.Error.WriteLine($"Invalid shape ID '{idPart}' in line: {line}");
                    continue;
                }

                // Retrieve the shape from the first page (adjust if needed)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram contains no pages.");
                    break;
                }

                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    continue;
                }

                // Clear existing text (if possible)
                try
                {
                    shape.Text.Value.Clear();
                }
                catch
                {
                    // If Clear is not supported, ignore and proceed to add new text
                }

                // Add new Unicode text to the shape
                Txt txt = new Txt(textPart);
                shape.Text.Value.Add(txt);
            }

            // Save the modified diagram preserving Unicode characters
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}