using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                using (Diagram diagram = new Diagram())
                {
                    // Determine the next page ID
                    int maxPageId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    // Create a new background page
                    Page backgroundPage = new Page();
                    backgroundPage.ID = maxPageId + 1;
                    backgroundPage.Name = "BackgroundPage";
                    backgroundPage.Background = BOOL.True; // Mark as background page

                    // Add the background page to the diagram
                    diagram.Pages.Add(backgroundPage);

                    // Retrieve page dimensions
                    double pageWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;

                    // Add a rectangle shape that spans the entire page
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;
                    long shapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");
                    Shape bgShape = backgroundPage.Shapes.GetShape(shapeId);

                    // Apply a solid fill color and remove the border
                    bgShape.Fill.FillPattern.Value = 1;               // Solid fill
                    bgShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue background
                    bgShape.Line.LinePattern.Value = 0;              // No border
                    bgShape.SendToBack();                            // Ensure it stays behind other content

                    // Create a SolutionXML entry that references the new background page
                    SolutionXML solutionXml = new SolutionXML();
                    solutionXml.Name = "PageBackgroundInfo";
                    solutionXml.XmlValue = $"<Page id=\"{backgroundPage.ID}\" background=\"true\" />";
                    diagram.SolutionXMLs.Add(solutionXml);

                    // Save the diagram to a VSDX file
                    diagram.Save("DiagramWithBackground.vsdx", SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram created with background page and SolutionXML entry.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }