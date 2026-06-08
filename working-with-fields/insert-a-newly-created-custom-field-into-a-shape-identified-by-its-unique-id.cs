using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Unique ID of the shape to modify (replace with actual ID)
                long targetShapeId = 12345;

                // Assume the shape is on the first page; adjust if needed
                Page page = diagram.Pages[0];

                // Retrieve the shape by its unique ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Create a new custom field
                Field customField = new Field();

                // Set the field's value (plain text)
                customField.Value.Val = "MyCustomValue";

                // Optionally, clear any existing format (use empty strings)
                customField.Format.Val = "";
                customField.Format.Ufev.F = "";
                customField.Format.Ufev.Unit = MeasureConst.Undefined;

                // Add the new field to the shape's Fields collection
                shape.Fields.Add(customField);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }