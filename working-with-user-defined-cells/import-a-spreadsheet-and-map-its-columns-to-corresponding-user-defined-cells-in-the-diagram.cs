using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram file path
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Input spreadsheet file path (Excel workbook)
        string spreadsheetPath = "data.xlsx";
        if (!File.Exists(spreadsheetPath))
        {
            Console.Error.WriteLine($"File not found: {spreadsheetPath}");
            return;
        }

        // Output diagram file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the Excel workbook
            Workbook workbook = new Workbook(spreadsheetPath);
            Worksheet sheet = workbook.Worksheets[0]; // use the first worksheet

            // Read header row to get user-defined cell names (starting from column 1)
            int headerRow = 0;
            int firstDataRow = 1; // assume data starts after header
            int totalColumns = sheet.Cells.MaxColumn + 1; // total columns in the sheet

            // Iterate over each data row
            for (int row = firstDataRow; row <= sheet.Cells.MaxRow; row++)
            {
                // First column contains the shape name (case‑insensitive match)
                string shapeName = sheet.Cells[row, 0].StringValue?.Trim();
                if (string.IsNullOrEmpty(shapeName))
                    continue; // skip rows without a shape identifier

                // Locate the shape by its universal name across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                // If the shape is not found, log and continue with next row
                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape not found for name: {shapeName}");
                    continue;
                }

                // Map each column (starting from 1) to a user‑defined cell
                for (int col = 1; col < totalColumns; col++)
                {
                    // Header cell provides the user‑defined cell name
                    string userCellName = sheet.Cells[headerRow, col].StringValue?.Trim();
                    if (string.IsNullOrEmpty(userCellName))
                        continue; // skip empty headers

                    // Data cell provides the value to store
                    string cellValue = sheet.Cells[row, col].StringValue?.Trim() ?? string.Empty;

                    // Search for an existing user‑defined cell with the same name
                    User existingUser = null;
                    foreach (User user in targetShape.Users)
                    {
                        if (user.Name.Equals(userCellName, StringComparison.OrdinalIgnoreCase))
                        {
                            existingUser = user;
                            break;
                        }
                    }

                    if (existingUser != null)
                    {
                        // Update the value of the existing user‑defined cell
                        existingUser.Value.Val = cellValue;
                    }
                    else
                    {
                        // Create a new user‑defined cell and add it to the shape
                        User newUser = new User
                        {
                            Name = userCellName,
                            Value = { Val = cellValue }
                        };
                        targetShape.Users.Add(newUser);
                    }
                }
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors from Aspose or I/O operations
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}