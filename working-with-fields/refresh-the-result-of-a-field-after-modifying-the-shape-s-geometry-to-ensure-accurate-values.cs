using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (use the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape whose geometry you want to modify (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Example geometry modification: change the shape's width
            // (Units are in inches; adjust the value as required)
            shape.XForm.Width.Value = 2.0;

            // Refresh the shape's data so that any fields reflecting geometry are updated
            shape.RefreshData();

            // If the shape is part of a timeline, you could also refresh the timeline:
            // TimelineHelper timelineHelper = new TimelineHelper(diagram);
            // timelineHelper.RefreshTimeLine();

            // Save the updated diagram (use the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
