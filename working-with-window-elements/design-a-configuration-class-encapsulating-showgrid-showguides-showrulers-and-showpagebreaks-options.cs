using System;
using System.IO;
using Aspose.Diagram;

class DisplayConfig
{
    // Configuration properties using Aspose BOOL enum
    public BOOL ShowGrid { get; set; }
    public BOOL ShowGuides { get; set; }
    public BOOL ShowRulers { get; set; }
    public BOOL ShowPageBreaks { get; set; }

    // Apply the configuration to the first window of a diagram
    public void Apply(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        // Ensure the diagram contains at least one window
        if (diagram.Windows == null || diagram.Windows.Count == 0)
        {
            Console.Error.WriteLine("The diagram does not contain any windows to configure.");
            return;
        }

        // Configure visibility settings on the first window
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
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a configuration instance with desired settings
            DisplayConfig config = new DisplayConfig
            {
                ShowGrid = BOOL.True,
                ShowGuides = BOOL.True,
                ShowRulers = BOOL.True,
                ShowPageBreaks = BOOL.True
            };

            // Apply the configuration to the diagram
            config.Apply(diagram);

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}