using ResultsAlgo;
using ResultsAlgo.Classes;
using System;
using System.Collections.Generic;

namespace PredictScoreDelta
{
    public class PredictDelta : StatsBase
    {
        public float GetScoreDelta(ResultsAlgoData statsData, Team? homeTeam, Team? awayTeam, DateTime? date)
        {
            var stats = new Stats(statsData.Fixtures);
            var aveHomeScoreLastFiveHomeResults = stats.GetAverageHomeScoreLastFiveHomeGames(statsData, homeTeam, date);
            var aveAwayScoreLastFiveAwayResults = stats.GetAverageAwayScoreLastFiveAwayGames(statsData, awayTeam, date);

            var scoreDeltalastFiveHomeResults = stats.GetAverageScoreDeltaLastFiveHomeGames(statsData, homeTeam, date);
            var scoreDeltalastFiveAwayResults = stats.GetAverageScoreDeltaLastFiveAwayGames(statsData, awayTeam, date);

            var lastTwoResultsBtwnTeams = stats.GetAverageScoreDeltaOfLastTwoResultsBetweenTeams(statsData,
                homeTeam, awayTeam, date);

            var prediction = (aveHomeScoreLastFiveHomeResults - aveAwayScoreLastFiveAwayResults
                                                    + scoreDeltalastFiveHomeResults
                                                    + scoreDeltalastFiveAwayResults
                                                    + lastTwoResultsBtwnTeams)
                                                    / 4;
           
            //return prediction;
            return ApplySpreadChangeForDate(prediction, (DateTime)date);
        }

        public float ApplySpreadChangeForDate(float prediction, DateTime date)  //weather factor
        {
            if (date.Month == 9 || date.Month == 3 || date.Month == 4)
            {
                return prediction = prediction * (float)0.35;
            }
            if (date.Month == 5 || date.Month == 6)
            {
                return prediction = prediction * (float)0.45;
            }

            if (date.Month == 2 || date.Month == 11 || date.Month == 10)
            {
                return prediction = prediction * (float)0.05;
            }
            if (date.Month == 1 || date.Month == 12)
            {
                return prediction = prediction * (float)0.025;
            }
            return prediction;
        }

        public List<Fixture> GetRangeOfFixtures(ResultsAlgoData statsData, DateTime startDate, DateTime endDate)
        {
            var selectedFixtures = new List<Fixture>();

            foreach(var fixture in statsData.Fixtures)
            {
                if(fixture.FixtureDate >= startDate && fixture.FixtureDate <= endDate)
                {
                    selectedFixtures.Add(fixture);
                }
            }
            return selectedFixtures;
        }

        public List<Fixture> GetPredictions (List<Fixture> selectedFixtures, ResultsAlgoData statsData)
        {
            var fixturesWithPredictions = new List<Fixture>();
            foreach(var fixture in selectedFixtures)
            {
                var predictedDelta = GetScoreDelta(statsData, fixture.HomeTeam, fixture.AwayTeam, fixture.FixtureDate);

                var predictedVersusActualDelta = fixture.ScoreDelta - predictedDelta;

                var predictedResult = predictedDelta > 0 ? Result.HomeWin : Result.HomeLoss;

                PredictionSuccess predictionSuccess;

                if (predictedResult == Result.HomeWin && predictedVersusActualDelta >= 0)
                {
                    predictionSuccess = PredictionSuccess.Success;
                }
                else if (predictedResult == Result.HomeLoss && predictedVersusActualDelta <= 0)
                {
                    predictionSuccess = PredictionSuccess.Success;
                }
                else
                {
                    predictionSuccess = PredictionSuccess.Fail;
                }

                fixturesWithPredictions.Add(new Fixture()
                {
                    FixtureReference = fixture.FixtureReference,
                    FixtureDate = fixture.FixtureDate,
                    HomeTeam = fixture.HomeTeam,
                    HomeScore = fixture.HomeScore,
                    AwayTeam = fixture.AwayTeam,
                    AwayScore = fixture.AwayScore,
                    ScoreDelta = fixture.ScoreDelta,
                    result = fixture.result,
                    PredictedDelta = predictedDelta,
                    ActualVersusPredictedDelta = predictedVersusActualDelta,
                    PredictedResult = predictedResult,
                    PredictionSuccess = predictionSuccess
                });
              }
            return fixturesWithPredictions;
        }
    }
}
