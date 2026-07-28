using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    // Entry point of the console application
    public static void Main(string[] args)
    {
        try
        {

            // Input diagram path (first argument) or default
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Output diagram path (second argument) or default
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Page count threshold (third argument) or default of 5 pages
            int pageThreshold = 5;
            if (args.Length > 2)
            {
                if (!int.TryParse(args[2], out pageThreshold))
                {
                    Console.WriteLine("Invalid page threshold supplied. Using default value of 5.");
                    pageThreshold = 5;
                }
            }

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Determine the number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // If the diagram exceeds the threshold, disable guide visibility globally
            if (pageCount > pageThreshold)
            {
                // Iterate over all windows (if any) and set ShowGuides to FALSE
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGuides = BOOL.False;
                }

                Console.WriteLine($"Diagram has {pageCount} pages, which exceeds the threshold of {pageThreshold}. ShowGuides set to FALSE.");
            }
            else
            {
                Console.WriteLine($"Diagram has {pageCount} pages, which does not exceed the threshold of {pageThreshold}. No changes made.");
            }

            // Save the modified diagram back to a file (using VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
