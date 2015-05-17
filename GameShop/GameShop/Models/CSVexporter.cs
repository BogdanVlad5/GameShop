using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameShop.Factory;

namespace GameShop.Models
{
    public class CSVexporter : Iexporter
    {

        public void createFile(List<Game> g)
        {
            var export = new CSV();
            foreach (Game gg in g){
                export.AddRow();
                export["ID"] = gg.ID;
                export["Name"] = gg.Name;
                export["Release Date"] = gg.ReleaseDate;
                export["Genre"] = gg.Genre;
                export["Details"] = gg.Details;
                export["Price"] = gg.Price;
            }
            export.ExportToFile("D:\\games.csv");
        }
    }
}