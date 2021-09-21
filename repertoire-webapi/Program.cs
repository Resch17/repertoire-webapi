using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using repertoire_webapi.Interfaces;
using repertoire_webapi.Models;
using repertoire_webapi.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //var repo = ActivatorUtilities.CreateInstance<NoteRepository>(host.Services);

            //LoadResources(repo);
        }

        //public static void LoadResources(INoteRepository repo)
        //{
        //    JObject jsonInput = JObject.Parse(json());
        //    IList<JToken> results = jsonInput["notes"].Children().ToList();
        //    IList<Note> objects = new List<Note>();
        //    foreach (JToken result in results)
        //    {
        //        var obj = result.ToObject<Note>();
        //        objects.Add(obj);
        //    }
        //    objects.OrderBy(o => o.Id);
        //    foreach (var obj in objects)
        //    {
        //        repo.AddNote(obj);
        //    }
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.UseStartup<Startup>();
         });

        //public static string json()
        //{
        //    return "";
        //}

    }
}

