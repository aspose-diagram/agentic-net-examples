using System;
using Aspose.Diagram;

namespace DiagramUtilities
{
    /// <summary>
    /// Encapsulates display options for a Visio diagram:
    /// ShowGrid, ShowGuides, ShowRulers, and ShowPageBreaks.
    /// </summary>
    public class DiagramDisplayConfig
    {
        // Aspose.Diagram uses the BOOL enum for these flags.
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }

        /// <summary>
        /// Initializes a new configuration instance.
        /// Parameters are plain bools for convenience and are converted to BOOL.
        /// </summary>
        public DiagramDisplayConfig(bool showGrid = true,
                                    bool showGuides = true,
                                    bool showRulers = true,
                                    bool showPageBreaks = true)
        {
            ShowGrid = showGrid ? BOOL.True : BOOL.False;
            ShowGuides = showGuides ? BOOL.True : BOOL.False;
            ShowRulers = showRulers ? BOOL.True : BOOL.False;
            ShowPageBreaks = showPageBreaks ? BOOL.True : BOOL.False;
        }

        /// <summary>
        /// Applies the stored display settings to all windows of the provided diagram.
        /// If the diagram has no windows, the method does nothing.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
        public void Apply(Diagram diagram)
        {
            if (diagram == null)
                throw new ArgumentNullException(nameof(diagram));

            // Ensure there is at least one window before accessing index 0.
            if (diagram.Windows == null || diagram.Windows.Count == 0)
                return;

            foreach (Window window in diagram.Windows)
            {
                window.ShowGrid = ShowGrid;
                window.ShowGuides = ShowGuides;
                window.ShowRulers = ShowRulers;
                window.ShowPageBreaks = ShowPageBreaks;
            }
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram (replace with a valid path).
                Diagram diagram = new Diagram("example.vsdx");

                // Create a configuration: hide grid and guides, keep rulers and page breaks visible.
                DiagramDisplayConfig config = new DiagramDisplayConfig(
                    showGrid: false,
                    showGuides: false,
                    showRulers: true,
                    showPageBreaks: true);

                // Apply the configuration to the diagram.
                config.Apply(diagram);

                // Save the modified diagram.
                diagram.Save("example_modified.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}