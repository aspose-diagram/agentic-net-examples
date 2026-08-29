using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the two source shapes by their universal names (adjust names as needed)
            Shape sourceShape1 = null;
            Shape sourceShape2 = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.NameU == "SourceShape1")
                    sourceShape1 = shp;
                else if (shp.NameU == "SourceShape2")
                    sourceShape2 = shp;
            }

            if (sourceShape1 == null || sourceShape2 == null)
                throw new Exception("Source shapes not found.");

            // Create a new rectangle shape that will hold the merged paragraphs
            long targetShapeId = page.AddShape(5.0, 5.0, 2.0, 2.0, "Rectangle");
            Shape targetShape = page.Shapes.GetShape(targetShapeId);

            // Clear any existing content in the target shape
            targetShape.Paras.Clear();
            targetShape.Text.Value.Clear();

            // Merge paragraphs from both source shapes
            CopyParagraphs(sourceShape1, targetShape);
            CopyParagraphs(sourceShape2, targetShape);

            // Save the modified diagram
            diagram.Save("merged_output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Copies all paragraphs (including formatting) from src to dest shape
    static void CopyParagraphs(Shape src, Shape dest)
    {
        // Split the source shape's plain text into lines (Visio uses line breaks for paragraphs)
        string[] paragraphTexts = src.Text.Value.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        int index = 0;
        foreach (Aspose.Diagram.Para srcPara in src.Paras)
        {
            // Create a new paragraph and copy formatting cells
            Aspose.Diagram.Para newPara = new Aspose.Diagram.Para();

            newPara.HorzAlign.Value = srcPara.HorzAlign.Value;
            newPara.IndLeft.Value = srcPara.IndLeft.Value;
            newPara.IndRight.Value = srcPara.IndRight.Value;
            newPara.IndFirst.Value = srcPara.IndFirst.Value;
            newPara.SpBefore.Value = srcPara.SpBefore.Value;
            newPara.SpAfter.Value = srcPara.SpAfter.Value;
            newPara.SpLine.Value = srcPara.SpLine.Value;
            newPara.Bullet.Value = srcPara.Bullet.Value;
            newPara.BulletStr.Value = srcPara.BulletStr.Value;

            // Add the paragraph to the destination shape
            dest.Paras.Add(newPara);

            // Add the corresponding text run (if available)
            if (index < paragraphTexts.Length)
            {
                string txt = paragraphTexts[index];
                dest.Text.Value.Add(new Txt(txt));
            }

            index++;
        }
    }
}
