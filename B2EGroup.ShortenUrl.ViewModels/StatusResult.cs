using B2EGroup.ShortenUrl.Models;
using System.Collections.Generic;

namespace B2EGroup.ShortenUrl.ViewModels
{
    public class StatusResult
    {
        public int Hits { get; set; }
        public int UrlsCount { get; set; }
        public List<Url> Urls { get; set; }
    }
}
