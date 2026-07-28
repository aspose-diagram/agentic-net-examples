using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                bool validationFailed = false;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Access the DistanceFromGround value (in points)
                        double distance = shape.ThreeDFormat.DistanceFromGround.Value;

                        // Check if the distance exceeds 100 points
                        if (distance > 100)
                        {
                            validationFailed = true;
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' exceeds 100 points: {distance}");
                        }
                    }
                }

                // Throw an exception if any shape failed the validation
                if (validationFailed)
                {
                    throw new Exception("One or more shapes have DistanceFromGround greater than 100 points.");
                }
                else
                {
                    Console.WriteLine("All shapes passed the DistanceFromGround validation (<= 100 points).");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }