using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page (adjust index as needed)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (replace 1 with the actual shape ID)
                Shape shape = page.Shapes.GetShape(1);

                // Ensure the shape has a Fields collection
                if (shape.Fields != null)
                {
                    // Collect existing fields to remove (cannot modify collection while iterating)
                    List<Field> fieldsToRemove = new List<Field>();
                    foreach (Field existingField in shape.Fields)
                    {
                        fieldsToRemove.Add(existingField);
                    }

                    // Remove each field from the shape
                    foreach (Field fieldToRemove in fieldsToRemove)
                    {
                        shape.Fields.Remove(fieldToRemove);
                    }
                }

                // Add new fields to the shape
                // Example: add a custom date field
                Field newField = new Field();
                // Set the field's value (e.g., a date string)
                newField.Value.Val = "2026-07-22";
                // Optionally set the field type (Undefined is safe)
                newField.Type.Value = TypeFieldValue.Undefined;
                // Add the field to the shape's collection
                shape.Fields.Add(newField);

                // Save the updated diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }