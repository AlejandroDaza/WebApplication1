using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Net;
using System.Text;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Gethtml(object sender, EventArgs e)
        {
            WebClient objWebClient = new WebClient();
            try
            {
                //get the information from the web site
                UTF8Encoding objUTF8 = new UTF8Encoding();
                var html = new HtmlDocument();
                html.LoadHtml(objUTF8.GetString(objWebClient.DownloadData(txtUrl.Text)));
                var root = html.DocumentNode;

                //get all the images form the website using tag img       
                string nameImage = "mypictureA";
                var position = 1;
                //create a second webclient for get the images
                WebClient webClient = new WebClient();
                Boolean imagenproblem = false;
                foreach (HtmlNode link in html.DocumentNode.SelectNodes("//img"))
                {
                    //get and safe all images
                    string filepath = Server.MapPath(nameImage + position + ".jpeg");

                    try
                    {
                        //trying to get the imagen form web
                        HtmlAttribute src = link.Attributes["src"];
                        string imageUrl = src.Value;
                        webClient.DownloadFile(imageUrl, filepath);
                    }
                    catch (Exception)
                    {
                        
                        //just jump this pic if can't get it
                        imagenproblem = true;
                    }
                    //if can't get hte image set with a defaul image
                    if (imagenproblem)
                    {
                        //changing the id for the image for can be recognized with the script
                        link.SetAttributeValue("id", nameImage + position);
                        //change the image for the local version
                        link.SetAttributeValue("src", "mypictureA0.jpeg");
                        position++;
                        imagenproblem = false;
                    }
                    else
                    {
                        //changing the id for the image for can be recognized with the script
                        link.SetAttributeValue("id", nameImage + position);
                        //change the image for the local version
                        link.SetAttributeValue("src", nameImage + position + ".jpeg");
                        position++;
                    }

                }

                //creating a html style that will be used in face recognition script
                string newContentStyle = "<style> .myface {  position: absolute;  border: 2px solid #FFF; } </ style >";

                //creating a new node with the style
                HtmlNode newNodeStyle = HtmlNode.CreateNode(newContentStyle);

                // Get header node of the page
                HtmlNode header = html.DocumentNode.SelectSingleNode("//head");
                // injecting the html into the original page      
                header.AppendChild(newNodeStyle);

                //creating the script for blur faces in every pic
                string newContentScript = "<script src='jquery.facedetection.min.js'></script> ";
                string fullscriptFaces = " <script>$(function() { 'use strict';  $('#try-it').click(function(e) {  e.preventDefault();";
                for (var numpic = 1; numpic < position; numpic++)
                {
                    fullscriptFaces = fullscriptFaces + "$('#mypictureA" + numpic + "').faceDetection({ complete: function(faces) {for (var i = 0; i < faces.length; i++)";
                    fullscriptFaces = fullscriptFaces + "{window.alert('Face detected!'); $('<div>', {  'class':'myface','position': 'absolute', 'border': '2px solid #FFF',  'css': { 'position': 'absolute',  'left':     faces[i].x * faces[i].scaleX + 'px',";
                    fullscriptFaces = fullscriptFaces + " 'top':      faces[i].y * faces[i].scaleY + 'px',  'width':    faces[i].width * faces[i].scaleX + 'px',  'height':   faces[i].height * faces[i].scaleY + 'px',  'filter': 'blur(10px)','-webkit-filter': 'blur(10px)','-moz-filter': 'blur(10px)', '-o-filter': 'blur(10px)', '-ms-filter': 'blur(10px)', 'z-index': '200'  ";
                    fullscriptFaces = fullscriptFaces + "} })  .insertAfter(this); } },  error: function(code, message) {  alert('Error: ' + message); } });";
                }
                fullscriptFaces = fullscriptFaces + "}); }); </script> ";


                // Get body node of the webpage
                HtmlNode body = html.DocumentNode.SelectSingleNode("//body");
                // Creating a node with the script for blur faces
                HtmlNode newNodeScriptFaces = HtmlNode.CreateNode(fullscriptFaces);
                HtmlNode newNodeScript = HtmlNode.CreateNode(newContentScript);
                // injecting the html in the original webpage
                body.AppendChild(newNodeScript);
                body.AppendChild(newNodeScriptFaces);
                //load the page in the html
                lblHTML.Text = html.DocumentNode.OuterHtml;
            }
            catch (Exception)
            {
                //do nothing if the page can't be loaded
            }

        }

    }
}