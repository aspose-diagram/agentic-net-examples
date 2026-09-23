using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path
        string inputPath = "input.vsdx";
        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Set the TheData event cell to call a custom script named "RecalcScript"
            // The formula uses the CALLTHIS function to invoke the script.
            shape.Event.TheData.Ufe.F = "CALLTHIS(\"RecalcScript\")";

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}