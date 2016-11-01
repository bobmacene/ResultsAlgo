using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResultsAlgo.Classes;
using System.Threading.Tasks;
using PredictScoreDelta;
using System.IO;

namespace ResultsAlgo
{
    class Program : StatsBase
    {
        static void Main(string[] args)
        {
            var results = new Results();
            var fixtures = results.AddResults();
            ResultsAlgoData statsData = new Stats(fixtures);
            var stats = new Stats(fixtures);

            var prediction = new PredictDelta();
            var selectedFixtures = prediction.GetRangeOfFixtures(statsData, new DateTime(2014, 08, 01), 
                new DateTime(2016, 10, 01));

            var fixturesWithPredictions = prediction.GetPredictions(selectedFixtures, statsData);

            var report = new StringBuilder();

            foreach(var fixture in fixturesWithPredictions)
            {
                var line = fixture.ToString();
                report.AppendLine(fixture.ToString());
            }

            var date = DateTime.Now;
            var filename = "Predictions" + date.ToString("_yyyy.MM.dd_HH.mm.ss") + ".Csv";
            var path = Path.Combine(@"C:\Users\TEMP\", filename);

            File.WriteAllText(path, report.ToString());

            var totalNumberOfFixturesWithPredictions = fixturesWithPredictions.Count;
            var totalPredictionSuccesses = fixturesWithPredictions.Where(x => x.PredictionSuccess == PredictionSuccess.Success).Count();
            var totalPredictionFails = fixturesWithPredictions.Where(x => x.PredictionSuccess == PredictionSuccess.Fail).Count();
            Console.WriteLine("SuccessRatio: " + totalPredictionSuccesses / (float)totalNumberOfFixturesWithPredictions);
        }
    }
}
