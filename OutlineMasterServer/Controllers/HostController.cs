using OutlineMasterServer.Models;
using System.Data;
using System.Web.Http;

namespace OutlineMasterServer.Controllers
{
    public class HostController : ApiController
    {
        // GET: api/Host
        public DataTable Get()
        {
            DatabaseInterface DBI = new DatabaseInterface();
            return DBI.GetAllServers();
        }

        // GET: api/Host/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Host
        public int Post(ServerData Data)
        {
            DatabaseInterface DBI = new DatabaseInterface();
            return DBI.PostData(Data);
        }

        // PUT: api/Host/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Host/5
        public void Delete(int id)
        {
            DatabaseInterface DBI = new DatabaseInterface();
            DBI.DeleteData();
        }
    }
}
