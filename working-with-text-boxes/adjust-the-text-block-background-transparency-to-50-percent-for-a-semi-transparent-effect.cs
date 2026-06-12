using System;
using System.IO;
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

            // Assume we want to modify the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[1]; // Index 1 is the first shape (0 is the page background)

            // Set the text block background transparency to 50% (0.5)
            // TextBkgndTrans is a DoubleValue; assign its Value property
            shape.TextBlock.TextBkgndTrans.Value = 0.5;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
