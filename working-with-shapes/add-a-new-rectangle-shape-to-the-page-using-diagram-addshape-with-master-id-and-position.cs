using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        // Path to a stencil that contains the "Rectangle" master.
        string stencilPath = "Basic_U.vssx";
        // Verify the stencil file exists before proceeding.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Import the "Rectangle" master from the stencil into the diagram.
            diagram.AddMaster(stencilPath, "Rectangle");

            // Add a rectangle shape to the first page (page index 0) at position (2.0, 2.0).
            // Use the master name (string) as required by the AddShape overload.
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the newly added shape for further modifications (e.g., adding text).
            Shape rectShape = diagram.Pages[0].Shapes.GetShape(shapeId);
            rectShape.Text.Value.Clear(); // Clear any existing text.
            rectShape.Text.Value.Add(new Txt("Rectangle")); // Add new text.

            // Save the diagram to a VSDX file.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}