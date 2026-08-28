using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the source Visio diagram
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Ensure there is at least a second page to clone
                    if (diagram.Pages.Count < 2)
                    {
                        throw new Exception("The diagram does not contain a second page to clone.");
                    }

                    // Get the source page (page index is zero‑based, so index 1 is page two)
                    Page sourcePage = diagram.Pages[1];

                    // Determine the maximum existing page ID to assign a unique ID to the new page
                    int maxPageId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    // Create a new blank page
                    Page clonedPage = new Page();
                    clonedPage.ID = maxPageId + 1;
                    clonedPage.Name = "ClonedPage";

                    // Add the new page to the diagram
                    diagram.Pages.Add(clonedPage);

                    // Copy the contents of the source page into the new page
                    clonedPage.Copy(sourcePage);

                    // -------------------------------------------------
                    // Create a new stylesheet to apply to the cloned page
                    // -------------------------------------------------
                    StyleSheet newStyle = new StyleSheet();
                    newStyle.ID = diagram.StyleSheets.Count + 1;
                    newStyle.Name = "NewStyle";

                    // Example style settings (customize as needed)
                    // Set line color to red
                    newStyle.Line.LineColor.Value = "#FF0000";
                    // Set line weight
                    newStyle.Line.LineWeight.Value = 0.02;
                    // Set fill foreground color to light blue
                    newStyle.Fill.FillForegnd.Value = "#ADD8E6";
                    // Set a simple character style (e.g., bold)
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0;
                    ch.Style.Value = StyleValue.Bold;
                    newStyle.Chars.Add(ch);

                    // Add the stylesheet to the diagram's collection
                    diagram.StyleSheets.Add(newStyle);

                    // Apply the new stylesheet to the cloned page (line, fill, and text styles)
                    clonedPage.ApplyStyle(newStyle.ID, newStyle.ID, newStyle.ID);

                    // Save the modified diagram
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }