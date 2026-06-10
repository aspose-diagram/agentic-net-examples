using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Holds the name of the master that will be used for all new shapes.
    private static string _defaultMasterName = "Rectangle";

    static void Main()
    {
        try
        {

            // Path to a stencil file that contains the master we want to use.
            // Adjust this path to point to a valid .vss or .vssx file on your system.
            string stencilPath = "Basic_U.vssx";

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Import the master from the stencil into the diagram.
            // The AddMaster method adds the master to the diagram's Masters collection.
            diagram.AddMaster(stencilPath, _defaultMasterName);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Add several shapes using the default master.
            AddShapeWithDefaultMaster(diagram, 2.0, 2.0);
            AddShapeWithDefaultMaster(diagram, 4.0, 2.0);
            AddShapeWithDefaultMaster(diagram, 6.0, 2.0);

            // Save the diagram to verify the result.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method that adds a shape on the first page using the default master.
    private static void AddShapeWithDefaultMaster(Diagram diagram, double pinX, double pinY)
    {
        // Use the first page (index 0).
        Page page = diagram.Pages[0];

        // Add the shape; AddShape returns the shape ID.
        long shapeId = page.AddShape(pinX, pinY, _defaultMasterName);

        // Retrieve the shape object if further modifications are needed.
        Shape shape = page.Shapes.GetShape(shapeId);

        // Example: set some basic properties, such as text.
        shape.Text.Value.Clear();
        shape.Text.Value.Add(new Txt($"Shape {shapeId}"));
    }
}
