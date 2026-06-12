using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Path for the diagram file
            const string filePath = "WatermarkedDiagram.vsdx";

            // Create a new diagram (empty)
            using (Diagram diagram = new Diagram())
            {
                // Ensure at least one page exists
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Add a watermark to every page
                const string watermarkText = "CONFIDENTIAL";
                const string fontName = "Arial";
                const string fontColor = "#CCCCCC"; // light gray
                const double fontSizeInches = 0.5;   // approx 36 points

                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a text shape that covers the whole page
                    // PinX and PinY are the lower‑left corner of the text box
                    page.AddText(0, 0, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInches);
                }

                // Save the diagram to a file (VSDX format)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

            // Load the saved diagram for validation
            using (Diagram loadedDiagram = new Diagram(filePath))
            {
                // Validate that each page contains the watermark text
                foreach (Page page in loadedDiagram.Pages)
                {
                    bool watermarkFound = false;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Get plain text of the shape
                        string shapeText = shape.Text.Value.Text;

                        if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("CONFIDENTIAL"))
                        {
                            watermarkFound = true;
                            break;
                        }
                    }

                    if (!watermarkFound)
                    {
                        throw new Exception($"Watermark not found on page '{page.Name}' (ID: {page.ID}).");
                    }
                }

                Console.WriteLine("Watermark validation succeeded: present on all pages.");
            }
        }
    }