using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Define the input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define the Y‑offset to add (in inches)
                double yOffset = 2.0;

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Add the offset to the current PinY value
                        shape.XForm.PinY.Value += yOffset;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Y coordinate of all non‑deleted shapes increased by {yOffset} inches.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }