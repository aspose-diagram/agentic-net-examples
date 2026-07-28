using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio diagram and the Excel spreadsheet
            string diagramPath = "input.vsdx";
            string excelPath = "data.xlsx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the Excel workbook (first worksheet is used)
            Workbook workbook = new Workbook(excelPath);
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;

            // Assume the first row contains column headers
            int headerRow = 0;
            int firstDataRow = 1;
            int totalColumns = cells.MaxDataColumn + 1;

            // Use the first page of the diagram for shape lookup
            Page page = diagram.Pages[0];

            // Iterate through each data row in the spreadsheet
            for (int row = firstDataRow; row <= cells.MaxDataRow; row++)
            {
                // First column is expected to contain the Shape ID (numeric)
                object shapeIdObj = cells[row, 0].Value;
                if (shapeIdObj == null) continue;

                if (!long.TryParse(shapeIdObj.ToString(), out long shapeIdLong))
                    continue; // Skip rows with invalid shape IDs

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeIdLong);
                if (shape == null) continue; // Shape not found

                // Map each remaining column to a user‑defined cell in the shape
                for (int col = 1; col < totalColumns; col++)
                {
                    // Header name becomes the user‑defined cell name
                    string userCellName = cells[headerRow, col].StringValue;
                    if (string.IsNullOrWhiteSpace(userCellName)) continue;

                    // Cell value to store
                    string cellValue = cells[row, col].StringValue ?? string.Empty;

                    // Search for an existing user cell with the same name
                    User existingUser = null;
                    foreach (User u in shape.Users)
                    {
                        if (u.Name == userCellName)
                        {
                            existingUser = u;
                            break;
                        }
                    }

                    if (existingUser != null)
                    {
                        // Update existing user cell
                        existingUser.Value.Val = cellValue;
                    }
                    else
                    {
                        // Create a new user‑defined cell and add it to the shape
                        User newUser = new User();
                        newUser.Name = userCellName;
                        newUser.Value.Val = cellValue;
                        shape.Users.Add(newUser);
                    }
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
