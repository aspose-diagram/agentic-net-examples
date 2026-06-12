using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Cells;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source files and the output diagram
                string excelPath = "data.xlsx";
                string diagramPath = "template.vsdx";
                string outputPath = "mapped_output.vsdx";

                // Load the Excel workbook
                Workbook workbook = new Workbook(excelPath);
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Assume first row contains headers
                int headerRow = 0;
                int firstDataRow = 1;

                // Read header names (user-defined cell names)
                int totalColumns = cells.MaxColumn + 1;
                string[] headers = new string[totalColumns];
                for (int col = 0; col < totalColumns; col++)
                {
                    headers[col] = cells[headerRow, col].StringValue?.Trim();
                }

                // Iterate over each data row
                for (int row = firstDataRow; row <= cells.MaxRow; row++)
                {
                    // Expect a column named "ShapeName" that identifies the target shape
                    string shapeName = null;
                    for (int col = 0; col < totalColumns; col++)
                    {
                        if (string.Equals(headers[col], "ShapeName", StringComparison.OrdinalIgnoreCase))
                        {
                            shapeName = cells[row, col].StringValue?.Trim();
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(shapeName))
                    {
                        Console.WriteLine($"Row {row + 1}: ShapeName not found, skipping row.");
                        continue;
                    }

                    // Locate the shape by its NameU (universal name) or Name
                    Shape targetShape = FindShapeByName(diagram, shapeName);
                    if (targetShape == null)
                    {
                        Console.WriteLine($"Row {row + 1}: Shape '{shapeName}' not found in diagram.");
                        continue;
                    }

                    // Map each column (except ShapeName) to a user-defined cell
                    for (int col = 0; col < totalColumns; col++)
                    {
                        string header = headers[col];
                        if (string.IsNullOrEmpty(header) || string.Equals(header, "ShapeName", StringComparison.OrdinalIgnoreCase))
                            continue; // Skip empty headers and the identifier column

                        string cellValue = cells[row, col].StringValue ?? string.Empty;

                        // Find existing user-defined cell
                        User userCell = null;
                        foreach (User u in targetShape.Users)
                        {
                            if (string.Equals(u.Name, header, StringComparison.OrdinalIgnoreCase))
                            {
                                userCell = u;
                                break;
                            }
                        }

                        // If not found, create a new one
                        if (userCell == null)
                        {
                            userCell = new User();
                            userCell.Name = header;
                            targetShape.Users.Add(userCell);
                        }

                        // Assign the value (as string)
                        userCell.Value.Val = cellValue;
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape by its NameU or Name across all pages
        private static Shape FindShapeByName(Diagram diagram, string shapeName)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (string.Equals(shape.NameU, shapeName, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(shape.Name, shapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        return shape;
                    }
                }
            }
            return null;
        }
    }