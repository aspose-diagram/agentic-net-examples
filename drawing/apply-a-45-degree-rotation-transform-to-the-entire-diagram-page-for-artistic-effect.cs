using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RotateDiagramPage
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // Replace "input.vsdx" with the path to your source file
            Diagram diagram = new Diagram("input.vsdx");

            // Define the rotation angle in radians (45 degrees)
            double angleInRadians = 45.0 * Math.PI / 180.0;

            // Apply the rotation to every shape on the active page
            foreach (Shape shape in diagram.ActivePage.Shapes)
            {
                shape.SetAngle(angleInRadians);
            }

            // Save the modified diagram
            // Replace "output.vsdx" with the desired output file path
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();
            diagram.Save("output.vsdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
