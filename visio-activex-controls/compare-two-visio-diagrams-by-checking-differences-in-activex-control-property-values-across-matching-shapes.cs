using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing files.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        try
        {
            // Load both diagrams inside using blocks (required pattern).
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Determine the number of pages to compare (use the smaller count for safety).
                int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);

                for (int p = 0; p < pageCount; p++)
                {
                    Page page1 = diagram1.Pages[p];
                    Page page2 = diagram2.Pages[p];

                    // Iterate all shapes on the first page.
                    foreach (Shape shape1 in page1.Shapes)
                    {
                        // Retrieve the universal name of the shape (may be null).
                        string shapeName = shape1.NameU ?? string.Empty;

                        // Find a shape with the same NameU on the second page.
                        Shape shape2 = FindShapeByName(page2, shapeName);
                        if (shape2 == null)
                        {
                            Console.WriteLine($"Shape '{shapeName}' exists in diagram1 but not in diagram2 (Page {p + 1}).");
                            continue;
                        }

                        // Compare ActiveX controls on the matching shapes.
                        CompareActiveXControls(shape1, shape2, shapeName, p + 1);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }

    // Helper to locate a shape by its NameU on a given page.
    private static Shape? FindShapeByName(Page page, string name)
    {
        foreach (Shape shp in page.Shapes)
        {
            string shpName = shp.NameU ?? string.Empty;
            if (shpName.Equals(name, StringComparison.OrdinalIgnoreCase))
                return shp;
        }
        return null;
    }

    // Compare ActiveX controls of two shapes and report differences.
    private static void CompareActiveXControls(Shape s1, Shape s2, string shapeName, int pageNumber)
    {
        // Retrieve the ActiveX controls (may be null).
        ActiveXControl? ctrl1 = s1.ActiveXControl;
        ActiveXControl? ctrl2 = s2.ActiveXControl;

        // Cases where only one shape has an ActiveX control.
        if (ctrl1 == null && ctrl2 != null)
        {
            Console.WriteLine($"Shape '{shapeName}' on page {pageNumber} has no ActiveX control in diagram1 but has one in diagram2.");
            return;
        }
        if (ctrl1 != null && ctrl2 == null)
        {
            Console.WriteLine($"Shape '{shapeName}' on page {pageNumber} has an ActiveX control in diagram1 but not in diagram2.");
            return;
        }
        if (ctrl1 == null && ctrl2 == null)
        {
            // Neither shape has an ActiveX control – nothing to compare.
            return;
        }

        // Both shapes have ActiveX controls – compare their types first.
        if (ctrl1.Type != ctrl2.Type)
        {
            Console.WriteLine($"Shape '{shapeName}' on page {pageNumber} has different ActiveX control types: {ctrl1.Type} vs {ctrl2.Type}.");
            return;
        }

        // Compare properties specific to each control type.
        switch (ctrl1.Type)
        {
            case ControlType.CheckBox:
                var cb1 = (CheckBoxActiveXControl)ctrl1;
                var cb2 = (CheckBoxActiveXControl)ctrl2;
                if (cb1.IsEnabled != cb2.IsEnabled)
                {
                    Console.WriteLine($"CheckBox '{shapeName}' on page {pageNumber} IsEnabled differs: {cb1.IsEnabled} vs {cb2.IsEnabled}.");
                }
                break;

            case ControlType.TextBox:
                var tb1 = (TextBoxActiveXControl)ctrl1;
                var tb2 = (TextBoxActiveXControl)ctrl2;
                string text1 = tb1.Text ?? string.Empty;
                string text2 = tb2.Text ?? string.Empty;
                if (!text1.Equals(text2, StringComparison.Ordinal))
                {
                    Console.WriteLine($"TextBox '{shapeName}' on page {pageNumber} Text differs: \"{text1}\" vs \"{text2}\".");
                }
                break;

            case ControlType.CommandButton:
                var btn1 = (CommandButtonActiveXControl)ctrl1;
                var btn2 = (CommandButtonActiveXControl)ctrl2;
                string cap1 = btn1.Caption ?? string.Empty;
                string cap2 = btn2.Caption ?? string.Empty;
                if (!cap1.Equals(cap2, StringComparison.Ordinal))
                {
                    Console.WriteLine($"CommandButton '{shapeName}' on page {pageNumber} Caption differs: \"{cap1}\" vs \"{cap2}\".");
                }
                break;

            case ControlType.Image:
                var img1 = (ImageActiveXControl)ctrl1;
                var img2 = (ImageActiveXControl)ctrl2;
                byte[]? pic1 = img1.Picture;
                byte[]? pic2 = img2.Picture;
                int len1 = pic1?.Length ?? 0;
                int len2 = pic2?.Length ?? 0;
                if (len1 != len2)
                {
                    Console.WriteLine($"Image control '{shapeName}' on page {pageNumber} picture size differs: {len1} bytes vs {len2} bytes.");
                }
                break;

            case ControlType.SpinButton:
                var sp1 = (SpinButtonActiveXControl)ctrl1;
                var sp2 = (SpinButtonActiveXControl)ctrl2;
                if (sp1.Position != sp2.Position)
                {
                    Console.WriteLine($"SpinButton '{shapeName}' on page {pageNumber} Position differs: {sp1.Position} vs {sp2.Position}.");
                }
                break;

            // Additional control types can be added here following the same pattern.

            default:
                // For unhandled control types, compare the generic IsEnabled property.
                if (ctrl1.IsEnabled != ctrl2.IsEnabled)
                {
                    Console.WriteLine($"{ctrl1.Type} control '{shapeName}' on page {pageNumber} IsEnabled differs: {ctrl1.IsEnabled} vs {ctrl2.IsEnabled}.");
                }
                break;
        }
    }
}
