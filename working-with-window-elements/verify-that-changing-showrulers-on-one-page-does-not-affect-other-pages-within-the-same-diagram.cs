using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add first page and assign a unique ID
            Page page1 = new Page();
            page1.ID = 1;
            diagram.Pages.Add(page1);

            // Add second page and assign a unique ID
            Page page2 = new Page();
            page2.ID = 2;
            diagram.Pages.Add(page2);

            // Create a window for the first page and disable rulers
            Window window1 = new Window();
            window1.Page = page1;               // associate with first page
            window1.ShowRulers = BOOL.False;    // turn off rulers for this page
            diagram.Windows.Add(window1);

            // Create a window for the second page (rulers remain at default)
            Window window2 = new Window();
            window2.Page = page2;               // associate with second page
            // Do NOT modify ShowRulers; it stays at its default value
            diagram.Windows.Add(window2);

            // Verify that changing ShowRulers on the first page does NOT affect the second page
            if (window1.ShowRulers == window2.ShowRulers)
            {
                throw new Exception("ShowRulers setting on one page affected another page.");
            }
            else
            {
                Console.WriteLine("ShowRulers is page‑specific: first page = {0}, second page = {1}",
                    window1.ShowRulers, window2.ShowRulers);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}