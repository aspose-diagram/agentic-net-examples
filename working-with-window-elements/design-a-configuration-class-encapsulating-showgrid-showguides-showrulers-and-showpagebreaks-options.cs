using System;
using Aspose.Diagram;

namespace DiagramUtilities
{
    /// <summary>
    /// Encapsulates display options for a Visio diagram: grid, guides, rulers and page breaks.
    /// </summary>
    public class DiagramDisplayConfig
    {
        // Public properties use native bool for easy consumption.
        public bool ShowGrid { get; set; } = true;
        public bool ShowGuides { get; set; } = true;
        public bool ShowRulers { get; set; } = true;
        public bool ShowPageBreaks { get; set; } = true;

        /// <summary>
        /// Applies the configured display settings to all windows of the supplied diagram.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
        public void Apply(Diagram diagram)
        {
            if (diagram == null)
                throw new ArgumentNullException(nameof(diagram));

            // Ensure the diagram contains at least one window; otherwise there is nothing to configure.
            if (diagram.Windows == null || diagram.Windows.Count == 0)
                throw new InvalidOperationException("The diagram does not contain any windows to configure.");

            // Iterate over each window and set the visibility flags.
            foreach (Window window in diagram.Windows)
            {
                window.ShowGrid = ShowGrid ? BOOL.True : BOOL.False;
                window.ShowGuides = ShowGuides ? BOOL.True : BOOL.False;
                window.ShowRulers = ShowRulers ? BOOL.True : BOOL.False;
                window.ShowPageBreaks = ShowPageBreaks ? BOOL.True : BOOL.False;
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
                string inputPath = "example.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Create a configuration instance and customize as needed.
                DiagramDisplayConfig config = new DiagramDisplayConfig
                {
                    ShowGrid = true,
                    ShowGuides = false,
                    ShowRulers = true,
                    ShowPageBreaks = false
                };

                // Apply the settings to the diagram.
                config.Apply(diagram);

                // Save the modified diagram.
                string outputPath = "example_modified.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}