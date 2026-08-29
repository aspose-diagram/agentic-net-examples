using System;
using Aspose.Diagram;

namespace DiagramConfiguration
{
    /// <summary>
    /// Encapsulates the visibility options for grid, guides, rulers, and page breaks in a Visio diagram window.
    /// </summary>
    public class DisplayOptionsConfig
    {
        // Aspose.Diagram uses the BOOL enumeration for boolean cell values.
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }

        /// <summary>
        /// Initializes a new instance with all options set to TRUE.
        /// </summary>
        public DisplayOptionsConfig()
        {
            ShowGrid = BOOL.True;
            ShowGuides = BOOL.True;
            ShowRulers = BOOL.True;
            ShowPageBreaks = BOOL.True;
        }

        /// <summary>
        /// Applies the configured visibility settings to the first window of the provided diagram.
        /// If the diagram has no windows, a new drawing window is created.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
        public void Apply(Diagram diagram)
        {
            if (diagram == null)
                throw new ArgumentNullException(nameof(diagram));

            Window targetWindow;

            // Use the first existing window if present; otherwise create a new drawing window.
            if (diagram.Windows.Count > 0)
            {
                targetWindow = diagram.Windows[0];
            }
            else
            {
                targetWindow = new Window
                {
                    // Set the window type to a drawing window so that UI‑related properties are valid.
                    WindowType = WindowTypeValue.Drawing,
                    // Provide a reasonable default size.
                    WindowWidth = 1100,
                    WindowHeight = 700,
                    // Maximize the window for better visibility.
                    WindowState = WindowStateValue.Maximized
                };
                diagram.Windows.Add(targetWindow);
            }

            // Apply the visibility settings.
            targetWindow.ShowGrid = ShowGrid;
            targetWindow.ShowGuides = ShowGuides;
            targetWindow.ShowRulers = ShowRulers;
            targetWindow.ShowPageBreaks = ShowPageBreaks;
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            // Load or create a diagram.
            Diagram diagram = new Diagram();

            // Create a configuration instance with custom settings.
            var config = new DisplayOptionsConfig
            {
                ShowGrid = BOOL.False,
                ShowGuides = BOOL.True,
                ShowRulers = BOOL.False,
                ShowPageBreaks = BOOL.True
            };

            // Apply the configuration to the diagram.
            config.Apply(diagram);

            // Save the diagram to verify the changes (optional).
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}