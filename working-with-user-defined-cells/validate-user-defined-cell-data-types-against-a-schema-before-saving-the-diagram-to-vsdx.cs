using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output VSDX path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputVsdxPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Define a simple schema for user-defined cells (cell name -> expected data type)
        // Supported types: "int", "double", "date", "string"
        var schema = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Amount", "double" },
            { "Count", "int" },
            { "Date", "date" }
            // Add more schema entries as needed
        };

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool validationFailed = false;

                // Iterate over all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Users collection is not null before iterating
                        if (shape.Users != null)
                        {
                            // Iterate over each user-defined cell
                            foreach (User userField in shape.Users)
                            {
                                // Check if the cell name is defined in the schema
                                if (schema.TryGetValue(userField.NameU, out string expectedType))
                                {
                                    string cellValue = userField.Value?.Val ?? string.Empty;

                                    // Validate the cell value against the expected type
                                    if (!ValidateCellValue(expectedType, cellValue, out string errorMessage))
                                    {
                                        Console.Error.WriteLine(
                                            $"Validation error in shape ID {shape.ID} on page ID {page.ID}: " +
                                            $"User cell '{userField.NameU}' has invalid value '{cellValue}'. " +
                                            $"Expected {expectedType}. {errorMessage}");

                                        validationFailed = true;
                                    }
                                }
                            }
                        }
                    }
                }

                // Abort saving if any validation errors were detected
                if (validationFailed)
                {
                    Console.Error.WriteLine("Diagram validation failed. The diagram will not be saved.");
                    return;
                }

                // Save the validated diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates a cell value against an expected data type.
    /// </summary>
    /// <param name="expectedType">The expected type (int, double, date, string).</param>
    /// <param name="value">The cell value to validate.</param>
    /// <param name="errorMessage">Detailed error message if validation fails.</param>
    /// <returns>True if the value matches the expected type; otherwise false.</returns>
    private static bool ValidateCellValue(string expectedType, string value, out string errorMessage)
    {
        errorMessage = string.Empty;
        switch (expectedType.ToLowerInvariant())
        {
            case "int":
                if (!int.TryParse(value, out _))
                {
                    errorMessage = "Value is not a valid integer.";
                    return false;
                }
                return true;

            case "double":
                if (!double.TryParse(value, out _))
                {
                    errorMessage = "Value is not a valid double.";
                    return false;
                }
                return true;

            case "date":
                if (!DateTime.TryParse(value, out _))
                {
                    errorMessage = "Value is not a valid date.";
                    return false;
                }
                return true;

            case "string":
                // All values are valid strings
                return true;

            default:
                errorMessage = $"Unsupported expected type '{expectedType}'.";
                return false;
        }
    }
}