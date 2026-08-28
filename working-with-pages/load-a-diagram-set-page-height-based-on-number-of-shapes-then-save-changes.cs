using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path (can overwrite the original or be a new file)
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Count the number of shapes on the page
                int shapeCount = page.Shapes.Count;

                // Define height per shape (in inches)
                double heightPerShape = 1.0; // 1 inch per shape

                // Calculate new page height (add a small margin)
                double newHeight = shapeCount * heightPerShape + 0.5; // 0.5 inch margin

                // Set the page height
                page.PageSheet.PageProps.PageHeight.Value = newHeight;

                Console.WriteLine($"Page height set to {newHeight} inches based on {shapeCount} shapes.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }