using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_APP.Models
{
    public class SSO
    {
        public string EntityID { get; set; }
        public string IdentityProvider { get; set; }
        public string MetadataLocation { get; set; }
        public string LogoutURL { get; set; }
    }
}
