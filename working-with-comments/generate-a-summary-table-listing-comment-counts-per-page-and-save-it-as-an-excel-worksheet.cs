using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write table headers
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Comment Count");

                int currentRow = 1;

                // Iterate through each page and count its comments (annotations)
                foreach (Page page in diagram.Pages)
                {
                    int commentCount = page.PageSheet.Annotations.Count;
                    string pageName = page.Name;

                    // Write the page name and comment count to the worksheet
                    sheet.Cells[currentRow, 0].PutValue(pageName);
                    sheet.Cells[currentRow, 1].PutValue(commentCount);

                    currentRow++;
                }

                // Save the summary as an Excel file
                workbook.Save("CommentSummary.xlsx", SaveFormat.Xlsx);

                // Clean up resources
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }