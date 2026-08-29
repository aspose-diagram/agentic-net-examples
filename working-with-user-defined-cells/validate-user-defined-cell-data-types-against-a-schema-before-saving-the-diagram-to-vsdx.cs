using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define a simple schema: user cell name -> expected .NET type
                var schema = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Cost", typeof(double) },
                    { "Quantity", typeof(int) },
                    { "StartDate", typeof(DateTime) },
                    { "IsActive", typeof(bool) }
                };

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Validate each user-defined cell against the schema
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (User userCell in shape.Users)
                        {
                            if (schema.TryGetValue(userCell.Name, out Type expectedType))
                            {
                                string cellValue = userCell.Value?.Val ?? string.Empty;
                                bool isValid = ValidateValue(cellValue, expectedType);
                                if (!isValid)
                                {
                                    throw new Exception(
                                        $"Validation failed for shape ID {shape.ID}, user cell '{userCell.Name}'. " +
                                        $"Expected {expectedType.Name}, got '{cellValue}'.");
                                }
                            }
                        }
                    }
                }

                // Save the validated diagram as VSDX
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to validate a string value against an expected .NET type
        static bool ValidateValue(string value, Type expectedType)
        {
            if (expectedType == typeof(int))
                return int.TryParse(value, out _);
            if (expectedType == typeof(double))
                return double.TryParse(value, out _);
            if (expectedType == typeof(bool))
                return bool.TryParse(value, out _);
            if (expectedType == typeof(DateTime))
                return DateTime.TryParse(value, out _);

            // If the expected type is not one of the above, treat it as a string (always valid)
            return true;
        }
    }