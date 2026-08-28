using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps of the current page
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Log original PrintProps values
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                    Console.WriteLine("Original PrintProps:");
                    Console.WriteLine($"  Orientation: {printProps.PrintPageOrientation.Value}");
                    Console.WriteLine($"  ScaleX: {printProps.ScaleX.Value}");
                    Console.WriteLine($"  ScaleY: {printProps.ScaleY.Value}");
                    Console.WriteLine($"  OnPage (Fit to Sheet): {printProps.OnPage.Value}");
                    Console.WriteLine($"  PagesX: {printProps.PagesX.Value}");
                    Console.WriteLine($"  PagesY: {printProps.PagesY.Value}");
                    Console.WriteLine($"  Top Margin: {printProps.PageTopMargin.Value}");
                    Console.WriteLine($"  Bottom Margin: {printProps.PageBottomMargin.Value}");
                    Console.WriteLine($"  Left Margin: {printProps.PageLeftMargin.Value}");
                    Console.WriteLine($"  Right Margin: {printProps.PageRightMargin.Value}");

                    // Perform batch modifications to PrintProps
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    printProps.ScaleX.Value = 0.75; // 75% scaling
                    printProps.ScaleY.Value = 0.75;
                    printProps.OnPage.Value = BOOL.True; // Enable Fit to Sheet
                    printProps.PagesX.Value = 1; // One page across
                    printProps.PagesY.Value = 1; // One page down
                    // Set margins to 0.5 inches (Visio units are inches)
                    printProps.PageTopMargin.Value = 0.5;
                    printProps.PageBottomMargin.Value = 0.5;
                    printProps.PageLeftMargin.Value = 0.5;
                    printProps.PageRightMargin.Value = 0.5;

                    // Log updated PrintProps values
                    Console.WriteLine("Updated PrintProps:");
                    Console.WriteLine($"  Orientation: {printProps.PrintPageOrientation.Value}");
                    Console.WriteLine($"  ScaleX: {printProps.ScaleX.Value}");
                    Console.WriteLine($"  ScaleY: {printProps.ScaleY.Value}");
                    Console.WriteLine($"  OnPage (Fit to Sheet): {printProps.OnPage.Value}");
                    Console.WriteLine($"  PagesX: {printProps.PagesX.Value}");
                    Console.WriteLine($"  PagesY: {printProps.PagesY.Value}");
                    Console.WriteLine($"  Top Margin: {printProps.PageTopMargin.Value}");
                    Console.WriteLine($"  Bottom Margin: {printProps.PageBottomMargin.Value}");
                    Console.WriteLine($"  Left Margin: {printProps.PageLeftMargin.Value}");
                    Console.WriteLine($"  Right Margin: {printProps.PageRightMargin.Value}");
                    Console.WriteLine(new string('-', 50));
                }

                // Save the modified diagram
                diagram.Save("output_modified.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
