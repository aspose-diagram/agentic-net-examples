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

            // Locate the shape by its universal name (NameU)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "TargetShapeName")
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            // If the shape is not found, abort with an error
            if (targetShape == null)
                throw new Exception("Shape with the specified name was not found.");

            // Create a new field and set its properties
            Field field = new Field();
            field.Type.Value = TypeFieldValue.Undefined; // field type (can be changed as needed)
            field.Value.Val = "Sample Field Value";      // field value

            // Add the field to the shape's field collection
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
