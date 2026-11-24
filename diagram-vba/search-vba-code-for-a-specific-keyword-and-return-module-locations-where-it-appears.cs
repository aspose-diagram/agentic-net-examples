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

        string visioPath = args[0];
        string keyword = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Access the VBA project; if none, inform the user.
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            bool foundAny = false;

            // Iterate through all VBA modules.
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                string code = module.Codes ?? string.Empty;

                // Search for the keyword line by line.
                string[] lines = code.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var matchingLines = new System.Collections.Generic.List<int>();

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Store 1‑based line number.
                        matchingLines.Add(i + 1);
                    }
                }

                if (matchingLines.Count > 0)
                {
                    foundAny = true;
                    Console.WriteLine($"Module: {module.Name}");
                    Console.WriteLine($"Keyword \"{keyword}\" found on line(s): {string.Join(", ", matchingLines)}");
                    Console.WriteLine();
                }
            }

            if (!foundAny)
            {
                Console.WriteLine($"Keyword \"{keyword}\" was not found in any VBA module.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}