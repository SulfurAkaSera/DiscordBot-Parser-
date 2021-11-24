using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public static class Parser
    {
        public static List<string> ParseWeather()
        {
            string url = "https://www.gismeteo.ru/weather-noginsk-11956/";
            List<string> result = new List<string>();
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler() {AllowAutoRedirect = false, AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None})
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (HttpResponseMessage response = client.GetAsync(url).Result)
                        {
                            if(response.IsSuccessStatusCode)
                            {
                                var html = response.Content.ReadAsStringAsync().Result;
                                if(!string.IsNullOrEmpty(html))
                                {
                                    HtmlDocument documents = new HtmlDocument();
                                    documents.LoadHtml(html);

                                    var weather = documents.DocumentNode.SelectSingleNode(".//div[@class='tab-content']");
                                    if(weather != null)
                                    {
                                        result.Add(weather.SelectSingleNode(".//div[@class='date']").InnerText);
                                        result.Add(weather.SelectSingleNode(".//div[@class='date xs fadeIn']//time[@data-dateformat='G:i']").InnerText);
                                        result.Add(weather.SelectSingleNode(".//div[@class='tab-weather']//div[@class='js_meas_container temperature tab-weather__value']//span[@class='unit unit_temperature_c']").InnerText);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No weather");
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            { Console.WriteLine(ex.Message);}
            return null;
        }

        public static List<string> ParseCourse()
        {
            string url = "https://www.banki.ru/products/currency/usd/";
            List<string> result = new List<string>();
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler() { AllowAutoRedirect = false, AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None })
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (HttpResponseMessage response = client.GetAsync(url).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var html = response.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlDocument documents = new HtmlDocument();
                                    documents.LoadHtml(html);

                                    var course = documents.DocumentNode.SelectSingleNode(".//div[@class='currency-table']//table[@class='currency-table__table']//tr[@class='currency-table__bordered-row']//td[@class='currency-table__rate currency-table__darken-bg']");
                                    if(course != null)
                                    {
                                        result.Add(course.SelectSingleNode(".//div[@class='currency-table__large-text']").InnerText);
                                        result.Add(course.SelectSingleNode(".//div[@class='currency-table__rate__text']").InnerText);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No course");
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return null;
        }

        public static List<string> ParseSnews()
        {
            string url = "https://k.snus-online-msk.com/Corvus/Corvus-brutal/";
            List<string> result = new List<string>();
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler() { AllowAutoRedirect = false, AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None })
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (HttpResponseMessage response = client.GetAsync(url).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var html = response.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlDocument documents = new HtmlDocument();
                                    documents.LoadHtml(html);

                                    var snewsName = documents.DocumentNode.SelectSingleNode(".//div[@itemprop='name']");
                                    if (snewsName != null)
                                    {
                                        result.Add(snewsName.InnerText);
                                        var snewsPrice = documents.DocumentNode.SelectSingleNode(".//div[@class='product-order']");
                                        if(snewsPrice != null)
                                        {
                                            result.Add(snewsPrice.SelectSingleNode(".//span[@class='product-price']").InnerText);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No snews");
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return null;
        }
    }
}
