using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps of the current page
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Modify orientation to Landscape
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Set horizontal scaling factor (ScaleX) to 75%
                    printProps.ScaleX.Value = 0.75;

                    // Output the current settings after modification
                    Console.WriteLine($"Page ID {page.ID}: Orientation = {printProps.PrintPageOrientation.Value}, ScaleX = {printProps.ScaleX.Value}");
                }

                // Save the modified diagram to a new file
                diagram.Save("output_modified.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
