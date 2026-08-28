using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the diagram at position (1,1) on page index 0
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

                // Retrieve the concrete Shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new text field and add it to the shape
                Field field = new Field();
                shape.Fields.Add(field);

                // Set the field's value (as a string that represents a numeric value)
                field.Value.Val = "10";

                // Example calculation: parse the field value to double
                double actualValue;
                if (!double.TryParse(field.Value.Val, out actualValue))
                {
                    throw new Exception("Failed to parse field value to a numeric type.");
                }

                // Define the expected threshold
                double expectedThreshold = 5.0;

                // Validate the calculated value against the expected threshold
                if (actualValue < expectedThreshold)
                {
                    throw new Exception($"Validation failed: actual value {actualValue} is below the expected threshold {expectedThreshold}.");
                }
                else
                {
                    Console.WriteLine($"Validation succeeded: actual value {actualValue} meets the expected threshold {expectedThreshold}.");
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