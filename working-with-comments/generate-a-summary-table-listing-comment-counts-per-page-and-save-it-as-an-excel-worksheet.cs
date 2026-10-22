using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputExcelPath>");
            return;
        }

        string visioPath = args[0];
        string excelPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Create a new Excel workbook.
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];

                // Write header row.
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Comment Count");

                int rowIndex = 1; // Start after header.

                // Iterate over each page explicitly typed as Page.
                foreach (Page page in diagram.Pages)
                {
                    // Count annotations (comments) on the current page.
                    int commentCount = page.PageSheet.Annotations.Count;

                    // Write page name and comment count to the worksheet.
                    sheet.Cells[rowIndex, 0].PutValue(page.NameU);
                    sheet.Cells[rowIndex, 1].PutValue(commentCount);
                    rowIndex++;
                }

                // Save the workbook as an Excel file.
                workbook.Save(excelPath, SaveFormat.Xlsx);
                Console.WriteLine($"Comment summary saved to: {excelPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing files: {ex.Message}");
        }
    }
}