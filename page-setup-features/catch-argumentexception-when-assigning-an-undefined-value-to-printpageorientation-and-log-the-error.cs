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

            // Load an existing Visio diagram (adjust the path as needed)
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            try
            {
                // Attempt to assign an undefined enum value to trigger ArgumentException
                page.PageSheet.PrintProps.PrintPageOrientation.Value = (PrintPageOrientationValue)999;
            }
            catch (ArgumentException ex)
            {
                // Log the caught exception
                Console.WriteLine($"ArgumentException caught: {ex.Message}");
            }

            // Save the diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
