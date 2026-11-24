using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output_modified.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // NOTE: The Aspose.Diagram API does not expose a Fill.Inherit property.
                // Therefore, toggling inheritance cannot be performed directly.
                // The following code benchmarks iterating over all shapes without modifying inheritance.

                // Benchmark iterating over all shapes (baseline)
                Stopwatch swBaseline = Stopwatch.StartNew();
                foreach (Page page in diagram.Pages)
                {
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];
                        // Ensure shape and its Fill object exist
                        if (shape != null && shape.Fill != null)
                        {
                            // Placeholder for Fill.Inherit toggling if it were available
                            // TODO: Implement Fill.Inherit toggling when supported by the API
                        }
                    }
                }
                swBaseline.Stop();
                Console.WriteLine($"Time to iterate over all shapes (baseline): {swBaseline.ElapsedMilliseconds} ms");

                // Save the diagram (unchanged) to verify loading/saving works
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}