using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jacobs.Api.Models.Response
{
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Errors { get; set; }
        public object Results { get; set; }

        public GenericResponse()
        {

        }
    }
}