using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (SolutionXML)
            Diagram diagram = new Diagram("SolutionXML.vsdx");

            // ------------------------------------------------------------
            // Locate the target shape.
            // Here we assume the shape we want to augment has the ID 5
            // on the first page. Adjust the ID or page index as needed.
            // ------------------------------------------------------------
            long targetShapeId = 5;
            Shape targetShape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

            // ------------------------------------------------------------
            // Ensure a DataRecordSet exists; create one if the document has none.
            // ------------------------------------------------------------
            DataRecordSet dataRecordSet;
            if (diagram.DataRecordSets.Count == 0)
            {
                dataRecordSet = new DataRecordSet
                {
                    ID = 1,
                    Name = "CustomData"
                };
                diagram.DataRecordSets.Add(dataRecordSet);
            }
            else
            {
                dataRecordSet = diagram.DataRecordSets[0];
            }

            // ------------------------------------------------------------
            // Create a new Row that links the shape to the DataRecordSet.
            // RowID uses the next available ID from the DataRecordSet.
            // ------------------------------------------------------------
            Row newRow = new Row
            {
                RowID = dataRecordSet.NextRowID,
                ShapeID = targetShapeId,
                PageID = diagram.Pages[0].ID
            };

            // Add the row to the DataRecordSet's RowMaps collection.
            dataRecordSet.RowMaps.Add(newRow);

            // ------------------------------------------------------------
            // Optionally store a custom value directly in the shape's data fields.
            // This demonstrates adding a custom data element to the shape itself.
            // ------------------------------------------------------------
            targetShape.Data1 = "MyCustomValue";

            // Refresh the shape so that Visio recalculates any dependent data.
            targetShape.RefreshData();

            // ------------------------------------------------------------
            // Save the updated diagram back to disk.
            // ------------------------------------------------------------
            diagram.Save("SolutionXML_Updated.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
