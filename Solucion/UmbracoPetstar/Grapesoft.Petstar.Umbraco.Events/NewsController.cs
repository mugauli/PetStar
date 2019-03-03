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

                    var news = context.sp_NewsPagination(page, size, parent, sort, param, dateI, dateE, filterDate, totalRow);

                    resp.news = news.Select(x => new cmsNoticias
                    {
                        id = x.id,
                        IdParent = x.IdParent,
                        IdUmbraco = x.IdUmbraco,
                        Fecha = x.Fecha,
                        Titulo = x.Titulo,
                        TextoFecha = x.TextoFecha,
                        Informacion = x.Informacion,
                        Link = x.Link,
                        Imagen = x.Imagen,

                    }).ToList();


                    resp.totalRows = context.cmsNoticias.Where(
                        x => x.IdParent == parent 
                        && (!filterDate  || x.Fecha >= dateI && x.Fecha <= dateE) 
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


                    resp.news = context.cmsNoticias.Where(x => x.IdParent == parent).OrderByDescending(x => x.Fecha).Take(count).ToList();

                    //resp.news = news.Select(x => new cmsNoticias
                    //{
                    //    id = x.id,
                    //    IdParent = x.IdParent,
                    //    IdUmbraco = x.IdUmbraco,
                    //    Fecha = x.Fecha,
                    //    Titulo = x.Titulo,
                    //    TextoFecha = x.TextoFecha,
                    //    Informacion = x.Informacion,
                    //    Link = x.Link,
                    //    Imagen = x.Imagen

                    //}).ToList();


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


                    resp.news = context.cmsNoticias.OrderByDescending(x => x.Fecha).Take(count).ToList();

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
        

        internal class ResponsePagination
        {

            public List<cmsNoticias> news { get; set; }
            public int totalRows { get; set; }
        }
    }
}