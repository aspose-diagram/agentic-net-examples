using System.IO;
using System;

using Aspose.Diagram;          // Aspose.Diagram for Visio handling
using Aspose.Cells;           // Aspose.Cells for Excel handling

class VisioToExcel
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioFile = "input.vsdx";

            // Path to the destination Excel file
            string excelFile = "output.xlsx";

            // Load the Visio diagram using the Diagram constructor (load rule)
            Diagram diagram = new Diagram(visioFile);

            // Create a new Excel workbook (create rule)
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];

            // Write header row
            int row = 0;
            sheet.Cells[row, 0].PutValue("Page");
            sheet.Cells[row, 1].PutValue("Shape ID");
            sheet.Cells[row, 2].PutValue("Shape Name");
            sheet.Cells[row, 3].PutValue("Property Name");
            sheet.Cells[row, 4].PutValue("Property Value");
            row++;

            // Iterate through all pages, shapes and their custom properties
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may contain zero or more custom properties (Prop objects)
                    foreach (Prop prop in shape.Props)
                    {
                        sheet.Cells[row, 0].PutValue(page.Name);
                        sheet.Cells[row, 1].PutValue(shape.ID);
                        sheet.Cells[row, 2].PutValue(shape.Name);
                        sheet.Cells[row, 3].PutValue(prop.Name);
                        // Prop.Value holds the property value; convert to string safely
                        sheet.Cells[row, 4].PutValue(prop.Value?.ToString() ?? string.Empty);
                        row++;
                    }
                }
            }

            // Save the Excel workbook to the specified file (save rule)
            workbook.Save(excelFile);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
