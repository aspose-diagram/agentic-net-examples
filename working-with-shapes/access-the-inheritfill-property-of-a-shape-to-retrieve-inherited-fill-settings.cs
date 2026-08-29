using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example uses ID = 1)
                // Adjust the ID as needed for your diagram
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    Console.WriteLine("Shape with ID 1 not found on the first page.");
                    return;
                }

                // Access the inherited fill settings
                var inheritFill = shape.InheritFill;
                if (inheritFill == null)
                {
                    Console.WriteLine("Inherited fill information is unavailable for this shape.");
                    return;
                }

                // Retrieve specific inherited fill properties
                string foregndColor = inheritFill.FillForegnd.Value;   // Foreground fill color (hex string)
                string bkgndColor = inheritFill.FillBkgnd.Value;      // Background fill color (hex string)
                int fillPattern = inheritFill.FillPattern.Value;      // Fill pattern index
                string shadowColor = inheritFill.ShdwForegnd.Value;   // Shadow foreground color
                int shadowPattern = inheritFill.ShdwPattern.Value;    // Shadow pattern index

                // Output the inherited fill values
                Console.WriteLine("Inherited Fill Settings for Shape ID 1:");
                Console.WriteLine($"  Foreground Color : {foregndColor}");
                Console.WriteLine($"  Background Color : {bkgndColor}");
                Console.WriteLine($"  Fill Pattern     : {fillPattern}");
                Console.WriteLine($"  Shadow Color     : {shadowColor}");
                Console.WriteLine($"  Shadow Pattern   : {shadowPattern}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }