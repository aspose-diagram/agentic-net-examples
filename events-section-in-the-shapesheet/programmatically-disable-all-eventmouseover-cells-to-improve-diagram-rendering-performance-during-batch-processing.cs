using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Event != null)
                    {
                        PropertyInfo mouseOverProp = shape.Event.GetType().GetProperty("EventMouseOver");
                        if (mouseOverProp != null)
                        {
                            object cellObj = mouseOverProp.GetValue(shape.Event);
                            if (cellObj != null)
                            {
                                PropertyInfo ufeProp = cellObj.GetType().GetProperty("Ufe");
                                if (ufeProp != null)
                                {
                                    object ufeObj = ufeProp.GetValue(cellObj);
                                    if (ufeObj != null)
                                    {
                                        PropertyInfo fProp = ufeObj.GetType().GetProperty("F");
                                        if (fProp != null && fProp.CanWrite)
                                        {
                                            fProp.SetValue(ufeObj, string.Empty);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}