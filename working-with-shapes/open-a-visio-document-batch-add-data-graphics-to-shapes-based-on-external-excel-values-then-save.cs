using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Cells;

class Program
{
    static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string visioPath = "input.vsdx";
            string excelPath = "data.xlsx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Load the Excel workbook
            Workbook workbook = new Workbook(excelPath);
            Worksheet sheet = workbook.Worksheets[0];

            // Build a map: Shape NameU -> Data value (assumes column A = shape name, column B = value)
            var shapeData = new System.Collections.Generic.Dictionary<string, string>();
            int lastRow = sheet.Cells.MaxDataRow;
            for (int i = 1; i <= lastRow; i++) // start at 1 to skip header row
            {
                string shapeName = sheet.Cells[i, 0].StringValue?.Trim();
                string value = sheet.Cells[i, 1].StringValue?.Trim();

                if (!string.IsNullOrEmpty(shapeName))
                {
                    shapeData[shapeName] = value ?? string.Empty;
                }
            }

            // Apply data graphics: set Data1 of each matching shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // If the shape's universal name exists in the Excel map, assign the value
                    if (shapeData.TryGetValue(shape.NameU, out string val))
                    {
                        shape.Data1 = val; // Direct assignment, no .Value
                        Console.WriteLine($"Data applied to shape '{shape.NameU}' (ID {shape.ID})");
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
