using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";

                // Output Excel file path
                string excelPath = "CommentSummary.xlsx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Create a new Excel workbook
                    Workbook workbook = new Workbook();
                    Worksheet sheet = workbook.Worksheets[0];

                    // Write header row
                    sheet.Cells[0, 0].PutValue("Page Name");
                    sheet.Cells[0, 1].PutValue("Comment Count");

                    int rowIndex = 1;

                    // Iterate through each page and count annotations (comments)
                    foreach (Page page in diagram.Pages)
                    {
                        // Annotations are stored in the PageSheet of each page
                        int commentCount = page.PageSheet.Annotations.Count;

                        // Write page name and comment count to the worksheet
                        sheet.Cells[rowIndex, 0].PutValue(page.Name);
                        sheet.Cells[rowIndex, 1].PutValue(commentCount);

                        rowIndex++;
                    }

                    // Save the workbook as an Excel file
                    workbook.Save(excelPath, SaveFormat.Xlsx);
                }

                Console.WriteLine("Comment summary has been saved to " + excelPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }