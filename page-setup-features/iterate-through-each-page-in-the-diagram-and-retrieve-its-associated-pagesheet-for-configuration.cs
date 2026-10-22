using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that an input file path was provided.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input_diagram_path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Retrieve the PageSheet associated with the current page.
                Aspose.Diagram.PageSheet pageSheet = page.PageSheet;

                // Example: output page information and size.
                Console.WriteLine($"Page Name: {page.Name}");
                double width = pageSheet.PageProps.PageWidth.Value;
                double height = pageSheet.PageProps.PageHeight.Value;
                Console.WriteLine($"Page Size (inches): Width = {width}, Height = {height}");

                // Additional configuration of the PageSheet can be performed here.
                // For instance, you could modify pageSheet properties as needed.
            }

            // Optionally, save the diagram after any modifications.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Release resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}