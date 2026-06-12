using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (placeholder for the provided load rule)
            // {LoadDiagram}
            // The loaded diagram should be assigned to the variable 'diagram'
            Diagram diagram = /* loaded diagram */ null;

            // Retrieve source and target shapes (example IDs: 1 and 2)
            Shape sourceShape = diagram.Pages[0].Shapes.GetShape(1);
            Shape targetShape = diagram.Pages[0].Shapes.GetShape(2);

            // Copy custom data cells from source to target using the Shape.Copy method (rule‑based)
            targetShape.Copy(sourceShape);

            // Verify that the custom data cells have been copied correctly
            bool dataCellsEqual = targetShape.Data1 == sourceShape.Data1 &&
                                  targetShape.Data2 == sourceShape.Data2 &&
                                  targetShape.Data3 == sourceShape.Data3;

            // Verify that custom properties (Props) have been copied correctly
            bool propsEqual = targetShape.Props.Count == sourceShape.Props.Count;
            if (propsEqual)
            {
                for (int i = 0; i < sourceShape.Props.Count; i++)
                {
                    if (targetShape.Props[i].Label != sourceShape.Props[i].Label ||
                        targetShape.Props[i].Value != sourceShape.Props[i].Value)
                    {
                        propsEqual = false;
                        break;
                    }
                }
            }

            Console.WriteLine($"Data cells copied correctly: {dataCellsEqual}");
            Console.WriteLine($"Custom properties copied correctly: {propsEqual}");

            // Save the modified diagram (placeholder for the provided save rule)
            // {SaveDiagram}

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
