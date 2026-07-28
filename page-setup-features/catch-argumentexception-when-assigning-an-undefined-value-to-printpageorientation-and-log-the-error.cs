using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            try
            {
                // Attempt to assign an undefined orientation value.
                // This will throw an ArgumentException because the value is not a valid enum member.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = (PrintPageOrientationValue)999;
            }
            catch (ArgumentException ex)
            {
                // Log the error details to the console.
                Console.WriteLine($"Error setting PrintPageOrientation: {ex.Message}");
            }

            // Save the diagram after handling the exception.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
