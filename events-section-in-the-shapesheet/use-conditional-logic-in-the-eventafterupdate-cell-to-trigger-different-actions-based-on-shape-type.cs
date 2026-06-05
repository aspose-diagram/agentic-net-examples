using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null)
                    {
                        string masterName = shape.Master.Name;

                        if (masterName == "Rectangle")
                        {
                            shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"RectUpdate\")";
                        }
                        else if (masterName == "Ellipse")
                        {
                            shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"EllipseUpdate\")";
                        }
                        else
                        {
                            shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"DefaultUpdate\")";
                        }
                    }
                }
            }

            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}