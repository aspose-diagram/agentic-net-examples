using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Excel file path
        string outputPath = "CommentSummary.xlsx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new Excel workbook
                Workbook workbook = new Workbook();
                // Access the first worksheet (created by default)
                Worksheet sheet = workbook.Worksheets[0];
                // Write header row
                sheet.Cells[0, 0].PutValue("Page Name");
                sheet.Cells[0, 1].PutValue("Comment Count");

                int rowIndex = 1; // Start after header

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page name (fallback to "Unnamed" if null)
                    string pageName = page.Name ?? "Unnamed";

                    // Count annotations (comments) on the page
                    int commentCount = page.PageSheet.Annotations.Count;

                    // Write data to the worksheet
                    sheet.Cells[rowIndex, 0].PutValue(pageName);
                    sheet.Cells[rowIndex, 1].PutValue(commentCount);
                    rowIndex++;
                }

                // Save the workbook to the specified Excel file
                workbook.Save(outputPath, SaveFormat.Xlsx);
                Console.WriteLine($"Comment summary saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}