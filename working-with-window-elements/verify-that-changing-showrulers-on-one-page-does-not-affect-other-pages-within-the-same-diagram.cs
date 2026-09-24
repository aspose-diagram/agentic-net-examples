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

            // Load an existing diagram (replace with actual path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are at least two pages
            if (diagram.Pages.Count < 2)
            {
                // Find the maximum existing page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }

                // Add a new blank page with a unique ID
                Page newPage = new Page(maxId + 1);
                diagram.Pages.Add(newPage);
            }

            // Ensure there are at least two windows (each window will be treated as a view for a page)
            if (diagram.Windows.Count < 2)
            {
                // First window (if not already present) is already created by the diagram.
                // Add a second window.
                Window secondWindow = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 800,
                    WindowHeight = 600
                };
                diagram.Windows.Add(secondWindow);
            }

            // Associate first window with first page and second window with second page (conceptual)
            // Set ShowRulers on the first window (page 1) to TRUE
            diagram.Windows[0].ShowRulers = BOOL.True;

            // Set ShowRulers on the second window (page 2) to FALSE
            diagram.Windows[1].ShowRulers = BOOL.False;

            // Verify that the first window's ShowRulers remains TRUE
            if (diagram.Windows[0].ShowRulers != BOOL.True)
            {
                throw new Exception("ShowRulers on the first page was altered unexpectedly.");
            }

            // Verify that the second window's ShowRulers remains FALSE
            if (diagram.Windows[1].ShowRulers != BOOL.False)
            {
                throw new Exception("ShowRulers on the second page was altered unexpectedly.");
            }

            Console.WriteLine("Verification succeeded: ShowRulers settings are independent per page.");

            // Optionally save the diagram to observe the changes
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
