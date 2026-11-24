using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagramPath, dataFilePath, outputDiagramPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <dataFilePath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string dataFilePath = args[1];
        string outputDiagramPath = args[2];

        // Guard: check diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: check data file exists
        if (!File.Exists(dataFilePath))
        {
            Console.Error.WriteLine($"Data file not found: {dataFilePath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Read all lines from the CSV data file
                string[] allLines = File.ReadAllLines(dataFilePath);
                if (allLines.Length == 0)
                {
                    Console.Error.WriteLine("Data file is empty.");
                    return;
                }

                // Define expected schema: Header must be "Id,Name,Value"
                string header = allLines[0];
                string[] headerParts = header.Split(',');
                if (headerParts.Length != 3 ||
                    headerParts[0].Trim() != "Id" ||
                    headerParts[1].Trim() != "Name" ||
                    headerParts[2].Trim() != "Value")
                {
                    Console.Error.WriteLine("Data file header does not match expected schema 'Id,Name,Value'.");
                    return;
                }

                // Process each data row
                for (int i = 1; i < allLines.Length; i++)
                {
                    string line = allLines[i];
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // Skip empty lines
                        continue;
                    }

                    string[] parts = line.Split(',');
                    if (parts.Length != 3)
                    {
                        Console.Error.WriteLine($"Row {i + 1} does not have exactly 3 columns.");
                        return;
                    }

                    // Validate Id (integer)
                    if (!int.TryParse(parts[0].Trim(), out int id))
                    {
                        Console.Error.WriteLine($"Row {i + 1}: Id '{parts[0]}' is not a valid integer.");
                        return;
                    }

                    // Name is a string, just trim
                    string name = parts[1].Trim();

                    // Validate Value (double)
                    if (!double.TryParse(parts[2].Trim(), out double value))
                    {
                        Console.Error.WriteLine($"Row {i + 1}: Value '{parts[2]}' is not a valid number.");
                        return;
                    }

                    // Map data to a new rectangle shape on the first page
                    // Simple horizontal grid layout
                    double pinX = 1.0 + (i - 1) * 2.0; // offset each shape horizontally
                    double pinY = 1.0; // constant Y

                    // AddShape returns a long shape ID; fourth parameter is a plain bool
                    long shapeId = diagram.Pages[0].AddShape(pinX, pinY, "Rectangle", false);

                    // Retrieve the shape instance
                    Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                    // Store external data in shape's Data fields
                    shape.Data1 = name;
                    shape.Data2 = value.ToString();

                    // Update shape's displayed text
                    shape.Text.Value.Clear();                     // clear existing text runs
                    shape.Text.Value.Add(new Txt(name));          // add new text run
                    shape.RefreshData();                          // refresh shape to apply changes
                }

                // Save the modified diagram to the output path using the correct overload
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}