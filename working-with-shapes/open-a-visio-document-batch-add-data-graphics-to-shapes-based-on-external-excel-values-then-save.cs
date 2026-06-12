using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string visioPath = "input.vsdx";
                string excelPath = "data.xlsx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Load Excel workbook and read shape‑name/value pairs
                Workbook workbook = new Workbook(excelPath);
                Worksheet sheet = workbook.Worksheets[0];
                // Dictionary to hold mapping from shape universal name to the value
                Dictionary<string, string> shapeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                int maxRow = sheet.Cells.MaxDataRow;
                for (int row = 0; row <= maxRow; row++)
                {
                    string shapeName = sheet.Cells[row, 0].StringValue?.Trim();
                    string value = sheet.Cells[row, 1].StringValue?.Trim();

                    if (!string.IsNullOrEmpty(shapeName))
                    {
                        shapeValues[shapeName] = value ?? string.Empty;
                    }
                }

                // Iterate all pages and shapes, applying the values as Data1 (you can also use Data2/Data3)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the universal name (NameU) to match the Excel entry
                        if (shapeValues.TryGetValue(shape.NameU, out string newValue))
                        {
                            shape.Data1 = newValue;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }