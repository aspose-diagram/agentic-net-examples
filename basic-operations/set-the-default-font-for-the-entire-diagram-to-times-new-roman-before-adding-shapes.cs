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

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Set the default font for the entire diagram
            FontConfigs.DefaultFontName = "Times New Roman";

            // Add a shape (e.g., a rectangle) to the first page
            // Parameters: PinX, PinY, master name, page index
            diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Save the diagram to a VDX file
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
