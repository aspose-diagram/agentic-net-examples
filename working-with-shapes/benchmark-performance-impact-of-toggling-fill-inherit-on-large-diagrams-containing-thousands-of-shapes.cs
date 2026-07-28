using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Benchmark scenario: rely on inherited fill (no overrides)
        Stopwatch inheritTimer = Stopwatch.StartNew();
        using (Diagram diagram = new Diagram(inputPath))
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Access inherited fill to simulate read operation
                    string inheritedForegnd = shape.InheritFill.FillForegnd.Value;
                }
            }
            inheritTimer.Stop();

            // Save the unchanged diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        Console.WriteLine($"Time with inherited fill (no overrides): {inheritTimer.ElapsedMilliseconds} ms");

        // Benchmark scenario: override fill for each shape
        Stopwatch overrideTimer = Stopwatch.StartNew();
        using (Diagram diagram = new Diagram(inputPath))
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Override fill foreground color
                    shape.Fill.FillForegnd.Value = "#FF0000";
                }
            }
            overrideTimer.Stop();

            // Save the diagram with overridden fills
            string overriddenPath = Path.Combine(
                Path.GetDirectoryName(outputPath) ?? string.Empty,
                "overridden_" + Path.GetFileName(outputPath));
            diagram.Save(overriddenPath, SaveFileFormat.Vsdx);
        }
        Console.WriteLine($"Time with fill overrides: {overrideTimer.ElapsedMilliseconds} ms");
    }
}
