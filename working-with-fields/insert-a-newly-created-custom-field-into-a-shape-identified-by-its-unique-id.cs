using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Unique ID of the shape to which the custom field will be added
                long targetShapeId = 12345; // replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Get the shape by its unique ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Create a new custom field
                Field customField = new Field();

                // Set the field's value (text to be displayed)
                customField.Value.Val = "MyCustomValue";

                // Optionally, you can set other properties such as format or type if required
                // customField.Format.Val = ""; // clear format
                // customField.Type.Value = (int)TypeFieldValue.Undefined; // default type

                // Add the field to the shape's Fields collection
                shape.Fields.Add(customField);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Custom field added and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }