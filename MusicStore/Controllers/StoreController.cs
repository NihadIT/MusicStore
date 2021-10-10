using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicStore.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        MusicStoreEntities storeDb;
        public StoreController(MusicStoreEntities storeDb)
        {
            this.storeDb = storeDb;
        }

        public ActionResult Index()
        {
            var genres = storeDb.Genres.ToList();
            return View(genres);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string genre)
        {
            var genreModel = storeDb.Genres.Include("Albums")
            .Single(g => g.Name == genre);

            return View(genreModel);
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var album = storeDb.Albums.Find(id);
            storeDb.Entry(album).Reference(a => a.Genre).Load();
            storeDb.Entry(album).Reference(a => a.Artist).Load();

            return View(album);
        }
       

    }
}
