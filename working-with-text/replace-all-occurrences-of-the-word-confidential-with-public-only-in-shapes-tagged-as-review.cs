using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine whether the shape is tagged with the name "Review".
                        bool isReviewTagged = false;
                        foreach (User user in shape.Users)
                        {
                            if (user.NameU != null && user.NameU.Equals("Review", StringComparison.OrdinalIgnoreCase))
                            {
                                isReviewTagged = true;
                                break;
                            }
                        }

                        // If the shape is not tagged as "Review", skip it.
                        if (!isReviewTagged)
                            continue;

                        // Ensure the shape has a Text object before processing.
                        if (shape.Text == null || shape.Text.Value == null)
                            continue;

                        // Iterate through each text run (FormatTxt) within the shape.
                        foreach (FormatTxt fmt in shape.Text.Value)
                        {
                            Txt txt = fmt as Txt;
                            if (txt != null && txt.Text != null && txt.Text.Contains("Confidential"))
                            {
                                // Replace the word "Confidential" with "Public".
                                txt.Text = txt.Text.Replace("Confidential", "Public");
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}