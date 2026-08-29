using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    targetShape = shp;
                    break;
                }

                if (targetShape == null)
                {
                    throw new Exception("No shape found on the first page.");
                }

                // Define new absolute coordinates
                double newPinX = 5.0; // inches
                double newPinY = 5.0; // inches

                // Move the shape to the new absolute position
                targetShape.MoveTo(newPinX, newPinY);

                // Retrieve the updated PinX and PinY values
                double actualPinX = targetShape.XForm.PinX.Value;
                double actualPinY = targetShape.XForm.PinY.Value;

                // Output the coordinates to confirm the move
                Console.WriteLine($"Shape ID {targetShape.ID} moved to:");
                Console.WriteLine($"PinX = {actualPinX} inches");
                Console.WriteLine($"PinY = {actualPinY} inches");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }