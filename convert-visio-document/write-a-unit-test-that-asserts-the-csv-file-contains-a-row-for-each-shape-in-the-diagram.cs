using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the input Visio file and the output CSV file.
                string diagramPath = "input.vsdx";
                string csvPath = "output.csv";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Export the diagram to CSV.
                diagram.Save(csvPath, SaveFileFormat.Csv);

                // Count the number of non-deleted shapes in the diagram.
                long shapeCount = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Exclude shapes marked as deleted.
                        if (shape.Del == BOOL.False)
                        {
                            shapeCount++;
                        }
                    }
                }

                // Read the CSV file and count non‑empty rows.
                if (!File.Exists(csvPath))
                {
                    throw new Exception($"CSV file was not created at path: {csvPath}");
                }

                string[] csvLines = File.ReadAllLines(csvPath);
                long csvRowCount = csvLines.Count(line => !string.IsNullOrWhiteSpace(line));

                // Verify that each shape has a corresponding CSV row.
                if (shapeCount != csvRowCount)
                {
                    throw new Exception($"Shape count ({shapeCount}) does not match CSV row count ({csvRowCount}).");
                }

                Console.WriteLine("Success: CSV file contains a row for each shape in the diagram.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }