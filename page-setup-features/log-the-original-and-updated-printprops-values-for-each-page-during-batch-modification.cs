using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                PrintProps props = page.PageSheet.PrintProps;

                // Log original PrintProps values
                Console.WriteLine($"Page {i + 1} - Original PrintProps:");
                LogPrintProps(props);

                // Example modification: set scaling to 100% (no scaling)
                props.ScaleX.Value = 100;
                props.ScaleY.Value = 100;

                // Log updated PrintProps values
                Console.WriteLine($"Page {i + 1} - Updated PrintProps:");
                LogPrintProps(props);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to output selected PrintProps properties
    static void LogPrintProps(PrintProps props)
    {
        Console.WriteLine($"  CenterX: {props.CenterX.Value}");
        Console.WriteLine($"  CenterY: {props.CenterY.Value}");
        Console.WriteLine($"  OnPage: {props.OnPage.Value}");
        Console.WriteLine($"  PagesX: {props.PagesX.Value}");
        Console.WriteLine($"  PagesY: {props.PagesY.Value}");
        Console.WriteLine($"  ScaleX: {props.ScaleX.Value}");
        Console.WriteLine($"  ScaleY: {props.ScaleY.Value}");
        Console.WriteLine($"  PaperKind: {props.PaperKind.Value}");
        Console.WriteLine($"  PrintGrid: {props.PrintGrid.Value}");
        // Add more properties here if needed
    }
}
