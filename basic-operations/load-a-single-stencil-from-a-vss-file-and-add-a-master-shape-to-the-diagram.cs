using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the stencil file (.vss) that contains the master shape.
                string stencilPath = @"C:\Stencils\Basic Shapes.vss";

                // Name of the master shape inside the stencil to be used.
                // Adjust this to match an actual master name present in the stencil.
                string masterName = "Rectangle";

                // Create a new empty Visio diagram.
                Diagram diagram = new Diagram();

                // Import the specified master from the stencil into the diagram.
                // This makes the master available for shape creation.
                diagram.AddMaster(stencilPath, masterName);

                // Define the position where the new shape will be placed (in inches).
                double pinX = 2.0;
                double pinY = 2.0;

                // Add a shape based on the imported master to the first page (page index 0).
                // The method returns the shape ID (long).
                long shapeId = diagram.AddShape(pinX, pinY, masterName, 0);

                // Retrieve the shape instance for further modifications (if needed).
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(shapeId);

                // Example: set some text on the newly added shape.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Hello Aspose.Diagram"));

                // Save the diagram to a VSDX file.
                string outputPath = @"C:\Output\DiagramWithMaster.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }