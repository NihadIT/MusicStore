using Microsoft.AspNetCore.Mvc;
using MusicStore.Models;
using MusicStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        MusicStoreEntities storeDB;

       public ShoppingCartController(MusicStoreEntities storeDB)
        {
            this.storeDB = storeDB;
        }
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
           
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(storeDB),
                CartTotal = cart.GetTotal(storeDB)
            };
            // storeDB.Entry(cart).Reference(a => a.).Load();
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedAlbum = storeDB.Albums
                .Single(album => album.AlbumId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum, storeDB);   //

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = storeDB.Carts
                .Single(item => item.RecordId == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id, storeDB);   //

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(storeDB),
                CartCount = cart.GetCount(storeDB),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount(storeDB);
            return PartialView("CartSummary");
        }
    }
}

