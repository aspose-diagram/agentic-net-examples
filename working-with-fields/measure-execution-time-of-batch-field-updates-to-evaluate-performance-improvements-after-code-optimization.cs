using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument) or default path
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output Visio file path
                string outputPath = "output_updated.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Start timing the batch field update operation
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has at least one field before accessing
                        if (shape.Fields.Count > 0)
                        {
                            // Update each field in the shape
                            foreach (Field field in shape.Fields)
                            {
                                // Set a new value
                                field.Value.Val = "Updated";

                                // Clear any existing formula and unit
                                field.Value.Ufev.F = "";
                                field.Value.Ufev.Unit = MeasureConst.Undefined;
                            }
                        }
                    }
                }

                // Stop timing
                stopwatch.Stop();

                // Output the elapsed time
                Console.WriteLine($"Batch field update completed in {stopwatch.ElapsedMilliseconds} ms.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }