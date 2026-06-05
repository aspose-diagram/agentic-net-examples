using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path where the filtered Visio file will be saved
                string outputPath = "output.vsdx";

                // Size threshold in megabytes (e.g., 5 MB)
                double sizeThresholdMb = 5.0;
                long sizeThresholdBytes = (long)(sizeThresholdMb * 1024 * 1024);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Access the embedded OLE binary data
                            byte[] oleData = shape.ForeignData.ObjectData;

                            // If data exists and exceeds the size limit, mark the shape for deletion
                            if (oleData != null && oleData.Length > sizeThresholdBytes)
                            {
                                shape.Del = BOOL.True;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }