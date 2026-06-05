using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the input Visio file path.
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");

                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that have no Event section.
                        if (shape.Event == null)
                            continue;

                        bool hasCustomEvent = false;

                        // Check each supported event cell.
                        if (!string.IsNullOrWhiteSpace(shape.Event.EventXFMod?.Ufe?.F))
                        {
                            if (!hasCustomEvent)
                            {
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                                hasCustomEvent = true;
                            }
                            Console.WriteLine($"    EventXFMod: {shape.Event.EventXFMod.Ufe.F}");
                        }

                        if (!string.IsNullOrWhiteSpace(shape.Event.EventDblClick?.Ufe?.F))
                        {
                            if (!hasCustomEvent)
                            {
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                                hasCustomEvent = true;
                            }
                            Console.WriteLine($"    EventDblClick: {shape.Event.EventDblClick.Ufe.F}");
                        }

                        if (!string.IsNullOrWhiteSpace(shape.Event.EventDrop?.Ufe?.F))
                        {
                            if (!hasCustomEvent)
                            {
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                                hasCustomEvent = true;
                            }
                            Console.WriteLine($"    EventDrop: {shape.Event.EventDrop.Ufe.F}");
                        }

                        if (!string.IsNullOrWhiteSpace(shape.Event.EventMultiDrop?.Ufe?.F))
                        {
                            if (!hasCustomEvent)
                            {
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                                hasCustomEvent = true;
                            }
                            Console.WriteLine($"    EventMultiDrop: {shape.Event.EventMultiDrop.Ufe.F}");
                        }

                        if (!string.IsNullOrWhiteSpace(shape.Event.TheText?.Ufe?.F))
                        {
                            if (!hasCustomEvent)
                            {
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                                hasCustomEvent = true;
                            }
                            Console.WriteLine($"    TheText: {shape.Event.TheText.Ufe.F}");
                        }

                        if (!string.IsNullOrWhiteSpace(shape.Event.TheData?.Ufe?.F))
                        {
                            if (!hasCustomEvent)
                            {
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                                hasCustomEvent = true;
                            }
                            Console.WriteLine($"    TheData: {shape.Event.TheData.Ufe.F}");
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }