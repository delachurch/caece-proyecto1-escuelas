using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Escuelas.UI.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            
            imgBuilder.MergeAttribute("alt", alt);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            if (alt.Contains("Borrar")||alt.Contains("Eliminar")) anchorBuilder.MergeAttribute("onclick", "return confirm('¿Está seguro que desea borrar este item?')");
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
        public static MvcHtmlString Menu(this HtmlHelper helper, string id, string smp,string usuario)
         {
             var sb = new StringBuilder();

             string[] roles = Roles.GetRolesForUser(usuario);
             
             // Render each top level node
             var nodosPadres = SiteMap.Providers[smp].RootNode.ChildNodes;

             //Create top main menu
                 // Create opening unordered list tag
                 sb.AppendLine("<ul id=\"" + id + "\" class='slimmenu'>");

                 // Add all nodes for main menu
                 foreach (SiteMapNode nodoPrincipal in nodosPadres)
                 {
                        if(EsAccesibleAlUsuario(roles,nodoPrincipal))
                            AgregarSubNodo(sb, nodoPrincipal, true, usuario);

                 }

                 // Close unordered list tag
                 sb.AppendLine("</ul>");
             

             return MvcHtmlString.Create(sb.ToString());
         }
         private static bool EsAccesibleAlUsuario(string[] roles,SiteMapNode nodo)
         {
            if ((nodo.Roles != null) && (nodo.Roles.Count > 0))
                {
                    //Check each roles in the node
                    foreach (string rol in nodo.Roles)
                    {
                        if (!Array.Exists(roles, r => r.Equals(rol)))
                        {
                            continue;
                        }
                        return true;
                    }
                }

            if (nodo.Roles == null || nodo.Roles.Count == 0)
                return true;

            
            return false;
         
         }
         private static void AgregarSubNodo(StringBuilder sb, SiteMapNode nodo, bool nodoPrincipal,string userName)
         {
             string[] roles = Roles.GetRolesForUser(userName);

             if (nodo.ChildNodes.Count > 0)
             {

                 sb.AppendLine("<li>");
                 sb.AppendLine(CrearItemMenu(nodo));
                 sb.AppendLine("<ul>");
   

                 foreach (SiteMapNode subNodo in nodo.ChildNodes)
                     if (EsAccesibleAlUsuario(roles, subNodo))
                         AgregarSubNodo(sb, subNodo, nodoPrincipal, userName);

                 sb.AppendLine("</ul></li>");
             }
             else
             {
                 sb.AppendLine("<li>");
                 sb.AppendLine(CrearItemMenu(nodo));
                 sb.AppendLine("</li>");
             }
         }
         private static string CrearItemMenu(SiteMapNode nodo)
         {
             var selected = string.Empty;

            

             var target = nodo["target"];

             if (!string.IsNullOrEmpty(target))
             {
                 if (string.IsNullOrEmpty(nodo.Description))
                     return string.Format("<a href=\"{0}\" {1} target=\"{2}\">{3}</a>", nodo.Url, selected, target, nodo.Title);

                 return string.Format("<a href=\"{0}\" title=\"{1}\" {2} target=\"{3}\">{4}</a>", nodo.Url, nodo.Description, selected, target, nodo.Title);
             }

             if (string.IsNullOrEmpty(nodo.Description))
                 return string.Format("<a href=\"{0}\" {1}>{2}</a>", nodo.Url, selected, nodo.Title);

             return string.Format("<a href=\"{0}\" title=\"{1}\" {2}>{3}</a>", nodo.Url, nodo.Description, selected, nodo.Title);
         }
    }
}