using System;
using Aspose.Diagram;
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
                string excelPath = "CustomProperties.xlsx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                // Write header row
                cells[0, 0].PutValue("Page Name");
                cells[0, 1].PutValue("Shape ID");
                cells[0, 2].PutValue("Shape Name");
                cells[0, 3].PutValue("Property Name");
                cells[0, 4].PutValue("Property Value");

                int currentRow = 1;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape has custom properties, iterate them
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                cells[currentRow, 0].PutValue(page.Name);
                                cells[currentRow, 1].PutValue(shape.ID);
                                cells[currentRow, 2].PutValue(shape.Name);
                                cells[currentRow, 3].PutValue(prop.Name);
                                cells[currentRow, 4].PutValue(prop.Value.Val);
                                currentRow++;
                            }
                        }
                    }
                }

                // Save the Excel workbook
                workbook.Save(excelPath, SaveFormat.Xlsx);

                Console.WriteLine($"Custom properties exported successfully to '{excelPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }