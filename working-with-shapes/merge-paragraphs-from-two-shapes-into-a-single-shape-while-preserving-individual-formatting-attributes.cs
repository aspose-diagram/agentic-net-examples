using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Helper to locate a shape on a page by its universal name (NameU)
    static Shape FindShapeByName(Page page, string nameU)
    {
        foreach (Shape shape in page.Shapes)
        {
            if (shape.NameU == nameU)
                return shape;
        }
        return null;
    }

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page
            Page page = diagram.Pages[0];

            // Locate the two source shapes (replace with actual shape names)
            Shape sourceShape1 = FindShapeByName(page, "SourceShape1");
            Shape sourceShape2 = FindShapeByName(page, "SourceShape2");

            if (sourceShape1 == null || sourceShape2 == null)
            {
                Console.WriteLine("One or both source shapes were not found.");
                return;
            }

            // Create a new target shape (using a simple rectangle master)
            long targetShapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape targetShape = page.Shapes.GetShape(targetShapeId);

            // Ensure the target shape starts with no paragraphs
            targetShape.Paras.Clear();

            // Method to copy paragraphs from a source shape to the target shape
            void CopyParagraphs(Shape src)
            {
                foreach (Para srcPara in src.Paras)
                {
                    // Create a new paragraph and copy formatting cells
                    Para newPara = new Para();

                    // Horizontal alignment
                    newPara.HorzAlign.Value = srcPara.HorzAlign.Value;

                    // Indentation
                    newPara.IndLeft.Value = srcPara.IndLeft.Value;
                    newPara.IndRight.Value = srcPara.IndRight.Value;
                    newPara.IndFirst.Value = srcPara.IndFirst.Value;

                    // Spacing
                    newPara.SpBefore.Value = srcPara.SpBefore.Value;
                    newPara.SpAfter.Value = srcPara.SpAfter.Value;
                    newPara.SpLine.Value = srcPara.SpLine.Value;

                    // Bullet settings
                    newPara.Bullet.Value = srcPara.Bullet.Value;
                    newPara.BulletStr.Value = srcPara.BulletStr.Value;
                    newPara.BulletFont.Value = srcPara.BulletFont.Value;
                    newPara.BulletFontSize.Value = srcPara.BulletFontSize.Value;
                    newPara.Flags.Value = srcPara.Flags.Value;
                    newPara.LocalizeBulletFont.Value = srcPara.LocalizeBulletFont.Value;
                    newPara.TextPosAfterBullet.Value = srcPara.TextPosAfterBullet.Value;

                    // Add the copied paragraph to the target shape
                    targetShape.Paras.Add(newPara);
                }
            }

            // Copy paragraphs from both source shapes
            CopyParagraphs(sourceShape1);
            CopyParagraphs(sourceShape2);

            // Save the modified diagram
            diagram.Save("merged_output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Paragraphs merged and diagram saved as merged_output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
