using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramExtensions
{
    /// <summary>
    /// Injects a predefined style sheet into the SolutionXML collection of the specified diagram.
    /// </summary>
    /// <param name="diagram">The diagram whose SolutionXML collection will receive the style sheet.</param>
    public static void InjectPredefinedStyleSheet(this Diagram diagram)
    {
        // Define the XML representation of the predefined style sheet.
        // This XML should conform to the Visio ShapeSheet schema.
        const string styleSheetXml =
            @"<StyleSheet Name=""MyPredefinedStyle"" ID=""1"">
                <Fill>
                    <FillForegnd>#FF0000</FillForegnd>
                    <FillPattern>1</FillPattern>
                </Fill>
                <Line>
                    <LineWeight>0.5 pt</LineWeight>
                    <LineColor>#0000FF</LineColor>
                </Line>
                <TextStyle>
                    <Font>Arial</Font>
                    <Size>10 pt</Size>
                    <Color>#000000</Color>
                </TextStyle>
              </StyleSheet>";

        // Create a new SolutionXML instance using the constructor that accepts name and XML value.
        var solutionXml = new SolutionXML("PredefinedStyleSheet", styleSheetXml);

        // Add the SolutionXML to the diagram's SolutionXMLs collection.
        diagram.SolutionXMLs.Add(solutionXml);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramExtensions.InjectPredefinedStyleSheet(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
