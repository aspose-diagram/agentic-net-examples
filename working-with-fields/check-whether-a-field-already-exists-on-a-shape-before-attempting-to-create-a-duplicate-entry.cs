using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page and the first shape on that page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(1); // assumes shape with ID 1 exists

                // The value we want to ensure is not duplicated
                string fieldValue = "SampleFieldValue";

                // Check if a field with the same value already exists
                if (FieldExists(shape, fieldValue))
                {
                    Console.WriteLine($"A field with value \"{fieldValue}\" already exists on the shape (ID: {shape.ID}).");
                }
                else
                {
                    // Create a new field and add it to the shape
                    Field newField = new Field();
                    newField.Value.Val = fieldValue;
                    shape.Fields.Add(newField);
                    Console.WriteLine($"Added new field with value \"{fieldValue}\" to the shape (ID: {shape.ID}).");
                }

                // Optionally, save the diagram to verify changes
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Returns true if a field with the specified value already exists on the shape
        static bool FieldExists(Shape shape, string value)
        {
            foreach (Field field in shape.Fields)
            {
                if (field.Value.Val == value)
                {
                    return true;
                }
            }
            return false;
        }
    }