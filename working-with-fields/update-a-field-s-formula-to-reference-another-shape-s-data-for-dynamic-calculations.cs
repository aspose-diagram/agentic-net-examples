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

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Locate the shape that provides the data (source shape)
            Shape sourceShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == "SourceShape")
                {
                    sourceShape = s;
                    break;
                }
            }
            if (sourceShape == null)
            {
                throw new Exception("Source shape not found.");
            }

            // Locate the shape whose field will be updated (target shape)
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == "TargetShape")
                {
                    targetShape = s;
                    break;
                }
            }
            if (targetShape == null)
            {
                throw new Exception("Target shape not found.");
            }

            // Ensure the target shape has at least one field; create one if necessary
            Field field;
            if (targetShape.Fields.Count > 0)
            {
                field = targetShape.Fields[0];
            }
            else
            {
                field = new Field();
                targetShape.Fields.Add(field);
            }

            // Build a Visio formula that references the source shape's Data1 cell
            // Formula format: Sheet.<ShapeID>!Data1
            string formula = $"Sheet.{sourceShape.ID}!Data1";

            // Assign the formula to the field's value (Ufev.F holds the formula string)
            field.Value.Ufev.F = formula;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
