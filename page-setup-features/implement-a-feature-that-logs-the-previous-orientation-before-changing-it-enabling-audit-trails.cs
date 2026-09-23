using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the current print orientation
                    PrintPageOrientationValue previousOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                    // Log the previous orientation for audit purposes
                    Console.WriteLine($"Page ID {page.ID} (Name: {page.Name}) - Previous Orientation: {previousOrientation}");

                    // Change the orientation to Landscape (example change)
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Confirm the change
                    Console.WriteLine($"Page ID {page.ID} orientation set to Landscape.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed and saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
