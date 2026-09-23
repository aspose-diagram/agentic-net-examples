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

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after updates
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Start measuring time
                Stopwatch sw = Stopwatch.StartNew();

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through all fields of the shape
                    foreach (Field field in shape.Fields)
                    {
                        // Update the field's displayed value
                        field.Value.Val = "Updated";

                        // Clear any formula or unit to keep it simple
                        field.Value.Ufev.F = "";
                        field.Value.Ufev.Unit = MeasureConst.Undefined;
                    }
                }

                // Stop measuring time
                sw.Stop();

                // Output the elapsed time
                Console.WriteLine($"Batch field update completed in {sw.ElapsedMilliseconds} ms.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }