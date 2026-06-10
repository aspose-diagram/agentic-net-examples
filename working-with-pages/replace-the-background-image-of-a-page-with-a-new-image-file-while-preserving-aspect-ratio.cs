using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string visioPath = "input.vsdx";
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        string imagePath = "newBackground.png";
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"File not found: {imagePath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                Page page = diagram.Pages[0];

                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Load image dimensions using Aspose.Drawing.Image
                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(imagePath))
                {
                    double imgWidthPx = img.Width;
                    double imgHeightPx = img.Height;
                    double imgAspect = imgWidthPx / imgHeightPx;
                    double pageAspect = pageWidth / pageHeight;

                    double shapeWidth, shapeHeight;
                    if (imgAspect > pageAspect)
                    {
                        shapeWidth = pageWidth;
                        shapeHeight = pageWidth / imgAspect;
                    }
                    else
                    {
                        shapeHeight = pageHeight;
                        shapeWidth = pageHeight * imgAspect;
                    }

                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        long shapeId = page.AddShape(pinX, pinY, shapeWidth, shapeHeight, imgStream);
                        Shape bgShape = page.Shapes.GetShape(shapeId);
                        bgShape.SendToBack();
                        bgShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}