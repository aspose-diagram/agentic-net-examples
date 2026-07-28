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
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the shape to modify (universal name)
            string targetShapeName = "MyShape";

            Shape targetShape = null;

            // Locate the shape by iterating through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeName)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            // If the shape was not found, abort with an error
            if (targetShape == null)
            {
                throw new Exception($"Shape with NameU '{targetShapeName}' not found.");
            }

            // Create a new text field
            Field field = new Field();

            // Set the field type (using Undefined as a generic type)
            field.Type.Value = TypeFieldValue.Undefined;

            // Assign a value to the field
            field.Value.Val = "Inserted Field Value";

            // Add the field to the shape's Fields collection
            targetShape.Fields.Add(field);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
