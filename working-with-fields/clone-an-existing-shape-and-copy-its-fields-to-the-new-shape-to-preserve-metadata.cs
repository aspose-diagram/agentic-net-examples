using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (index 0)
            Page page = diagram.Pages[0];

            // Identify the shape to clone (example uses shape ID 1)
            long sourceShapeId = 1;
            Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
            if (sourceShape == null)
            {
                throw new Exception($"Shape with ID {sourceShapeId} not found.");
            }

            // Ensure the source shape has an associated master
            if (sourceShape.Master == null)
            {
                throw new Exception("Source shape does not have a master.");
            }

            // Determine a new position for the cloned shape (offset X by 2 inches)
            double newPinX = sourceShape.XForm.PinX.Value + 2.0;
            double newPinY = sourceShape.XForm.PinY.Value;

            // Add a new shape using the same master as the source shape
            string masterName = sourceShape.Master.Name;
            long newShapeId = page.AddShape(newPinX, newPinY, masterName);
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Copy all fields from the source shape to the new shape
            foreach (Field srcField in sourceShape.Fields)
            {
                Field dstField = new Field();

                // Copy the field's value
                dstField.Value.Val = srcField.Value.Val;

                // Copy the field's format string
                dstField.Format.Val = srcField.Format.Val;

                // Copy the field type (if needed)
                dstField.Type.Value = srcField.Type.Value;

                // Copy the calendar setting (if needed)
                dstField.Calendar.Value = srcField.Calendar.Value;

                // Preserve the deletion flag
                dstField.Del = srcField.Del;

                // Add the cloned field to the new shape
                newShape.Fields.Add(dstField);
            }

            // Save the diagram with the cloned shape
            string outputPath = "cloned_output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
