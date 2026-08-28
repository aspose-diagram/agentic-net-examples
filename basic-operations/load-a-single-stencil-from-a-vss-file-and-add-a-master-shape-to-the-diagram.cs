using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Path to the VSS stencil file
            string stencilPath = "stencil.vss";

            // Name (or universal name) of the master shape inside the stencil
            string masterName = "MyMaster";

            // Add the master from the stencil to the diagram
            // Returns the unique ID of the added master (not used further here)
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Define the position where the shape instance will be placed (in inches)
            double pinX = 5.0;
            double pinY = 5.0;

            // Add an instance of the master shape to the active page
            diagram.ActivePage.AddShape(pinX, pinY, masterName);

            // Save the diagram to a VDX file
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
