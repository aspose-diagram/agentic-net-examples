using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_modified.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page to log and modify PrintProps
            foreach (Page page in diagram.Pages)
            {
                // Access the PrintProps collection for the current page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Store original values
                double origLeftMargin = printProps.PageLeftMargin.Value;
                double origRightMargin = printProps.PageRightMargin.Value;
                double origTopMargin = printProps.PageTopMargin.Value;
                double origBottomMargin = printProps.PageBottomMargin.Value;
                PrintPageOrientationValue origOrientation = printProps.PrintPageOrientation.Value;
                BOOL origOnPage = printProps.OnPage.Value;

                // Log original values
                Console.WriteLine($"Page ID {page.ID} - Original PrintProps:");
                Console.WriteLine($"  LeftMargin  = {origLeftMargin}");
                Console.WriteLine($"  RightMargin = {origRightMargin}");
                Console.WriteLine($"  TopMargin   = {origTopMargin}");
                Console.WriteLine($"  BottomMargin= {origBottomMargin}");
                Console.WriteLine($"  Orientation = {origOrientation}");
                Console.WriteLine($"  OnPage      = {origOnPage}");

                // Modify PrintProps (example: set margins to 0.5 inches, orientation to Landscape, enable OnPage)
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                printProps.OnPage.Value = BOOL.True;

                // Log updated values
                Console.WriteLine($"Page ID {page.ID} - Updated PrintProps:");
                Console.WriteLine($"  LeftMargin  = {printProps.PageLeftMargin.Value}");
                Console.WriteLine($"  RightMargin = {printProps.PageRightMargin.Value}");
                Console.WriteLine($"  TopMargin   = {printProps.PageTopMargin.Value}");
                Console.WriteLine($"  BottomMargin= {printProps.PageBottomMargin.Value}");
                Console.WriteLine($"  Orientation = {printProps.PrintPageOrientation.Value}");
                Console.WriteLine($"  OnPage      = {printProps.OnPage.Value}");
                Console.WriteLine(); // Blank line for readability
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}