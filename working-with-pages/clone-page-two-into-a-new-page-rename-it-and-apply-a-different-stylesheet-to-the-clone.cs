using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least two pages
            if (diagram.Pages.Count < 2)
            {
                Console.WriteLine("The diagram does not contain a second page to clone.");
                diagram.Dispose();
                return;
            }

            // Get the second page (index 1)
            Page sourcePage = diagram.Pages[1];

            // Create a new blank page
            Page clonedPage = new Page();

            // Copy the contents of the source page into the new page
            clonedPage.Copy(sourcePage);

            // Add the cloned page to the diagram
            diagram.Pages.Add(clonedPage);

            // Rename the cloned page
            clonedPage.Name = "ClonedPage";

            // -------------------------------------------------
            // Apply a different stylesheet to the cloned page
            // -------------------------------------------------

            // Try to find an existing stylesheet named "ClonedStyle"
            StyleSheet style = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "ClonedStyle")
                {
                    style = ss;
                    break;
                }
            }

            // If not found, create a new stylesheet with simple formatting
            if (style == null)
            {
                style = new StyleSheet();
                style.ID = diagram.StyleSheets.Count + 1;
                style.Name = "ClonedStyle";

                // Text character formatting (red text)
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0;
                ch.Color.Value = "#FF0000";
                style.Chars.Add(ch);

                // Line formatting (green line)
                style.Line.LineColor.Value = "#00FF00";

                // Fill formatting (blue fill)
                style.Fill.FillForegnd.Value = "#0000FF";

                // Add the new stylesheet to the diagram
                diagram.StyleSheets.Add(style);
            }

            // Apply the stylesheet to the cloned page (shape, line, and text styles)
            clonedPage.ApplyStyle(style.ID, style.ID, style.ID);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Page cloned and stylesheet applied successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
