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

                // Desired uniform page size (in inches)
                const double targetWidth = 11.0;   // e.g., Letter width
                const double targetHeight = 8.5;   // e.g., Letter height

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // -------------------------------------------------
                    // Approach 1: Resize each page individually
                    // -------------------------------------------------
                    Stopwatch swIndividual = Stopwatch.StartNew();

                    foreach (Page page in diagram.Pages)
                    {
                        // Set width and height for the current page
                        page.PageSheet.PageProps.PageWidth.Value = targetWidth;
                        page.PageSheet.PageProps.PageHeight.Value = targetHeight;
                    }

                    swIndividual.Stop();
                    Console.WriteLine($"Individual resize time: {swIndividual.ElapsedMilliseconds} ms");

                    // -------------------------------------------------
                    // Approach 2: Apply uniform size using a helper method
                    // (still iterates, but demonstrates a single point of size definition)
                    // -------------------------------------------------
                    Stopwatch swUniform = Stopwatch.StartNew();

                    ApplyUniformPageSize(diagram, targetWidth, targetHeight);

                    swUniform.Stop();
                    Console.WriteLine($"Uniform resize time: {swUniform.ElapsedMilliseconds} ms");

                    // Save the modified diagram (optional)
                    const string outputPath = "output_resized.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Sets the same width and height for all pages in the diagram.
        /// </summary>
        static void ApplyUniformPageSize(Diagram diagram, double width, double height)
        {
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = width;
                page.PageSheet.PageProps.PageHeight.Value = height;
            }
        }
    }