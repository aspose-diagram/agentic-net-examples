using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, spreadsheet (CSV) file, output diagram file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagram.vsdx> <data.csv> <output.vsdx>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Read all lines from the CSV file
                string[] allLines = File.ReadAllLines(csvPath);
                if (allLines.Length < 2)
                {
                    Console.Error.WriteLine("CSV file does not contain data rows.");
                    return;
                }

                // Parse header
                string[] headers = SplitCsvLine(allLines[0]);
                int shapeNameIndex = Array.IndexOf(headers, "ShapeName");
                if (shapeNameIndex == -1)
                {
                    Console.Error.WriteLine("CSV must contain a 'ShapeName' column.");
                    return;
                }

                // Build a lookup of shapes by their NameU for quick access
                Dictionary<string, Shape> shapeLookup = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (!string.IsNullOrEmpty(shape.NameU))
                        {
                            if (!shapeLookup.ContainsKey(shape.NameU))
                                shapeLookup.Add(shape.NameU, shape);
                        }
                    }
                }

                // Process each data row
                for (int i = 1; i < allLines.Length; i++)
                {
                    string[] fields = SplitCsvLine(allLines[i]);
                    if (fields.Length != headers.Length)
                    {
                        Console.Error.WriteLine($"Row {i + 1} column count does not match header.");
                        continue;
                    }

                    string shapeName = fields[shapeNameIndex];
                    if (!shapeLookup.TryGetValue(shapeName, out Shape targetShape))
                    {
                        Console.Error.WriteLine($"Shape with NameU '{shapeName}' not found in diagram.");
                        continue;
                    }

                    // Map each column (except ShapeName) to a user-defined cell with the same name
                    for (int col = 0; col < headers.Length; col++)
                    {
                        if (col == shapeNameIndex) continue; // skip the key column

                        string userCellName = headers[col];
                        string cellValue = fields[col];

                        // Find the user-defined cell in the shape
                        User matchingUser = null;
                        foreach (User user in targetShape.Users)
                        {
                            if (string.Equals(user.NameU, userCellName, StringComparison.OrdinalIgnoreCase))
                            {
                                matchingUser = user;
                                break;
                            }
                        }

                        if (matchingUser != null)
                        {
                            // Assign the value to the user-defined cell
                            matchingUser.Value.Val = cellValue;
                        }
                        else
                        {
                            Console.Error.WriteLine($"User-defined cell '{userCellName}' not found in shape '{shapeName}'.");
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple CSV line splitter handling commas; does not handle quoted commas for brevity
    private static string[] SplitCsvLine(string line)
    {
        return line.Split(new char[] { ',' }, StringSplitOptions.None);
    }
}