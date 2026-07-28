using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Path to the stencil file and the master name to be added
            string stencilPath = "basic.vssx";
            string masterName = "Rectangle";

            // Import the master from the stencil into the diagram
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Retrieve the target page (e.g., the first page)
            Page page = diagram.Pages[0];

            // Position where the shape will be placed (in inches)
            double pinX = 5.0;
            double pinY = 5.0;

            // Add an instance of the master shape to the page
            long shapeId = page.AddShape(pinX, pinY, masterName);

            // Save the updated diagram
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
