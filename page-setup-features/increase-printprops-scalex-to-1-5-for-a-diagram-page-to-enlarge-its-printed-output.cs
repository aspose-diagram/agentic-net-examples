using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Path for the modified Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count > 0)
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Increase the horizontal print scaling factor to 1.5 (150%)
                // PrintProps.ScaleX is a DoubleValue; assign via .Value
                page.PageSheet.PrintProps.ScaleX.Value = 1.5;

                // Optionally, you can also adjust vertical scaling if needed:
                // page.PageSheet.PrintProps.ScaleY.Value = 1.5;
            }
            else
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
