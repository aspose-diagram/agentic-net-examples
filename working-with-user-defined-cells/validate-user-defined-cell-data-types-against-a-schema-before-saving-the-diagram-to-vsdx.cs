using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Define a simple schema: user-defined cell name -> expected .NET type
        private static readonly Dictionary<string, Type> UserCellSchema = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { "Cost", typeof(double) },
            { "Quantity", typeof(int) },
            { "StartDate", typeof(DateTime) }
        };

        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "validated_output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

                // Validate user-defined cells against the schema
                ValidateUserDefinedCells(diagram);

                // Save the diagram after successful validation
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        private static void ValidateUserDefinedCells(Diagram diagram)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate over all user-defined cells in the shape
                    foreach (User userCell in shape.Users)
                    {
                        // Check if the cell name is defined in the schema
                        if (UserCellSchema.TryGetValue(userCell.Name, out Type expectedType))
                        {
                            string cellValue = userCell.Value?.Val ?? string.Empty;

                            bool isValid = expectedType switch
                            {
                                Type t when t == typeof(int) => int.TryParse(cellValue, out _),
                                Type t when t == typeof(double) => double.TryParse(cellValue, out _),
                                Type t when t == typeof(DateTime) => DateTime.TryParse(cellValue, out _),
                                _ => true // If type is not specifically handled, consider it valid
                            };

                            if (!isValid)
                            {
                                throw new Exception($"Validation failed for shape ID {shape.ID} on page '{page.Name}'. " +
                                                    $"User cell '{userCell.Name}' expects a value of type {expectedType.Name} " +
                                                    $"but got '{cellValue}'.");
                            }
                        }
                    }
                }
            }
        }
    }