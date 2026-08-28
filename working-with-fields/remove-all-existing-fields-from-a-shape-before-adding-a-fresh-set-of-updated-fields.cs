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
                var diagram = new Diagram("input.vsdx");

                // Access the first page (adjust index as needed)
                Page page = diagram.Pages[0];

                // Retrieve the target shape (example: shape with ID = 1)
                // Ensure the shape exists before proceeding
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    throw new Exception("Shape with ID 1 not found.");
                }

                // ------------------------------------------------------------
                // Remove all existing fields from the shape
                // ------------------------------------------------------------
                // Collect fields to remove to avoid modifying the collection while iterating
                List<Field> fieldsToRemove = new List<Field>();
                foreach (Field fld in shape.Fields)
                {
                    fieldsToRemove.Add(fld);
                }

                // Remove each collected field
                foreach (Field fld in fieldsToRemove)
                {
                    shape.Fields.Remove(fld);
                }

                // ------------------------------------------------------------
                // Add a fresh set of updated fields
                // ------------------------------------------------------------
                // Example: add a single custom field with a new value
                Field newField = new Field();
                // Set the displayed value of the field
                newField.Value.Val = "Updated Value";
                // Optional: clear any formatting string
                newField.Format.Val = "";
                // Add the field to the shape's field collection
                shape.Fields.Add(newField);

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }