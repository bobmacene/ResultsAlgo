using System;
using System.Collections.Generic;

namespace ResultsAlgo.Classes
{
    public interface ResultsAlgoData
    {
        List<StatsBase.Fixture> Fixtures { get; }
        Dictionary<StatsBase.Team?, List<StatsBase.Fixture>> HomeResults { get; }
        Dictionary<StatsBase.Team?, List<StatsBase.Fixture>> AwayResults { get; }
    }

    public class StatsBase
    {  
        public interface StatsDate
        {
            DateTime Date { get; }
        }

        [Serializable]
        public class Fixture
        {
            public string FixtureReference { get; set; }
            public DateTime? FixtureDate { get; set; }
            public Result? result { get; set; }
            public Team? HomeTeam { get; set; }
            public int HomeScore { get; set; }
            public Team? AwayTeam { get; set; }
            public int AwayScore { get; set; }
            public int ScoreDelta { get; set; }
            public double PredictedDelta { get; set; }
            public float ActualVersusPredictedDelta { get; set; }
            public Result? PredictedResult { get; set; }
            public PredictionSuccess? PredictionSuccess { get; set; }

            public override string ToString()
            {
                return $"{FixtureReference}, {FixtureDate}, {result}, {HomeTeam}, {HomeScore}, {AwayTeam}, {AwayScore}, {PredictedDelta}, {ActualVersusPredictedDelta}, {PredictedResult}, {PredictionSuccess}";
                 
            }
        }
        public enum Result { HomeWin = 1, Draw = 0, HomeLoss = -1, NoPrectiction = -2 }
        public enum PredictionSuccess { Success = 1, Fail = 0 }
       
        public enum Team
        {
            BenettonTreviso, CardiffBlues, Connacht, Edinburgh,
            GlasgowWarriors, Leinster, Munster, Dragons,
            Ospreys, Scarlets, Ulster, Zebre, Aironi,
            //Toulon, LaRochelle, BordeauxBegles, Grenoble, Montpellier, Toulouse, StadeFrancais, ClermontAuvergne,
            //Agen, Oyonnax, Pau, Racing92, Brive, Castres, LyonAU, Bayonne
        }

        public class Teams
        {
            public List<Team> teams;
            public Teams()
            {
                teams = new List<Team>
                {
                    Team.BenettonTreviso, Team.CardiffBlues, Team.Connacht, Team.Edinburgh,
                    Team.GlasgowWarriors, Team.Leinster, Team.Munster, Team.Dragons,
                    Team.Ospreys, Team.Scarlets, Team.Ulster, Team.Zebre, Team.Aironi,
                    //Team.Toulon, Team.LaRochelle, Team.BordeauxBegles, Team.Grenoble, Team.Montpellier,
                    //Team.Toulouse, Team.StadeFrancais, Team.ClermontAuvergne, Team.Agen, Team.Oyonnax,
                    //Team.Pau, Team.Racing92, Team.Brive, Team.Castres, Team.LyonAU, Team.Bayonne
                };
            }
        }
    }
}
//public class TeamStats
//{
//    public Team Team { get; set; }
//    public float HomeAverageScore { get; set; }
//    public float HomeAverageScoreLastfiveGames { get; set; }
//    public float AwayAverageScore { get; set; }
//    public float AwayAverageScoreLastfiveGames { get; set; }
//}