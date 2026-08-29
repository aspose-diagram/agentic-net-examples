using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            string inputFile = "input.vsdx";
            string outputFile = "output.svg";

            // Load the Visio diagram (uses the Diagram constructor as the load rule)
            Diagram diagram = null;
            try
            {
                diagram = new Diagram(inputFile);
            }
            catch (DiagramException loadEx)
            {
                // Log loading errors and exit
                Console.Error.WriteLine($"Error loading diagram: {loadEx.Message}");
                Console.Error.WriteLine(loadEx.StackTrace);
                return;
            }

            // Configure SVG save options (uses the SVGSaveOptions class)
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Example: set the page index if needed
            // svgOptions.PageIndex = 0;

            // Attempt to save the diagram as SVG and handle conversion errors
            try
            {
                diagram.Save(outputFile, svgOptions);
                Console.WriteLine("Diagram successfully converted to SVG.");
            }
            catch (DiagramException svgEx)
            {
                // Log conversion errors
                Console.Error.WriteLine($"Error during SVG conversion: {svgEx.Message}");
                Console.Error.WriteLine(svgEx.StackTrace);
            }
            finally
            {
                // Ensure resources are released
                diagram?.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
