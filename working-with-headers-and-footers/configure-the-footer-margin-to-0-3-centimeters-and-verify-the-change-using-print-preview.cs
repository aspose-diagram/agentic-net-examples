using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after modification
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Convert 0.3 centimeters to inches (1 cm = 0.393701 inches)
                double centimeters = 0.3;
                double inches = centimeters * 0.393701; // ≈0.1181 inches

                // Set the global footer margin (in inches)
                diagram.HeaderFooter.FooterMargin.Value = inches;

                // Verify the change by reading back the value and outputting it
                double verifiedMarginInches = diagram.HeaderFooter.FooterMargin.Value;
                Console.WriteLine($"Footer margin set to {verifiedMarginInches:F4} inches (expected ≈{inches:F4}).");

                // Simple validation: ensure the set value matches the expected value within a small tolerance
                double tolerance = 0.0001;
                if (Math.Abs(verifiedMarginInches - inches) > tolerance)
                {
                    throw new Exception("Footer margin verification failed.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }