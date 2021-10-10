using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class GenreMenu: ViewComponent
    {
        MusicStoreEntities storeDB;
        public GenreMenu(MusicStoreEntities storeDB)
        {
            this.storeDB = storeDB;
        }

        public IViewComponentResult Invoke()
        {
            var genres = storeDB.Genres.ToList();
            return View(genres);
        }
    }
}
