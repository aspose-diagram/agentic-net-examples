using System.IO;
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
            Diagram diagram = new Diagram("input.vsdx");

            // Define the name of the shape whose field we want to update
            string targetShapeNameU = "TargetShape";

            // Define the formula that references another shape's data (e.g., shape with ID 2, property "MyProp")
            string referenceFormula = "Sheet.2!Prop.MyProp";

            // Iterate through all pages to find the target shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Match shape by its universal name
                    if (shape.NameU == targetShapeNameU)
                    {
                        // If the shape already has at least one field, update the first field's formula
                        if (shape.Fields.Count > 0)
                        {
                            Field existingField = shape.Fields[0];
                            existingField.Value.Ufev.F = referenceFormula;
                        }
                        else
                        {
                            // Otherwise, create a new field and set its formula
                            Field newField = new Field();
                            newField.Value.Ufev.F = referenceFormula;
                            shape.Fields.Add(newField);
                        }

                        // Exit after updating the target shape
                        break;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
