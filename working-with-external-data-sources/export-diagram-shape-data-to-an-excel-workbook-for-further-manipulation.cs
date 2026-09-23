using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram file path
                string diagramPath = "input.vsdx";

                // Output Excel workbook path
                string excelPath = "ShapeData.xlsx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write header row
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Shape ID");
                sheet.Cells[0, 2].PutValue("Shape NameU");
                sheet.Cells[0, 3].PutValue("Master Name");
                sheet.Cells[0, 4].PutValue("Text");
                sheet.Cells[0, 5].PutValue("Data1");
                sheet.Cells[0, 6].PutValue("Data2");
                sheet.Cells[0, 7].PutValue("Data3");

                int currentRow = 1;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Extract required information
                        string pageName = page.Name;
                        long shapeId = shape.ID;
                        string shapeNameU = shape.NameU;
                        string masterName = shape.Master != null ? shape.Master.Name : string.Empty;
                        string shapeText = shape.Text.Value.Text; // plain text
                        string data1 = shape.Data1 ?? string.Empty;
                        string data2 = shape.Data2 ?? string.Empty;
                        string data3 = shape.Data3 ?? string.Empty;

                        // Write data to the worksheet
                        sheet.Cells[currentRow, 0].PutValue(pageName);
                        sheet.Cells[currentRow, 1].PutValue(shapeId);
                        sheet.Cells[currentRow, 2].PutValue(shapeNameU);
                        sheet.Cells[currentRow, 3].PutValue(masterName);
                        sheet.Cells[currentRow, 4].PutValue(shapeText);
                        sheet.Cells[currentRow, 5].PutValue(data1);
                        sheet.Cells[currentRow, 6].PutValue(data2);
                        sheet.Cells[currentRow, 7].PutValue(data3);

                        currentRow++;
                    }
                }

                // Save the Excel workbook
                workbook.Save(excelPath, SaveFormat.Xlsx);

                Console.WriteLine($"Export completed. Excel file saved to: {excelPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }