using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public class ActiveXHelper
{
    /// <summary>
    /// Returns the number of ActiveX controls present on the specified page.
    /// </summary>
    /// <param name="page">The page to inspect.</param>
    /// <returns>Count of shapes that contain an ActiveX control.</returns>
    public int GetActiveXControlCount(Page page)
    {
        int count = 0;

        // Iterate through all shapes on the page.
        foreach (Shape shape in page.Shapes)
        {
            // Shape.ActiveXControl is non‑null only for ActiveX controls.
            if (shape.ActiveXControl != null)
            {
                count++;
            }
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
