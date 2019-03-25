using System;
using Grapesoft.Petstar.Events;
using Grapesoft.Petstar.Events.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using umbraco.NodeFactory;

namespace Grapesoft.Petstar.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Your custom code goes here
            new Data().save();
        }
        [TestMethod]
        public void TestMethodEmail()
        {

            //preparacion de la prueba
            String Msg_HTMLx = "";
            Msg_HTMLx += "Nombre: prueba <br />";
            Msg_HTMLx += "Email: prueba <br />";
            Msg_HTMLx += "Teléfono:  prueba. <br />";
            Msg_HTMLx += "Razón social: prueba. <br />";
            Msg_HTMLx += "RFC: prueba. <br />";

            
            //host:  smtp.office365.com
            //port:  587
            //Usuario: redessociales@petstar.mx
            //Contraseña: PSTcomunicacion1

            //ejecución
            var resultado = new EmailControl().SendMail("redessociales@petstar.mx", "mugauli2@hotmail.com,mugauli@gmail.com", "prueba", true, Msg_HTMLx, "smtp.office365.com", 587, "redessociales@petstar.mx", "PSTcomunicacion1", true, true);

            //Comprobación
            Assert.AreEqual(resultado.code, 0);
        }
    }
}
