using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramFillInheritanceBenchmark <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Benchmark: apply an explicit fill color to every shape (break inheritance)
            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Set a solid red fill color
                    shape.Fill.FillForegnd.Value = "#FF0000";
                }
            }

            sw.Stop();
            Console.WriteLine($"Time to set explicit fill on all shapes: {sw.ElapsedMilliseconds} ms");

            // Benchmark: reset fill to the inherited value for every shape (enable inheritance)
            sw.Restart();

            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Restore the fill color from the inherited fill values
                    shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                }
            }

            sw.Stop();
            Console.WriteLine($"Time to reset fill to inherited values on all shapes: {sw.ElapsedMilliseconds} ms");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Processing completed.");
        }
    }