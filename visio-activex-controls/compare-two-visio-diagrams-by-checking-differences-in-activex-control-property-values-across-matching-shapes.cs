using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VisioActiveXComparer <DiagramPath1> <DiagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        // Guard to ensure the first file exists.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }

        string diagramPath2 = args[1];
        // Guard to ensure the second file exists.
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        Diagram diagram1;
        Diagram diagram2;
        try
        {
            // Load the two Visio diagrams.
            diagram1 = new Diagram(diagramPath1);
            diagram2 = new Diagram(diagramPath2);
        }
        catch (Exception ex)
        {
            // Report any loading errors.
            Console.Error.WriteLine($"Error loading diagrams: {ex.Message}");
            return;
        }

        // Assume both diagrams have the same number of pages and matching page order.
        int pageCount = Math.Min(diagram1.Pages.Count, diagram2.Pages.Count);

        for (int i = 0; i < pageCount; i++)
        {
            Page page1 = diagram1.Pages[i];
            Page page2 = diagram2.Pages[i];

            // Output page comparison info (Name is a plain string, not a cell).
            Console.WriteLine($"Comparing Page {i + 1}: \"{page1.Name}\" vs \"{page2.Name}\"");

            // Iterate all shapes on the first page.
            foreach (Shape shape1 in page1.Shapes)
            {
                // Find a shape with the same universal name on the second page.
                Shape shape2 = FindShapeByNameU(page2, shape1.NameU);

                if (shape2 == null)
                {
                    Console.WriteLine($"  Shape \"{shape1.NameU}\" exists only in Diagram 1.");
                    continue;
                }

                // Both shapes exist – compare their ActiveX controls.
                CompareActiveXControls(shape1, shape2);
            }

            // Detect shapes that exist only in Diagram 2.
            foreach (Shape shape2 in page2.Shapes)
            {
                Shape shape1 = FindShapeByNameU(page1, shape2.NameU);
                if (shape1 == null)
                {
                    Console.WriteLine($"  Shape \"{shape2.NameU}\" exists only in Diagram 2.");
                }
            }
        }
    }

    // Finds a shape on a page by its universal name (NameU). Returns null if not found.
    private static Shape FindShapeByNameU(Page page, string nameU)
    {
        foreach (Shape shape in page.Shapes)
        {
            // NameU is a plain string; compare directly.
            if (shape.NameU == nameU)
            {
                return shape;
            }
        }
        return null;
    }

    // Compares the ActiveX control properties of two matching shapes.
    private static void CompareActiveXControls(Shape shape1, Shape shape2)
    {
        // If neither shape has an ActiveX control, nothing to compare.
        if (shape1.ActiveXControl == null && shape2.ActiveXControl == null)
        {
            return;
        }

        // One shape has a control while the other does not.
        if (shape1.ActiveXControl == null || shape2.ActiveXControl == null)
        {
            Console.WriteLine($"  Shape \"{shape1.NameU}\": ActiveX control presence differs between diagrams.");
            return;
        }

        // Both have ActiveX controls – ensure they are of the same type.
        if (shape1.ActiveXControl.Type != shape2.ActiveXControl.Type)
        {
            Console.WriteLine($"  Shape \"{shape1.NameU}\": Control type differs (Diagram1={shape1.ActiveXControl.Type}, Diagram2={shape2.ActiveXControl.Type}).");
            return;
        }

        // Compare based on the specific control type.
        switch (shape1.ActiveXControl.Type)
        {
            case ControlType.CommandButton:
                CompareCommandButton(shape1, shape2);
                break;
            case ControlType.TextBox:
                CompareTextBox(shape1, shape2);
                break;
            case ControlType.CheckBox:
                CompareCheckBox(shape1, shape2);
                break;
            case ControlType.Image:
                CompareImage(shape1, shape2);
                break;
            // Add other control types as needed.
            default:
                Console.WriteLine($"  Shape \"{shape1.NameU}\": Control type {shape1.ActiveXControl.Type} is not explicitly handled.");
                break;
        }
    }

    private static void CompareCommandButton(Shape s1, Shape s2)
    {
        var btn1 = (CommandButtonActiveXControl)s1.ActiveXControl;
        var btn2 = (CommandButtonActiveXControl)s2.ActiveXControl;

        if (btn1.Caption != btn2.Caption)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" CommandButton Caption differs: \"{btn1.Caption}\" vs \"{btn2.Caption}\"");
        }

        if (Math.Abs(btn1.Width - btn2.Width) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" CommandButton Width differs: {btn1.Width} vs {btn2.Width}");
        }

        if (Math.Abs(btn1.Height - btn2.Height) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" CommandButton Height differs: {btn1.Height} vs {btn2.Height}");
        }
    }

    private static void CompareTextBox(Shape s1, Shape s2)
    {
        var txt1 = (TextBoxActiveXControl)s1.ActiveXControl;
        var txt2 = (TextBoxActiveXControl)s2.ActiveXControl;

        if (txt1.Text != txt2.Text)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" TextBox Text differs: \"{txt1.Text}\" vs \"{txt2.Text}\"");
        }

        if (Math.Abs(txt1.Width - txt2.Width) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" TextBox Width differs: {txt1.Width} vs {txt2.Width}");
        }

        if (Math.Abs(txt1.Height - txt2.Height) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" TextBox Height differs: {txt1.Height} vs {txt2.Height}");
        }
    }

    private static void CompareCheckBox(Shape s1, Shape s2)
    {
        var chk1 = (CheckBoxActiveXControl)s1.ActiveXControl;
        var chk2 = (CheckBoxActiveXControl)s2.ActiveXControl;

        if (chk1.Value != chk2.Value)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" CheckBox Value differs: {chk1.Value} vs {chk2.Value}");
        }

        if (Math.Abs(chk1.Width - chk2.Width) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" CheckBox Width differs: {chk1.Width} vs {chk2.Width}");
        }

        if (Math.Abs(chk1.Height - chk2.Height) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" CheckBox Height differs: {chk1.Height} vs {chk2.Height}");
        }
    }

    private static void CompareImage(Shape s1, Shape s2)
    {
        var img1 = (ImageActiveXControl)s1.ActiveXControl;
        var img2 = (ImageActiveXControl)s2.ActiveXControl;

        // Compare image byte arrays.
        byte[] pic1 = img1.Picture;
        byte[] pic2 = img2.Picture;

        bool imagesEqual = true;
        if (pic1 == null || pic2 == null)
        {
            imagesEqual = pic1 == pic2;
        }
        else if (pic1.Length != pic2.Length)
        {
            imagesEqual = false;
        }
        else
        {
            for (int i = 0; i < pic1.Length; i++)
            {
                if (pic1[i] != pic2[i])
                {
                    imagesEqual = false;
                    break;
                }
            }
        }

        if (!imagesEqual)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" Image content differs.");
        }

        if (Math.Abs(img1.Width - img2.Width) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" Image Width differs: {img1.Width} vs {img2.Width}");
        }

        if (Math.Abs(img1.Height - img2.Height) > 0.0001)
        {
            Console.WriteLine($"  Shape \"{s1.NameU}\" Image Height differs: {img1.Height} vs {img2.Height}");
        }
    }
}