using System.Collections.Generic;
using System.Collections.Concurrent;

namespace KoyKurtarmaOyunu.Models
{
    public class GameState
    {
        // Kurtarılması gereken köyler kuyruğu
        public Queue<Koy> KurtarilacakKoyler { get; set; }

        // Kurtarılan köyler listesi
        public List<Koy> KurtarilanKoyler { get; set; }

        // Oyuncunun çantası
        public Canta OyuncuCantasi { get; set; }

        // Mevcut köy (oyuncunun şu anda olduğu köy)
        public Koy MevcutKoy { get; set; }

        public GameState()
        {
            KurtarilacakKoyler = new Queue<Koy>();
            KurtarilanKoyler = new List<Koy>();
            OyuncuCantasi = new Canta();
        }
    }
}
