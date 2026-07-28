using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Attempt to retrieve the user-defined cell named "CustomValue"
                    string cellValue = GetUserCellValue(shape, "CustomValue");

                    if (cellValue == null)
                    {
                        Console.WriteLine($"Shape ID {shape.ID}: Missing user-defined cell 'CustomValue'. Skipping calculation.");
                        continue; // Skip this shape because the required cell is absent
                    }

                    // Try to parse the cell value as a double for calculation
                    if (!double.TryParse(cellValue, out double numericValue))
                    {
                        Console.WriteLine($"Shape ID {shape.ID}: Invalid numeric value '{cellValue}' in 'CustomValue'. Skipping calculation.");
                        continue; // Skip this shape due to invalid data
                    }

                    // Perform a simple calculation (e.g., double the value)
                    double result = numericValue * 2;

                    // Store the result back into a user-defined cell named "Result"
                    SetOrUpdateUserCell(shape, "Result", result.ToString());

                    Console.WriteLine($"Shape ID {shape.ID}: Calculated result {result} and stored in 'Result' cell.");
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Retrieves the value of a user-defined cell; returns null if not found
    static string GetUserCellValue(Shape shape, string cellName)
    {
        if (shape.Users == null)
            return null;

        foreach (User user in shape.Users)
        {
            if (user.Name == cellName || user.NameU == cellName)
            {
                // Return the cell's string value (may be null)
                return user.Value?.Val;
            }
        }
        return null;
    }

    // Updates an existing user-defined cell or adds a new one if it does not exist
    static void SetOrUpdateUserCell(Shape shape, string cellName, string value)
    {
        if (shape.Users == null)
        {
            Console.Error.WriteLine($"Shape ID {shape.ID} has a null Users collection.");
            return;
        }

        foreach (User user in shape.Users)
        {
            if (user.Name == cellName || user.NameU == cellName)
            {
                // Update the existing cell value
                user.Value.Val = value;
                return;
            }
        }

        // Cell not found; create a new one and add it to the collection
        User newUser = new User
        {
            Name = cellName,
            // Initialise the Value object and set its Val property
            Value = { Val = value }
        };
        shape.Users.Add(newUser);
    }
}