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

            // Path to the stencil file (VSS) and the master name to import
            string stencilPath = @"C:\Stencils\Basic_U.vss";
            string masterName = "Rectangle";

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Import the master shape from the stencil into the diagram
            // Returns the unique ID of the master within the diagram's Masters collection
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Optionally, add an instance of the imported master to the first page
            // (page index 0, coordinates in inches)
            double pinX = 4.0;
            double pinY = 3.0;
            long shapeId = diagram.AddShape(pinX, pinY, masterName, 0);

            // Save the resulting diagram to a VDX file
            diagram.Save(@"C:\Output\Result.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
