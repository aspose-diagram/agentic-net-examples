using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Count the number of shapes on the current page
                    int shapeCount = page.Shapes.Count;

                    // Define a height factor (e.g., 1 inch per shape)
                    double heightPerShape = 1.0; // inches

                    // Calculate the new page height
                    double newHeight = shapeCount * heightPerShape;

                    // Set the page height (in inches)
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;
                }

                // Save the modified diagram back to a Visio file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }