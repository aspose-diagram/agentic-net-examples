using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Simple spinner animation that runs on a separate thread.
    private static void ShowSpinner(string message, Func<bool> stopCondition)
    {
        // Characters to rotate for the spinner.
        char[] sequence = new[] { '|', '/', '-', '\\' };
        int idx = 0;

        // Continue looping until the stop condition returns true.
        while (!stopCondition())
        {
            // Write the spinner character with the provided message.
            Console.Write($"\r{message} {sequence[idx++ % sequence.Length]}");
            Thread.Sleep(100);
        }

        // Clear the spinner line after completion.
        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
    }

    static void Main(string[] args)
    {
        // Prompt for the Visio file path if not supplied via arguments.
        string inputPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine()?.Trim() ?? "";
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prompt for the output directory if not supplied via arguments.
        string outputDir = args.Length > 1 ? args[1] : "";
        if (string.IsNullOrWhiteSpace(outputDir))
        {
            Console.Write("Enter the output directory for exported images: ");
            outputDir = Console.ReadLine()?.Trim() ?? "";
        }

        // Guard: ensure the output directory exists (create if missing).
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a using block to ensure disposal.
        try
        {
            using Diagram diagram = new Diagram(inputPath);
            int pageCount = diagram.Pages.Count;

            Console.WriteLine($"Diagram loaded. Total pages: {pageCount}");

            // Iterate over each page and export it as a PNG image.
            for (int i = 0; i < pageCount; i++)
            {
                // Prepare the output file name for the current page.
                string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");

                // Flag to signal the spinner thread when export finishes.
                bool exportDone = false;

                // Start the spinner animation on a background thread.
                Thread spinnerThread = new Thread(() =>
                    ShowSpinner($"Exporting page {i + 1}/{pageCount}", () => exportDone));
                spinnerThread.Start();

                try
                {
                    // Configure image save options for a single page export.
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page.
                        PageIndex = i,
                        PageCount = 1,
                        // Do not export hidden pages (optional).
                        ExportHiddenPage = false
                    };

                    // Perform the actual save operation.
                    diagram.Save(outputPath, options);
                }
                catch (Exception ex)
                {
                    // Write any export errors to the error stream.
                    Console.Error.WriteLine($"Error exporting page {i + 1}: {ex.Message}");
                }
                finally
                {
                    // Signal the spinner to stop and wait for the thread to finish.
                    exportDone = true;
                    spinnerThread.Join();
                }

                // Inform the user that the page has been exported.
                Console.WriteLine($"Page {i + 1} exported to: {outputPath}");
            }

            Console.WriteLine("All pages have been processed.");
        }
        catch (Exception ex)
        {
            // Catch any errors that occur while loading the diagram.
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
        }
    }
}