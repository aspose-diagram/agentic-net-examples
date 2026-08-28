using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                Page page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                    throw new Exception("The first page contains no shapes.");

                // Retrieve the first shape
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Ensure the shape has at least one field (text insertion field)
                if (shape.Fields.Count == 0)
                    throw new Exception("The selected shape does not contain any fields.");

                // Example: we will work with the first field
                Field field = shape.Fields[0];

                // New formula to assign
                string newFormula = "Width*Height";

                // Validate the formula before applying
                if (ValidateFormula(diagram, shape, newFormula))
                {
                    // Apply the validated formula
                    field.Value.Ufev.F = newFormula;
                    // Optionally refresh shape data after modification
                    shape.RefreshData();
                    Console.WriteLine("Formula applied successfully.");
                }
                else
                {
                    Console.WriteLine($"Invalid formula: \"{newFormula}\". Operation aborted.");
                    return;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Validates a Visio formula by temporarily assigning it to a new field,
        /// refreshing the shape, and catching any exceptions that indicate syntax errors.
        /// </summary>
        /// <param name="diagram">The diagram containing the shape.</param>
        /// <param name="shape">The shape to which the formula will be applied.</param>
        /// <param name="formula">The formula string to validate.</param>
        /// <returns>True if the formula is syntactically correct; otherwise false.</returns>
        static bool ValidateFormula(Diagram diagram, Shape shape, string formula)
        {
            // Create a temporary field for validation
            Field tempField = new Field();

            try
            {
                // Assign the formula to the temporary field
                tempField.Value.Ufev.F = formula;

                // Add the temporary field to the shape
                shape.Fields.Add(tempField);

                // Refresh data to force evaluation of the formula
                shape.RefreshData();

                // Remove the temporary field after successful validation
                shape.Fields.Remove(tempField);

                return true;
            }
            catch (Exception ex)
            {
                // Cleanup in case of failure
                try
                {
                    shape.Fields.Remove(tempField);
                }
                catch { /* ignore cleanup errors */ }

                Console.WriteLine($"Formula validation error: {ex.Message}");
                return false;
            }
        }
    }