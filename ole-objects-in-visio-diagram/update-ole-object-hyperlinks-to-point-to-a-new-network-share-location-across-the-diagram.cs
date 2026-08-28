using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // New network share base path (ensure it ends with a backslash)
                string newNetworkShare = @"\\newshare\folder\";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object (Foreign) and contains an embedded object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Ensure the shape has a Hyperlinks collection
                            if (shape.Hyperlinks != null)
                            {
                                // Update each hyperlink address to point to the new network share
                                foreach (Hyperlink link in shape.Hyperlinks)
                                {
                                    // Preserve the original file name (if any) and prepend the new share path
                                    string originalAddress = link.Address?.Value ?? string.Empty;
                                    string fileName = Path.GetFileName(originalAddress);
                                    link.Address.Value = newNetworkShare + fileName;
                                }
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