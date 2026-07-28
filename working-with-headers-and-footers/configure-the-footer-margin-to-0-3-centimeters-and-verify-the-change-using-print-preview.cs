using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Convert 0.3 centimeters to inches (1 cm = 0.393701 inches)
            double centimeters = 0.3;
            double inches = centimeters * 0.393701; // ≈ 0.1181103 inches

            // Set the global footer margin (in inches)
            diagram.HeaderFooter.FooterMargin.Value = inches;

            // Verify the change by outputting the current footer margin value
            Console.WriteLine($"Footer margin set to {diagram.HeaderFooter.FooterMargin.Value:F6} inches "
                + $"({centimeters} cm).");

            // Optional: Save the diagram to a file to inspect the result manually
            string outputPath = "FooterMarginDemo.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }