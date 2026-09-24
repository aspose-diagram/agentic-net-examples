using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsv = "UserDefinedCells.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare a StringBuilder for CSV content
                StringBuilder csvBuilder = new StringBuilder();

                // Write CSV header
                csvBuilder.AppendLine("ShapeID,UserCellName,UserCellValue");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through user-defined cells (Users collection)
                        foreach (User userCell in shape.Users)
                        {
                            // Escape commas in values if necessary
                            string cellName = EscapeCsv(userCell.Name);
                            string cellValue = EscapeCsv(userCell.Value.Val);

                            // Append a CSV line: ShapeID,UserCellName,UserCellValue
                            csvBuilder.AppendLine($"{shape.ID},{cellName},{cellValue}");
                        }
                    }
                }

                // Write the CSV content to file
                File.WriteAllText(outputCsv, csvBuilder.ToString(), Encoding.UTF8);

                Console.WriteLine($"Export completed. CSV saved to: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to escape CSV fields containing commas or quotes
        private static string EscapeCsv(string field)
        {
            if (field == null)
                return string.Empty;

            if (field.Contains("\""))
                field = field.Replace("\"", "\"\"");

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
                field = $"\"{field}\"";

            return field;
        }
    }