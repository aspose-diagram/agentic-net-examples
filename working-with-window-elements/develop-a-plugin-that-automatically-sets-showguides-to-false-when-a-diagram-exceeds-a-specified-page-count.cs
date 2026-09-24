using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPlugins
{
    // Plugin that disables guide visibility when page count exceeds a threshold.
    public static class ShowGuidesPlugin
    {
        // Applies the rule to the diagram located at inputPath and saves to outputPath.
        public static void Apply(string inputPath, string outputPath, int maxPageCount)
        {
            // Load the diagram from file.
            Diagram diagram = new Diagram(inputPath);

            // Check the total number of pages.
            int pageCount = diagram.Pages.Count;

            // If the diagram has more pages than allowed, hide guides globally.
            if (pageCount > maxPageCount)
            {
                // Ensure there is at least one window; create one if none exist.
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowType = WindowTypeValue.Drawing;
                    window.WindowState = WindowStateValue.Maximized;
                    diagram.Windows.Add(window);
                }

                // Set ShowGuides to FALSE for all windows.
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGuides = BOOL.False;
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }

    // Console entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect three arguments: input file, output file, max page count.
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramPlugins <inputPath> <outputPath> <maxPageCount>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!int.TryParse(args[2], out int maxPageCount))
            {
                Console.WriteLine("Invalid maxPageCount value.");
                return;
            }

            try
            {
                ShowGuidesPlugin.Apply(inputPath, outputPath, maxPageCount);
                Console.WriteLine("Processing completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}