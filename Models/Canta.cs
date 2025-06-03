using System;
using System.Collections.Generic;
using System.Linq;

namespace KoyKurtarmaOyunu.Models
{
    public class Canta
    {
        private LinkedList<Item> envanter = new LinkedList<Item>();
        public int MaksimumKapasite { get; } = 10;

        public int MevcutKapasite => envanter.Count;

        // Çantaya öğe ekle
        public bool Push(Item item)
        {
            if (MevcutKapasite >= MaksimumKapasite)
            {
                return false; // Kapasite dolu
            }
            envanter.AddLast(item);
            return true;
        }

        // Çantadan son öğeyi çıkar
        public Item Pop()
        {
            if (envanter.Count == 0)
                return null;

            var item = envanter.Last.Value;
            envanter.RemoveLast();
            return item;
        }

        // Çantadaki tüm öğeleri liste olarak al
        public List<Item> GetItems()
        {
            return envanter.ToList();
        }

        // Belirli bir öğeyi arama (örnek: ada göre)
        public bool Contains(string itemAdi)
        {
            return envanter.Any(i => i.Ad.Equals(itemAdi, StringComparison.OrdinalIgnoreCase));
        }

        // Belirli öğeyi çıkarma
        public bool Remove(string itemAdi)
        {
            var node = envanter.First;
            while (node != null)
            {
                if (node.Value.Ad.Equals(itemAdi, StringComparison.OrdinalIgnoreCase))
                {
                    envanter.Remove(node);
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        // Öğeyi kullan ve çantadan çıkar
        public bool UseItem(string itemAdi)
        {
            var node = envanter.First;
            while (node != null)
            {
                if (node.Value.Ad.Equals(itemAdi, StringComparison.OrdinalIgnoreCase))
                {
                    envanter.Remove(node);
                    return true; // Başarıyla kullanıldı
                }
                node = node.Next;
            }
            return false; // Bulunamadı
        }
    }
}

