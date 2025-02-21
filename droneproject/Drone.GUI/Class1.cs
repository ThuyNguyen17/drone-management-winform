using System;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

public class RequestData
{
    [LoadColumn(0)]
    public string RequestText;

    [LoadColumn(1)]
    public string Shape;
}

public class ShapePrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedShape;
}

class Program
{
    static void Main(string[] args)
    {
        var context = new MLContext();

        // Load data
        var data = context.Data.LoadFromTextFile<RequestData>("data.csv", separatorChar: ',', hasHeader: true);

        // Split data into training and testing
        var split = context.Data.TrainTestSplit(data, testFraction: 0.2);

        // Data pipeline
        var pipeline = context.Transforms.Text.FeaturizeText("Features", nameof(RequestData.RequestText))
            .Append(context.Transforms.Conversion.MapValueToKey("Label", nameof(RequestData.Shape)))
            .Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        // Train model
        var model = pipeline.Fit(split.TrainSet);

        // Evaluate model
        var predictions = model.Transform(split.TestSet);
        var metrics = context.MulticlassClassification.Evaluate(predictions);
        Console.WriteLine($"MicroAccuracy: {metrics.MicroAccuracy}, MacroAccuracy: {metrics.MacroAccuracy}");

        // Test prediction
        var predictionEngine = context.Model.CreatePredictionEngine<RequestData, ShapePrediction>(model);
        var result = predictionEngine.Predict(new RequestData { RequestText = "Tạo một ngôi sao 5 cánh cạnh dài 5m" });
        Console.WriteLine($"Predicted Shape: {result.PredictedShape}");
    }
}
