using System.IO;
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

            // Paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputIndividual = "output_individual.vsdx";
            string outputUniform = "output_uniform.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Preserve original page sizes for later reset
                double[] originalWidths = new double[diagram.Pages.Count];
                double[] originalHeights = new double[diagram.Pages.Count];
                int i = 0;
                foreach (Page page in diagram.Pages)
                {
                    originalWidths[i] = page.PageSheet.PageProps.PageWidth.Value;
                    originalHeights[i] = page.PageSheet.PageProps.PageHeight.Value;
                    i++;
                }

                // -------------------------------------------------
                // Approach 1: Resize each page individually (different sizes)
                // -------------------------------------------------
                Stopwatch swIndividual = Stopwatch.StartNew();
                i = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Example: increase width by (i+1) inches, height by (i+1)*0.5 inches
                    page.PageSheet.PageProps.PageWidth.Value = originalWidths[i] + (i + 1) * 1.0;
                    page.PageSheet.PageProps.PageHeight.Value = originalHeights[i] + (i + 1) * 0.5;
                    i++;
                }
                swIndividual.Stop();

                // Save the result of individual resizing
                diagram.Save(outputIndividual, SaveFileFormat.Vsdx);

                // Reset pages to original dimensions
                i = 0;
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = originalWidths[i];
                    page.PageSheet.PageProps.PageHeight.Value = originalHeights[i];
                    i++;
                }

                // -------------------------------------------------
                // Approach 2: Apply a uniform size to all pages
                // -------------------------------------------------
                double uniformWidth = 11.0;  // inches
                double uniformHeight = 8.5;  // inches

                Stopwatch swUniform = Stopwatch.StartNew();
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = uniformWidth;
                    page.PageSheet.PageProps.PageHeight.Value = uniformHeight;
                }
                swUniform.Stop();

                // Save the result of uniform resizing
                diagram.Save(outputUniform, SaveFileFormat.Vsdx);

                // Report performance
                Console.WriteLine($"Individual resizing time: {swIndividual.ElapsedMilliseconds} ms");
                Console.WriteLine($"Uniform resizing time: {swUniform.ElapsedMilliseconds} ms");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
