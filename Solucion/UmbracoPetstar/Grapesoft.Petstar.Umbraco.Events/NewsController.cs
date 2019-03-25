using Grapesoft.Petstar.Events.Properties;
using Grapesoft.Petstar.Events.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using umbraco.NodeFactory;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace Grapesoft.Petstar.Events
{
    public class NewsController : UmbracoApiController
    {
        public IEnumerable<string> GetAllProducts()
        {
            return new[] { "Table", "Chair", "Desk", "Computer", "Beer fridge" };
        }

        [HttpGet]
        public string Prueba(string data)
        {
            return "Prueba : " + data;
        }

        [HttpPost]
        public string SendEmail([FromBody]string data)
        {
            try
            {


                var emailData = JsonConvert.DeserializeObject<EmailData>(data);

                //preparacion de la prueba
                String Msg_HTMLx = @"<html>
                                  <head>
                                    <style>
                                      .colored {
                                        color: blue;
                                      }
                                      #body {
                                        font-size: 14px;
                                      }
                                      
                                    </style>
                                  </head>
                                  <body>
                                    <div id='body'>
                                      <h2>Forma de Contacto</h2>
                                      <p>Nombre: " + emailData.nombre + @"</p>            
                                    <p>Correo electrónico: " + emailData.email + @" </p>
                                    <p>Ciudad: " + emailData.ciudad + @" </p>
                                    <p>Tipo de teléfono:  " + (emailData.rndTipoT.Equals("1") ? "Fijo" : "Movil") + @" </p>
                                    <p>Teléfono:  " + emailData.telefono + @" </p>
                                    <p>Mensaje: " + emailData.mensaje + @" </p>                                      
                                    </div>
                                  </body>
                                </html>";

                var resultado = new EmailControl().SendMail(Settings.Default.Email_From, emailData.emailsSend, emailData.asuntoStr, true, Msg_HTMLx, Settings.Default.Email_Host, Settings.Default.Email_Port, Settings.Default.Email_User, Settings.Default.Email_Pass, true, true);


                return JsonConvert.SerializeObject(resultado);
            }
            catch (Exception ex)
            {
                LogError("error {0}", ex.ToString());
                return "error " + ex.ToString();
            }
        }

        [HttpPost]
        public string SendEmailSus([FromBody]string data)
        {
            try
            {


                var emailData = JsonConvert.DeserializeObject<EmailDataSus>(data);

                //preparacion de la prueba
                String Msg_HTMLx = @"<html>
                                  <head>
                                    <style>
                                      .colored {
                                        color: blue;
                                      }
                                      #body {
                                        font-size: 14px;
                                      }
                                      
                                    </style>
                                  </head>
                                  <body>
                                    <div id='body'>
                                      <h2>Forma de Contacto</h2>
                                      <p>Nombre: " + emailData.nombre + @"</p>            
                                    <p>Correo electrónico: " + emailData.email + @" </p>
                                    </div>
                                  </body>
                                </html>";

                Log("Datos nombre: {0}, email -{1}-, EmailsSend {2}, Asunto: {3}", emailData.nombre, emailData.email, emailData.emailsSend, emailData.asuntoStr);

                var resultado = new EmailControl().SendMail(Settings.Default.Email_From, emailData.emailsSend, emailData.asuntoStr, true, Msg_HTMLx, Settings.Default.Email_Host, Settings.Default.Email_Port, Settings.Default.Email_User, Settings.Default.Email_Pass, true, true);


                return JsonConvert.SerializeObject(resultado);
            }
            catch (Exception ex)
            {
                LogError("error {0}", ex.ToString());
                return "error " + ex.ToString();
            }
        }


        [HttpGet]
        public string GetNews(int page, int size, string sort, string dateIni, string dateEnd, string param, int parent)
        {
            var response = new MethodResponse<ResponsePagination> { code = 0 };
            try
            {
                using (var context = new PetstarEntities())
                {

                    var resp = new ResponsePagination();

                    var dateI = DateTime.Now;
                    var dateE = DateTime.Now;

                    var filterDate = false;

                    if (!string.IsNullOrEmpty(dateIni) && !string.IsNullOrEmpty(dateEnd))
                    {
                        dateI = DateTime.ParseExact(dateIni, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dateE = DateTime.ParseExact(dateEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        filterDate = true;
                    }




                    var totalRow = new ObjectParameter("totalrow", typeof(int));

                    var news = context.sp_NewsPagination(page, size, parent, sort, param, dateI, dateE, filterDate, totalRow).Select(x => new cmsNoticias
                    {
                        id = x.id,
                        IdParent = x.IdParent,
                        IdUmbraco = x.IdUmbraco,
                        Fecha = x.Fecha,
                        Titulo = x.Titulo,
                        TextoFecha = x.TextoFecha,
                        Resumen = x.Resumen,
                        Informacion = x.Informacion,
                        Link = x.Link,
                        Imagen = x.Imagen,

                    }).ToList();

                    resp.news = getNewsWithUrl(news.ToList());


                    resp.totalRows = context.cmsNoticias.Where(
                        x => x.IdParent == parent
                        && (!filterDate || x.Fecha >= dateI && x.Fecha <= dateE)
                        && (param == "0" || x.Titulo.Contains(param))).Count();

                    response.Result = resp;
                }
            }
            catch (Exception ex)
            {
                response.code = -100;
                response.message = ex.ToString();
            }
            return JsonConvert.SerializeObject(response);

        }

        [HttpGet]
        public string GetLastNews(int parent, int count)
        {
            var response = new MethodResponse<ResponsePagination> { code = 0 };
            try
            {
                using (var context = new PetstarEntities())
                {

                    var resp = new ResponsePagination();

                    var newsResp = context.cmsNoticias.Where(x => x.IdParent == parent).OrderByDescending(x => x.Fecha).Take(count);

                    resp.news = getNewsWithUrl(newsResp.ToList());

                    resp.totalRows = context.cmsNoticias.Where(x => x.IdParent == parent).Count();

                    response.Result = resp;
                }
            }
            catch (Exception ex)
            {
                response.code = -100;
                response.message = ex.ToString();
            }
            return JsonConvert.SerializeObject(response);

        }


        [HttpGet]
        public string GetLastNewsAll(int count)
        {
            var response = new MethodResponse<ResponsePagination> { code = 0 };
            try
            {
                using (var context = new PetstarEntities())
                {

                    var resp = new ResponsePagination();


                    var newsResp = context.cmsNoticias.OrderByDescending(x => x.Fecha).Take(count);

                    resp.news = getNewsWithUrl(newsResp.ToList());

                    resp.totalRows = 0;

                    response.Result = resp;
                }
            }
            catch (Exception ex)
            {
                response.code = -100;
                response.message = ex.ToString();
            }
            return JsonConvert.SerializeObject(response);

        }

        private List<Noticias> getNewsWithUrl(List<cmsNoticias> list)
        {
            var response = new List<Noticias>();

            foreach (var a in list)
            {
                var noticia = new Noticias();

                noticia.id = a.id;
                noticia.IdUmbraco = a.IdUmbraco;
                noticia.IdParent = a.IdParent;
                noticia.Fecha = a.Fecha;
                noticia.Titulo = a.Titulo;
                noticia.Resumen = a.Resumen;
                noticia.Informacion = a.Informacion;
                noticia.Link = GetUrlNode(a.IdUmbraco);
                noticia.TextoFecha = a.TextoFecha;
                noticia.Imagen = a.Imagen;
                noticia.Active = a.Active;

                response.Add(noticia);
            }

            return response;
        }

        private string GetUrlNode(int id)
        {
            var url = string.Empty;
            try
            {


                Node nd = new Node(id);

                url = nd.Url;

                //Log("Encontrado {0}", JsonConvert.SerializeObject(nd));
            }
            catch (Exception ex)
            {
                LogError("error {0}", ex.ToString());
                url = ex.Message;
            }
            return url;
        }

        private void Log(string message, params object[] parameters)
        {
            try
            {
                if (Settings.Default.LogIsActive)
                    using (var context = new PetstarEntities())
                    {

                        context.cmsLogError.Add(new cmsLogError { Message = string.Format(message, parameters), Date = DateTime.Now });

                        context.SaveChanges();
                    }
            }
            catch (Exception)
            {

            }

        }
        private void LogError(string message, params object[] parameters)
        {
            try
            {
                using (var context = new PetstarEntities())
                {

                    context.cmsLogError.Add(new cmsLogError { Message = string.Format(message, parameters), Date = DateTime.Now });

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

        }

        internal class ResponsePagination
        {

            public List<Noticias> news { get; set; }
            public int totalRows { get; set; }
        }

        internal class Noticias
        {
            public int id { get; set; }
            public int IdUmbraco { get; set; }
            public Nullable<int> IdParent { get; set; }
            public Nullable<System.DateTime> Fecha { get; set; }
            public string Titulo { get; set; }
            public string Informacion { get; set; }
            public string Link { get; set; }
            public string TextoFecha { get; set; }
            public string Imagen { get; set; }
            public bool Active { get; set; }
            public string Resumen { get; internal set; }
        }
    }
}