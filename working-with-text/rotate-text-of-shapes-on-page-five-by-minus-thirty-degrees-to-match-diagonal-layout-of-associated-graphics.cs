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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access page five (zero‑based index 4)
            Page pageFive = diagram.Pages[4];

            // Rotate text of every shape on this page by -30 degrees
            foreach (Shape shape in pageFive.Shapes)
            {
                if (shape.TextXForm != null)
                {
                    shape.TextXForm.TxtAngle.Value = -30;   // set rotation angle
                    shape.RefreshData();                    // apply changes
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
