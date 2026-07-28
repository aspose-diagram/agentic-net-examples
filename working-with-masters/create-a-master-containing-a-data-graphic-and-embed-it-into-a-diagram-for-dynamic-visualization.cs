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

            // Path to the stencil (template) that contains the master with a data graphic.
            string stencilPath = @"C:\Stencils\DataGraphicStencil.vssx";

            // Name (or universal name) of the master inside the stencil.
            string masterName = "DataGraphicMaster";

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Add the master from the stencil to the diagram.
            // This returns the unique ID of the master within the diagram's Masters collection.
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Ensure there is at least one page to place the shape on.
            // If the diagram is empty, a default page is automatically created.
            Page page = diagram.Pages[0];

            // Add a shape instance of the master to the page.
            // Parameters: PinX, PinY, master name, master ID.
            // Adjust PinX and PinY as needed for positioning.
            diagram.AddShape(5.0, 5.0, masterName, masterId);

            // Save the resulting diagram to a VDX file.
            diagram.Save(@"C:\Output\DiagramWithDataGraphic.vdx", SaveFileFormat.Vdx);

            // Clean up resources.
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
