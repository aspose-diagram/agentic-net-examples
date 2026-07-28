using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Cells;
using System.IO;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram file path (adjust as needed)
                string diagramPath = "input.vsdx";

                // Output Excel workbook file path
                string excelPath = "ShapeData.xlsx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                // Write header row
                cells[0, 0].PutValue("Page Name");
                cells[0, 1].PutValue("Shape ID");
                cells[0, 2].PutValue("Name");
                cells[0, 3].PutValue("NameU");
                cells[0, 4].PutValue("Data1");
                cells[0, 5].PutValue("Data2");
                cells[0, 6].PutValue("Data3");

                int currentRow = 1;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Populate cells with shape information
                        cells[currentRow, 0].PutValue(page.Name);
                        cells[currentRow, 1].PutValue(shape.ID);
                        cells[currentRow, 2].PutValue(shape.Name);
                        cells[currentRow, 3].PutValue(shape.NameU);
                        cells[currentRow, 4].PutValue(shape.Data1);
                        cells[currentRow, 5].PutValue(shape.Data2);
                        cells[currentRow, 6].PutValue(shape.Data3);

                        currentRow++;
                    }
                }

                // Save the Excel workbook
                workbook.Save(excelPath);

                Console.WriteLine($"Shape data exported successfully to '{excelPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }