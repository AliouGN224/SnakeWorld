using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace SnakeWorld;
public class XMLUtils
{
    public XMLUtils(){ }
    
    /*public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath, int idInfirmiere) {
        XsltArgumentList argList = new XsltArgumentList();
        argList.AddParam("idInfirmiere", "", idInfirmiere);
        
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        
        // Activer la fonction document()
        XsltSettings settings = new XsltSettings(enableDocumentFunction: true, enableScript: false);

        
        xslt.Load(xsltFilePath, settings, new XmlUrlResolver());
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null);
        xslt.Transform(xpathDoc, argList, htmlWriter);
    }*/
    
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath, string idInfirmiere) {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
    
        XsltArgumentList argList = new XsltArgumentList();
        argList.AddParam("idInfirmiere", "", idInfirmiere);
        
        // Activer la fonction document() via XsltSettings
        var settings = new XsltSettings(enableDocumentFunction: true, enableScript: false);
        var resolver = new XmlUrlResolver
        {
            Credentials = System.Net.CredentialCache.DefaultCredentials // Si nécessaire pour les URI sécurisés
        };

        xslt.Load(xsltFilePath, settings, resolver);

        try
        {
            using (XmlWriter htmlWriter = XmlWriter.Create(htmlFilePath))
            {
                xslt.Transform(xpathDoc, argList, htmlWriter, resolver);
            }
            Console.WriteLine("Chemin absolu du fichier de sortie : " + Path.GetFullPath(htmlFilePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de l'écriture du fichier : " + ex.Message);
        }
    }


}