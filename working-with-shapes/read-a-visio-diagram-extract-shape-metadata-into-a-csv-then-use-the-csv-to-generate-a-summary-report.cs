using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output CSV file path
        string csvPath = "shapes_metadata.csv";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Write shape metadata to CSV
            using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
            {
                // Header
                writer.WriteLine("PageName,ShapeID,Name,NameU,MasterName,PinX,PinY,Width,Height,Text");

                // Iterate pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Gather metadata
                        long shapeId = shape.ID; // shape.ID is a long
                        string shapeName = shape.Name ?? string.Empty;
                        string shapeNameU = shape.NameU ?? string.Empty;
                        string masterName = shape.Master != null ? shape.Master.Name : string.Empty;
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Get plain text, replace line breaks and commas to keep CSV format simple
                        string text = shape.Text.Value.ToString()
                                        .Replace("\r\n", " ")
                                        .Replace("\n", " ")
                                        .Replace(",", " ");

                        // Write CSV line (values are quoted to protect commas in future text)
                        writer.WriteLine(string.Format(CultureInfo.InvariantCulture,
                            "\"{0}\",{1},\"{2}\",\"{3}\",\"{4}\",{5},{6},{7},{8},\"{9}\"",
                            page.Name,
                            shapeId,
                            shapeName,
                            shapeNameU,
                            masterName,
                            pinX,
                            pinY,
                            width,
                            height,
                            text));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Generate a simple summary report from the CSV
        int totalShapes = 0;
        double totalWidth = 0.0;
        double totalHeight = 0.0;
        HashSet<string> distinctMasters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        using (StreamReader reader = new StreamReader(csvPath))
        {
            // Read header line
            string headerLine = reader.ReadLine();

            // Process each data line
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Simple CSV split (fields are quoted, but we only need a few columns)
                List<string> fields = new List<string>();
                bool inQuotes = false;
                string current = string.Empty;
                foreach (char c in line)
                {
                    if (c == '\"')
                    {
                        inQuotes = !inQuotes;
                        continue;
                    }

                    if (c == ',' && !inQuotes)
                    {
                        fields.Add(current);
                        current = string.Empty;
                    }
                    else
                    {
                        current += c;
                    }
                }
                fields.Add(current); // last field

                if (fields.Count < 9)
                    continue; // malformed line

                // Parse required fields
                string master = fields[4];
                string widthStr = fields[7];
                string heightStr = fields[8];

                double widthVal = double.TryParse(widthStr, NumberStyles.Any, CultureInfo.InvariantCulture, out widthVal) ? widthVal : 0.0;
                double heightVal = double.TryParse(heightStr, NumberStyles.Any, CultureInfo.InvariantCulture, out heightVal) ? heightVal : 0.0;

                totalShapes++;
                totalWidth += widthVal;
                totalHeight += heightVal;
                if (!string.IsNullOrEmpty(master))
                    distinctMasters.Add(master);
            }
        }

        // Output summary to console
        Console.WriteLine("=== Shape Metadata Summary ===");
        Console.WriteLine("Total shapes processed: " + totalShapes);
        Console.WriteLine("Distinct master shapes: " + distinctMasters.Count);
        if (totalShapes > 0)
        {
            double avgWidth = totalWidth / totalShapes;
            double avgHeight = totalHeight / totalShapes;
            Console.WriteLine("Average width (inches): " + avgWidth.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("Average height (inches): " + avgHeight.ToString("F2", CultureInfo.InvariantCulture));
        }
        else
        {
            Console.WriteLine("No shapes found.");
        }
    }
}