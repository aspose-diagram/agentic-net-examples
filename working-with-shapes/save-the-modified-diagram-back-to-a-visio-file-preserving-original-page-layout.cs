using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input Visio file and output Visio file paths
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramSaveExample <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Perform any modifications to the diagram here.
            // For example, you might add shapes, update text, change styles, etc.
            // -----------------------------------------------------------------
            // (Modification code omitted for brevity)

            // Prepare save options to preserve the original page layout.
            // AutoFitPageToDrawingContent = false ensures the page size is not altered.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.AutoFitPageToDrawingContent = false;

            // Save the modified diagram back to a Visio file.
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
    }