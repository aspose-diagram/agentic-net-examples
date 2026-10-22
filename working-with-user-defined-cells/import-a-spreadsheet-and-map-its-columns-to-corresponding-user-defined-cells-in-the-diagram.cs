using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, spreadsheet file, output diagram file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <excelPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string excelPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load the Excel workbook
                Workbook workbook = new Workbook(excelPath);
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                // Determine used range dimensions (zero‑based indices)
                int maxRow = cells.MaxDataRow;      // last row with data
                int maxCol = cells.MaxDataColumn;   // last column with data

                if (maxRow < 1 || maxCol < 0)
                {
                    Console.Error.WriteLine("Excel sheet does not contain enough data.");
                    return;
                }

                // Read header row (first row) to get user‑defined cell names
                // Header column 0 is expected to contain the shape identifier
                var headerNames = new string[maxCol + 1];
                for (int col = 0; col <= maxCol; col++)
                {
                    headerNames[col] = cells[0, col].StringValue?.Trim() ?? string.Empty;
                }

                // Iterate over each data row (starting from row 1)
                for (int row = 1; row <= maxRow; row++)
                {
                    // First column contains the shape universal name (NameU)
                    string shapeName = cells[row, 0].StringValue?.Trim();
                    if (string.IsNullOrEmpty(shapeName))
                        continue; // Skip rows without a shape identifier

                    // Find the shape with matching NameU across all pages
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (!string.IsNullOrEmpty(shape.NameU) &&
                                shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                            {
                                targetShape = shape;
                                break;
                            }
                        }
                        if (targetShape != null)
                            break;
                    }

                    if (targetShape == null)
                    {
                        Console.Error.WriteLine($"Shape with NameU '{shapeName}' not found in diagram.");
                        continue;
                    }

                    // Map each column value to the corresponding user‑defined cell
                    for (int col = 1; col <= maxCol; col++)
                    {
                        string cellName = headerNames[col];
                        if (string.IsNullOrEmpty(cellName))
                            continue; // Skip empty header cells

                        string cellValue = cells[row, col].StringValue ?? string.Empty;

                        // Search for an existing user‑defined cell with the same NameU
                        User existingUser = null;
                        foreach (User user in targetShape.Users)
                        {
                            if (!string.IsNullOrEmpty(user.NameU) &&
                                user.NameU.Equals(cellName, StringComparison.OrdinalIgnoreCase))
                            {
                                existingUser = user;
                                break;
                            }
                        }

                        if (existingUser != null)
                        {
                            // Update the existing cell value
                            existingUser.Value.Val = cellValue;
                        }
                        else
                        {
                            // Create a new user‑defined cell and add it to the shape
                            User newUser = new User();
                            newUser.NameU = cellName;          // universal row name
                            newUser.Name = cellName;           // row name (optional)
                            newUser.Value.Val = cellValue;     // cell value
                            newUser.Prompt.Value = string.Empty; // optional prompt
                            targetShape.Users.Add(newUser);
                        }
                    }
                }

                // Save the updated diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}