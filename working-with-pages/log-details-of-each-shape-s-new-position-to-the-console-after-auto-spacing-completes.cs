using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Set up auto‑spacing options (example distances)
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    options.DistanceInHorizontal = 2.0;
                    options.DistanceInVertical = 2.0;

                    // Perform auto‑spacing on the current page
                    page.AutoSpaceShapes(page.Shapes, options);

                    // Log the new position of each shape that is not deleted
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        Console.WriteLine($"Page '{page.NameU}' Shape ID {shape.ID}: PinX={pinX}, PinY={pinY}");
                    }
                }

                // Save the updated diagram (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }