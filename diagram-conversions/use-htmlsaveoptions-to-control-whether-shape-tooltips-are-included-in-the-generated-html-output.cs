using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output HTML file, includeTooltips (true/false)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath> <includeTooltips:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string includeTooltipsArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse includeTooltips argument
        bool includeTooltips = false;
        if (!bool.TryParse(includeTooltipsArg, out includeTooltips))
        {
            Console.Error.WriteLine($"Invalid value for includeTooltips: {includeTooltipsArg}. Expected true or false.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // NOTE: The Aspose.Diagram version used here does not expose an ExportTooltips property.
                // Therefore, the includeTooltips flag cannot be directly applied. We log the intention.
                Console.WriteLine($"[Info] includeTooltips flag is {(includeTooltips ? "enabled" : "disabled")}, but the current API does not support toggling tooltips via HTMLSaveOptions.");

                // As an example of a valid BOOL property, we disable exporting hidden pages.
                htmlOptions.ExportHiddenPage = false; // ExportHiddenPage expects a native bool

                // Save the diagram as HTML with the specified options
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram saved to HTML at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
