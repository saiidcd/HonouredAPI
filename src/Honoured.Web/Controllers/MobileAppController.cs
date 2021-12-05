using Honoured.Dimensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Web.Controllers
{
    [AllowAnonymous]
    public class MobileAppController: Controller
    {
        private IRepository<Dimension, long> dimService;

        public MobileAppController(IRepository<Dimension, long> dimService)
        {
            this.dimService = dimService;
        }
        [AllowAnonymous]
        public JsonResult GetProtfolioScreenInfo()
        {

            /*
             * dimensions
             * disciplines
             * tags
             * artstatus
             * */
            var dimensions  =dimService.GetListAsync().GetAwaiter().GetResult();
            var res = JsonConvert.SerializeObject(dimensions);
            return new JsonResult(res);
        }
    }
}
