using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GameShop.Models
{
    public class JsonExporter : Iexporter
    {
        public void createFile(List<Game> g)
        {
            using (StreamWriter file = File.CreateText("D:\\games.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, g);
            }
        }
    }
}