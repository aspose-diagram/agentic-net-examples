using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Simple DTO to hold window visibility settings
    class WindowSettings
    {
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }

        // Equality based on all properties
        public override bool Equals(object obj)
        {
            if (obj is not WindowSettings other) return false;
            return ShowGrid == other.ShowGrid &&
                   ShowGuides == other.ShowGuides &&
                   ShowRulers == other.ShowRulers &&
                   ShowPageBreaks == other.ShowPageBreaks &&
                   DynamicGridEnabled == other.DynamicGridEnabled &&
                   ShowConnectionPoints == other.ShowConnectionPoints;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ShowGrid, ShowGuides, ShowRulers, ShowPageBreaks, DynamicGridEnabled, ShowConnectionPoints);
        }
    }

    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Cache to store window settings per diagram file
        Dictionary<string, WindowSettings> windowCache = new Dictionary<string, WindowSettings>();

        // Retrieve all Visio files (VSDX) in the input folder
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

        foreach (string diagramPath in diagramFiles)
        {
            // Guard each file existence (redundant but per rule)
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Ensure there is at least one window; if not, create a default one
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowState = WindowStateValue.Maximized;
                    defaultWindow.WindowHeight = 800;
                    defaultWindow.WindowWidth = 1000;
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(defaultWindow);
                }

                // Work with the first window (index 0)
                Window window = diagram.Windows[0];

                // If settings are not cached, read and store them
                if (!windowCache.ContainsKey(diagramPath))
                {
                    WindowSettings settings = new WindowSettings
                    {
                        ShowGrid = window.ShowGrid,
                        ShowGuides = window.ShowGuides,
                        ShowRulers = window.ShowRulers,
                        ShowPageBreaks = window.ShowPageBreaks,
                        DynamicGridEnabled = window.DynamicGridEnabled,
                        ShowConnectionPoints = window.ShowConnectionPoints
                    };
                    windowCache[diagramPath] = settings;
                }

                // Example modification: enable all visibility options
                window.ShowGrid = BOOL.True;
                window.ShowGuides = BOOL.True;
                window.ShowRulers = BOOL.True;
                window.ShowPageBreaks = BOOL.True;
                window.DynamicGridEnabled = BOOL.True;
                window.ShowConnectionPoints = BOOL.True;

                // Prepare output path
                string outputPath = Path.Combine(outputFolder,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_out.vsdx");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any Aspose or IO errors
                Console.Error.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        }
    }
}