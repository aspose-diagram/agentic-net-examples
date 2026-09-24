using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page in the diagram
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found on the page.");
                return;
            }

            // Access inherited fill settings
            var inheritFill = targetShape.InheritFill;

            Console.WriteLine("Inherited Fill Settings:");
            Console.WriteLine($"Fill Foreground: {inheritFill.FillForegnd.Value}");
            Console.WriteLine($"Fill Background: {inheritFill.FillBkgnd.Value}");
            Console.WriteLine($"Fill Pattern: {inheritFill.FillPattern.Value}");
            Console.WriteLine($"Shadow Foreground: {inheritFill.ShdwForegnd.Value}");
            Console.WriteLine($"Shadow Background: {inheritFill.ShdwBkgnd.Value}");
            Console.WriteLine($"Shadow Pattern: {inheritFill.ShdwPattern.Value}");
            Console.WriteLine($"Shape Shadow Type: {inheritFill.ShapeShdwType.Value}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
