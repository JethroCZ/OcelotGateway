using SourceData.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceData
{
    public static class Database
    {
        public static ConcurrentDictionary<int, SkladModel> Sklady = new ConcurrentDictionary<int, SkladModel>();
        public static ConcurrentDictionary<int, ZasobyModel> Zasoby = new ConcurrentDictionary<int, ZasobyModel>();

        static Database()
        {
            Sklady.TryAdd(1, new SkladModel { Id = 1, Name = "Doma", Note = "Byt" });
            Sklady.TryAdd(2, new SkladModel { Id = 2, Name = "Prace", Note = "Kancl" });
            Zasoby.TryAdd(1, new ZasobyModel { Id = 1, Name = "Lahváč", SkladId = 1 });
            Zasoby.TryAdd(2, new ZasobyModel { Id = 2, Name = "Notebook", SkladId = 2 });
            Zasoby.TryAdd(3, new ZasobyModel { Id = 3, Name = "Monitor", SkladId = 1 });
        }

    }
}
