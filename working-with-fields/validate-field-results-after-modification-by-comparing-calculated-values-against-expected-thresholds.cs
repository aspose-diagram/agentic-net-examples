using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual path)
                string inputPath = "input.vsdx";
                // Path to the output Visio file after modifications
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                // Work with the first page
                Page page = diagram.Pages[0];

                // Example: target shape ID (replace with actual ID)
                long targetShapeId = 1; // placeholder ID

                // Retrieve the shape by ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                    throw new Exception($"Shape with ID {targetShapeId} not found.");

                // Verify the shape has at least one field
                if (shape.Fields.Count == 0)
                    throw new Exception("The selected shape does not contain any fields.");

                // Access the first field
                Field field = shape.Fields[0];

                // Modify the field's displayed value
                field.Value.Val = "123.45";

                // Optionally, set a formula (example)
                field.Value.Ufev.F = ""; // clear any existing formula
                field.Value.Ufev.Unit = MeasureConst.Undefined;

                // Parse the numeric value for validation
                if (!double.TryParse(field.Value.Val, out double numericValue))
                    throw new Exception("Failed to parse the field value to a number.");

                // Define expected threshold
                double expectedThreshold = 100.0;

                // Validate the calculated value against the threshold
                if (numericValue >= expectedThreshold)
                {
                    Console.WriteLine($"Validation passed: {numericValue} >= {expectedThreshold}");
                }
                else
                {
                    // Throw an exception to indicate validation failure
                    throw new Exception($"Validation failed: {numericValue} is less than the expected threshold of {expectedThreshold}.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }