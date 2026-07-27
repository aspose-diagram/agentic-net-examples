using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process; use the first argument or the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Generate a uniform BatchId for this run.
            string batchId = Guid.NewGuid().ToString();

            // Process all Visio files with .vsdx extension in the folder.
            string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in visioFiles)
            {
                // Load the diagram from file.
                Diagram diagram = new Diagram(filePath);

                // Create a new custom property.
                CustomProp batchProp = new CustomProp
                {
                    Name = "BatchId",
                    PropType = PropType.String,
                    CustomValue = { ValueString = batchId }
                };

                // Add the custom property to the document.
                diagram.DocumentProps.CustomProps.Add(batchProp);

                // Save the diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                // Release resources.
                diagram.Dispose();

                Console.WriteLine($"Processed file: {Path.GetFileName(filePath)}");
            }

            Console.WriteLine("Batch processing completed.");
        }
    }