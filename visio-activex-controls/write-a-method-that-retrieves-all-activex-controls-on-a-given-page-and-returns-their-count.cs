using System.IO;
using Aspose.Diagram;
using System;

public static class ActiveXHelper
{
    // Returns the number of ActiveX controls present on the specified page.
    public static int GetActiveXControlCount(Page page)
    {
        if (page == null) throw new ArgumentNullException(nameof(page));

        int count = 0;
        // Iterate through all shapes on the page.
        foreach (Shape shape in page.Shapes)
        {
            // Shape.ActiveXControl is non‑null only for ActiveX controls.
            if (shape.ActiveXControl != null)
                count++;
        }

        return count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
