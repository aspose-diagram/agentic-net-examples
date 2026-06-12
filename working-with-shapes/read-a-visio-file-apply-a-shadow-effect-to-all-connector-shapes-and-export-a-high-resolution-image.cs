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

        string outputPath = "output.png";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD)
                    {
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }
            }

            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300;
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = diagram.Pages.Count;

            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}