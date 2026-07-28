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

            // Load the original Visio diagram.
            // The master shape definitions are loaded together with the diagram.
            Diagram diagram = new Diagram("input.vdx");

            // -----------------------------------------------------------------
            // Example modification: add a shape that uses an existing master.
            // The master ("Rectangle") is already part of the diagram, so its
            // definition is preserved automatically.
            // -----------------------------------------------------------------
            // Parameters: PinX, PinY, MasterName, PageID
            diagram.AddShape(2.0, 2.0, "Rectangle", 1);

            // Save the modified diagram to a new file.
            // Using Save(string, SaveFileFormat) ensures the original masters are kept.
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
