using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

namespace DiagramProcessing
{
    // Service interface for diagram operations
    public interface IDiagramService
    {
        void AutoSpace(string inputPath, string outputPath);
    }

    // Implementation that uses injected AutoSpaceOptions
    public class DiagramService : IDiagramService
    {
        private readonly AutoSpaceOptions _options;

        public DiagramService(AutoSpaceOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void AutoSpace(string inputPath, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input path must be provided.", nameof(inputPath));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];
                // Apply auto-spacing using the injected options
                page.AutoSpaceShapes(page.Shapes, _options);
            }
            else
            {
                throw new InvalidOperationException("The diagram contains no pages.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }

    // Simple composition root
    public class Program
    {
        public static void Main()
        {
            try
            {

                // Configure AutoSpaceOptions (could be loaded from config or tests)
                var autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2,
                    DistanceInVertical = 2
                };

                // Inject options into the service
                IDiagramService diagramService = new DiagramService(autoSpaceOptions);

                // Example file paths (replace with actual paths as needed)
                string inputFile = "input.vsdx";
                string outputFile = "output.vsdx";

                // Execute the operation
                diagramService.AutoSpace(inputFile, outputFile);

                Console.WriteLine("Auto-spacing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}