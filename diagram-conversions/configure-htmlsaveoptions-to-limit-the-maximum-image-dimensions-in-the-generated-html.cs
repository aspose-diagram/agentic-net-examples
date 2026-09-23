using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options (max image dimensions are not directly supported in this version)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // Example: you could adjust other available options here if needed
            // htmlOptions.ExportHiddenPage = false; // sample option

            // Save the diagram as HTML using the configured options
            diagram.Save("output.html", htmlOptions);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}