using System;
using System.IO;
using Aspose.Diagram;

class MergeParagraphsExample
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the two shapes whose paragraphs will be merged.
            // Replace 1 and 2 with the actual shape IDs.
            Shape shape1 = diagram.Pages[0].Shapes.GetShape(1);
            Shape shape2 = diagram.Pages[0].Shapes.GetShape(2);

            // Iterate through each paragraph in the second shape.
            foreach (var para in shape2.Paras)
            {
                // Clone the paragraph to keep its individual formatting intact.
                // The Clone method creates a deep copy of the Para object.
                var clonedPara = (Para)para.Clone();

                // Add the cloned paragraph to the first shape's paragraph collection.
                shape1.Paras.Add(clonedPara);
            }

            // Optionally remove the second shape if it is no longer needed.
            // shape2.Delete();

            // Save the modified diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
