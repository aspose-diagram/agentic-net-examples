using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path where the modified Visio file will be saved
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (you can change the index as needed)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Example condition: modify the first shape that is not deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Retrieve the current line pattern
                        LinePatternValue currentPattern = shape.Line.LinePattern.Value;
                        Console.WriteLine($"Shape ID {shape.ID} current line pattern: {currentPattern}");

                        // Change the line pattern to a dotted style
                        shape.Line.LinePattern.Value = LinePatternValue.Dot;
                        Console.WriteLine($"Shape ID {shape.ID} line pattern set to: {shape.Line.LinePattern.Value}");

                        // If you only want to modify a specific shape, break after the change
                        // break;
                    }
                }

                // Save the modified diagram back to a file using the Vsdx format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }