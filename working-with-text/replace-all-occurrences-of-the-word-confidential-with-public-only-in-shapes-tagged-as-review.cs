using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramTagReplace <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    bool isReviewTagged = false;

                    // Check custom properties (Props) for a tag named "Tag" with value "Review".
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Tag" && prop.Value != null && prop.Value.Val == "Review")
                            {
                                isReviewTagged = true;
                                break;
                            }
                        }
                    }

                    // If the shape is tagged as "Review", replace text.
                    if (isReviewTagged && shape.Text != null && shape.Text.Value != null)
                    {
                        foreach (object item in shape.Text.Value)
                        {
                            if (item is Txt txt && txt.Text != null && txt.Text.Contains("Confidential"))
                            {
                                txt.Text = txt.Text.Replace("Confidential", "Public");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }