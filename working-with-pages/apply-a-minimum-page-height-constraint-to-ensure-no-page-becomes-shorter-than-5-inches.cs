using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths can be passed as command‑line arguments.
                // If not provided, default placeholders are used.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Ensure every page has a height of at least 5 inches.
                foreach (Page page in diagram.Pages)
                {
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value;
                    if (currentHeight < 5.0)
                    {
                        page.PageSheet.PageProps.PageHeight.Value = 5.0;
                    }
                }

                // Save the modified diagram back to Visio format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }