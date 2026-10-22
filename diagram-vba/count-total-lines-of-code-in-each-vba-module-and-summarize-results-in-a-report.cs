using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Check if the diagram contains a VBA project
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Prepare summary variables
            int totalLines = 0;
            Console.WriteLine("VBA Modules Line Count Report");
            Console.WriteLine("--------------------------------");

            // Iterate through each VBA module
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                // Retrieve the VBA code string; handle null safely
                string code = module.Codes ?? string.Empty;

                // Split the code into lines (handle both CRLF and LF)
                string[] lines = code.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                int lineCount = lines.Length;
                totalLines += lineCount;

                // Output module-specific line count
                Console.WriteLine($"Module: {module.Name}");
                Console.WriteLine($"Lines of Code: {lineCount}");
                Console.WriteLine();
            }

            // Output total summary
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Total VBA Modules: {diagram.VbaProject.Modules.Count}");
            Console.WriteLine($"Total Lines of Code: {totalLines}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}