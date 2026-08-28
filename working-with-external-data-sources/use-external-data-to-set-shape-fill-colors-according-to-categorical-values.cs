using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string csvPath = "shapeCategories.csv";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read external CSV data (format: ShapeName,Category)
                // Example line: Process,High
                var shapeCategoryMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        string shapeName = parts[0].Trim();
                        string category = parts[1].Trim();
                        shapeCategoryMap[shapeName] = category;
                    }
                }

                // Define category‑to‑color mapping (hex strings)
                var categoryColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "High",   "#FF0000" }, // Red
                    { "Medium", "#FFFF00" }, // Yellow
                    { "Low",    "#00FF00" }  // Green
                };

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True) continue;

                        // Determine if the shape name exists in the CSV map
                        if (shape.NameU != null && shapeCategoryMap.TryGetValue(shape.NameU, out string category))
                        {
                            // Find the corresponding color for the category
                            if (categoryColorMap.TryGetValue(category, out string hexColor))
                            {
                                // Apply solid fill pattern and set foreground color
                                shape.Fill.FillPattern.Value = 1; // Solid fill
                                shape.Fill.FillForegnd.Value = hexColor;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Csv);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }