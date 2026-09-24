using System;
using System.IO;
using Aspose.Diagram;

/// <summary>
/// Encapsulates global visibility options for grid, guides, rulers, and page breaks.
/// </summary>
public class DisplayOptionsConfig
{
    /// <summary>
    /// Show or hide the drawing grid.
    /// </summary>
    public BOOL ShowGrid { get; set; }

    /// <summary>
    /// Show or hide the alignment guides.
    /// </summary>
    public BOOL ShowGuides { get; set; }

    /// <summary>
    /// Show or hide the rulers.
    /// </summary>
    public BOOL ShowRulers { get; set; }

    /// <summary>
    /// Show or hide page break indicators.
    /// </summary>
    public BOOL ShowPageBreaks { get; set; }

    /// <summary>
    /// Initializes a new instance with all options enabled by default.
    /// </summary>
    public DisplayOptionsConfig()
    {
        ShowGrid = BOOL.True;
        ShowGuides = BOOL.True;
        ShowRulers = BOOL.True;
        ShowPageBreaks = BOOL.True;
    }

    /// <summary>
    /// Applies the configured options to the first window of the provided diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
    public void Apply(Diagram diagram)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        // Ensure at least one window exists; if not, create a default drawing window.
        if (diagram.Windows.Count == 0)
        {
            var window = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 1100,
                WindowHeight = 700
            };
            diagram.Windows.Add(window);
        }

        // Apply settings to the first window (global view settings).
        Window firstWindow = diagram.Windows[0];
        firstWindow.ShowGrid = ShowGrid;
        firstWindow.ShowGuides = ShowGuides;
        firstWindow.ShowRulers = ShowRulers;
        firstWindow.ShowPageBreaks = ShowPageBreaks;
    }
}

/// <summary>
/// Console entry point demonstrating the use of DisplayOptionsConfig.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Expect an input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];
        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Derive an output path by inserting "_updated" before the extension.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated" + Path.GetExtension(inputPath));

        try
        {
            // Load the diagram from the provided file.
            Diagram diagram = new Diagram(inputPath);

            // Create a configuration instance (defaults to all true) and apply it.
            var config = new DisplayOptionsConfig();
            config.Apply(diagram);

            // Save the modified diagram back to a new file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with updated display options to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}