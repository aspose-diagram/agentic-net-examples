using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Find the first rectangle shape on the page
                Shape? rectangle = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and that the master name is "Rectangle"
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        rectangle = shape;
                        break;
                    }
                }

                if (rectangle == null)
                {
                    throw new Exception("No rectangle shape found on the page.");
                }

                // Calculate position for the text box (above the rectangle)
                double rectPinY = rectangle.XForm.PinY.Value;
                double rectHeight = rectangle.XForm.Height.Value;
                double offset = 0.2; // inches above the rectangle
                double textPinY = rectPinY + (rectHeight / 2) + offset;
                double textPinX = rectangle.XForm.PinX.Value; // center horizontally with rectangle

                // Define size of the text box
                double textWidth = 2.0;   // inches
                double textHeight = 0.5;  // inches

                // Add a text shape (text box) to the page
                Shape textShape = page.AddText(textPinX, textPinY, textWidth, textHeight, "Sample Text");

                // Center the text horizontally within the text box
                if (textShape.Paras.Count > 0)
                {
                    textShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                }

                // Apply bold formatting to the entire text
                // Clear any existing characters and add a new Char with bold style
                textShape.Chars.Clear();
                Aspose.Diagram.Char boldChar = new Aspose.Diagram.Char();
                boldChar.IX = 0; // start index
                boldChar.Style.Value = StyleValue.Bold;
                textShape.Chars.Add(boldChar);

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