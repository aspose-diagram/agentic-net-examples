using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files (default: "InputDiagrams")
            string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";

            // Output folder where modified files will be saved (default: "OutputDiagrams")
            string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

            // Size threshold in inches (default: 5.0). Shapes larger than this in width or height will have gluing disabled.
            double sizeThreshold = args.Length > 2 ? double.Parse(args[2]) : 5.0;

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Process each Visio file in the input folder.
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions (in inches).
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // If either dimension exceeds the threshold, disable dynamic gluing.
                        if (width > sizeThreshold || height > sizeThreshold)
                        {
                            shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                        }
                    }
                }

                // Build the output file path.
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string outPath = Path.Combine(outputFolder, $"{fileName}_NoGlue.vsdx");

                // Save the modified diagram.
                diagram.Save(outPath, SaveFileFormat.Vsdx);

                // Release resources.
                diagram.Dispose();
            }
        }
    }