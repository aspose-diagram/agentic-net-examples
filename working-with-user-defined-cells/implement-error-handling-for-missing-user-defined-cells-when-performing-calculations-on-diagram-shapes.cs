using System;
using System.IO;
using System.Globalization;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, user‑defined cell name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <UserCellName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetCellName = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        bool cellFound = false;

                        // Search for the user‑defined cell by NameU
                        for (int userIndex = 0; userIndex < shape.Users.Count; userIndex++)
                        {
                            User userCell = shape.Users[userIndex];

                            if (string.Equals(userCell.NameU, targetCellName, StringComparison.OrdinalIgnoreCase))
                            {
                                cellFound = true;

                                // Attempt to parse the cell value as a double
                                double currentValue;
                                if (double.TryParse(userCell.Value.Val, NumberStyles.Any, CultureInfo.InvariantCulture, out currentValue))
                                {
                                    // Example calculation: double the value
                                    double newValue = currentValue * 2;

                                    // Write the new value back to the cell
                                    userCell.Value.Val = newValue.ToString(CultureInfo.InvariantCulture);
                                    Console.WriteLine($"Shape ID {shape.ID}: Updated \"{targetCellName}\" from {currentValue} to {newValue}.");
                                }
                                else
                                {
                                    Console.Error.WriteLine($"Shape ID {shape.ID}: Unable to parse value \"{userCell.Value.Val}\" of cell \"{targetCellName}\" as a number.");
                                }

                                // No need to continue searching this shape
                                break;
                            }
                        }

                        // If the cell was not found, report the missing cell for this shape
                        if (!cellFound)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID}: User‑defined cell \"{targetCellName}\" not found.");
                        }
                    }
                }

                // Save the modified diagram to the output path
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
                }
                catch (Exception saveEx)
                {
                    Console.Error.WriteLine($"Error saving diagram: {saveEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}