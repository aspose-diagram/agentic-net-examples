using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputFile = "input.vsdx";
                string outputFile = "output.vsdx";

                Diagram diagram = null;

                try
                {
                    // Load the diagram from the file
                    diagram = new Diagram(inputFile);

                    // ----- Place any diagram processing logic here -----
                    // For example, you could add shapes, modify pages, etc.

                    // Save the diagram to the desired format
                    diagram.Save(outputFile, SaveFileFormat.Vsdx);
                }
                finally
                {
                    // Ensure the Diagram object is properly disposed even if an exception occurs
                    if (diagram != null)
                    {
                        diagram.Dispose();
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }