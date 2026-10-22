using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramAutoSpaceDemo
{
    // Interface defining auto-space functionality
    public interface IAutoSpaceService
    {
        void AutoSpace(Diagram diagram);
    }

    // Implementation that receives AutoSpaceOptions via constructor injection
    public class AutoSpaceService : IAutoSpaceService
    {
        private readonly AutoSpaceOptions _options;

        // Constructor injection of AutoSpaceOptions
        public AutoSpaceService(AutoSpaceOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        // Auto-space all shapes on every page of the diagram
        public void AutoSpace(Diagram diagram)
        {
            if (diagram == null) throw new ArgumentNullException(nameof(diagram));

            // Iterate over each page using explicit type (no var)
            foreach (Page page in diagram.Pages)
            {
                // Auto-space the entire shape collection on the page using the injected options
                page.AutoSpaceShapes(page.Shapes, _options);
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input diagram path and output diagram path
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Usage: DiagramAutoSpaceDemo <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Guard: ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Configure AutoSpaceOptions (horizontal and vertical distances in points)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                // Example distances; adjust as needed
                autoSpaceOptions.DistanceInHorizontal = 2; // horizontal spacing
                autoSpaceOptions.DistanceInVertical = 2;   // vertical spacing

                // Instantiate the service with DI (options injected via constructor)
                IAutoSpaceService autoSpaceService = new AutoSpaceService(autoSpaceOptions);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Perform auto-spacing using the service
                autoSpaceService.AutoSpace(diagram);

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}