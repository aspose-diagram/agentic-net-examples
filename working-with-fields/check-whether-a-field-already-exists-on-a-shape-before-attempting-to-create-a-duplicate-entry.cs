using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we are working with the first page
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (replace with a valid ID)
                long shapeId = 1; // example ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found.");
                }

                // Check if a field of a specific type already exists on the shape
                // Here we check for any existing field; adjust the condition as needed
                bool fieldExists = ShapeHasField(shape);

                if (fieldExists)
                {
                    Console.WriteLine("A field already exists on the shape. No duplicate will be added.");
                }
                else
                {
                    // Create a new field and add it to the shape
                    Field newField = new Field();

                    // Set the field type to Undefined (safe default)
                    newField.Type.Value = TypeFieldValue.Undefined;

                    // Assign a value to the field
                    newField.Value.Val = "Sample Value";

                    // Add the field to the shape's Fields collection
                    shape.Fields.Add(newField);

                    Console.WriteLine("New field added to the shape.");
                }

                // Optionally save the diagram to verify changes
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to determine if the shape already contains any field
        static bool ShapeHasField(Shape shape)
        {
            // The Fields collection does not support LINQ; iterate manually
            foreach (Field field in shape.Fields)
            {
                // If at least one field is present, return true
                if (field != null)
                {
                    return true;
                }
            }
            return false;
        }
    }