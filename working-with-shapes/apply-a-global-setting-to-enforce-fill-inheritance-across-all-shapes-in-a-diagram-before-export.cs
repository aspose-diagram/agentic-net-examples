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

        string outputPath = "output.pdf";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.InheritFill != null && shape.Fill != null)
                    {
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        shape.Fill.FillForegndTrans.Value = shape.InheritFill.FillForegndTrans.Value;
                        shape.Fill.FillBkgndTrans.Value = shape.InheritFill.FillBkgndTrans.Value;
                        shape.Fill.ShdwForegnd.Value = shape.InheritFill.ShdwForegnd.Value;
                        shape.Fill.ShdwBkgnd.Value = shape.InheritFill.ShdwBkgnd.Value;
                        shape.Fill.ShdwPattern.Value = shape.InheritFill.ShdwPattern.Value;
                        shape.Fill.ShapeShdwType.Value = shape.InheritFill.ShapeShdwType.Value;
                        shape.Fill.ShapeShdwOffsetX.Value = shape.InheritFill.ShapeShdwOffsetX.Value;
                        shape.Fill.ShapeShdwOffsetY.Value = shape.InheritFill.ShapeShdwOffsetY.Value;
                        shape.Fill.ShapeShdwScaleFactor.Value = shape.InheritFill.ShapeShdwScaleFactor.Value;
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Pdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}