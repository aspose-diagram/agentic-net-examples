using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVsdxPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define a simple schema for user‑defined cells.
        // Key   : cell name (case‑insensitive)
        // Value : expected data type ("int", "double", "datetime", "string")
        var schema = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "UserInt", "int" },
            { "UserDouble", "double" },
            { "UserDate", "datetime" },
            { "UserText", "string" }
        };

        bool validationFailed = false;

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Users collection before accessing.
                        if (shape.Users == null)
                            continue;

                        // Iterate over each user‑defined cell.
                        foreach (User userField in shape.Users)
                        {
                            string cellName = userField.NameU ?? string.Empty;
                            string cellValue = userField.Value?.Val ?? string.Empty;

                            // Check if this cell is defined in the schema.
                            if (!schema.TryGetValue(cellName, out string expectedType))
                            {
                                // Cell not in schema – optionally ignore or warn.
                                Console.WriteLine($"Warning: Cell '{cellName}' on shape ID {shape.ID} is not defined in the schema.");
                                continue;
                            }

                            // Perform type‑specific validation.
                            bool isValid = true;
                            switch (expectedType.ToLowerInvariant())
                            {
                                case "int":
                                    isValid = int.TryParse(cellValue, out _);
                                    break;
                                case "double":
                                    isValid = double.TryParse(cellValue, out _);
                                    break;
                                case "datetime":
                                    isValid = DateTime.TryParse(cellValue, out _);
                                    break;
                                case "string":
                                    // All values are valid strings.
                                    isValid = true;
                                    break;
                                default:
                                    // Unknown schema type – treat as invalid.
                                    isValid = false;
                                    break;
                            }

                            if (!isValid)
                            {
                                Console.Error.WriteLine($"Validation error: Cell '{cellName}' on shape ID {shape.ID} expects {expectedType} but value '{cellValue}' is invalid.");
                                validationFailed = true;
                            }
                        }
                    }
                }

                // If validation passed, save the diagram to VSDX.
                if (!validationFailed)
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Diagram not saved due to validation errors.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}