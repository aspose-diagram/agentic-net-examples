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

                // Path to the source Visio file
                const string inputPath = "input.vsdx";

                // Target page size (in inches)
                const double targetWidth = 11.0;   // Width
                const double targetHeight = 8.5;   // Height

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // -------------------------------------------------
                    // 1. Resize each page individually (loop over pages)
                    // -------------------------------------------------
                    Stopwatch swIndividual = Stopwatch.StartNew();

                    foreach (Page page in diagram.Pages)
                    {
                        // Set width and height for the current page
                        page.PageSheet.PageProps.PageWidth.Value = targetWidth;
                        page.PageSheet.PageProps.PageHeight.Value = targetHeight;
                    }

                    swIndividual.Stop();
                    Console.WriteLine($"Individual resizing time: {swIndividual.ElapsedMilliseconds} ms");

                    // Save the result of the first approach
                    diagram.Save("output_individual.vsdx", SaveFileFormat.Vsdx);

                    // -------------------------------------------------
                    // 2. Apply the same size to all pages (bulk assignment)
                    // -------------------------------------------------
                    // Note: Aspose.Diagram does not provide a single method to set all pages at once,
                    // so we still iterate, but this block represents the "uniform" approach.
                    Stopwatch swUniform = Stopwatch.StartNew();

                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PageProps.PageWidth.Value = targetWidth;
                        page.PageSheet.PageProps.PageHeight.Value = targetHeight;
                    }

                    swUniform.Stop();
                    Console.WriteLine($"Uniform resizing time: {swUniform.ElapsedMilliseconds} ms");

                    // Save the result of the second approach
                    diagram.Save("output_uniform.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }