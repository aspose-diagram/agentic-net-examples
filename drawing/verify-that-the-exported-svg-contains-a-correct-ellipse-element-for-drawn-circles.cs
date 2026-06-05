using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Parameters for a circle (ellipse with equal width and height)
        double centerX = 5.0;
        double centerY = 5.0;
        double radius = 2.0;
        double width = radius * 2;
        double height = radius * 2;

        // Draw the ellipse (circle) on the page
        // DrawEllipse expects the pin (top‑left corner of bounding box) and size
        page.DrawEllipse(centerX - radius, centerY - radius, width, height);

        // Save the diagram as SVG
        string svgPath = "circle.svg";
        diagram.Save(svgPath, SaveFileFormat.Svg);

        // Load the generated SVG content
        string svgContent = File.ReadAllText(svgPath);

        // Verify that an <ellipse> element is present
        bool containsEllipse = svgContent.Contains("<ellipse");

        Console.WriteLine(containsEllipse
            ? "SVG contains an <ellipse> element."
            : "SVG does NOT contain an <ellipse> element.");
    }
}
