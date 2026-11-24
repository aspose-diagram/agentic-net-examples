using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Backup structure to store original user-defined cell values per shape
            var backup = new List<(Shape shape, Dictionary<string, string> originalValues)>();

            // Iterate all pages and shapes to capture original values
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    var originalValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (User userCell in shape.Users)
                    {
                        // Store the current value of each user-defined cell
                        originalValues[userCell.Name] = userCell.Value.Val;
                    }
                    // Keep a reference to the shape and its original values
                    backup.Add((shape, originalValues));
                }
            }

            // -----------------------------------------------------------------
            // Example modification: set each user-defined cell to a new value
            // In a real scenario, these values could come from external input
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        // For demonstration, prepend "New_" to the existing value
                        userCell.Value.Val = "New_" + userCell.Value.Val;
                    }
                }
            }

            // -----------------------------------------------------------------
            // Validation: ensure that a specific user-defined cell (e.g., "CustomValue")
            // contains a numeric value. If any validation fails, trigger rollback.
            // -----------------------------------------------------------------
            bool validationFailed = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        // Example rule: if the cell name is "CustomValue", it must be numeric
                        if (string.Equals(userCell.Name, "CustomValue", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!double.TryParse(userCell.Value.Val, out _))
                            {
                                Console.Error.WriteLine($"Validation failed for shape ID {shape.ID}, cell '{userCell.Name}' with value '{userCell.Value.Val}'.");
                                validationFailed = true;
                                // Break out early if desired; continue to collect all errors
                            }
                        }
                    }
                }
            }

            // -----------------------------------------------------------------
            // Rollback mechanism: if validation failed, restore original values
            // -----------------------------------------------------------------
            if (validationFailed)
            {
                Console.WriteLine("Validation failed. Rolling back changes to user-defined cells.");

                foreach (var entry in backup)
                {
                    Shape shape = entry.shape;
                    Dictionary<string, string> originalValues = entry.originalValues;

                    foreach (User userCell in shape.Users)
                    {
                        if (originalValues.TryGetValue(userCell.Name, out string originalVal))
                        {
                            userCell.Value.Val = originalVal;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("All validations passed. Changes will be saved.");
            }

            // Save the diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}