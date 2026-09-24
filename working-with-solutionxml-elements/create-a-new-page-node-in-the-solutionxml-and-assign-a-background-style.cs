using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Determine the next available page ID
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
                backgroundPage.Background = BOOL.True; // Mark as a background page

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Retrieve page dimensions
                double pageWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a rectangle shape that covers the entire page
                long shapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);
                Shape bgShape = backgroundPage.Shapes.GetShape(shapeId);

                // Apply solid fill color and remove border
                bgShape.Fill.FillPattern.Value = 1;               // Solid fill
                bgShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue background
                bgShape.Line.LinePattern.Value = 0;              // No border

                // Send the shape to the back so other content appears above it
                bgShape.SendToBack();

                // Link the first (foreground) page to the new background page
                if (diagram.Pages.Count > 0)
                {
                    Page foregroundPage = diagram.Pages[0];
                    foregroundPage.BackPage = backgroundPage;
                }

                // Add a SolutionXML entry describing the background page
                SolutionXML solutionXml = new SolutionXML();
                solutionXml.Name = "PageBackgroundInfo";
                solutionXml.XmlValue = $"<BackgroundPage id=\"{backgroundPage.ID}\" name=\"{backgroundPage.Name}\" />";
                diagram.SolutionXMLs.Add(solutionXml);

                // Save the diagram to a VSDX file
                diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }