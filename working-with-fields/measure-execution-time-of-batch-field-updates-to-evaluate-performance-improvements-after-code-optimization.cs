using System;
using System.Diagnostics;
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
                string outputPath = "output_updated.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Start timing the batch field update operation
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through all fields of the shape
                        foreach (Field field in shape.Fields)
                        {
                            // Example update: set a new value and clear formatting
                            field.Value.Val = "UpdatedValue";
                            field.Value.Ufev.F = "";
                            field.Value.Ufev.Unit = MeasureConst.Undefined;
                        }
                    }
                }

                // Stop timing
                stopwatch.Stop();
                Console.WriteLine($"Batch field update completed in {stopwatch.ElapsedMilliseconds} ms.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }