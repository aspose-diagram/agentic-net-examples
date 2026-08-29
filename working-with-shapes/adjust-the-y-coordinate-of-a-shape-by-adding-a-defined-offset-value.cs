using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define the Y offset to add (in inches)
                double yOffset = 1.0;

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure there is at least one shape on the page
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("The page contains no shapes.");
                }

                // Retrieve the first shape (you can replace this with any shape selection logic)
                Shape shape = page.Shapes.GetShape(1); // Shape IDs start at 1

                // Adjust the Y coordinate (PinY) by adding the offset
                shape.XForm.PinY.Value += yOffset;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }