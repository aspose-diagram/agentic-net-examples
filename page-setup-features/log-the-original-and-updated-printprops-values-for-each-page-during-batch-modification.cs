using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the PrintProps of the current page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Log original values (example: ScaleX and ScaleY)
                Console.WriteLine($"Page ID: {page.ID}");
                Console.WriteLine($"  Original ScaleX: {printProps.ScaleX.Value}");
                Console.WriteLine($"  Original ScaleY: {printProps.ScaleY.Value}");

                // Perform batch modification (example: set both scales to 100%)
                printProps.ScaleX.Value = 100.0;
                printProps.ScaleY.Value = 100.0;

                // Log updated values
                Console.WriteLine($"  Updated ScaleX: {printProps.ScaleX.Value}");
                Console.WriteLine($"  Updated ScaleY: {printProps.ScaleY.Value}");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
