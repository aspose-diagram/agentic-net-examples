using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the PrintProps of the current page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Log original PrintProps values
                Console.WriteLine($"Page ID {page.ID} - Original PrintProps:");
                Console.WriteLine($"  Left Margin   : {printProps.PageLeftMargin.Value}");
                Console.WriteLine($"  Right Margin  : {printProps.PageRightMargin.Value}");
                Console.WriteLine($"  Top Margin    : {printProps.PageTopMargin.Value}");
                Console.WriteLine($"  Bottom Margin : {printProps.PageBottomMargin.Value}");
                Console.WriteLine($"  Orientation   : {printProps.PrintPageOrientation.Value}");
                Console.WriteLine($"  Fit To Sheet  : {printProps.OnPage.Value}");
                Console.WriteLine($"  Pages Across  : {printProps.PagesX.Value}");
                Console.WriteLine($"  Pages Down    : {printProps.PagesY.Value}");
                Console.WriteLine($"  Scale X       : {printProps.ScaleX.Value}");
                Console.WriteLine($"  Scale Y       : {printProps.ScaleY.Value}");

                // Modify PrintProps values
                printProps.PageLeftMargin.Value = 0.5;   // inches
                printProps.PageRightMargin.Value = 0.5;  // inches
                printProps.PageTopMargin.Value = 0.5;    // inches
                printProps.PageBottomMargin.Value = 0.5; // inches

                // Set orientation to Landscape
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Enable Fit to Sheet across 1 page and down 1 page
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Set scaling factors to 1 (100%)
                printProps.ScaleX.Value = 1;
                printProps.ScaleY.Value = 1;

                // Log updated PrintProps values
                Console.WriteLine($"Page ID {page.ID} - Updated PrintProps:");
                Console.WriteLine($"  Left Margin   : {printProps.PageLeftMargin.Value}");
                Console.WriteLine($"  Right Margin  : {printProps.PageRightMargin.Value}");
                Console.WriteLine($"  Top Margin    : {printProps.PageTopMargin.Value}");
                Console.WriteLine($"  Bottom Margin : {printProps.PageBottomMargin.Value}");
                Console.WriteLine($"  Orientation   : {printProps.PrintPageOrientation.Value}");
                Console.WriteLine($"  Fit To Sheet  : {printProps.OnPage.Value}");
                Console.WriteLine($"  Pages Across  : {printProps.PagesX.Value}");
                Console.WriteLine($"  Pages Down    : {printProps.PagesY.Value}");
                Console.WriteLine($"  Scale X       : {printProps.ScaleX.Value}");
                Console.WriteLine($"  Scale Y       : {printProps.ScaleY.Value}");
                Console.WriteLine(); // Blank line for readability
            }

            // Save the modified diagram to a new file
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}