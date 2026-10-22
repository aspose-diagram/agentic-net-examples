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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram for the first test (individual resizing)
            using (Diagram diagramIndividual = new Diagram(inputPath))
            {
                // Define the target page size (in inches)
                double targetWidth = 8.5;
                double targetHeight = 11.0;

                // Measure time for resizing each page individually
                Stopwatch swIndividual = Stopwatch.StartNew();

                // Iterate over all pages and set size directly
                foreach (Page page in diagramIndividual.Pages)
                {
                    // Set width and height via PageProps
                    page.PageSheet.PageProps.PageWidth.Value = targetWidth;
                    page.PageSheet.PageProps.PageHeight.Value = targetHeight;
                }

                swIndividual.Stop();
                Console.WriteLine($"Individual page resizing time: {swIndividual.ElapsedMilliseconds} ms");
            }

            // Load the diagram again for the second test (uniform resizing)
            using (Diagram diagramUniform = new Diagram(inputPath))
            {
                // Define the target page size (same as above)
                double uniformWidth = 8.5;
                double uniformHeight = 11.0;

                // Measure time for applying a uniform size to all pages
                Stopwatch swUniform = Stopwatch.StartNew();

                // Apply the same size to each page using a helper method
                ApplyUniformPageSize(diagramUniform, uniformWidth, uniformHeight);

                swUniform.Stop();
                Console.WriteLine($"Uniform page resizing time: {swUniform.ElapsedMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the same width and height for every page in the diagram.
    /// </summary>
    /// <param name="diagram">The diagram whose pages will be resized.</param>
    /// <param name="width">Target page width in inches.</param>
    /// <param name="height">Target page height in inches.</param>
    private static void ApplyUniformPageSize(Diagram diagram, double width, double height)
    {
        // Iterate over pages and assign the uniform dimensions
        foreach (Page page in diagram.Pages)
        {
            page.PageSheet.PageProps.PageWidth.Value = width;
            page.PageSheet.PageProps.PageHeight.Value = height;
        }
    }
}