using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GameShop.Models
{
    public class ExporterFactory
    {
        public enum ExporterType {Json, CSV};

        public static Iexporter GetFactory(ExporterType type)
        {
            if (type == ExporterType.Json)
            {
                Iexporter t = new JsonExporter();
                return t; 
            }
            else
            {
                Iexporter t = new CSVexporter();
                return t; 
            }
        }
    }
}