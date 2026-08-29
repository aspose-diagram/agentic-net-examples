using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to process
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Processing Page: {page.NameU}");

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure LocPinX and LocPinY cells are present
                        if (shape.XForm.LocPinX == null || shape.XForm.LocPinY == null)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} ('{shape.NameU}') lacks LocPinX/Y cells. Skipping.");
                            continue;
                        }

                        try
                        {
                            // Retrieve necessary values
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double locPinX = shape.XForm.LocPinX.Value;
                            double locPinY = shape.XForm.LocPinY.Value;
                            double angleDeg = shape.XForm.Angle.Value; // Angle is in degrees

                            // Convert angle to radians for trigonometric calculations
                            double angleRad = angleDeg * Math.PI / 180.0;

                            // Calculate absolute coordinates considering the local pin offset and rotation
                            double offsetX = -locPinX;
                            double offsetY = -locPinY;

                            double cos = Math.Cos(angleRad);
                            double sin = Math.Sin(angleRad);

                            double absoluteX = pinX + (offsetX * cos - offsetY * sin);
                            double absoluteY = pinY + (offsetX * sin + offsetY * cos);

                            Console.WriteLine($"Shape ID {shape.ID} ('{shape.NameU}'): Absolute X = {absoluteX:F3}, Y = {absoluteY:F3}");
                        }
                        catch (Exception ex)
                        {
                            // Log the error and continue with the next shape
                            Console.WriteLine($"Error processing Shape ID {shape.ID}: {ex.Message}. Skipping.");
                        }
                    }
                }

                // Optionally save the diagram after processing (no changes made here)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }