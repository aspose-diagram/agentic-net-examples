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
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first rectangle shape on the page
                Shape rectangleShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.Master != null && shp.Master.Name == "Rectangle")
                    {
                        rectangleShape = shp;
                        break;
                    }
                }

                if (rectangleShape == null)
                {
                    throw new Exception("Rectangle shape not found on the page.");
                }

                // Calculate position for the text box (above the rectangle)
                double rectPinX = rectangleShape.XForm.PinX.Value;
                double rectPinY = rectangleShape.XForm.PinY.Value;
                double rectHeight = rectangleShape.XForm.Height.Value;

                // Define text box size
                double textBoxWidth = rectHeight;   // arbitrary width
                double textBoxHeight = 0.5;         // height in inches

                // Position: same PinX, PinY above the rectangle (add half height + offset)
                double offset = 0.2; // extra space above the rectangle
                double textPinX = rectPinX;
                double textPinY = rectPinY + (rectHeight / 2) + (textBoxHeight / 2) + offset;

                // Add the text box shape
                Shape textShape = page.AddText(textPinX, textPinY, textBoxWidth, textBoxHeight, "Sample Text");

                // Clear any existing text runs and add our text
                textShape.Text.Value.Clear();
                textShape.Text.Value.Add(new Txt("Sample Text"));

                // Ensure there is at least one paragraph to set alignment
                if (textShape.Paras.Count == 0)
                {
                    textShape.Paras.Add(new Para());
                }

                // Center align the paragraph horizontally
                textShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                // Optionally set vertical alignment to middle within the text block
                textShape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                // Add a character formatting run for bold style
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0; // start index
                ch.Style.Value = StyleValue.Bold;
                textShape.Chars.Clear();
                textShape.Chars.Add(ch);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }