using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked for deletion.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Users collection.
                        if (shape.Users == null)
                            continue;

                        // Locate the user‑defined cells we need.
                        User widthCell = null;
                        User heightCell = null;
                        User resultCell = null;

                        foreach (User user in shape.Users)
                        {
                            // Compare universal names (NameU) to be case‑insensitive.
                            string nameU = user.NameU?.Trim();
                            if (string.Equals(nameU, "Width", StringComparison.OrdinalIgnoreCase))
                                widthCell = user;
                            else if (string.Equals(nameU, "Height", StringComparison.OrdinalIgnoreCase))
                                heightCell = user;
                            else if (string.Equals(nameU, "Result", StringComparison.OrdinalIgnoreCase))
                                resultCell = user;
                        }

                        // Proceed only if both operand cells are present.
                        if (widthCell != null && heightCell != null)
                        {
                            // Try to parse the numeric values.
                            if (double.TryParse(widthCell.Value.Val, out double width) &&
                                double.TryParse(heightCell.Value.Val, out double height))
                            {
                                double result = width * height; // Example expression: multiplication.

                                // If the Result cell exists, update it; otherwise, create it.
                                if (resultCell != null)
                                {
                                    resultCell.Value.Val = result.ToString();
                                }
                                else
                                {
                                    User newResult = new User
                                    {
                                        Name = "Result",
                                        NameU = "Result",
                                        Value = { Val = result.ToString() },
                                        Prompt = { Value = "Calculated result" }
                                    };
                                    shape.Users.Add(newResult);
                                }
                            }
                            else
                            {
                                Console.Error.WriteLine($"Failed to parse numeric values on shape ID {shape.ID} (Width: '{widthCell.Value.Val}', Height: '{heightCell.Value.Val}').");
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}