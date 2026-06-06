using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
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

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null)
                    {
                        // Attempt to clear the read‑only ActiveXControl via reflection
                        FieldInfo field = typeof(Shape).GetField("activeXControl", BindingFlags.Instance | BindingFlags.NonPublic);
                        if (field != null)
                        {
                            field.SetValue(shape, null);
                        }
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}