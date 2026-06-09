using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Read the current center header text
                string headerCenter = diagram.HeaderFooter.HeaderCenter;

                // Replace occurrences of "Draft" with "Final"
                if (!string.IsNullOrEmpty(headerCenter) && headerCenter.Contains("Draft"))
                {
                    headerCenter = headerCenter.Replace("Draft", "Final");
                    diagram.HeaderFooter.HeaderCenter = headerCenter;
                    Console.WriteLine("Header center text updated.");
                }
                else
                {
                    Console.WriteLine("No 'Draft' text found in header center.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }