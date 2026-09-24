using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "validated_output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define a simple schema for user-defined cells
                // Key: cell name, Value: expected .NET type
                var schema = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Length", typeof(double) },
                    { "Count", typeof(int) },
                    { "StartDate", typeof(DateTime) }
                    // Add more entries as needed
                };

                // Validate each shape's user-defined cells against the schema
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users == null) continue;

                        foreach (User userCell in shape.Users)
                        {
                            if (!schema.TryGetValue(userCell.Name, out Type expectedType))
                                continue; // No validation rule for this cell

                            string cellValue = userCell.Value?.Val ?? string.Empty;
                            bool isValid = true;

                            if (expectedType == typeof(int))
                            {
                                isValid = int.TryParse(cellValue, out _);
                            }
                            else if (expectedType == typeof(double))
                            {
                                isValid = double.TryParse(cellValue, out _);
                            }
                            else if (expectedType == typeof(DateTime))
                            {
                                isValid = DateTime.TryParse(cellValue, out _);
                            }
                            else
                            {
                                // For string or other types, consider it always valid
                                isValid = true;
                            }

                            if (!isValid)
                            {
                                string message = $"Validation failed: Shape ID {shape.ID}, User Cell '{userCell.Name}' " +
                                                 $"has value '{cellValue}' which is not a valid {expectedType.Name}.";
                                // Throwing stops the process; alternatively, log and continue
                                throw new Exception(message);
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
    }