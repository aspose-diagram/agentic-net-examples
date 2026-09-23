using System;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        // Asynchronous entry point
        static async Task Main(string[] args)
        {
            try
            {

                // Example file paths; replace with actual paths as needed
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                try
                {
                    await AutoSpaceAsync(inputPath, outputPath);
                    Console.WriteLine("Auto‑spacing completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Performs auto‑spacing on the first page of a Visio diagram without blocking the UI thread
        private static async Task AutoSpaceAsync(string inputFile, string outputFile)
        {
            // Load the diagram (I/O operation)
            Diagram diagram = new Diagram(inputFile);

            // Ensure there is at least one page to process
            if (diagram.Pages.Count == 0)
                throw new InvalidOperationException("The diagram contains no pages.");

            // Get the first page (or modify to select a specific page)
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 2.0, // horizontal gap in inches
                DistanceInVertical = 2.0    // vertical gap in inches
            };

            // Run the potentially time‑consuming auto‑spacing on a background thread
            await Task.Run(() =>
            {
                // Auto‑space all shapes on the page using the defined options
                page.AutoSpaceShapes(page.Shapes, options);
            });

            // Save the modified diagram
            diagram.Save(outputFile, SaveFileFormat.Vsdx);
        }
    }