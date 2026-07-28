using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram instance
            Diagram diagram = new Diagram();

            // Add first page to the diagram
            Page page1 = new Page();
            diagram.Pages.Add(page1);

            // Add second page to the diagram
            Page page2 = new Page();
            diagram.Pages.Add(page2);

            // Create a window for the first page and enable rulers
            Window window1 = new Window();
            window1.WindowType = WindowTypeValue.Drawing;
            window1.Page = page1;               // associate window with page1 (expects Page, not ID)
            window1.ShowRulers = BOOL.True;     // enable rulers on this window
            diagram.Windows.Add(window1);

            // Create a window for the second page without changing rulers (default is FALSE)
            Window window2 = new Window();
            window2.WindowType = WindowTypeValue.Drawing;
            window2.Page = page2;               // associate window with page2
            // Do NOT modify ShowRulers; it should remain FALSE
            diagram.Windows.Add(window2);

            // Verify that ShowRulers on the first window is TRUE
            if (window1.ShowRulers != BOOL.True)
                throw new Exception("ShowRulers was not set to TRUE on the first page's window.");

            // Verify that ShowRulers on the second window is still FALSE
            if (window2.ShowRulers == BOOL.True)
                throw new Exception("ShowRulers on the second page's window was unexpectedly set to TRUE.");

            Console.WriteLine("Verification succeeded: ShowRulers on one page does not affect other pages.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}