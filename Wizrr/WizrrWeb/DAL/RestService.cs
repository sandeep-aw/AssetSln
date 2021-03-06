﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WizrrWeb.Models;


namespace WizrrWeb.DAL
{
    public class RestService
    {
        string accessToken;


        private string URLBuilder(string url, RESTOption restoption)
        {


            if (restoption.filter != null)
            {
                url += ((url.IndexOf('?') > -1) ? "&" : "?") + "$filter=" + restoption.filter;
            }


            if (restoption.select != null)
            {
                url += ((url.IndexOf('?') > -1) ? "&" : "?") + "$select=" + restoption.select;
            }


            if (restoption.expand != null)
            {
                url += ((url.IndexOf('?') > -1) ? "&" : "?") + "$expand=" + restoption.expand;
            }


            if (restoption.top != null)
            {
                url += ((url.IndexOf('?') > -1) ? "&" : "?") + "$top=" + restoption.top;
            }

            if (restoption.orderby != null)
            {
                url += ((url.IndexOf('?') > -1) ? "&" : "?") + "$orderby=" + restoption.orderby;
            }




            return url;
        }


        public JArray GetAllItemFromList(ClientContext ctx, string listname, RESTOption restoption)
        {
            try
            {
                RetrieveAccessToken(ctx);

                var Url = ctx.Url + "/_api/web/lists/getByTitle('" + listname + "')/items";
                Url = URLBuilder(Url, restoption);

                HttpWebRequest request = HttpWebRequest.CreateHttp(Url);
                request.Accept = "application/json;odata=verbose";
                request.Headers.Add("Authorization", accessToken);
                Stream webStream = request.GetResponse().GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                JObject jobj = JObject.Parse(response);
                JArray jarr = (JArray)jobj["d"]["results"];


                return jarr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }

        public JArray GetItemByID(ClientContext ctx, string listname, RESTOption restoption, string ID)
        {
            try
            {
                RetrieveAccessToken(ctx);

                var Url = ctx.Url + "/_api/web/lists/getByTitle('" + listname + "')/items(" + ID + ")";
                Url = URLBuilder(Url, restoption);

                HttpWebRequest request = HttpWebRequest.CreateHttp(Url);
                request.Accept = "application/json;odata=verbose";
                request.Headers.Add("Authorization", accessToken);
                Stream webStream = request.GetResponse().GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                JObject jobj = JObject.Parse(response);
                JArray jarr = (JArray)jobj["d"]["results"];


                return jarr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }


        public string SaveItem(ClientContext ctx, string ListName, string Itemdate)
        {
            var responseText = "";
            try
            {
                RetrieveAccessToken(ctx);

                string Metadata = String.Format("SP.Data.{0}ListItem", ListName);

                string listname = "'__metadata':{ 'type': '" + Metadata + "'}";

                string itemPostBody = "{" + listname + "," + Itemdate + "}";
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);
                HttpWebRequest ItemRequest;
                ItemRequest = (HttpWebRequest)HttpWebRequest.Create(String.Format("{0}_api/web/lists/getByTitle('" + ListName + "')/items", ctx.Url));
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentType = "application/json;odata=verbose;charset=UTF-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                ItemRequest.Headers.Add("Authorization", accessToken);

                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        responseText = Convert.ToString(jobj["d"]["Id"]);
                    }
                }
                return responseText;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }


        public string UpdateItem(ClientContext ctx, string ListName, string Itemdate, string ID)
        {
            var responseText = "";
            try
            {
                RetrieveAccessToken(ctx);

                string Metadata = String.Format("SP.Data.{0}ListItem", ListName);

                string listname = "'__metadata':{ 'type': '" + Metadata + "'}";

                string itemPostBody = "{" + listname + "," + Itemdate + "}";
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);
                HttpWebRequest ItemRequest;
                ItemRequest = (HttpWebRequest)HttpWebRequest.Create(String.Format("{0}_api/web/lists/getByTitle('" + ListName + "')/items(" + ID + ")", ctx.Url));
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentType = "application/json;odata=verbose;charset=UTF-8";
                ItemRequest.Accept = "application/json;odata=verbose";

                ItemRequest.Headers.Add("Authorization", accessToken);
                ItemRequest.Headers.Add("X-HTTP-Method", "MERGE");
                ItemRequest.Headers.Add("If-Match", "*");

                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        //string response = responseReader.ReadToEnd();
                        //JObject jobj = JObject.Parse(response);
                        //responseText = Convert.ToString(jobj["d"]["Id"]);
                        responseText = "Update";
                    }
                }
                return responseText;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }



        public JArray GetAllUserListList(ClientContext ctx)
        {
            try
            {
                RetrieveAccessToken(ctx);

                var Url = ctx.Url + "/_api/web/siteusers";

                HttpWebRequest request = HttpWebRequest.CreateHttp(Url);
                request.Accept = "application/json;odata=verbose";
                request.Headers.Add("Authorization", accessToken);
                Stream webStream = request.GetResponse().GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                JObject jobj = JObject.Parse(response);
                JArray jarr = (JArray)jobj["d"]["results"];


                return jarr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }

        public JArray GetAllUserGroupList(ClientContext ctx, string ID)
        {
            try
            {
                RetrieveAccessToken(ctx);

                var Url = ctx.Url + "/_api/Web/GetUserById(" + ID + ")/Groups";

                HttpWebRequest request = HttpWebRequest.CreateHttp(Url);
                request.Accept = "application/json;odata=verbose";
                request.Headers.Add("Authorization", accessToken);
                Stream webStream = request.GetResponse().GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                JObject jobj = JObject.Parse(response);
                JArray jarr = (JArray)jobj["d"]["results"];


                return jarr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }


        private void RetrieveAccessToken(ClientContext ctx)
        {
            ctx.ExecutingWebRequest += ctx_ExecutingWebRequest;
            ctx.ExecuteQuery();
        }
        private void ctx_ExecutingWebRequest(object sender, WebRequestEventArgs e)
        {
            accessToken = e.WebRequestExecutor.RequestHeaders.Get("Authorization");
        }

    }

}