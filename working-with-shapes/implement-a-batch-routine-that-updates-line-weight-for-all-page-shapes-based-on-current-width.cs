using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramBatchProcessor <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram from the specified file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve the current width of the shape (in inches)
                            double shapeWidth = shape.XForm.Width.Value;

                            // Calculate a new line weight based on the width.
                            // Example: 1% of the shape's width.
                            double newLineWeight = shapeWidth * 0.01;

                            // Ensure the line weight is a positive value
                            if (newLineWeight < 0.001)
                                newLineWeight = 0.001; // Minimum visible line weight

                            // Update the shape's line weight
                            shape.Line.LineWeight.Value = newLineWeight;
                        }
                    }

                    // Save the updated diagram to the output path in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed successfully.");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }