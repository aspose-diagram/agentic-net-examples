using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write header row
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Shape ID");
                sheet.Cells[0, 2].PutValue("Shape Name");
                sheet.Cells[0, 3].PutValue("Universal Name");
                sheet.Cells[0, 4].PutValue("Data1");
                sheet.Cells[0, 5].PutValue("Data2");
                sheet.Cells[0, 6].PutValue("Data3");

                int currentRow = 1;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Populate Excel cells with shape information
                        sheet.Cells[currentRow, 0].PutValue(page.Name);
                        sheet.Cells[currentRow, 1].PutValue(shape.ID);
                        sheet.Cells[currentRow, 2].PutValue(shape.Name);
                        sheet.Cells[currentRow, 3].PutValue(shape.NameU);
                        sheet.Cells[currentRow, 4].PutValue(shape.Data1);
                        sheet.Cells[currentRow, 5].PutValue(shape.Data2);
                        sheet.Cells[currentRow, 6].PutValue(shape.Data3);

                        currentRow++;
                    }
                }

                // Save the workbook to an Excel file
                string excelPath = "DiagramShapeData.xlsx";
                workbook.Save(excelPath);

                Console.WriteLine($"Export completed. Excel file saved to: {excelPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }