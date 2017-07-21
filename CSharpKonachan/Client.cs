using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CSharpKonachan.Models;
using Newtonsoft.Json;

[assembly: CLSCompliant(true)]
namespace CSharpKonachan
{
    public class KonachanClient
    {


        private string site = "https://konachan";

        private string Sfw = ".net/";
        private string Nsfw = ".com/";

        public KonachanClient(bool isnsfw = false)
        {
            site = (isnsfw) ? site + Nsfw : site + Sfw;
        }

        private string post = "post.json?";
        private string tag = "tag.json?";
        private string note = "note.json?";
        private string user = "user.json?";
        private string artist = "artist.json?";

        private string Limit(int? limit) { return "limit=" + limit + "&"; }
        private string Page(int? page) { return "page=" + page + "&"; }
        private string Tags(List<string> tags) { string listTags = ""; foreach (var str in tags) { listTags += str + "+"; }; return "tags=" + listTags + "&"; }
        private string Order(orderTag? order) { return "order=" + order.ToString() + "&"; }
        private string Id(int? id) { return "id=" + id + "&"; }
        private string After_id(int? after_id) { return "after_id=" + after_id + "&"; }
        private string Name(string name) { return "name=" + name + "&"; }
        private string Name_pattern(string name_pattern) { return "name_pattern=" + name_pattern + "&"; }
        private string Post_id(int? post_id) { return "post_id" + post_id + "&"; }

        public enum orderTag { date, count, name }

        string GetUrl(string url)
        {
            var x = new WebClient(); x.Encoding = Encoding.UTF8; return x.DownloadString(url);
        }

        public Post[] GetPost(int? limit = null, int? page = null, List<string> tags = null)
        {
            var url = site + post;
            if (limit != null)
            {
                url += Limit(limit);
            }
            else if (page != null)
            {
                url += Page(page);
            }
            else if (tags != null)
            {
                url += Tags(tags);
            }

            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<Post[]>(html);
        }

        public Tag[] GetTag(int? id = null, int? after_id = null, int? limit = null, int? page = null, orderTag? order = null)

        {
            var url = site+ tag;
            if (id != null)
            {
                url += Id(id);
            }
            else if (after_id != null)
            {
                url += After_id(after_id);
            }
            else if (limit != null)
            {
                url += Limit(limit);
            }
            else if (page != null)
            {
                url += Page(page);
            }
            else if (order != null)
            {
                url += Order(order);
            }

            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<Tag[]>(html);
        }
        public Tag[] GetTag(string name = null, string name_pattern = null, int? limit = null, int? page = null, orderTag? order = null)

        {
            var url = site + tag;
            if (name != null)
            {
                url += Name(name);
            }
            else if (name_pattern != null)
            {
                url += Name_pattern(name_pattern);
            }
            else if (limit != null)
            {
                url += Limit(limit);
            }
            else if (page != null)
            {
                url += Page(page);
            }
            else if (order != null)
            {
                url += Order(order);
            }
            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<Tag[]>(html);
        }

        public Artist[] GetArtist(string name = null, orderTag? order = null, int? page = null)
        {
            var url = site + artist;
            if (name != null)
            {
                url += Name(name);
            }
            else if (order != null)
            {
                url += Order(order);
            }
            else if (page != null)
            {
                url += Page(page);
            }
            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<Artist[]>(html);
        }

        public Note[] GetNote(int? post_id = null)
        {
            var url = site + note;
            if (post_id != null)
            {
                url += Post_id(post_id);
            }
            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<Note[]>(html);
        }

        public User[] GetUser(int? id= null)
        {
            var url = site + user;
            if (id != null)
            {
                url += Id(id);
            }

            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<User[]>(html);
        }

        public User[] GetUser(string name = null)
        {
            var url = site + user;
            if (name != null)
            {
                url += Name(name);
            }

            string html = GetUrl(url);
            return JsonConvert.DeserializeObject<User[]>(html);
        }
    }
}