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

            // Access page three (zero‑based index)
            Page pageThree = diagram.Pages[2];

            // Iterate through all shapes on the page
            foreach (Shape shape in pageThree.Shapes)
            {
                // Set the text rotation angle to 90 degrees
                shape.TextXForm.TxtAngle.Value = 90;
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
