using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
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

        string outputPath = "output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            int glueBefore = GetTotalGlueCount(diagram);
            Console.WriteLine($"Gluing relationships before modification: {glueBefore}");

            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];

                long rectIdLong = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape rectShape = page.Shapes.GetShape((int)rectIdLong);

                Shape targetShape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.ID != rectShape.ID)
                    {
                        targetShape = s;
                        break;
                    }
                }

                if (targetShape != null)
                {
                    page.GlueShapes(rectShape.ID, ConnectionPointPlace.Center, targetShape.ID);
                }
            }

            int glueAfter = GetTotalGlueCount(diagram);
            Console.WriteLine($"Gluing relationships after modification: {glueAfter}");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    static int GetTotalGlueCount(Diagram diagram)
    {
        int total = 0;
        foreach (Page page in diagram.Pages)
        {
            total += page.Connects.Count;
        }
        return total;
    }
}