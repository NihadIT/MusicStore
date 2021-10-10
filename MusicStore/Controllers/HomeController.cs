using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Models;


namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        MusicStoreEntities storeDB;
        public HomeController(MusicStoreEntities storeDB)
        {
            this.storeDB = storeDB;
        }

        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }
        private List<Album> GetTopSellingAlbums(int count)
        {
            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }
}
