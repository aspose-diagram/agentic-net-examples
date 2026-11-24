using System;
using System.IO;
using Aspose.Diagram;

class DisplayOptionsConfig
{
    // Encapsulated visibility options using Aspose BOOL enum
    public BOOL ShowGrid { get; set; }
    public BOOL ShowGuides { get; set; }
    public BOOL ShowRulers { get; set; }
    public BOOL ShowPageBreaks { get; set; }

    // Applies the configuration to the first window of the diagram
    public void Apply(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        // Ensure there is at least one window; Aspose.Diagram always creates a default window
        if (diagram.Windows.Count == 0)
        {
            Console.Error.WriteLine("No windows found in the diagram.");
            return;
        }

        // Retrieve the first window and set the visibility flags
        Window window = diagram.Windows[0];
        window.ShowGrid = ShowGrid;
        window.ShowGuides = ShowGuides;
        window.ShowRulers = ShowRulers;
        window.ShowPageBreaks = ShowPageBreaks;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a configuration instance with desired settings
            var displayConfig = new DisplayOptionsConfig
            {
                ShowGrid = BOOL.True,
                ShowGuides = BOOL.False,
                ShowRulers = BOOL.True,
                ShowPageBreaks = BOOL.False
            };

            // Apply the configuration to the diagram
            displayConfig.Apply(diagram);

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}