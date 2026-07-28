using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPageHeightAdjuster <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Count the number of shapes on the current page
                int shapeCount = page.Shapes.Count;

                // Define a height factor (e.g., 1 inch per shape)
                double heightPerShape = 1.0; // inches

                // Calculate the new page height based on the shape count
                double newHeight = shapeCount * heightPerShape;

                // Set the page height (values are in inches)
                page.PageSheet.PageProps.PageHeight.Value = newHeight;
            }

            // Save the modified diagram back to a Visio file (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }