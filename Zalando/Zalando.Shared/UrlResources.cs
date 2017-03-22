using System;
using System.Collections.Generic;
using System.Text;

namespace Zalando
{
    public static class UrlResources
    {
        public const string facetsUrl = "https://api.zalando.com/facets";
        public const string articles = "https://api.zalando.com/articles?fields=id,name,units.id,units.size,units.price.value,media";
    }
}
