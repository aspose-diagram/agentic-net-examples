using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Cells;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // Output Excel file path
                string excelPath = "ShapeData.xlsx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write header row
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Shape ID");
                sheet.Cells[0, 2].PutValue("Shape Name");
                sheet.Cells[0, 3].PutValue("Shape NameU");
                sheet.Cells[0, 4].PutValue("Shape Text");
                sheet.Cells[0, 5].PutValue("Data1");
                sheet.Cells[0, 6].PutValue("Data2");
                sheet.Cells[0, 7].PutValue("Data3");

                int currentRow = 1;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string plainText = shape.Text.Value.Text;

                        // Write shape data to the worksheet
                        sheet.Cells[currentRow, 0].PutValue(page.Name);
                        sheet.Cells[currentRow, 1].PutValue(shape.ID);
                        sheet.Cells[currentRow, 2].PutValue(shape.Name);
                        sheet.Cells[currentRow, 3].PutValue(shape.NameU);
                        sheet.Cells[currentRow, 4].PutValue(plainText);
                        sheet.Cells[currentRow, 5].PutValue(shape.Data1);
                        sheet.Cells[currentRow, 6].PutValue(shape.Data2);
                        sheet.Cells[currentRow, 7].PutValue(shape.Data3);

                        currentRow++;
                    }
                }

                // Save the workbook to an Excel file
                workbook.Save(excelPath, SaveFormat.Xlsx);

                Console.WriteLine($"Shape data exported successfully to '{excelPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }