using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Create a new Excel workbook
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];

            // Write header row
            sheet.Cells[0, 0].PutValue("Page Index");
            sheet.Cells[0, 1].PutValue("Shape ID");
            sheet.Cells[0, 2].PutValue("Shape NameU");
            sheet.Cells[0, 3].PutValue("Property Name");
            sheet.Cells[0, 4].PutValue("Property Value");

            int row = 1;

            // Iterate through pages and shapes
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // If the shape has custom properties, write each one
                    if (shape.Props != null && shape.Props.Count > 0)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            sheet.Cells[row, 0].PutValue(p);
                            sheet.Cells[row, 1].PutValue(shape.ID);
                            sheet.Cells[row, 2].PutValue(shape.NameU);
                            sheet.Cells[row, 3].PutValue(prop.Name);
                            sheet.Cells[row, 4].PutValue(prop.Value.Val);
                            row++;
                        }
                    }
                    else
                    {
                        // Shape without custom properties – write empty property columns
                        sheet.Cells[row, 0].PutValue(p);
                        sheet.Cells[row, 1].PutValue(shape.ID);
                        sheet.Cells[row, 2].PutValue(shape.NameU);
                        sheet.Cells[row, 3].PutValue(string.Empty);
                        sheet.Cells[row, 4].PutValue(string.Empty);
                        row++;
                    }
                }
            }

            // Save the Excel file
            string excelPath = "output.xlsx";
            workbook.Save(excelPath);
            Console.WriteLine($"Custom properties exported to {excelPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
