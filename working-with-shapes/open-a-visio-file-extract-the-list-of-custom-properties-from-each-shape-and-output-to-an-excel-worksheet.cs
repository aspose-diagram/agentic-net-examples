using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to input Visio file and output Excel file
                string visioPath = "input.vsdx";
                string excelPath = "output.xlsx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                // Write header row
                cells[0, 0].PutValue("Page");
                cells[0, 1].PutValue("ShapeID");
                cells[0, 2].PutValue("ShapeName");
                cells[0, 3].PutValue("PropName");
                cells[0, 4].PutValue("PropValue");

                int rowIndex = 1;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has custom properties
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                cells[rowIndex, 0].PutValue(page.Name);
                                cells[rowIndex, 1].PutValue(shape.ID);
                                cells[rowIndex, 2].PutValue(shape.Name);
                                cells[rowIndex, 3].PutValue(prop.Name);
                                cells[rowIndex, 4].PutValue(prop.Value.Val);
                                rowIndex++;
                            }
                        }
                    }
                }

                // Save the Excel workbook
                workbook.Save(excelPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }