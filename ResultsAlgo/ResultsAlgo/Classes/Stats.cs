using System;
using System.Collections.Generic;
using System.Linq;
using ResultsAlgo.Classes;

namespace ResultsAlgo 
{
    public class Stats : StatsBase, ResultsAlgoData
    {
        public List<Fixture> Fixtures { get; } = new List<Fixture>();
        public Dictionary<Team?, List<Fixture>> HomeResults { get; set; } = new Dictionary<Team?, List<Fixture>>();
        public Dictionary<Team?, List<Fixture>> AwayResults { get; set; } = new Dictionary<Team?, List<Fixture>>();

        public Stats (List<Fixture> results)
        {
            Fixtures = results;
            HomeResults = GetHomeTeamResults(results);
            AwayResults = GetAwayTeamResults(results);
        }

        public static Dictionary<Team?, List<Fixture>> GetHomeTeamResults(List<Fixture> results)
        {
            var TeamResults = new Dictionary<Team?, List<Fixture>>(results.Count / new Teams().teams.Count);

            foreach (var fixture in results)
            {
                if (TeamResults.ContainsKey(fixture.HomeTeam))
                {
                    TeamResults[fixture.HomeTeam].Add(fixture);
                }
                else
                {
                    TeamResults.Add(fixture.HomeTeam, new List<Fixture> { fixture });
                }
            }
            return TeamResults;
        }

        public static Dictionary<Team?, List<Fixture>> GetAwayTeamResults(List<Fixture> results)
        {
            var TeamResults = new Dictionary<Team?, List<Fixture>>(results.Count / new Teams().teams.Count);

            foreach (var fixture in results)
            {
                if (TeamResults.ContainsKey(fixture.AwayTeam))
                {
                    TeamResults[fixture.AwayTeam].Add(fixture);
                }
                else
                {
                    TeamResults.Add(fixture.AwayTeam, new List<Fixture> { fixture });
                }
            }
            return TeamResults;
        }

        public float GetAverageHomeScoreLastFiveHomeGames(ResultsAlgoData raData, Team? team, DateTime? date)
        {
            return (float)
                raData.HomeResults[team].Where(X => X.FixtureDate <= date)
                .Take(5)
                .Select(x => x.HomeScore)
                .Average();
        }

        public float GetAverageScoreDeltaLastFiveHomeGames(ResultsAlgoData raData, Team? team, DateTime? date)
        {
            return (float)
                raData.HomeResults[team].Where(X => X.FixtureDate <= date)
                .Take(5)
                .Select(x => x.ScoreDelta)
                .Average();
        }

        public float GetAverageAwayScoreLastFiveAwayGames(ResultsAlgoData raData, Team? team, DateTime? date)
        {
            return (float)
                raData.AwayResults[team].Where(X => X.FixtureDate <= date)
                .Take(5)
                .Select(x => x.AwayScore)
                .Average();
        }

        public float GetAverageScoreDeltaLastFiveAwayGames(ResultsAlgoData raData, Team? team, DateTime? date)
        {
            return (float)
                raData.AwayResults[team].Where(X => X.FixtureDate <= date)
                .Take(5)
                .Select(x => x.ScoreDelta)
                .Average();
        }
        public float GetAverageScoreDeltaOfLastTwoResultsBetweenTeams(ResultsAlgoData raData, 
            Team? homeTeam, Team? awayTeam, DateTime? date)
        {
            return (float) 
                raData.HomeResults[homeTeam].Where(X => (X.AwayTeam == awayTeam
                && X.FixtureDate <= date))
                .Take(2)
                .Select(x => x.ScoreDelta)
                .Average();
        }

        public IEnumerable<Fixture> Test(ResultsAlgoData raData,
            Team? homeTeam, Team? awayTeam, DateTime? date)
        {
            return
                raData.HomeResults[homeTeam].Where(X => (X.AwayTeam == awayTeam
                && X.FixtureDate <= date))
                .Take(2);
        }
    }
}

