using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load a template diagram that contains the master with a data graphic.
            // The template file (e.g., a VSDX stencil) must have a master named "DataGraphicMaster".
            Diagram templateDiagram = new Diagram("DataGraphicTemplate.vsdx");

            // Create a new, empty diagram.
            Diagram diagram = new Diagram();

            // Add the master from the template into the new diagram.
            // Returns the unique ID of the added master (not used further here).
            int masterId = diagram.AddMaster(templateDiagram, "DataGraphicMaster");

            // Get the active page where the shape will be placed.
            var page = diagram.ActivePage;

            // Define the position (PinX, PinY) for the shape instance.
            double pinX = 5.0;
            double pinY = 5.0;

            // Add a shape instance of the master onto the active page.
            diagram.AddShape(pinX, pinY, "DataGraphicMaster", page.ID);

            // Save the resulting diagram to a file.
            diagram.Save("ResultDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
