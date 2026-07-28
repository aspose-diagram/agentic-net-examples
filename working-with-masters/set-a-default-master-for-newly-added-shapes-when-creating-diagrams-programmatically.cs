using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Path to a stencil (VSS) or template (VDX) that contains the desired master shape
            string stencilPath = "basic_u.vss";

            // Name of the master shape inside the stencil (e.g., "Rectangle")
            string masterName = "Rectangle";

            // Add the master from the stencil to the diagram and obtain its unique ID
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Retrieve the Master object that was just added
            Master defaultMaster = diagram.Masters[masterId];

            // Use the first page of the diagram (a new diagram contains one default page)
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Example: Add several shapes that automatically use the default master
            // -----------------------------------------------------------------
            for (int i = 0; i < 3; i++)
            {
                // Create a new Shape instance
                Shape shape = new Shape();

                // Assign the default master to the shape
                shape.Master = defaultMaster;

                // Define position for the shape (PinX, PinY) – spaced horizontally
                double pinX = 2.0 + i * 2.0; // inches
                double pinY = 2.0;          // inches

                // Add the shape to the page using the overload that accepts a Shape object
                // The master name is also required by the API; we pass the same master name.
                page.AddShape(shape, masterName);
            }

            // Save the diagram to a VDX file
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
