using Umbraco.Web;

using Grapesoft.Petstar.Events.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Newtonsoft.Json;

namespace Grapesoft.Petstar.Events
{
    public class Events_News : ApplicationEventHandler
    {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Umbraco.Core.Services.ContentService.Published += ContentService_Published;
            Umbraco.Core.Services.ContentService.Deleted += ContentService_Delete;
            Umbraco.Core.Services.ContentService.UnPublished += ContentService_UnPublished;




        }

        private void ContentService_UnPublished(Umbraco.Core.Publishing.IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            try
            {
                if (e.PublishedEntities.Count() > 0)
                {

                    foreach (var entity in e.PublishedEntities)
                    {
                        var alias = entity.ContentType.Name;
                        if (alias.Equals("itemPrensa"))
                        {
                            var id = entity.ContentType.Id;
                            using (var context = new PetstarEntities())
                            {
                                var news = context.cmsNoticias.Where(x => x.IdUmbraco == id).First();
                                news.Active = false;
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Error en PublishedEntities: {0}", ex.Message);
            }
        }

        private void ContentService_Delete(IContentService sender, DeleteEventArgs<IContent> e)
        {
            try
            {
                if (e.DeletedEntities.Count() > 0)
                {

                    foreach (var entity in e.DeletedEntities)
                    {
                        var alias = entity.ContentType.Name;
                        if (alias.Equals("itemPrensa"))
                        {
                            var id = entity.ContentType.Id;
                            using (var context = new PetstarEntities())
                            {
                                var news = context.cmsNoticias.Where(x => x.IdUmbraco == id).First();
                                news.Active = false;
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Error en delete: {0}", ex.Message);
            }

        }

        void ContentService_Published(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e)
        {
            try
            {
                new Thread(() =>
                {
                    try
                    {
                        foreach (var entity in e.PublishedEntities)
                        {

                            if (entity.ContentType.Name.ToLowerInvariant().Equals("itemprensa"))
                            {

                                //                   var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);


                                //var mediaItem = umbracoHelper.TypedContent(idNode);

                                //if (mediaItem == null)
                                //    return "nada";
                                //return mediaItem.Url;

                                var urlImg = string.Empty;

                                if (ApplicationContext.Current != null)
                                {
                                    var ms = ApplicationContext.Current.Services.MediaService;

                                    var imageGuidUdi = GuidUdi.Parse(entity.GetValue("imagen").ToString());

                                    // Get the ID of the node!
                                    if (imageGuidUdi != null)
                                    {
                                        var imageNodeId = ApplicationContext.Current.Services.EntityService.GetIdForKey(imageGuidUdi.Guid, (UmbracoObjectTypes)Enum.Parse(typeof(UmbracoObjectTypes), imageGuidUdi.EntityType, true));

                                        //// Finally, get the node.
                                        //var imageNode = Umbraco.TypedMedia(imageNodeId.Result);

                                        //var Guid = new Guid(entity.GetValue("imagen").ToString());
                                        var img = ms.GetById(imageNodeId.Result);

                                        if (img != null)
                                        {
                                            urlImg = JsonConvert.DeserializeObject<ImageUmb>(img.GetValue("umbracoFile").ToString()).src;
                                        }
                                    }

                                }

                                //Media file = new Media("imagen", entity.Id, (IMediaType)entity);
                                //if (file != null)
                                //    urlImg = file.GetValue("umbracoFile").ToString();

                                // Your custom code goes here
                                using (var context = new PetstarEntities())
                                {
                                    Log("Id entity {0}", entity.Id);

                                    var elements = context.cmsNoticias.Where(x => x.IdUmbraco == entity.Id).ToList();

                                    Log("Encontrado {0}", elements.Count == 0);
                                    if (elements.Count == 0)
                                    {


                                        var New = new cmsNoticias
                                        {
                                            IdUmbraco = entity.Id,
                                            IdParent = entity.ParentId,
                                            Titulo = entity.Name,
                                            Informacion = getData(entity.GetValue("informacion")),
                                            Link = getData(entity.GetValue("link")),
                                            Imagen = urlImg,
                                            TextoFecha = getData(entity.GetValue("fechaYMedios")),
                                            Active = true
                                        };

                                        var fechaI = getDate(getData(entity.GetValue("fecha")));
                                        if (fechaI.code == 0)
                                            New.Fecha = fechaI.Result;
                                        else
                                            New.Fecha = DateTime.Now;

                                        context.cmsNoticias.Add(New);
                                    }
                                    else
                                    {
                                        var New = elements.FirstOrDefault();

                                        New.IdUmbraco = entity.Id;
                                        New.IdParent = entity.ParentId;
                                        New.Titulo = entity.Name;
                                        New.Informacion = getData(entity.GetValue("informacion"));
                                        New.Link = getData(entity.GetValue("link"));
                                        New.Imagen = urlImg;
                                        New.TextoFecha = getData(entity.GetValue("fechaYMedios"));
                                        New.Active = true;

                                        var fechaI = getDate(getData(entity.GetValue("fecha")));
                                        if (fechaI.code == 0)
                                            New.Fecha = fechaI.Result;


                                    }

                                    context.SaveChanges();
                                }
                            }
                            else
                            {
                                Log("Element published no save: {0}", entity.ContentType.Name);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError("Error en hilo : {0}", ex.Message);
                    }

                }).Start();



            }
            catch (Exception ex)
            {
                LogError("Error en published: {0}", ex.ToString());
            }
        }

        private string getData(object data)
        {
            var response = string.Empty;
            try
            {
                response = data.ToString();
            }
            catch (Exception)
            {
                response = "error al extraer el dato";
            }
            return response;
        }




        private MethodResponse<DateTime> getDate(string date)
        {
            var response = new MethodResponse<DateTime> { code = 0 };
            try
            {
                if (date.ToLowerInvariant().Contains("err"))
                    response.code = -100;
                else
                {
                    //2/13/2019 12:00:00 AM
                    //response.Result = DateTime.ParseExact(date.Replace(" 12:00:00 AM", ""), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    response.Result = DateTime.Parse(date);
                }


            }
            catch (Exception ex)
            {
                LogError("Error getDate {0} : ", date, ex.Message);
                response.code = -100;
            }
            return response;
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

                throw;
            }

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

                throw;
            }

        }
        public string GetMediaUrlFromPropertyName(dynamic contentItem, string propertyName)
        {
            var media = contentItem.GetValue<IMedia>(propertyName);

            //Media md = new Media("imagen", media, media.ContentType);
            //var url = media.GetValue("umbracoFile").ToString();
            //var url = media.Properties["umbracoFile"].Value.ToString();

            return "";
        }

        public string GetMediaUrl(dynamic contentItem)
        {
            return contentItem.src;
        }

        public string GetMediaUrl(IMedia media)
        {
            return media.GetValue("umbracoFile") != null ? media.GetValue("umbracoFile").ToString() : string.Empty;
        }

    }

    internal class MethodResponse<T>
    {

        public int code { get; set; }
        public string message { get; set; }
        public T Result { get; set; }
    }

    internal class ImageUmb
    {
        public string src { get; set; }
        public string[] crops { get; set; }

    }

}