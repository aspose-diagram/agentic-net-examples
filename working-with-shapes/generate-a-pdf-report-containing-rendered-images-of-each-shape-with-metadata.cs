using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string imageFolder = "ShapeImages";
        Directory.CreateDirectory(imageFolder);

        List<ShapeInfo> shapeInfos = new List<ShapeInfo>();

        try
        {
            Diagram diagram = new Diagram(diagramPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    var info = new ShapeInfo
                    {
                        PageId = page.ID,
                        ShapeId = shape.ID,
                        Name = shape.Name,
                        NameU = shape.NameU,
                        MasterName = shape.Master?.Name ?? string.Empty,
                        Type = shape.Type.ToString(),
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value,
                        Width = shape.XForm.Width.Value,
                        Height = shape.XForm.Height.Value
                    };
                    shapeInfos.Add(info);

                    string imageFile = Path.Combine(imageFolder, $"Page{page.ID}_Shape{shape.ID}.png");
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    shape.ToImage(imageFile, imgOptions);
                }
            }

            // Create PDF report using Aspose.Pdf (fully qualified)
            var pdfDoc = new Aspose.Pdf.Document();
            foreach (var info in shapeInfos)
            {
                var pdfPage = pdfDoc.Pages.Add();

                // Add image
                var pdfImage = new Aspose.Pdf.Image
                {
                    File = Path.Combine(imageFolder, $"Page{info.PageId}_Shape{info.ShapeId}.png"),
                    FixWidth = 500
                };
                pdfPage.Paragraphs.Add(pdfImage);

                // Add metadata text
                string meta = $"Page: {info.PageId}, Shape ID: {info.ShapeId}, Name: {info.Name}, " +
                              $"NameU: {info.NameU}, Master: {info.MasterName}, Type: {info.Type}, " +
                              $"Position: ({info.PinX}, {info.PinY}), Size: ({info.Width} x {info.Height})";
                var textFragment = new Aspose.Pdf.Text.TextFragment(meta);
                pdfPage.Paragraphs.Add(textFragment);
            }

            string pdfPath = "ShapeReport.pdf";
            pdfDoc.Save(pdfPath);
            Console.WriteLine($"PDF report generated: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// DTO for shape metadata
public class ShapeInfo
{
    public int PageId { get; set; }
    public long ShapeId { get; set; }
    public string Name { get; set; }
    public string NameU { get; set; }
    public string MasterName { get; set; }
    public string Type { get; set; }
    public double PinX { get; set; }
    public double PinY { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}