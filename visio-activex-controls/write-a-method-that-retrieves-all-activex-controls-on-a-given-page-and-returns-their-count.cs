using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public class DiagramHelper
{
    // Returns the number of ActiveX controls on the specified page index.
    public static int GetActiveXControlCount(Diagram diagram, int pageIndex)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        // Validate page index.
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Invalid page index.");

        Page page = diagram.Pages[pageIndex];
        int count = 0;

        // Iterate through all shapes on the page.
        foreach (Shape shape in page.Shapes)
        {
            // Shape.ActiveXControl is null for non‑ActiveX shapes.
            if (shape.ActiveXControl != null)
                count++;
        }

        return count;
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load a Visio diagram.
            using (Diagram diagram = new Diagram("sample.vsdx"))
            {
                // Get the count of ActiveX controls on the first page (index 0).
                int activeXCount = DiagramHelper.GetActiveXControlCount(diagram, 0);
                Console.WriteLine($"Number of ActiveX controls on page 0: {activeXCount}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}