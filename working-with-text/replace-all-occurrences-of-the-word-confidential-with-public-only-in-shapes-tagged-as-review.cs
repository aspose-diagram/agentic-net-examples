using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a custom property (Prop) named "Review"
                    bool hasReviewTag = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "Review")
                        {
                            hasReviewTag = true;
                            break;
                        }
                    }

                    // If the shape is tagged as "Review", replace text occurrences
                    if (hasReviewTag)
                    {
                        // Iterate over each text run within the shape
                        foreach (var item in shape.Text.Value)
                        {
                            if (item is Txt txt && txt.Text != null)
                            {
                                // Replace "Confidential" with "Public"
                                if (txt.Text.Contains("Confidential"))
                                {
                                    txt.Text = txt.Text.Replace("Confidential", "Public");
                                }
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
