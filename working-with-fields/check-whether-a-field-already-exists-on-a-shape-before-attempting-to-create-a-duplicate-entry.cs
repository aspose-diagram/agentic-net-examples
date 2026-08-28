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

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate the shape we want to work with (by universal name)
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null && shape.NameU.Equals("MyShape", StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Define the field index (IX) we intend to add
            int desiredIx = 0;

            // Check if a field with the same IX already exists
            bool fieldExists = false;
            foreach (Field fld in targetShape.Fields)
            {
                if (fld.IX == desiredIx)
                {
                    fieldExists = true;
                    break;
                }
            }

            if (fieldExists)
            {
                Console.WriteLine($"Field with IX={desiredIx} already exists on shape '{targetShape.NameU}'.");
            }
            else
            {
                // Create a new field and add it to the shape
                Field newField = new Field();
                newField.IX = desiredIx;
                newField.Type.Value = TypeFieldValue.Undefined; // Adjust type as needed
                newField.Value.Val = "NewValue";
                targetShape.Fields.Add(newField);
                Console.WriteLine($"Added new field with IX={desiredIx} to shape '{targetShape.NameU}'.");
            }

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
