using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file; can be passed as a command‑line argument.
            string filePath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(filePath);

            double totalAreaCm2 = 0.0;
            int pageNumber = 0;

            // Iterate through each page and calculate dimensions and area.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                pageNumber++;

                // Width and height are stored in inches.
                double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Convert inches to centimeters (1 inch = 2.54 cm).
                double widthCm = widthInches * 2.54;
                double heightCm = heightInches * 2.54;

                // Area in square centimeters.
                double areaCm2 = widthCm * heightCm;
                totalAreaCm2 += areaCm2;

                Console.WriteLine($"Page {pageNumber}: Width = {widthCm:F2} cm, Height = {heightCm:F2} cm, Area = {areaCm2:F2} cm²");
            }

            Console.WriteLine($"Total diagram area: {totalAreaCm2:F2} cm²");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
