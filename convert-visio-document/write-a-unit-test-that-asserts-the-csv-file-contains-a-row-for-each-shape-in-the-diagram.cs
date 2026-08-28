using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string csvPath = "output.csv";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Count all shapes across all pages (excluding deleted shapes)
                    int shapeCount = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Del == BOOL.False)
                            {
                                shapeCount++;
                            }
                        }
                    }

                    // Export the diagram to CSV
                    diagram.Save(csvPath, SaveFileFormat.Csv);

                    // Read the generated CSV file
                    if (!File.Exists(csvPath))
                    {
                        throw new Exception($"CSV file was not created at path: {csvPath}");
                    }

                    string[] csvLines = File.ReadAllLines(csvPath);

                    // Some CSV exports include a header row; detect and adjust if present
                    int dataRowCount = csvLines.Length;
                    if (dataRowCount > 0 && csvLines[0].Contains("ShapeID", StringComparison.OrdinalIgnoreCase))
                    {
                        dataRowCount--; // exclude header
                    }

                    // Validate that each shape has a corresponding CSV row
                    if (dataRowCount != shapeCount)
                    {
                        throw new Exception($"CSV row count ({dataRowCount}) does not match shape count ({shapeCount}).");
                    }

                    Console.WriteLine($"Success: CSV contains a row for each of the {shapeCount} shapes.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }