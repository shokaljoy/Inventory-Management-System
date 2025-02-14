﻿using ATP2_Term_Project.Attribute;
using ATP2_Term_Project.Models;
using ATP2_Term_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATP2_Term_Project.Controllers
{
    [RoutePrefix("api/users")]
    [BasicAuthorization]
    public class UserController : ApiController
    {
        UserRepository uRepo = new UserRepository();

        [Route("username")]
        
        public IHttpActionResult GetUser()
        {
            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
            string username = headers.GetValues("userName").First();
            return Ok(uRepo.GetByUsername(username));
        }

        [Route("")]

        public IHttpActionResult Get()
        {
            List<User> users = uRepo.GetWorkers();

            return Ok(uRepo.GetWorkers());
        }

        [Route("{id}", Name = "GetUserById")]
        public IHttpActionResult Get(int id)
        {
            User info = uRepo.GetById(id);
            if (info == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }


            return Ok(info);
        }

        [Route("")]
        public IHttpActionResult Post(User info)
        {
            uRepo.Insert(info);
            string url = Url.Link("GetUserById", new { id = info.Id });
            return Created(url, info);
        }
        [Route("{id}")]
        public IHttpActionResult Put([FromBody]User info, [FromUri]int id)
        {
            info.Id = id;
            uRepo.Edit(info);
            return Ok(info);
        }



    }
}
