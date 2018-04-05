using Jacobs.Api.DB;
using Jacobs.Api.Models.Request;
using Jacobs.Api.Models.Response;
using System;
using System.Linq;
using System.Web.Http;

namespace Jacobs.Api.Controllers
{
    public class JobsController : ApiController
    {
        private DataClasses1DataContext _context;

        public JobsController()
        {
            if (_context == null) _context = new DataClasses1DataContext(System.Configuration.ConfigurationManager.ConnectionStrings["FFXIVSimConnectionString"].ToString());
        }

        [HttpPost]
        public IHttpActionResult Create(string name)
        {
            var job = new Job
            {
                Name = name
            };

            try
            {
                _context.Jobs.InsertOnSubmit(job);
                _context.SubmitChanges();

                return Ok(new GenericResponse
                {
                    Success = true
                });
            }
            catch (Exception exc)
            {
                //Logging goes here
                return Ok(new GenericResponse
                {
                    Success = false,
                    Errors = exc.Message
                });
            }
        }

        [HttpGet]
        public IHttpActionResult Read(string name)
        {
            var results = _context.Jobs.Where(x => x.Name.ToLower() == name.ToLower());

            return Ok(new GenericResponse
            {
                Success = true,
                Results = results
            });
        }

        [HttpPost]
        public IHttpActionResult Update(JobsRequestModel jrm)
        {
            if (jrm.Id < 1) return Ok(new GenericResponse { Success = false, Errors = "Cannot update a record that does not exist." });

            var record = _context.Jobs.FirstOrDefault(x => x.Id == jrm.Id);

            if (record == null) return Ok(new GenericResponse { Success = false, Errors = "The requested record could not be found." });
            
            record.Name = jrm.Name;

            _context.SubmitChanges();

            return Ok(new GenericResponse
            {
                Success = true,
                Results = record
            });
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id < 1) return Ok(new GenericResponse { Success = false, Errors = "Cannot update a record that does not exist." });

            var record = _context.Jobs.FirstOrDefault(x => x.Id == id);

            if (record == null) return Ok(new GenericResponse { Success = false, Errors = "The requested record could not be found." });

            try
            {
                _context.Jobs.DeleteOnSubmit(record);

                _context.SubmitChanges();

                return Ok(new GenericResponse
                {
                    Success = true
                });
            }
            catch (Exception exc)
            {
                //Logging goes here
                return Ok(new GenericResponse
                {
                    Success = false,
                    Errors = exc.Message
                });
            }
        }
    }
}
