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

        string outputPath = "output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diagram contains no pages.");
                return;
            }

            Page sourcePage = diagram.Pages[0];
            Shape sourceShape = null;
            foreach (Shape shp in sourcePage.Shapes)
            {
                if (shp.Del == BOOL.False)
                {
                    sourceShape = shp;
                    break;
                }
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to clone on the source page.");
                return;
            }

            string masterName = sourceShape.Master?.Name;
            if (string.IsNullOrEmpty(masterName))
            {
                Console.Error.WriteLine("Source shape does not have an associated master.");
                return;
            }

            Page targetPage;
            if (diagram.Pages.Count > 1)
            {
                targetPage = diagram.Pages[1];
            }
            else
            {
                targetPage = new Page();
                diagram.Pages.Add(targetPage);
            }

            long newShapeId = targetPage.AddShape(
                sourceShape.XForm.PinX.Value,
                sourceShape.XForm.PinY.Value,
                masterName,
                false);

            Shape newShape = targetPage.Shapes.GetShape(newShapeId);
            if (newShape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                return;
            }

            newShape.Text.Value.Clear();
            foreach (object item in sourceShape.Text.Value)
            {
                if (item is Txt txt)
                {
                    newShape.Text.Value.Add(new Txt(txt.Text));
                }
            }

            newShape.PresetTheme = PresetThemeValue.Bubble;
            newShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}