using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file (replace with actual path)
            string diagramPath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Prepare a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write table headers
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Comment Count");

                int rowIndex = 1; // Start writing data from the second row

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the number of comments (annotations) on the current page
                    int commentCount = page.PageSheet.Annotations != null ? page.PageSheet.Annotations.Count : 0;

                    // Write page name and comment count to the worksheet
                    sheet.Cells[rowIndex, 0].PutValue(page.Name);
                    sheet.Cells[rowIndex, 1].PutValue(commentCount);

                    rowIndex++;
                }

                // Save the Excel file
                string excelPath = "CommentSummary.xlsx";
                workbook.Save(excelPath, SaveFormat.Xlsx);
                Console.WriteLine($"Comment summary saved to '{excelPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
