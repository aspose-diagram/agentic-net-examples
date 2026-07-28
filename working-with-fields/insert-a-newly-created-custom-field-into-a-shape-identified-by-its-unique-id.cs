using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // Unique ID of the shape to modify
                long targetShapeId = 5; // replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Retrieve the shape by its unique ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Create a new custom text field
                Field customField = new Field();

                // Set the field's displayed value
                customField.Value.Val = "Custom Value";

                // Optionally, clear any formatting (use empty strings)
                customField.Format.Val = "";
                customField.Format.Ufev.F = "";
                customField.Format.Ufev.Unit = MeasureConst.Undefined;

                // Add the field to the shape's Fields collection
                shape.Fields.Add(customField);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }