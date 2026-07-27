using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (by default a diagram has at least one page)
            Page page = diagram.Pages[0];

            // Draw a circle using DrawEllipse (pinX, pinY, width, height)
            // Center at (5,5) inches, radius 2 inches => width and height = 4 inches
            double pinX = 5.0;
            double pinY = 5.0;
            double diameter = 4.0;
            page.DrawEllipse(pinX, pinY, diameter, diameter);

            // Export the diagram to SVG
            string svgPath = "output.svg";
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            diagram.Save(svgPath, svgOptions);

            // Verify that the exported SVG contains an <ellipse> element
            if (!File.Exists(svgPath))
            {
                throw new Exception($"SVG file was not created at path: {svgPath}");
            }

            string svgContent = File.ReadAllText(svgPath);
            if (svgContent.Contains("<ellipse"))
            {
                Console.WriteLine("Verification succeeded: <ellipse> element found in the SVG.");
            }
            else
            {
                throw new Exception("Verification failed: <ellipse> element not found in the exported SVG.");
            }
        }
    }