using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputHtmlPath> <includeComments:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string includeCommentsArg = args[2];

        // Guard: check input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the includeComments flag.
        bool includeComments;
        if (!bool.TryParse(includeCommentsArg, out includeComments))
        {
            Console.Error.WriteLine($"Invalid value for includeComments: {includeCommentsArg}. Expected true or false.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Enable or disable comment export based on the flag.
            // The IsExportComments property expects a bool.
            htmlOptions.IsExportComments = includeComments;

            // Save the diagram to HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}