using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (replace with an actual file path)
            string inputPath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                try
                {
                    // Attempt to assign an undefined value to PrintPageOrientation.
                    // The enum cast to an invalid integer will trigger an ArgumentException.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = (PrintPageOrientationValue)999;
                }
                catch (ArgumentException ex)
                {
                    // Log the error details to the console
                    Console.WriteLine($"Error setting PrintPageOrientation: {ex.Message}");
                }

                // Save the diagram (optional)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
