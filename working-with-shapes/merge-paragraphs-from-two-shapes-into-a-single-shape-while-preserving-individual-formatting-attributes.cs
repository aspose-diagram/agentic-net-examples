using System.IO;
using System;
using Aspose.Diagram;

class MergeShapeParagraphs
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the two shapes to merge (by ID, Name, or NameU)
            // Here we assume the shapes have known IDs; replace with actual IDs
            long shapeId1 = 1; // ID of the target shape that will receive the paragraphs
            long shapeId2 = 2; // ID of the source shape whose paragraphs will be merged

            Shape targetShape = page.Shapes.GetShape(shapeId1);
            Shape sourceShape = page.Shapes.GetShape(shapeId2);

            // Iterate through each paragraph in the source shape
            foreach (Para sourcePara in sourceShape.Paras)
            {
                // Clone the paragraph to preserve its formatting (font, size, color, etc.)
                // The Para class implements ICloneable, so we can use its Clone method
                Para clonedPara = (Para)sourcePara.Clone();

                // Append the cloned paragraph to the target shape's paragraph collection
                targetShape.Paras.Add(clonedPara);
            }

            // Optional: remove the source shape if it is no longer needed
            // page.Shapes.Delete(shapeId2);

            // Save the modified diagram (replace with your desired output path and format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
