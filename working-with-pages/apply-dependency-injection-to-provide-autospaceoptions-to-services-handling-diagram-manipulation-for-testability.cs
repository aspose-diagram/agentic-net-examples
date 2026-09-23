using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

namespace DiagramAutoSpaceDemo
{
    // Service contract for auto‑spacing operations
    public interface IAutoSpaceService
    {
        void AutoSpace(Page page, AutoSpaceOptions options);
    }

    // Concrete implementation that performs auto‑spacing on a page
    public class AutoSpaceService : IAutoSpaceService
    {
        public void AutoSpace(Page page, AutoSpaceOptions options)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Apply auto‑spacing to all shapes on the page using the provided options
            page.AutoSpaceShapes(page.Shapes, options);
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Paths to input and output Visio files (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the first page (or any target page)
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2.0, // horizontal gap in inches
                    DistanceInVertical = 2.0    // vertical gap in inches
                };

                // Inject the service (manual DI for testability)
                IAutoSpaceService autoSpaceService = new AutoSpaceService();

                // Perform auto‑spacing
                autoSpaceService.AutoSpace(page, autoSpaceOptions);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}