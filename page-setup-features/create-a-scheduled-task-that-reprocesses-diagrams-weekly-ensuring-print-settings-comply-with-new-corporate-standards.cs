using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

public class Program
{
    // Interval of one week
    private static readonly TimeSpan WeeklyInterval = TimeSpan.FromDays(7);

    // Folder that contains the Visio diagrams to process
    private const string DiagramsFolder = "Diagrams";

    public static void Main()
    {
        // Run the processing immediately at startup
        ProcessAllDiagrams();

        // Set up a timer to run the processing every week
        Timer timer = new Timer(_ => ProcessAllDiagrams(),
                                null,
                                WeeklyInterval,   // first due time (after the initial run)
                                WeeklyInterval);  // subsequent period

        // Prevent the application from exiting
        Console.WriteLine("Diagram reprocessing service started. Press Enter to stop.");
        Console.ReadLine();

        // Clean up timer before exit
        timer.Dispose();
    }

    private static void ProcessAllDiagrams()
    {
        try
        {
            if (!Directory.Exists(DiagramsFolder))
            {
                Console.WriteLine($"Folder '{DiagramsFolder}' does not exist. Skipping processing.");
                return;
            }

            string[] diagramFiles = Directory.GetFiles(DiagramsFolder, "*.vsdx", SearchOption.AllDirectories);

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply corporate print settings to every page
                    foreach (Page page in diagram.Pages)
                    {
                        // Orientation: Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Scaling: 75%
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                        // Fit to sheet (single page)
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // Margins: 0.5 inches on each side
                        page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
                    }

                    // Save the updated diagram (overwrite original)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    diagram.Dispose();

                    Console.WriteLine($"Processed and saved: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error during batch processing: {ex.Message}");
        }
    }
}
