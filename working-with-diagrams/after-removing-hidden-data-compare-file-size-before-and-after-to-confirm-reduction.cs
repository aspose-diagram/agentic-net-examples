using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_cleaned.vsdx";

                // Ensure the input file exists
                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException($"Input file not found: {inputPath}");
                }

                // Get original file size
                long originalSize = new FileInfo(inputPath).Length;
                Console.WriteLine($"Original file size: {originalSize} bytes");

                // Load the diagram, remove hidden information, and save the cleaned file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Remove hidden shapes and masters (add other flags as needed)
                    diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                    // Save the cleaned diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                // Get cleaned file size
                long cleanedSize = new FileInfo(outputPath).Length;
                Console.WriteLine($"Cleaned file size: {cleanedSize} bytes");

                // Compare sizes
                if (cleanedSize < originalSize)
                {
                    Console.WriteLine("Hidden data removal successful: file size reduced.");
                }
                else if (cleanedSize == originalSize)
                {
                    Console.WriteLine("No size reduction detected after hidden data removal.");
                }
                else
                {
                    // Unexpected increase; raise an exception to indicate failure
                    throw new Exception("File size increased after hidden data removal, which is unexpected.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }