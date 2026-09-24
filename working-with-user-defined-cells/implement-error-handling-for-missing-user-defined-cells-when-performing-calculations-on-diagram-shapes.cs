using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Retrieves the value of a user‑defined cell by name.
    // Returns null if the cell does not exist.
    static string GetUserCellValue(Shape shape, string cellName)
    {
        if (shape == null || shape.Users == null)
            return null;

        foreach (User user in shape.Users)
        {
            if (user == null)
                continue;

            // Compare both the internal name and the universal name.
            if (string.Equals(user.Name, cellName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(user.NameU, cellName, StringComparison.OrdinalIgnoreCase))
            {
                return user.Value?.Val;
            }
        }

        return null; // Cell not found.
    }

    static void Main()
    {
        try
        {

            // Path to the source Visio file.
            string inputPath = "input.vsdx";
            // Path for the output Visio file after processing.
            string outputPath = "output.vsdx";

            // Load the diagram.
            Diagram diagram;
            using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                diagram = new Diagram(fs);
            }

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example: we expect a user‑defined cell named "Multiplier".
                    string cellValue = GetUserCellValue(shape, "Multiplier");

                    if (cellValue == null)
                    {
                        Console.WriteLine($"Warning: Shape ID {shape.ID} on page '{page.Name}' is missing the 'Multiplier' user‑defined cell. Skipping calculation.");
                        continue; // Skip this shape because required data is absent.
                    }

                    // Try to parse the cell value as a double.
                    if (!double.TryParse(cellValue, out double multiplier))
                    {
                        Console.WriteLine($"Error: Shape ID {shape.ID} has an invalid 'Multiplier' value ('{cellValue}'). Expected a numeric value. Skipping.");
                        continue;
                    }

                    // Perform a simple calculation: increase the shape's width by the multiplier.
                    try
                    {
                        double originalWidth = shape.XForm.Width.Value;
                        double newWidth = originalWidth * multiplier;
                        shape.XForm.Width.Value = newWidth;

                        Console.WriteLine($"Shape ID {shape.ID}: Width changed from {originalWidth} to {newWidth} using multiplier {multiplier}.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception while updating shape ID {shape.ID}: {ex.Message}");
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
