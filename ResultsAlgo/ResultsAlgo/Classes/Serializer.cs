using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ResultsAlgo.Classes
{
    class Serializer
    {
        public void Save( string path, XmlBacking obj)
        {
            var serializer = new XmlSerializer(typeof(XmlBacking));

            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, obj);
            }
        }
        
    }
}
