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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page
            // Parameters: PinX, PinY, master name, isCalculate (bool)
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape instance
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // -------------------------------------------------
            // Create a custom StyleSheet that defines paragraph spacing
            // -------------------------------------------------
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1; // assign a unique ID

            // Define paragraph spacing (in inches)
            Para para = new Para();
            para.SpBefore.Value = 0.1;   // space before paragraph
            para.SpAfter.Value = 0.1;    // space after paragraph
            para.SpLine.Value = 0.2;     // line spacing

            // Add the paragraph definition to the stylesheet
            customStyle.Paras.Add(para);

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(customStyle);

            // Apply the stylesheet to the shape's text formatting
            shape.TextStyle = customStyle;

            // -------------------------------------------------
            // Verify that the paragraph spacing has been applied
            // -------------------------------------------------
            if (shape.Paras.Count == 0)
            {
                throw new Exception("Paragraph collection is empty; style was not applied.");
            }

            Para appliedPara = shape.Paras[0];

            if (Math.Abs(appliedPara.SpBefore.Value - 0.1) > 0.0001)
                throw new Exception($"SpBefore mismatch. Expected 0.1, got {appliedPara.SpBefore.Value}");

            if (Math.Abs(appliedPara.SpAfter.Value - 0.1) > 0.0001)
                throw new Exception($"SpAfter mismatch. Expected 0.1, got {appliedPara.SpAfter.Value}");

            if (Math.Abs(appliedPara.SpLine.Value - 0.2) > 0.0001)
                throw new Exception($"SpLine mismatch. Expected 0.2, got {appliedPara.SpLine.Value}");

            Console.WriteLine("Paragraph spacing applied and verified successfully.");

            // Save the diagram to a VSDX file
            diagram.Save("StyledDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
