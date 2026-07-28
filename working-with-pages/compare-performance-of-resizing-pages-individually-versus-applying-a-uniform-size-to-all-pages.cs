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

                // Paths – replace with actual file locations as needed
                const string inputPath = "input.vsdx";
                const string outputIndividual = "output_individual.vsdx";
                const string outputUniform = "output_uniform.vsdx";

                // -------------------------------------------------
                // 1. Load diagram and resize each page individually
                // -------------------------------------------------
                using (Diagram diagram = new Diagram(inputPath))
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    // Resize each page to the same dimensions (8.5 x 11 inches)
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PageProps.PageWidth.Value = 8.5;
                        page.PageSheet.PageProps.PageHeight.Value = 11;
                    }

                    sw.Stop();
                    Console.WriteLine($"Individual resizing time: {sw.ElapsedMilliseconds} ms");

                    // Save the result
                    diagram.Save(outputIndividual, SaveFileFormat.Vsdx);
                }

                // -------------------------------------------------
                // 2. Reload diagram and apply a uniform size using a helper method
                // -------------------------------------------------
                using (Diagram diagram = new Diagram(inputPath))
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    // Apply uniform size to all pages via a dedicated method
                    ApplyUniformPageSize(diagram, 8.5, 11);

                    sw.Stop();
                    Console.WriteLine($"Uniform resizing time (via helper): {sw.ElapsedMilliseconds} ms");

                    // Save the result
                    diagram.Save(outputUniform, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Sets the same width and height for every page in the diagram.
        /// </summary>
        /// <param name="diagram">The diagram whose pages will be resized.</param>
        /// <param name="widthInches">Desired page width in inches.</param>
        /// <param name="heightInches">Desired page height in inches.</param>
        private static void ApplyUniformPageSize(Diagram diagram, double widthInches, double heightInches)
        {
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = widthInches;
                page.PageSheet.PageProps.PageHeight.Value = heightInches;
            }
        }
    }