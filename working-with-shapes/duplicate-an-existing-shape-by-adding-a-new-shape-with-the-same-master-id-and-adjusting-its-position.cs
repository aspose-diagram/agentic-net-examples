using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Find the first shape on the page to duplicate
                Shape originalShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    originalShape = shape;
                    break;
                }

                if (originalShape == null)
                {
                    Console.WriteLine("No shapes found on the page to duplicate.");
                    return;
                }

                // Ensure the shape has an associated master
                if (originalShape.Master == null)
                {
                    Console.WriteLine("The selected shape does not have a master and cannot be duplicated via master.");
                    return;
                }

                // Retrieve the master name from the original shape
                string masterName = originalShape.Master.Name;

                // Get the original shape's position
                double originalPinX = originalShape.XForm.PinX.Value;
                double originalPinY = originalShape.XForm.PinY.Value;

                // Define an offset for the duplicated shape's position
                double offsetX = 2.0; // inches to the right
                double offsetY = 2.0; // inches upward

                double newPinX = originalPinX + offsetX;
                double newPinY = originalPinY + offsetY;

                // Add a new shape using the same master name and the new position
                long newShapeId = page.AddShape(newPinX, newPinY, masterName);

                // Optionally retrieve the newly added shape for further modifications
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Example: copy the text from the original shape to the new shape
                string originalText = originalShape.Text.Value.ToString();
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt(originalText));

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape duplicated successfully. New shape ID: {newShapeId}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }