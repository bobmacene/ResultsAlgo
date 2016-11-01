using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ResultsAlgo.Classes
{
    [Serializable]
    public class XmlBacking : StatsBase
    {
        public XmlBacking() {}

        //public List<Fixture> GetDataToSave(List<Fixture> dataToSave)
        //{
        //   return fixturesToSave = new List<Fixture>(dataToSave.ToList());
        //}
        public List<Fixture> fixturesToSave { get; set; } = new List<Fixture>();

        public XmlBacking(List<Fixture> dataToSave)
        {
            fixturesToSave = dataToSave;
        }

    }
}
