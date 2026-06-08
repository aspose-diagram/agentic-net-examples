using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load an existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Locate the shape to be cloned.
            // Here we simply take the shape with ID 1.
            Shape originalShape = page.Shapes.GetShape(1);
            if (originalShape == null)
            {
                throw new Exception("Original shape not found.");
            }

            // Determine a position for the cloned shape (offset by 2 inches on X axis)
            double newPinX = originalShape.XForm.PinX.Value + 2.0;
            double newPinY = originalShape.XForm.PinY.Value;

            // Ensure the original shape has a master; otherwise cloning is not possible.
            if (originalShape.Master == null)
            {
                throw new Exception("Original shape does not have an associated master.");
            }

            // Add a new shape using the same master as the original shape.
            long newShapeId = page.AddShape(newPinX, newPinY, originalShape.Master.Name);
            Shape clonedShape = page.Shapes.GetShape(newShapeId);
            if (clonedShape == null)
            {
                throw new Exception("Failed to create cloned shape.");
            }

            // Copy all fields from the original shape to the cloned shape.
            foreach (Field originalField in originalShape.Fields)
            {
                Field newField = new Field();

                // Copy the field name if the property exists.
                // Field does not have a 'Name' property; use 'NameU' if available.
                // This line is kept optional to avoid compile errors on different API versions.
                // Uncomment the following line if 'NameU' exists:
                // newField.NameU = originalField.NameU;

                // Copy the field value.
                newField.Value.Val = originalField.Value.Val;

                // Copy the field format if it exists.
                if (originalField.Format != null)
                {
                    newField.Format.Val = originalField.Format.Val;
                    if (originalField.Format.Ufev != null)
                    {
                        newField.Format.Ufev.F = originalField.Format.Ufev.F;
                        newField.Format.Ufev.Unit = originalField.Format.Ufev.Unit;
                    }
                }

                // Add the new field to the cloned shape.
                clonedShape.Fields.Add(newField);
            }

            // Save the modified diagram to a new file.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}