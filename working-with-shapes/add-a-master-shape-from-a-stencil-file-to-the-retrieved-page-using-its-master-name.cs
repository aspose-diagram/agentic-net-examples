using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Path to the stencil file that contains the master shape
            string stencilPath = "MyStencil.vss";

            // Name of the master shape inside the stencil
            string masterName = "MyMaster";

            // Import the master from the stencil into the diagram
            diagram.AddMaster(stencilPath, masterName);

            // Retrieve the first page (index 0) from the diagram
            Page page = diagram.Pages[0];

            // Position where the shape will be placed (in inches)
            double pinX = 4.0;
            double pinY = 5.0;

            // Add the master shape to the page using its master name
            page.AddShape(pinX, pinY, masterName);

            // Save the updated diagram
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
