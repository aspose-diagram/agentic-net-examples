using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // Get the full plain text of the shape
                            string fullText = shape.Text.Value.Text;

                            // Split the text into paragraphs (lines)
                            string[] paragraphs = fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                            // Clear existing text runs and character formatting
                            shape.Text.Value.Clear();
                            shape.Chars.Clear();

                            int charPos = 0; // Tracks the character index within the shape's text

                            for (int i = 0; i < paragraphs.Length; i++)
                            {
                                string paragraph = paragraphs[i];

                                // Determine font size based on paragraph length
                                // Base size 10pt, increase 2pt for each 20 characters
                                double baseSizePt = 10.0;
                                double sizePt = baseSizePt + (paragraph.Length / 20) * 2.0;
                                double sizeInches = sizePt / 72.0; // Convert points to inches

                                // Insert a character position marker (Cp) at the start of the paragraph
                                shape.Text.Value.Add(new Cp(charPos));

                                // Add the paragraph text as a Txt run
                                shape.Text.Value.Add(new Txt(paragraph));

                                // If not the last paragraph, add a line break
                                if (i < paragraphs.Length - 1)
                                {
                                    shape.Text.Value.Add(new Txt("\n"));
                                }

                                // Create a Char object to apply font size (and optional font name) to this paragraph
                                Aspose.Diagram.Char ch = new Aspose.Diagram.Char
                                {
                                    IX = charPos,                     // Index of the first character of the paragraph
                                    Size = { Value = sizeInches },    // Font size in inches
                                    FontName = { Value = "Calibri" } // Example font name
                                };

                                // Add the Char formatting to the shape
                                shape.Chars.Add(ch);

                                // Update the character position for the next paragraph
                                // +1 accounts for the line break added (if any)
                                charPos += paragraph.Length + (i < paragraphs.Length - 1 ? 1 : 0);
                            }

                            // Refresh shape data to apply changes
                            shape.RefreshData();
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }