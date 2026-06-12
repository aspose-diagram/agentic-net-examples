using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputPdfPath> [commaSeparatedShapeIds]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        string shapeIdsArg = args.Length > 2 ? args[2] : null;

        try
        {
            Diagram diagram = new Diagram(inputPath);

            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            if (shapeIdsArg != null && shapeIdsArg.Trim().Length > 0)
            {
                string[] parts = shapeIdsArg.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (long.TryParse(part.Trim(), out long shapeId))
                    {
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null && shape.Del != BOOL.True)
                        {
                            shape.XForm.Angle.Value = 45;
                        }
                    }
                }
            }
            else
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del != BOOL.True)
                    {
                        shape.XForm.Angle.Value = 45;
                    }
                }
            }

            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}