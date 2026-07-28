using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for input and output file paths
            Console.Write("Enter the path of the Visio diagram to load: ");
            string inputPath = Console.ReadLine();

            Console.Write("Enter the path where the updated diagram will be saved: ");
            string outputPath = Console.ReadLine();

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to apply the line style change
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply new line style
                    shape.Line.LineColor.Value = "#FF0000";               // Red line color
                    shape.Line.LineWeight.Value = 0.02;                  // Line weight (in inches)
                    shape.Line.LinePattern.Value = LinePatternValue.Solid; // Solid line pattern
                }
            }

            // Save the updated diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved successfully.");
        }
    }