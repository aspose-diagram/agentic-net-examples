using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Desired page size (in inches).
        double newWidth = 8.5;
        double newHeight = 11.0;

        // -----------------------------------------------------------------
        // Approach 1: Resize each page individually and save after each change.
        // -----------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                Stopwatch sw = Stopwatch.StartNew();

                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Set new dimensions for the current page.
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // Save the diagram after modifying each page to simulate per‑page processing.
                    string perPageOutput = Path.Combine(outputDir, $"Resized_Individual_Page_{pageIndex + 1}.vsdx");
                    diagram.Save(perPageOutput, SaveFileFormat.Vsdx);
                    pageIndex++;
                }

                sw.Stop();
                Console.WriteLine($"Individual page resizing and saving took: {sw.ElapsedMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during individual page resizing: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Approach 2: Resize all pages first, then save once.
        // -----------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                Stopwatch sw = Stopwatch.StartNew();

                foreach (Page page in diagram.Pages)
                {
                    // Apply the same new dimensions to each page.
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;
                }

                // Save the diagram after all pages have been resized.
                string uniformOutput = Path.Combine(outputDir, "Resized_Uniform.vsdx");
                diagram.Save(uniformOutput, SaveFileFormat.Vsdx);

                sw.Stop();
                Console.WriteLine($"Uniform resizing and single save took: {sw.ElapsedMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during uniform page resizing: {ex.Message}");
            return;
        }
    }
}