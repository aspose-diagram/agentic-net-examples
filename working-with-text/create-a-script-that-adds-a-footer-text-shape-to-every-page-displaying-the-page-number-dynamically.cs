using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: FooterExample <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Set a dynamic footer that shows the page number on the right side.
                // '&p' is the Visio field code for the current page number.
                diagram.HeaderFooter.FooterRight = "Page &p";

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Footer added and diagram saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }