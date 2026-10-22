using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0). Adjust as needed for other pages.
                Page page = diagram.Pages[0];

                // Read the auto‑expand (auto‑resize) setting.
                bool autoExpandEnabled = page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically;

                Console.WriteLine($"Auto‑expand is {(autoExpandEnabled ? "enabled" : "disabled")} on page \"{page.Name}\".");

                // If auto‑expand is disabled, apply custom page size adjustments.
                if (!autoExpandEnabled)
                {
                    // Define custom dimensions (in inches). Adjust these values as required.
                    double customWidth = 11.0;   // e.g., 11 inches
                    double customHeight = 8.5;   // e.g., 8.5 inches (landscape)

                    // Apply the new size to the page.
                    page.PageSheet.PageProps.PageWidth.Value = customWidth;
                    page.PageSheet.PageProps.PageHeight.Value = customHeight;

                    Console.WriteLine($"Custom page size applied: Width = {customWidth}\" , Height = {customHeight}\".");
                }
                else
                {
                    Console.WriteLine("Skipping custom size adjustment because auto‑expand is enabled.");
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}