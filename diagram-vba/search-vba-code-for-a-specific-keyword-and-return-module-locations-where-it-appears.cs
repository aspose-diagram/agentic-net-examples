using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <Keyword>");
            return;
        }

        string diagramPath = args[0];
        string keyword = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains a VBA project.
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            bool foundAny = false;

            // Iterate through each VBA module in the project.
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                // Retrieve the VBA source code of the module.
                string code = module.Codes ?? string.Empty;

                // Search for the keyword (case-insensitive) within the module code.
                if (code.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    foundAny = true;
                    Console.WriteLine($"Keyword \"{keyword}\" found in module: {module.Name}");

                    // Split the code into lines to report line numbers.
                    string[] lines = code.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Line numbers are 1-based for user friendliness.
                            Console.WriteLine($"  Line {i + 1}: {lines[i].Trim()}");
                        }
                    }
                }
            }

            if (!foundAny)
            {
                Console.WriteLine($"Keyword \"{keyword}\" was not found in any VBA module.");
            }
        }
        catch (Exception ex)
        {
            // Capture and report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}