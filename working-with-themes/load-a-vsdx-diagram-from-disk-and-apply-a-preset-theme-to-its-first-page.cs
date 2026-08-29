using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ApplyPresetTheme
{
    static void Main()
    {
        try
        {

            // Path to the diagram that will receive the theme
            string targetDiagramPath = "input.vsdx";

            // Path to a diagram (or template) that contains the desired preset theme
            string themeDiagramPath = "theme.vsdx";

            // Load the target diagram from disk
            using (Diagram targetDiagram = new Diagram(targetDiagramPath))
            {
                // Load the source diagram that holds the preset theme
                using (Diagram sourceThemeDiagram = new Diagram(themeDiagramPath))
                {
                    // Copy the theme from the source diagram to the target diagram
                    targetDiagram.CopyTheme(sourceThemeDiagram);
                }

                // The theme is now applied to the whole document.
                // If you need to ensure the first page is the active one, you can access it via index:
                // Page firstPage = targetDiagram.Pages[0];
                // (ActivePage is read‑only; operations are performed on the diagram as a whole.)

                // Save the modified diagram back to disk
                targetDiagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
