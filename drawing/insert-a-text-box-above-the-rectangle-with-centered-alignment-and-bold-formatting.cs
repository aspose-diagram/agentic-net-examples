using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (a new diagram contains one page by default)
            Page page = diagram.Pages[0];

            // -----------------------------
            // Add a rectangle shape
            // -----------------------------
            double rectPinX = 5.0;   // X coordinate of rectangle center
            double rectPinY = 5.0;   // Y coordinate of rectangle center
            double rectWidth = 2.0;  // Width of rectangle
            double rectHeight = 1.0; // Height of rectangle

            // DrawRectangle returns the shape ID (long)
            long rectId = page.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);
            Shape rectShape = page.Shapes.GetShape(rectId);

            // -----------------------------
            // Add a text box above the rectangle
            // -----------------------------
            double textPinX = rectPinX;                                 // Align horizontally with rectangle
            double textPinY = rectPinY + (rectHeight / 2) + 0.5;        // Position above rectangle
            double textWidth = rectWidth;                               // Same width as rectangle
            double textHeight = 0.5;                                    // Height for the text box
            string textContent = "Sample Text";

            // AddText creates a text shape with the specified text
            Shape textShape = page.AddText(textPinX, textPinY, textWidth, textHeight, textContent);

            // -----------------------------
            // Apply bold formatting to the text
            // -----------------------------
            // Ensure there is at least one character run
            if (textShape.Chars.Count > 0)
            {
                // Apply bold style to the first character run
                textShape.Chars[0].Style.Value = StyleValue.Bold;
            }

            // -----------------------------
            // Center align the text horizontally
            // -----------------------------
            if (textShape.Paras.Count > 0)
            {
                // Set horizontal alignment to Center
                textShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }

            // -----------------------------
            // Save the diagram to a VSDX file
            // -----------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}