using System.Collections.Generic;
using System.Web.Http;
using WinMan.Models;
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

        [Authorize]
        [HttpGet]
        public List<DriveModel> Drives()
        {
            return Storage.Utils.Drives.GetDrives();
        }

        [HttpPost]
        public FolderModel Folders([FromBody]PostFolderModel folder)
        {
            folder.Folder = folder.Folder.Replace("|", "\\");
            return Storage.Utils.Folders.GetFolder(folder.Folder );
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
