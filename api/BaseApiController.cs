using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskManager.Helpers;
using TaskManager.Infrastructure;
using TaskManager.Models;

namespace TaskManager.api
{

    public class BaseApiController : ApiController
    {
        public EntitiesDb db = new EntitiesDb();
    }

}