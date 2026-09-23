using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file that contains ActiveX controls
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape embeds an ActiveX control, ActiveXControl will not be null
                    if (shape.ActiveXControl != null)
                    {
                        // Output relevant information about the shape and its ActiveX control
                        Console.WriteLine(
                            $"Page: {page.NameU}, " +               // Page name (string)
                            $"Shape ID: {shape.ID}, " +            // Unique shape identifier
                            $"Shape Name: {shape.NameU}, " +       // Shape name (string)
                            $"Control Type: {shape.ActiveXControl.Type}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}