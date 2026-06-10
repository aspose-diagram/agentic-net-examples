using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output XPS file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramOrientationExport <inputVisioPath> <outputXpsPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and set orientation to Landscape for non‑background pages
                foreach (Page page in diagram.Pages)
                {
                    // Background pages have Background == BOOL.True
                    if (page.Background != BOOL.True)
                    {
                        // Set print orientation to Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                }

                // Prepare XPS save options (default settings are sufficient)
                XPSSaveOptions xpsOptions = new XPSSaveOptions();

                // Save the diagram as XPS using the save options
                diagram.Save(outputPath, xpsOptions);

                Console.WriteLine($"Diagram saved successfully to XPS: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }