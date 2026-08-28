using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

namespace DiagramAutoSpaceDemo
{
    // Service interface for auto‑spacing a page
    public interface IAutoSpaceService
    {
        void AutoSpace(Page page);
    }

    // Concrete implementation that receives AutoSpaceOptions via DI
    public class AutoSpaceService : IAutoSpaceService
    {
        private readonly AutoSpaceOptions _options;

        public AutoSpaceService(AutoSpaceOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void AutoSpace(Page page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            // Apply auto‑spacing using the injected options
            page.AutoSpaceShapes(page.Shapes, _options);
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram (uses the Diagram(string) constructor)
                Diagram diagram = new Diagram(inputPath);

                // Configure AutoSpaceOptions (horizontal and vertical distances in inches)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2.0,
                    DistanceInVertical = 2.0
                };

                // Dependency injection: create the service with the configured options
                IAutoSpaceService autoSpaceService = new AutoSpaceService(autoSpaceOptions);

                // Apply auto‑spacing to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    autoSpaceService.AutoSpace(page);
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}