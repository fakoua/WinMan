using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinMan.Storage.Models;

namespace WinMan.api
{
  public class StorageController : ApiController
    {
        public IEnumerable<string> Get()
        {
            var drives =  System.IO.DriveInfo.GetDrives();
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public List<DriveModel> Drives()
        {
            return Storage.Utils.Drives.GetDrives();
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }

    }
}
