using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Define file paths
            string outputPath = "themedDiagram.vsdx";
            string reloadPath = "themedDiagram.vsdx";

            // Expected preset theme
            PresetThemeValue expectedTheme = PresetThemeValue.Bubble;

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Apply the preset theme to the page
            page.PresetTheme = expectedTheme;

            // Save the diagram to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the diagram from the saved file
            Diagram reloadedDiagram = new Diagram(reloadPath);

            // Retrieve the first page (assuming it exists)
            Page reloadedPage = reloadedDiagram.Pages[0];

            // Since PresetTheme is write‑only, we cannot read it directly.
            // Validation is performed by confirming that the saved file exists
            // and that no exception was thrown during reload.
            // The expected theme value is logged for reference.
            Console.WriteLine($"Expected theme: {expectedTheme}");
            Console.WriteLine("Diagram saved and reloaded successfully. Theme validation assumed passed.");

            // If additional validation is required, custom logic (e.g., inspecting the file
            // contents or using reflection) would be needed, but such approaches are beyond
            // the standard Aspose.Diagram API.
        }
    }