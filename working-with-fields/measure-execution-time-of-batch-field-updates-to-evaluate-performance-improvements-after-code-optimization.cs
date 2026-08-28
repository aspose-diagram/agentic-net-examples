using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Start measuring execution time
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Perform batch field updates
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Update each text field in the shape
                    foreach (Field field in shape.Fields)
                    {
                        // Set a new value
                        field.Value.Val = "Updated";

                        // Clear any existing formatting
                        field.Format.Val = "";
                        field.Format.Ufev.F = "";
                        field.Format.Ufev.Unit = MeasureConst.Undefined;
                    }
                }
            }

            // Stop measuring and output the elapsed time
            stopwatch.Stop();
            Console.WriteLine($"Batch field update elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
