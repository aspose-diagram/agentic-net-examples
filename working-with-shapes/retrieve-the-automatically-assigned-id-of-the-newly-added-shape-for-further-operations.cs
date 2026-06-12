using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load or create a new diagram
                Diagram diagram = new Diagram(); // empty diagram

                // Ensure there is at least one page to work with
                if (diagram.Pages.Count == 0)
                    diagram.Pages.Add(new Page());

                // Load a master shape (e.g., Rectangle) from a Visio stencil
                // Adjust the stencil path to a valid .vss/.vssx file on your system
                string stencilPath = @"C:\VisioStencils\basic_u.vss";
                string masterName = "Rectangle";
                diagram.AddMaster(stencilPath, masterName);

                // Add a new shape using the master; AddShape returns the automatically assigned ID (long)
                double pinX = 2.0;   // X position in inches
                double pinY = 2.0;   // Y position in inches
                long newShapeId = diagram.Pages[0].AddShape(pinX, pinY, masterName);

                // Output the assigned ID
                Console.WriteLine($"New shape ID: {newShapeId}");

                // Retrieve the concrete Shape object for further manipulation
                Shape newShape = diagram.Pages[0].Shapes.GetShape(newShapeId);

                // Example: set some text on the newly added shape
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt("Hello Aspose.Diagram!"));

                // (Optional) Save the diagram to verify the changes
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }