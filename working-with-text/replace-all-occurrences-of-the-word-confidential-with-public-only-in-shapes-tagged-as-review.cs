using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine if the shape is tagged as 'Review' via a user-defined field named "Tag"
                        bool isReviewTagged = false;
                        foreach (User userField in shape.Users)
                        {
                            // Compare the user field name and its value
                            if (string.Equals(userField.NameU, "Tag", StringComparison.OrdinalIgnoreCase) &&
                                userField.Value != null &&
                                string.Equals(userField.Value.Val, "Review", StringComparison.OrdinalIgnoreCase))
                            {
                                isReviewTagged = true;
                                break;
                            }
                        }

                        // If the shape is not tagged as 'Review', skip it
                        if (!isReviewTagged)
                        {
                            continue;
                        }

                        // Replace occurrences of 'Confidential' with 'Public' in the shape's text runs
                        foreach (FormatTxt fmtTxt in shape.Text.Value)
                        {
                            Txt txtRun = fmtTxt as Txt;
                            if (txtRun != null && txtRun.Text != null && txtRun.Text.Contains("Confidential"))
                            {
                                txtRun.Text = txtRun.Text.Replace("Confidential", "Public");
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}