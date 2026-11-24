using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Read the auto‑expand setting.
                var resizeType = page.PageSheet.PageProps.DrawingResizeType.Value;

                // Determine if auto‑expand is enabled (Automatically).
                bool isAutoExpandEnabled = resizeType == DrawingResizeTypeValue.Automatically;

                Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) auto‑expand enabled: {isAutoExpandEnabled}");

                // Apply custom page size only when auto‑expand is enabled.
                if (isAutoExpandEnabled)
                {
                    // Example custom dimensions (in inches).
                    double customWidth = 10.0;   // Width in inches
                    double customHeight = 8.0;   // Height in inches

                    // Set the new page dimensions.
                    page.PageSheet.PageProps.PageWidth.Value = customWidth;
                    page.PageSheet.PageProps.PageHeight.Value = customHeight;

                    Console.WriteLine($"  -> Custom size applied: {customWidth}\" x {customHeight}\"");
                }
                else
                {
                    Console.WriteLine("  -> Skipping size adjustment because auto‑expand is disabled.");
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}