using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram file
        string diagramPath = "input.vsdx";
        // Guard: ensure the Visio file exists
        if (!File.Exists(diagramPath)) { Console.Error.WriteLine($"File not found: {diagramPath}"); return; }

        // Output Excel file
        string excelPath = "CommentSummary.xlsx";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write table header
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Comment Count");

                int rowIndex = 1;

                // Iterate through each page and count comments
                foreach (Page page in diagram.Pages)
                {
                    // Count annotations (comments) on the page
                    int commentCount = page.PageSheet.Annotations.Count;
                    // Page.Name is a plain string, no .Value property
                    string pageName = page.Name;

                    // Write page name and comment count to the worksheet
                    sheet.Cells[rowIndex, 0].PutValue(pageName);
                    sheet.Cells[rowIndex, 1].PutValue(commentCount);
                    rowIndex++;
                }

                // Save the workbook as an Excel file
                workbook.Save(excelPath, SaveFormat.Xlsx);
            }

            Console.WriteLine($"Comment summary saved to {excelPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}