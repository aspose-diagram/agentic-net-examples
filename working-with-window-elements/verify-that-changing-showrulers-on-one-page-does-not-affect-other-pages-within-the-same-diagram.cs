using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add first page and set its name
            Page page1 = new Page();
            page1.Name = "Page1";
            diagram.Pages.Add(page1);

            // Add second page and set its name
            Page page2 = new Page();
            page2.Name = "Page2";
            diagram.Pages.Add(page2);

            // Create a window for the first page and enable rulers
            Window win1 = new Window();
            win1.WindowType = WindowTypeValue.Drawing; // associate with a drawing window
            win1.Page = page1;                         // link to first page (assign Page object)
            win1.ShowRulers = BOOL.True;               // show rulers on page 1
            diagram.Windows.Add(win1);

            // Create a window for the second page and disable rulers
            Window win2 = new Window();
            win2.WindowType = WindowTypeValue.Drawing;
            win2.Page = page2;                         // link to second page (assign Page object)
            win2.ShowRulers = BOOL.False;              // hide rulers on page 2
            diagram.Windows.Add(win2);

            // Verify initial settings
            if (win1.ShowRulers != BOOL.True)
                throw new Exception("Initial ShowRulers for Page1 is not TRUE.");
            if (win2.ShowRulers != BOOL.False)
                throw new Exception("Initial ShowRulers for Page2 is not FALSE.");

            // Change ShowRulers on the first page
            win1.ShowRulers = BOOL.False;

            // Verify that the second page's setting remains unchanged
            if (win2.ShowRulers != BOOL.False)
                throw new Exception("ShowRulers for Page2 changed unexpectedly when modifying Page1.");

            // Save the diagram (optional, just to have an output file)
            string outputPath = "ShowRulersTest.vsdx";
            // No need to check existence for output file; just ensure the directory is writable
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}