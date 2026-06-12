using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the ID of the shape whose master will be replaced
                long targetShapeId = 1; // replace with actual shape ID

                // Define the ID of the new master to apply
                int newMasterId = 5; // replace with actual master ID present in the diagram

                // Retrieve the target shape from the first page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(targetShapeId);

                // Verify that the new master exists in the diagram
                if (!diagram.Masters.IsExist(newMasterId))
                {
                    throw new Exception($"Master with ID {newMasterId} does not exist in the diagram.");
                }

                // Retrieve the new master object
                Master newMaster = diagram.Masters.GetMaster(newMasterId);

                // Assign the new master to the shape
                shape.Master = newMaster;

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Shape master replacement completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }