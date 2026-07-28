using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Cells;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the Visio file and the Excel data source
                string visioPath = "input.vsdx";
                string excelPath = "data.xlsx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Load the Excel workbook
                Workbook workbook = new Workbook(excelPath);
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                // Build a map of column header (shape name) to column index
                Dictionary<string, int> headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                int maxColumn = cells.MaxColumn + 1; // columns are zero‑based
                for (int col = 0; col < maxColumn; col++)
                {
                    string header = cells[0, col].StringValue?.Trim();
                    if (!string.IsNullOrEmpty(header) && !headerMap.ContainsKey(header))
                    {
                        headerMap[header] = col;
                    }
                }

                // Assume data starts from the second row (index 1)
                int dataRowIndex = 1;

                // Process shapes on the first page
                Page page = diagram.Pages[0];
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the universal name of the shape to find matching Excel column
                    string shapeName = shape.NameU?.Trim();
                    if (string.IsNullOrEmpty(shapeName))
                        continue;

                    if (headerMap.TryGetValue(shapeName, out int colIndex))
                    {
                        // Retrieve the cell value from the Excel sheet
                        string cellValue = cells[dataRowIndex, colIndex].StringValue ?? string.Empty;

                        // Assign the value to the shape's Data1 field (you can also use Data2/Data3 as needed)
                        shape.Data1 = cellValue;

                        Console.WriteLine($"Shape '{shapeName}' (ID {shape.ID}) updated with value '{cellValue}'.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }