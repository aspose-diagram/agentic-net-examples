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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape is tagged as "Review"
                        bool isReviewTag = false;
                        foreach (Prop prop in shape.Props)
                        {
                            // Assuming a custom property named "Tag" holds the tag value
                            if (prop.Name == "Tag" && prop.Value != null && prop.Value.Val == "Review")
                            {
                                isReviewTag = true;
                                break;
                            }
                        }

                        // If the shape is tagged, replace "Confidential" with "Public"
                        if (isReviewTag)
                        {
                            shape.ReplaceText("Confidential", "Public");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Replacement completed. Diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }