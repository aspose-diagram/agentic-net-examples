using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output Excel file path (second argument or default)
        string excelPath = args.Length > 1 ? args[1] : "output.xlsx";

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Create a new Excel workbook and get the first worksheet
        Workbook workbook = new Workbook();
        Worksheet sheet = workbook.Worksheets[0];

        // Write header row
        sheet.Cells[0, 0].Value = "PageName";
        sheet.Cells[0, 1].Value = "ShapeID";
        sheet.Cells[0, 2].Value = "ShapeName";
        sheet.Cells[0, 3].Value = "PropertyName";
        sheet.Cells[0, 4].Value = "PropertyValue";

        int currentRow = 1; // Start writing data from the second row

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page name (fallback to empty string if unavailable)
                    string pageName = page.Name ?? string.Empty;

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape identifiers
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU ?? string.Empty;

                        // Guard: skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through custom properties (named shape properties)
                        foreach (Prop prop in shape.Props)
                        {
                            // Property name
                            string propName = prop.Name ?? string.Empty;
                            // Property value (may be empty)
                            string propValue = prop.Value != null ? prop.Value.Val : string.Empty;

                            // Write data to the worksheet
                            sheet.Cells[currentRow, 0].Value = pageName;
                            sheet.Cells[currentRow, 1].Value = shapeId;
                            sheet.Cells[currentRow, 2].Value = shapeName;
                            sheet.Cells[currentRow, 3].Value = propName;
                            sheet.Cells[currentRow, 4].Value = propValue;

                            currentRow++;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
            return;
        }

        try
        {
            // Save the workbook to the specified Excel file
            workbook.Save(excelPath, SaveFormat.Xlsx);
            Console.WriteLine($"Custom properties exported successfully to: {excelPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving Excel file: {ex.Message}");
        }
    }
}