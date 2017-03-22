using System;
using System.Collections.Generic;
using System.Text;

namespace Zalando.Data
{
    public class Facet
    {
        public string key { get; set; }
        public string displayName { get; set; }
        public int count { get; set; }
    }

    public class Facets
    {
        public string filter { get; set; }

        private List<Facet> _facets = new List<Facet>();
        public List<Facet> facets
        {
            get
            {
                return _facets;
            }
            set
            {
                _facets = value;
            }
        }
    }
}
