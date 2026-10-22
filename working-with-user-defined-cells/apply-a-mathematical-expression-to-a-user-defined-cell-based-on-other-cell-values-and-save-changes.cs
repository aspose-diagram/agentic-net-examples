using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for input Visio file path
        Console.Write("Enter the path to the input Visio file: ");
        string inputPath = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prompt for output Visio file path
        Console.Write("Enter the desired path for the output Visio file: ");
        string outputPath = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(outputPath))
        {
            Console.Error.WriteLine("Output path cannot be empty.");
            return;
        }

        // Prompt for the name of the target user-defined cell (where the result will be stored)
        Console.Write("Enter the name of the target user-defined cell: ");
        string targetCellName = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(targetCellName))
        {
            Console.Error.WriteLine("Target cell name cannot be empty.");
            return;
        }

        // Prompt for the first operand cell name
        Console.Write("Enter the name of the first operand cell: ");
        string operand1Name = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(operand1Name))
        {
            Console.Error.WriteLine("First operand cell name cannot be empty.");
            return;
        }

        // Prompt for the second operand cell name
        Console.Write("Enter the name of the second operand cell: ");
        string operand2Name = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(operand2Name))
        {
            Console.Error.WriteLine("Second operand cell name cannot be empty.");
            return;
        }

        // Prompt for the mathematical operation (+, -, *, /)
        Console.Write("Enter the operation (+, -, *, /): ");
        string operation = Console.ReadLine()?.Trim() ?? string.Empty;
        if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
        {
            Console.Error.WriteLine("Invalid operation. Supported operations are +, -, *, /.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Users collection
                        if (shape.Users == null)
                            continue;

                        User operand1 = null;
                        User operand2 = null;
                        User targetCell = null;

                        // Locate the required user-defined cells by NameU
                        foreach (User user in shape.Users)
                        {
                            if (user.NameU.Equals(operand1Name, StringComparison.OrdinalIgnoreCase))
                                operand1 = user;
                            else if (user.NameU.Equals(operand2Name, StringComparison.OrdinalIgnoreCase))
                                operand2 = user;
                            else if (user.NameU.Equals(targetCellName, StringComparison.OrdinalIgnoreCase))
                                targetCell = user;
                        }

                        // Both operand cells must exist to compute the expression
                        if (operand1 == null || operand2 == null)
                            continue;

                        // Try parsing numeric values from operand cells
                        if (!double.TryParse(operand1.Value.Val, out double val1) ||
                            !double.TryParse(operand2.Value.Val, out double val2))
                        {
                            // Skip shapes where operands are not numeric
                            continue;
                        }

                        // Compute the result based on the selected operation
                        double result = operation switch
                        {
                            "+" => val1 + val2,
                            "-" => val1 - val2,
                            "*" => val1 * val2,
                            "/" => val2 != 0 ? val1 / val2 : double.NaN,
                            _ => double.NaN
                        };

                        // If division by zero occurred, skip updating
                        if (double.IsNaN(result))
                            continue;

                        // If the target cell exists, update its value; otherwise, create it
                        if (targetCell != null)
                        {
                            targetCell.Value.Val = result.ToString();
                        }
                        else
                        {
                            User newUser = new User
                            {
                                Name = targetCellName,
                                NameU = targetCellName,
                                Value = { Val = result.ToString() }
                            };
                            shape.Users.Add(newUser);
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}