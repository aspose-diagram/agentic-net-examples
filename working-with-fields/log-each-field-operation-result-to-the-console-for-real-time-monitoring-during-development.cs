using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages and shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    Console.WriteLine($"Processing Shape ID: {shape.ID}");

                    // Log existing fields
                    if (shape.Fields != null && shape.Fields.Count > 0)
                    {
                        foreach (Aspose.Diagram.Field field in shape.Fields)
                        {
                            string fieldValue = field.Value != null ? field.Value.Val : "null";
                            Console.WriteLine($"  Field IX: {field.IX}, Value: {fieldValue}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No fields on this shape.");
                    }

                    // Add a new text-insertion field and log the operation
                    Aspose.Diagram.Field newField = new Aspose.Diagram.Field();
                    newField.Type.Value = Aspose.Diagram.TypeFieldValue.Undefined;
                    newField.Value.Val = "NewFieldValue";
                    shape.Fields.Add(newField);
                    Console.WriteLine("  Added new field with value: NewFieldValue");
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
