using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

interface IDiagramService
{
    void ApplyAutoSpace(Diagram diagram);
}

class DiagramService : IDiagramService
{
    private readonly AutoSpaceOptions _options;

    public DiagramService(AutoSpaceOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public void ApplyAutoSpace(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        // Use the first page for auto-spacing
        Page page = diagram.Pages[0];
        page.AutoSpaceShapes(page.Shapes, _options);
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Configure AutoSpaceOptions (injected via DI)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2.0;
            options.DistanceInVertical = 2.0;

            // Create the service with injected options
            IDiagramService service = new DiagramService(options);

            // Load the diagram (replace with actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply auto-spacing using the service
            service.ApplyAutoSpace(diagram);

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