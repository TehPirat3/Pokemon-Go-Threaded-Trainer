using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface ILog
    {
        void Log_(int id, Color color, String message);
    }
}
