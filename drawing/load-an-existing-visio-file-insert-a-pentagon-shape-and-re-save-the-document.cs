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

            // Load the existing Visio document (replace with your file path)
            using (var diagram = new Diagram("input.vsdx"))
            {
                // Insert a pentagon shape on the active page at coordinates (5, 5) inches
                // "Pentagon" is the master name of the shape in the default Visio stencil
                diagram.ActivePage.AddShape(5.0, 5.0, "Pentagon");

                // Save the modified document (replace with desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
