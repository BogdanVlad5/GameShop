using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameShop.Models;

namespace GameShop.Models
{
    public interface Iexporter
    {
        void createFile(List<Game> g);
    }
}
