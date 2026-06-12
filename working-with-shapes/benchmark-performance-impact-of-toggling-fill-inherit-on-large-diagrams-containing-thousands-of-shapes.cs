using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BenchmarkFillInheritance <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Assume processing the first page; adjust as needed for multi‑page diagrams
        Page page = diagram.Pages[0];

        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Iterate through all shapes and toggle fill inheritance
        foreach (Shape shape in page.Shapes)
        {
            // Skip deleted shapes
            if (shape.Del == BOOL.True)
                continue;

            // Determine if the shape currently uses inherited fill color
            bool usesInheritedFill = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;

            if (usesInheritedFill)
            {
                // Break inheritance by assigning a distinct fill color
                shape.Fill.FillForegnd.Value = "#FF0000"; // Red
            }
            else
            {
                // Re‑enable inheritance by copying the inherited fill value
                shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
            }
        }

        sw.Stop();

        Console.WriteLine($"Toggled fill inheritance for {page.Shapes.Count} shapes in {sw.ElapsedMilliseconds} ms.");

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
