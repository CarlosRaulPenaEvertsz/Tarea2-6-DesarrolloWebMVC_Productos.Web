using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesarrolloWebMVC_Productos.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Models.RegistroProducto rp = new Models.RegistroProducto();

            return View(rp.RecuperarTodos());
        }

        public ActionResult Grabar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Grabar(FormCollection collection)
        {

            Models.RegistroProducto rp = new Models.RegistroProducto();

            Models.Producto producto = new Models.Producto
            {
                Descripcion= collection["Descripcion"].ToString(),
                Tipo = collection["Tipo"].ToString(),
                Precio = float.Parse(collection["Precio"].ToString())
            };

            rp.GrabarProducto(producto);
            return RedirectToAction("Index");
        }


        public ActionResult Borrar(string Prod)
        {
            Models.RegistroProducto producto = new Models.RegistroProducto();
            producto.Borrar(Prod);
            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(string Prod)
        {

            Models.RegistroProducto producto = new Models.RegistroProducto();
            Models.Producto prod = producto.Recuperar(Prod);

            return View(prod);
        }

        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            Models.RegistroProducto producto = new Models.RegistroProducto();
            Models.Producto Prod = new Models.Producto
            {
                Descripcion = collection["Descripcion"].ToString(),
                Tipo = collection["Tipo"].ToString(),
                Precio = float.Parse(collection["Precio"].ToString())
            };
            producto.Modificar(Prod);

            return RedirectToAction("Index");
        }

        





    }
}