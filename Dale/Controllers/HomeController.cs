using Dale.Models;
using DaleCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Dale.Controllers
{
    public class HomeController : Controller
    {

        static HttpClient client = new HttpClient();
        private static List<DetalleVenta> detalleVentas = new List<DetalleVenta>();
        private static Cliente clienteSesion = new Cliente();
        private static VentaViewModel VentaSesion = new VentaViewModel();
        
        public int Number { get; set; }
        public ActionResult Index()
        {
            List<Producto> productos = ListaProductos();
            return View(productos);
        }

        public ActionResult About()
        {
            List<Cliente> clientes = ListaClientes();
            return View(clientes);
        }

        public ActionResult Contact()
        {
            detalleVentas = new List<DetalleVenta>();
            List<Venta> ventas = ListaVentas();
            return View(ventas);
        }
        /// <summary>
        /// Metodo vista agg producto
        /// </summary>
        /// <returns></returns>
        public ActionResult Producto()
        {
            
            return View();
        }
        /// <summary>
        /// Metodo vista agg cliente
        /// </summary>
        /// <returns></returns>
        public ActionResult Cliente()
        {

            return View();
        }
        /// <summary>
        /// Metodo vista agg venta
        /// </summary>
        /// <returns></returns>
        public ActionResult Venta()
        {
            VentaViewModel venta = new VentaViewModel();
            venta.Clientes = ListaClientes().Select(option => new SelectListItem
            {
                Text = option.Cedula.ToString(),
                Value = option.Id.ToString()
            }); 
            venta.Productos = ListaProductos().Select(option => new SelectListItem
            {
                Text = option.Nombre.ToString(),
                Value = option.Id.ToString()
            });
            detalleVentas = new List<DetalleVenta>();
            venta.DetallesVenta = detalleVentas;
            venta.DetalleVenta = new DetalleVenta();
            VentaSesion = new VentaViewModel();
            return View(venta);
        }

        /// <summary>
        /// Metodo agrega y actualiza productos
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ActionResult NuevoProducto(Producto producto)
        {
            bool agg = false;
            List<Producto> productos = new List<Producto>();
            try
            {
                if(producto.Id == 0)
                {
                    agg = AddProducto(producto);
                }
                else
                {
                    agg = UpdateProducto(producto);
                }
                
                if (agg)
                {
                   productos = ListaProductos();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Index", productos);
        }
        /// <summary>
        /// Metodo vista para editar producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditarProducto(int id)
        {
            try
            {
                var producto = ListaProductos().Where(s => s.Id == id).FirstOrDefault();
                return View("Producto", producto);
            }
            catch(Exception ex)
            {
                throw ex;
                
            }
        }
        /// <summary>
        /// Metodo para eliminar producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EliminarProducto(int id)
        {
            bool agg = false;
            List<Producto> productos = new List<Producto>();
            try
            {
                var producto =  ListaProductos().Where(s=>s.Id == id).FirstOrDefault();
                var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44358/api/Producto/DeleteProducto");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(JsonConvert.SerializeObject(producto,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                }), Encoding.UTF8, "application/json");
                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
                if (agg)
                {
                    productos = ListaProductos();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Index", productos);
        } 
        /// <summary>
        /// Metodo para agg y actualizar cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public ActionResult NuevoCliente(Cliente cliente)
        {
            bool agg = false;
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                if (cliente.Id == 0)
                {
                    agg = AddCliente(cliente);
                }
                else
                {
                    agg = UpdateCliente(cliente);
                }
                
                if (agg)
                {
                    clientes = ListaClientes();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("About", clientes);
        }
        /// <summary>
        /// Metodo vista edita cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditarCliente(int id)
        {
            try
            {
                var cliente = ListaClientes().Where(s => s.Id == id).FirstOrDefault();
                return View("Cliente", cliente);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// Metodo elimina cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EliminarCliente(int id)
        {
            bool agg = false;
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                var cliente = ListaClientes().Where(s => s.Id == id).FirstOrDefault();
                var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44358/api/Cliente/DeleteCliente");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(JsonConvert.SerializeObject(cliente,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                }), Encoding.UTF8, "application/json");
                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
                if (agg)
                {
                    clientes = ListaClientes();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("About", clientes);
        }
        /// <summary>
        /// Metodo lista productos
        /// </summary>
        /// <returns></returns>
        private List<Producto> ListaProductos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
               
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44358/api/Producto/GetObtenerProductos");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                var respuesta = response.Result.Content.ReadAsStringAsync().Result;
                productos = JsonConvert.DeserializeObject<List<Producto>>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productos;
        }
        /// <summary>
        /// Metodo lista clientes
        /// </summary>
        /// <returns></returns>
        private List<Cliente> ListaClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44358/api/Cliente/GetObtenerClientes");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                var respuesta = response.Result.Content.ReadAsStringAsync().Result;
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientes;
        }
        /// <summary>
        /// Metodo agg producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        private bool AddProducto(Producto producto)
        {
            bool agg = false;
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/api/Producto/AddProducto");
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonConvert.SerializeObject(producto,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }), Encoding.UTF8, "application/json");
            var response = client.SendAsync(request, CancellationToken.None);
            response.Result.Content.ReadAsStringAsync();
            response.Wait();
            agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
            return agg;
        }
        /// <summary>
        /// Metodo actualiza producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        private bool UpdateProducto(Producto producto)
        {
            bool agg = false;
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44358/api/Producto/UpdateProducto");
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonConvert.SerializeObject(producto,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }), Encoding.UTF8, "application/json");
            var response = client.SendAsync(request, CancellationToken.None);
            response.Result.Content.ReadAsStringAsync();
            response.Wait();
            agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
            return agg;
        }
        /// <summary>
        /// Metodo agg cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        private bool AddCliente(Cliente cliente)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/api/Cliente/AddCliente");
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonConvert.SerializeObject(cliente,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }), Encoding.UTF8, "application/json");
            var response = client.SendAsync(request, CancellationToken.None);
            response.Result.Content.ReadAsStringAsync();
            response.Wait();
            bool agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
            return agg;
        }
        /// <summary>
        /// Metodo actualiza cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        private bool UpdateCliente(Cliente cliente)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44358/api/Cliente/UpdateCliente");
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonConvert.SerializeObject(cliente,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }), Encoding.UTF8, "application/json");
            var response = client.SendAsync(request, CancellationToken.None);
            response.Result.Content.ReadAsStringAsync();
            response.Wait();
            bool agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
            return agg;
        }
        /// <summary>
        /// Metodo lista ventas
        /// </summary>
        /// <returns></returns>
        private List<Venta> ListaVentas()
        {
            List<Venta> ventas = new List<Venta>();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44358/api/Venta/GetObtenerVentas");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                var respuesta = response.Result.Content.ReadAsStringAsync().Result;
                ventas = JsonConvert.DeserializeObject<List<Venta>>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ventas;
        }
        /// <summary>
        /// Metodo obtiene cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCliente(int idCliente)
        {

            clienteSesion = ListaClientes().Where(s => s.Id == idCliente).FirstOrDefault();
            return Json(clienteSesion, JsonRequestBehavior.AllowGet);
            
        }
        /// <summary>
        /// Metodo obtiene producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetProducto(int idProducto)
        {
            return Json(ListaProductos().Where(s => s.Id == idProducto).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo agg productos  a la venta
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        public ActionResult NuevoProductoVenta(VentaViewModel venta)
        {
            try
            {
                venta.Clientes = ListaClientes().Select(option => new SelectListItem
                {
                    Text = option.Cedula.ToString(),
                    Value = option.Id.ToString()
                });
                venta.Cliente = clienteSesion;
                venta.DetalleVenta.Producto = ListaProductos().Where(s => s.Id == venta.Producto.Id).FirstOrDefault();
                venta.Productos = ListaProductos().Select(option => new SelectListItem
                {
                    Text = option.Nombre.ToString(),
                    Value = option.Id.ToString()
                });
                if (venta.DetalleVenta.Id == 0)
                {
                    if (venta.DetalleVenta.IdTemporal != 0)
                    {
                        DetalleVenta removeDetalle = detalleVentas.Where(s => s.IdTemporal == venta.DetalleVenta.IdTemporal).FirstOrDefault();
                        detalleVentas.Remove(removeDetalle);
                    }
                    else
                    {
                        venta.DetalleVenta.IdTemporal = detalleVentas.OrderByDescending(s => s.IdTemporal).Select(s => s.IdTemporal).FirstOrDefault() + 1;
                    }
                    detalleVentas.Add(venta.DetalleVenta);
                }
                else
                {
                    DetalleVenta removeDetalle = detalleVentas.Where(s => s.Id == venta.DetalleVenta.Id).FirstOrDefault();
                    detalleVentas.Remove(removeDetalle);
                    detalleVentas.Add(venta.DetalleVenta);
                }
                venta.DetallesVenta = detalleVentas;
                venta.DetalleVenta = new DetalleVenta();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return View("Venta",venta);
        }
        /// <summary>
        /// Metodo agg nueva venta
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        public ActionResult NuevoVenta(VentaViewModel venta)
        {
            bool agg = false;
            try
            {
                venta.idVenta = VentaSesion.idVenta;
                if (venta.idVenta == 0)
                {
                    venta.Fecha = DateTime.Now;
                }
                venta.DetallesVenta = detalleVentas;
                venta.ValorTotal = venta.DetallesVenta.Sum(s => s.ValorTotal);
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/api/Venta/AddVenta");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(JsonConvert.SerializeObject(venta,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                }), Encoding.UTF8, "application/json");
                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
                VentaSesion = new VentaViewModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
            return View("Contact", ListaVentas());
        }
        /// <summary>
        /// Metodo elimina venta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EliminarVenta(int id)
        {
            bool agg = false;
            try
            {
                var venta = ListaVentas().Where(s => s.Id == id).FirstOrDefault();
                var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44358/api/Venta/EliminarVenta?id=" + id);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.SendAsync(request, CancellationToken.None);
                response.Result.Content.ReadAsStringAsync();
                response.Wait();
                agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Contact", ListaVentas());
        }
        /// <summary>
        /// Metodo elimina detalles de la venta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public ActionResult EliminarDetalleVenta(int id, int idProducto)
        {
            VentaViewModel venta = new VentaViewModel();
            bool agg = false;
            try
            {
                if (id > 0)
                {
                    var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44358/Venta/EliminarDetalleVenta/" + id);
                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.SendAsync(request, CancellationToken.None);
                    response.Result.Content.ReadAsStringAsync();
                    response.Wait();
                    agg = Convert.ToBoolean(response.Result.Content.ReadAsStringAsync().Result);
                }
                DetalleVenta detalle = detalleVentas.Where(s => s.Producto.Id == idProducto).FirstOrDefault();
                detalleVentas.Remove(detalle);

                venta = VentaSesion;
                venta.Clientes = ListaClientes().Select(option => new SelectListItem
                {
                    Text = option.Cedula.ToString(),
                    Value = option.Id.ToString()
                });
                venta.Productos = ListaProductos().Select(option => new SelectListItem
                {
                    Text = option.Nombre.ToString(),
                    Value = option.Id.ToString()
                });
                venta.Cliente = clienteSesion;
                venta.DetallesVenta = detalleVentas;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Venta", venta);
        }
        /// <summary>
        /// Metodo edita venta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditarVenta(int id)
        {
            VentaViewModel venta = new VentaViewModel();
            try
            {
                venta = ObtenerDatosVentaById(id);
                venta.Clientes = ListaClientes().Select(option => new SelectListItem
                {
                    Text = option.Cedula.ToString(),
                    Value = option.Id.ToString()
                });
                venta.Productos = ListaProductos().Select(option => new SelectListItem
                {
                    Text = option.Nombre.ToString(),
                    Value = option.Id.ToString()
                });
                detalleVentas = venta.DetallesVenta;
                clienteSesion = venta.Cliente;
                VentaSesion = venta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return View("Venta", venta);
        }
        /// <summary>
        /// Metodo obtiene datos de la venta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        private VentaViewModel ObtenerDatosVentaById(int id)
        {
            VentaViewModel venta = new VentaViewModel();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44358/Venta/GetObtenerVenta/"+ id);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.SendAsync(request, CancellationToken.None);
            response.Result.Content.ReadAsStringAsync();
            response.Wait();
            var respuesta = response.Result.Content.ReadAsStringAsync().Result;
            venta = JsonConvert.DeserializeObject<VentaViewModel> (respuesta);
            return venta;
        }
       /// <summary>
       /// Metodo obtiene detalle venta
       /// </summary>
       /// <param name="id"></param>
       /// <param name="idProducto"></param>
       /// <returns></returns>
        public ActionResult GetDetalleVenta(int id, int idProducto)
        {

            DetalleVenta detalleVenta = new DetalleVenta();
            try
            {
                detalleVenta = id == 0 ? detalleVentas.Where(s => s.Producto.Id == idProducto).FirstOrDefault() : detalleVentas.Where(s => s.Id == id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(detalleVenta, JsonRequestBehavior.AllowGet);
        }
    }
}