using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
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
                            double width = shape.XForm.Width.Value;

                            // Determine the new line weight based on the width.
                            // Example logic: line weight is 1% of the shape's width, minimum 0.01 inches.
                            double newLineWeight = Math.Max(0.01, width * 0.01);

                            // Update the line weight (in inches)
                            shape.Line.LineWeight.Value = newLineWeight;
                        }
                    }

                    // Save the updated diagram using VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Batch update completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }