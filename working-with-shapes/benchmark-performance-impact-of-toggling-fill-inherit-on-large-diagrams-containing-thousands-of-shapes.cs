using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // -----------------------------------------------------------------
                // NOTE: The Aspose.Diagram API does not expose a Fill.Inherit
                // property on Shape or Fill objects. The available read‑only
                // property is Shape.InheritFill, which provides inherited fill
                // values but cannot be toggled. Therefore the intended benchmark
                // of toggling Fill.Inherit cannot be implemented directly.
                // -----------------------------------------------------------------
                // TODO: Replace the following placeholder logic with the actual
                // operation once a suitable API for toggling fill inheritance is
                // available. For now we measure the time taken to iterate over
                // all shapes as a baseline.

                // Benchmark: iterate over all shapes (baseline).
                Stopwatch swBaseline = Stopwatch.StartNew();
                foreach (Page page in diagram.Pages)
                {
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];
                        // Access the read‑only inherited fill data (no modification).
                        var inheritFill = shape.InheritFill;
                        // Example access to inherited fill foreground color.
                        string fg = inheritFill.FillForegnd.Value;
                        // Prevent compiler optimizations by using the value.
                        if (fg == null) { /* no-op */ }
                    }
                }
                swBaseline.Stop();

                // Output the timing result.
                Console.WriteLine($"Iterated over all shapes (baseline): {swBaseline.ElapsedMilliseconds} ms");

                // Save the (unchanged) diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}