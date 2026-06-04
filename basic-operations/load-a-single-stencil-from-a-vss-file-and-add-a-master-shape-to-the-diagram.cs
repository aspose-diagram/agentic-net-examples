using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the stencil (VSS) file that contains the master shape
            string stencilPath = "stencil.vss";

            // Name (or universal name) of the master shape inside the stencil
            string masterName = "MyMaster";

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Load the master shape from the stencil into the diagram
            // Returns the unique ID of the master within the diagram's masters collection
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Define the position where the shape will be placed (in inches)
            double pinX = 5.0; // X‑coordinate of the shape's pin
            double pinY = 5.0; // Y‑coordinate of the shape's pin

            // Add an instance of the master shape to the active page
            // Returns the unique ID of the newly added shape
            long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName);

            // Save the resulting diagram to a VDX file
            string outputPath = "output.vdx";
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
