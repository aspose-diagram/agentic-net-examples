using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagram1.vsdx> <diagram2.vsdx>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing input files
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
            // Load both diagrams inside using blocks (required pattern)
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Compare pages up to the smaller page count
                int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    Page page1 = diagram1.Pages[pageIndex];
                    Page page2 = diagram2.Pages[pageIndex];

                    // Iterate through shapes on the first page
                    for (int i = 0; i < page1.Shapes.Count; i++)
                    {
                        Shape shape1 = page1.Shapes[i];
                        string shapeName = !string.IsNullOrEmpty(shape1.NameU) ? shape1.NameU : $"Shape_{i}";

                        // Find matching shape on the second page by NameU
                        Shape shape2 = FindShapeByName(page2, shapeName);
                        if (shape2 == null)
                        {
                            Console.WriteLine($"[Page {pageIndex + 1}] Shape '{shapeName}' not found in second diagram.");
                            continue;
                        }

                        // Both shapes must contain an ActiveX control to compare
                        if (shape1.ActiveXControl == null && shape2.ActiveXControl == null)
                            continue; // No controls on either shape

                        if (shape1.ActiveXControl == null || shape2.ActiveXControl == null)
                        {
                            Console.WriteLine($"[Page {pageIndex + 1}] Shape '{shapeName}' ActiveX control presence differs between diagrams.");
                            continue;
                        }

                        // Compare the ActiveX controls
                        CompareActiveXControls(shape1, shape2, pageIndex + 1, shapeName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }

    // Locate a shape on a page by its universal name (NameU)
    private static Shape FindShapeByName(Page page, string nameU)
    {
        for (int i = 0; i < page.Shapes.Count; i++)
        {
            Shape shp = page.Shapes[i];
            if (shp.NameU == nameU)
                return shp;
        }
        return null;
    }

    // Compare two ActiveX controls and report any differences
    private static void CompareActiveXControls(Shape shape1, Shape shape2, int pageNumber, string shapeName)
    {
        ActiveXControl ctrl1 = shape1.ActiveXControl;
        ActiveXControl ctrl2 = shape2.ActiveXControl;

        // Compare control type first
        if (ctrl1.Type != ctrl2.Type)
        {
            Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' control type differs: {ctrl1.Type} vs {ctrl2.Type}");
            return; // Different types – further property comparison is not meaningful
        }

        // Generic property comparisons (Width, Height) – these are plain double values
        if (ctrl1.Width != ctrl2.Width)
        {
            Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' Width differs: {ctrl1.Width} vs {ctrl2.Width}");
        }
        if (ctrl1.Height != ctrl2.Height)
        {
            Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' Height differs: {ctrl1.Height} vs {ctrl2.Height}");
        }

        // Type‑specific comparisons based on the control type
        switch (ctrl1.Type)
        {
            case ControlType.CommandButton:
                var btn1 = ctrl1 as CommandButtonActiveXControl;
                var btn2 = ctrl2 as CommandButtonActiveXControl;
                if (btn1 != null && btn2 != null && btn1.Caption != btn2.Caption)
                {
                    Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' CommandButton Caption differs: '{btn1.Caption}' vs '{btn2.Caption}'");
                }
                break;

            case ControlType.TextBox:
                var txt1 = ctrl1 as TextBoxActiveXControl;
                var txt2 = ctrl2 as TextBoxActiveXControl;
                if (txt1 != null && txt2 != null && txt1.Text != txt2.Text)
                {
                    Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' TextBox Text differs: '{txt1.Text}' vs '{txt2.Text}'");
                }
                break;

            case ControlType.SpinButton:
                var spin1 = ctrl1 as SpinButtonActiveXControl;
                var spin2 = ctrl2 as SpinButtonActiveXControl;
                if (spin1 != null && spin2 != null && spin1.Position != spin2.Position)
                {
                    Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' SpinButton Position differs: {spin1.Position} vs {spin2.Position}");
                }
                break;

            case ControlType.CheckBox:
                var chk1 = ctrl1 as CheckBoxActiveXControl;
                var chk2 = ctrl2 as CheckBoxActiveXControl;
                if (chk1 != null && chk2 != null && chk1.Value != chk2.Value)
                {
                    Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' CheckBox Value differs: {chk1.Value} vs {chk2.Value}");
                }
                break;

            case ControlType.Image:
                var img1 = ctrl1 as ImageActiveXControl;
                var img2 = ctrl2 as ImageActiveXControl;
                if (img1 != null && img2 != null)
                {
                    int len1 = img1.Picture?.Length ?? 0;
                    int len2 = img2.Picture?.Length ?? 0;
                    if (len1 != len2)
                    {
                        Console.WriteLine($"[Page {pageNumber}] Shape '{shapeName}' Image picture size differs (bytes): {len1} vs {len2}");
                    }
                }
                break;

            // Additional control types can be added here following the same pattern
        }
    }
}