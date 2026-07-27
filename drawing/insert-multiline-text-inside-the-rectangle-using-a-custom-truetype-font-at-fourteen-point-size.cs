using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (empty Visio document)
        Diagram diagram = new Diagram();

        // Get the first page (a new diagram contains one default page)
        Page page = diagram.Pages[0];

        // Define rectangle position and size
        double rectPinX = 5.0;   // X coordinate of rectangle center
        double rectPinY = 5.0;   // Y coordinate of rectangle center
        double rectWidth = 4.0;  // Width of rectangle
        double rectHeight = 2.0; // Height of rectangle

        // Draw the rectangle on the page
        page.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);

        // Multiline text to be placed inside the rectangle
        string multilineText = "First line\nSecond line\nThird line";

        // Add a text shape using the custom TrueType font at 14‑point size
        // Font color "0" represents black (you can change it to any valid color string)
        Shape textShape = page.AddText(
            rectPinX,          // pinX – same as rectangle centre
            rectPinY,          // pinY – same as rectangle centre
            rectWidth,         // width of the text block
            rectHeight,        // height of the text block
            multilineText,     // the actual text (uses \n for new lines)
            "MyCustomFont",    // custom TrueType font name installed on the system
            "0",               // font color (black)
            14);               // font size in points

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
