using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (must be macro-enabled if it contains VBA)
                string inputPath = "input.vsdm";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram has a VBA project
                if (diagram.VbaProject == null || diagram.VbaProject.Modules == null)
                {
                    Console.WriteLine("No VBA project or modules found in the diagram.");
                    return;
                }

                int totalLines = 0;

                // Iterate through each VBA module and count lines
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    // Split the code by line breaks to count lines
                    int lineCount = module.Codes.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;
                    Console.WriteLine($"Module \"{module.Name}\": {lineCount} lines");
                    totalLines += lineCount;
                }

                // Summarize the total line count
                Console.WriteLine($"Total lines of VBA code across all modules: {totalLines}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }