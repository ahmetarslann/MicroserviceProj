using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceProj.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("61bfa466-c2b9-4076-a9dc-ceaf80f81ae0");

        public string UserName => "Ahmet34";
    }
}
