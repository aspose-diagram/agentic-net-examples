using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // ID of the shape to duplicate (example uses shape with ID = 1)
                long originalShapeId = 1;

                // Retrieve the original shape
                Shape originalShape = page.Shapes.GetShape(originalShapeId);
                if (originalShape == null)
                    throw new Exception($"Shape with ID {originalShapeId} not found.");

                // Ensure the shape has an associated master
                if (originalShape.Master == null)
                    throw new Exception("The original shape does not have a master to duplicate from.");

                // Get the master name to use for the new shape
                string masterName = originalShape.Master.Name;

                // Get original position
                double originalPinX = originalShape.XForm.PinX.Value;
                double originalPinY = originalShape.XForm.PinY.Value;

                // Define offset for the duplicated shape
                double offsetX = 2.0; // inches to the right
                double offsetY = 2.0; // inches upward

                // Add a new shape using the same master and adjusted position
                long newShapeId = page.AddShape(originalPinX + offsetX, originalPinY + offsetY, masterName);

                // Optionally retrieve the new shape (e.g., to modify further)
                Shape newShape = page.Shapes.GetShape(newShapeId);
                if (newShape == null)
                    throw new Exception("Failed to retrieve the newly added shape.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }