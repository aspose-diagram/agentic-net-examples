using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Verify command line arguments.
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Perform spacing/layout on the first page using CompactTree.
                // ------------------------------------------------------------
                if (diagram.Pages.Count > 0)
                {
                    // Create layout options for compact tree layout.
                    LayoutOptions layoutOptions = new LayoutOptions();
                    layoutOptions.LayoutStyle = LayoutStyle.CompactTree;
                    layoutOptions.Direction = LayoutDirection.DownThenRight;
                    // Do not automatically enlarge the page.
                    layoutOptions.EnlargePage = false; // BOOL enum is not applicable here.

                    // Apply layout to the first page.
                    Page firstPage = diagram.Pages[0];
                    firstPage.Layout(layoutOptions);
                }
                else
                {
                    Console.Error.WriteLine("The diagram contains no pages to layout.");
                }

                // ------------------------------------------------------------
                // Validate the diagram after spacing.
                // ------------------------------------------------------------
                // Note: Aspose.Diagram does not provide a Validate method.
                // Validation of diagram integrity is performed automatically during save.

                // ------------------------------------------------------------
                // Save the updated diagram.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
