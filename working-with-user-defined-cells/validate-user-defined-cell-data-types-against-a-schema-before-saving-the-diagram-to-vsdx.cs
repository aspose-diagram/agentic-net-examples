using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "validated_output.vsdx";

                // Define a simple schema: user cell name -> expected data type
                // Supported types: int, double, datetime, string
                var schema = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Cost", "double" },
                    { "Quantity", "int" },
                    { "StartDate", "datetime" },
                    { "Description", "string" }
                };

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Validate all user-defined cells against the schema
                ValidateUserDefinedCells(diagram, schema);

                // Save the validated diagram as VSDX
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Iterates through all shapes in the diagram and validates each user-defined cell
        /// according to the provided schema. Throws an exception if any validation fails.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to validate.</param>
        /// <param name="schema">A dictionary mapping user cell names to expected data types.</param>
        private static void ValidateUserDefinedCells(Diagram diagram, Dictionary<string, string> schema)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Users collection may be empty but is never null
                    foreach (User userCell in shape.Users)
                    {
                        if (schema.TryGetValue(userCell.Name, out string expectedType))
                        {
                            string cellValue = userCell.Value?.Val ?? string.Empty;
                            bool isValid = false;

                            switch (expectedType.Trim().ToLowerInvariant())
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
                                    // Any value is acceptable for string type
                                    isValid = true;
                                    break;
                                default:
                                    // Unknown type in schema; treat as invalid
                                    isValid = false;
                                    break;
                            }

                            if (!isValid)
                            {
                                throw new Exception(
                                    $"Validation error: Shape ID {shape.ID}, User Cell '{userCell.Name}' " +
                                    $"has value '{cellValue}' which does not match expected type '{expectedType}'.");
                            }
                        }
                    }
                }
            }
        }
    }