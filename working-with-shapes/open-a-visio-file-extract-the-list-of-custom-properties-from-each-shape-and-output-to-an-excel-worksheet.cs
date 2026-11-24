using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputExcelPath>");
            return;
        }

        string visioPath = args[0];
        string excelPath = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Load the Visio diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio file: {ex.Message}");
            return;
        }

        // Create a new Excel workbook.
        Workbook workbook = new Workbook();
        Worksheet sheet = workbook.Worksheets[0];

        // Write header row.
        sheet.Cells[0, 0].PutValue("Page Name");
        sheet.Cells[0, 1].PutValue("Shape ID");
        sheet.Cells[0, 2].PutValue("Shape Name");
        sheet.Cells[0, 3].PutValue("Property Name");
        sheet.Cells[0, 4].PutValue("Property Value");

        int currentRow = 1; // Start after header.

        // Iterate through pages, shapes, and custom properties.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Each shape may have zero or more custom properties (Props).
                foreach (Prop prop in shape.Props)
                {
                    // Write a row for each custom property.
                    sheet.Cells[currentRow, 0].PutValue(page.NameU ?? string.Empty);
                    sheet.Cells[currentRow, 1].PutValue(shape.ID);
                    sheet.Cells[currentRow, 2].PutValue(shape.NameU ?? string.Empty);
                    sheet.Cells[currentRow, 3].PutValue(prop.Name);
                    sheet.Cells[currentRow, 4].PutValue(prop.Value.Val);
                    currentRow++;
                }
            }
        }

        // Save the Excel workbook.
        try
        {
            workbook.Save(excelPath);
            Console.WriteLine($"Custom properties exported successfully to '{excelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving Excel file: {ex.Message}");
        }
    }
}