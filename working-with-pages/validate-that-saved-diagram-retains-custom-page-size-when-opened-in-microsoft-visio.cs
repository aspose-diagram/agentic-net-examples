using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the custom page size (in inches).
        double customWidth = 8.5;
        double customHeight = 11.0;

        try
        {
            // Load the original diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page.
                Page page = diagram.Pages[0];

                // Set the custom page dimensions.
                page.PageSheet.PageProps.PageWidth.Value = customWidth;
                page.PageSheet.PageProps.PageHeight.Value = customHeight;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify that the custom page size persisted.
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                if (savedDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The saved diagram contains no pages.");
                    return;
                }

                // Retrieve the first page from the saved diagram.
                Page savedPage = savedDiagram.Pages[0];
                double savedWidth = savedPage.PageSheet.PageProps.PageWidth.Value;
                double savedHeight = savedPage.PageSheet.PageProps.PageHeight.Value;

                // Compare the saved dimensions with the expected custom size (allowing a tiny tolerance).
                const double tolerance = 0.001;
                bool widthMatches = Math.Abs(savedWidth - customWidth) <= tolerance;
                bool heightMatches = Math.Abs(savedHeight - customHeight) <= tolerance;

                if (widthMatches && heightMatches)
                {
                    Console.WriteLine("Custom page size retained after saving.");
                }
                else
                {
                    Console.Error.WriteLine($"Page size mismatch. Expected: {customWidth}x{customHeight}, Actual: {savedWidth}x{savedHeight}");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}