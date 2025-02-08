using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace articulos_vista
{
    public static class Validaciones
    {

        public static void mostrarMensajeError(HtmlContainerControl control, string mensaje = "ocurrió un error inesperado, intente de nuevo")
        {
            control.Visible = true;
            control.InnerText = mensaje;
        }

        public static bool validarVacio(string cadena)
        {
            return cadena.Replace(" ", "") == "";
        }

        public static bool validarImagen(string archivo)
        {
            string[] extensionesValidas = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".tiff", ".tif" };

            foreach (string ext in extensionesValidas)
            {
                if (archivo.Contains(ext))
                    return true;
            }

            return false;
        }
    }
}