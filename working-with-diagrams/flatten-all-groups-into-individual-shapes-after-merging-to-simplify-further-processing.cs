using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Flatten all group shapes on each page
            foreach (Page page in diagram.Pages)
            {
                FlattenGroups(page);
            }

            // Save the flattened diagram
            string outputPath = "output_flattened.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Recursively ungroup all group shapes on a page
    static void FlattenGroups(Page page)
    {
        bool groupsRemaining = true;
        while (groupsRemaining)
        {
            groupsRemaining = false;
            var groupIds = new System.Collections.Generic.List<long>();

            // Collect IDs of current group shapes
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupIds.Add(shape.ID);
                }
            }

            // Ungroup each collected group shape
            foreach (long id in groupIds)
            {
                Shape groupShape = page.Shapes.GetShape(id);
                if (groupShape != null && groupShape.Type == TypeValue.Group)
                {
                    groupShape.Ungroup();
                    groupsRemaining = true; // New groups may appear after ungrouping
                }
            }
        }
    }
}
