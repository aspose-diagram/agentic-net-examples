using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to a Visio stencil that contains the master we want to use.
                // Replace with an actual .vss or .vssx file path on your system.
                string stencilPath = @"C:\Stencils\Basic Shapes.vssx";

                // Name of the master inside the stencil that will be used as the default.
                string defaultMasterName = "Rectangle";

                // Create a new empty diagram.
                Diagram diagram = new Diagram();

                // Import the master from the stencil into the diagram.
                // This makes the master available for shape creation.
                diagram.AddMaster(stencilPath, defaultMasterName);

                // Add a shape using the default master.
                // Parameters: pinX, pinY (position), master name, page index (0 for the first page).
                long shapeId = diagram.AddShape(2.0, 2.0, defaultMasterName, 0);

                // Retrieve the newly added shape to modify its properties if needed.
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                shape.Text.Value.Add(new Txt("Default Master Shape"));
                shape.Fill.FillForegnd.Value = "#FFCC00"; // Set fill color.

                // Save the diagram to a VSDX file.
                diagram.Save("DefaultMasterDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }