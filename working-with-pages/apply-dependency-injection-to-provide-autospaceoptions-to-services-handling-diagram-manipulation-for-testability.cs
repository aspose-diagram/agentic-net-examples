using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramAutoSpaceApp
{
    // Interface defining diagram manipulation operations
    public interface IDiagramService
    {
        void AutoSpace(string inputPath, string outputPath);
    }

    // Service that applies auto-spacing to shapes using injected AutoSpaceOptions
    public class DiagramAutoSpaceService : IDiagramService
    {
        private readonly AutoSpaceOptions _options;

        // AutoSpaceOptions are provided via constructor injection for testability
        public DiagramAutoSpaceService(AutoSpaceOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void AutoSpace(string inputPath, string outputPath)
        {
            // Guard: ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the diagram inside a using block (ensures proper disposal)
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure the diagram has at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        Console.Error.WriteLine("The diagram contains no pages.");
                        return;
                    }

                    // Work with the first page (can be extended to handle multiple pages)
                    Page page = diagram.Pages[0];

                    // Apply auto-spacing using the injected options
                    page.AutoSpaceShapes(page.Shapes, _options);

                    // Save the modified diagram to the specified output path
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: inputPath outputPath horizontalSpacing verticalSpacing
            if (args.Length < 4)
            {
                Console.Error.WriteLine("Usage: DiagramAutoSpaceApp <inputPath> <outputPath> <horizontalSpacing> <verticalSpacing>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Parse spacing values; report errors if parsing fails
            if (!double.TryParse(args[2], out double horizontalSpacing))
            {
                Console.Error.WriteLine("Invalid horizontal spacing value.");
                return;
            }

            if (!double.TryParse(args[3], out double verticalSpacing))
            {
                Console.Error.WriteLine("Invalid vertical spacing value.");
                return;
            }

            // Configure AutoSpaceOptions (injected into the service)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = horizontalSpacing,
                DistanceInVertical = verticalSpacing
            };

            // Instantiate the service with DI and execute the auto-spacing operation
            IDiagramService diagramService = new DiagramAutoSpaceService(options);
            diagramService.AutoSpace(inputPath, outputPath);
        }
    }
}