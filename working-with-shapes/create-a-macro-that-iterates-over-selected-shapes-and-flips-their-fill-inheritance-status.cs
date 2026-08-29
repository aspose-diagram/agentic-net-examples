using System.IO;
using System;
using Aspose.Diagram;

class FlipFillInheritanceMacro
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Access the ShapePlaceFlip property which controls how the shape is flipped
                    ShapePlaceFlipValue currentFlip = shape.Layout.ShapePlaceFlip.Value;

                    // Toggle between FlipHorizontal and NoFlip (you can adjust the logic as needed)
                    if (currentFlip == ShapePlaceFlipValue.FlipHorizontal)
                    {
                        shape.Layout.ShapePlaceFlip.Value = ShapePlaceFlipValue.NoFlip;
                    }
                    else
                    {
                        shape.Layout.ShapePlaceFlip.Value = ShapePlaceFlipValue.FlipHorizontal;
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
