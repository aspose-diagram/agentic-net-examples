using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Store original user-defined cell values for rollback.
            // Key: Shape ID, Value: Dictionary of User cell name -> original value.
            var originalValues = new Dictionary<long, Dictionary<string, string>>();

            // Iterate all pages and shapes to capture original values.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    var userValues = new Dictionary<string, string>();

                    foreach (User user in shape.Users)
                    {
                        // Use NameU as the unique identifier for the user-defined cell.
                        string cellName = user.NameU;
                        string cellValue = user.Value?.Val ?? string.Empty;
                        userValues[cellName] = cellValue;
                    }

                    originalValues[shapeId] = userValues;
                }
            }

            // -----------------------------------------------------------------
            // Example modification: append "_mod" to each user-defined cell value.
            // In a real scenario, this would be where you apply business logic.
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User user in shape.Users)
                    {
                        // Modify the cell value.
                        string current = user.Value?.Val ?? string.Empty;
                        user.Value.Val = current + "_mod";
                    }
                }
            }

            // -----------------------------------------------------------------
            // Validation: ensure all user-defined cell values are numeric.
            // If any value fails validation, trigger rollback.
            // -----------------------------------------------------------------
            bool validationFailed = false;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User user in shape.Users)
                    {
                        string val = user.Value?.Val ?? string.Empty;
                        // Simple numeric validation.
                        if (!double.TryParse(val, out _))
                        {
                            validationFailed = true;
                            Console.WriteLine($"Validation failed for Shape ID {shape.ID}, User Cell '{user.NameU}' with value '{val}'.");
                            // No need to continue checking once a failure is detected.
                            break;
                        }
                    }
                    if (validationFailed) break;
                }
                if (validationFailed) break;
            }

            // -----------------------------------------------------------------
            // Rollback if validation failed.
            // -----------------------------------------------------------------
            if (validationFailed)
            {
                Console.WriteLine("Rolling back to original user-defined cell values...");

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;
                        if (originalValues.TryGetValue(shapeId, out var userValues))
                        {
                            foreach (User user in shape.Users)
                            {
                                string cellName = user.NameU;
                                if (userValues.TryGetValue(cellName, out string originalVal))
                                {
                                    user.Value.Val = originalVal;
                                }
                            }
                        }
                    }
                }
            }

            // Save the (potentially modified) diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}