using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (you can change the index as needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape you want to clone.
            // Here we simply take the first shape on the page.
            // In a real scenario you might locate the shape by ID, NameU, etc.
            Shape originalShape = page.Shapes[0];

            // Get the master name of the original shape (used to create a shape of the same type)
            string masterName = originalShape.Master?.Name ?? throw new Exception("Original shape has no master.");

            // Add a new shape on the same page using the same master.
            // Position it slightly offset so it does not overlap the original.
            double offsetX = 1.0; // inches
            double offsetY = 1.0; // inches
            long newShapeId = page.AddShape(
                originalShape.XForm.PinX.Value + offsetX,
                originalShape.XForm.PinY.Value + offsetY,
                masterName);

            // Retrieve the newly created shape instance
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Copy all text fields from the original shape to the new shape
            foreach (Field originalField in originalShape.Fields)
            {
                // Create a new field and copy its core properties
                Field clonedField = new Field();

                // Copy the displayed value
                clonedField.Value.Val = originalField.Value.Val;

                // Copy the format string (if any)
                clonedField.Format.Val = originalField.Format.Val;

                // Copy the field type (e.g., date, time, string)
                clonedField.Type.Value = originalField.Type.Value;

                // Copy the calendar setting (if present)
                clonedField.Calendar.Value = originalField.Calendar.Value;

                // Copy the deletion flag (preserve metadata about hidden fields)
                clonedField.Del = originalField.Del;

                // Add the cloned field to the new shape
                newShape.Fields.Add(clonedField);
            }

            // Optionally, copy other metadata such as custom properties, user-defined cells, etc.
            // For brevity, only fields are cloned in this example.

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
