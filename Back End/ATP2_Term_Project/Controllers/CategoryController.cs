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
    [RoutePrefix("api/categories")]
    [BasicAuthorization]
    public class CategoryController : ApiController
    {
        CategoryRepository catRepo = new CategoryRepository();

        ProductRepository proRepo = new ProductRepository();


        [Route("")]

        public IHttpActionResult Get()
        {
            return Ok(catRepo.GetAll());
        }

        [Route("{id}", Name = "GetCategoryById")]
        public IHttpActionResult Get(int id)
        {
            Category cat = catRepo.GetById(id);
            if (cat == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            cat.HyperLinks.Add(new HyperLink() { HRef = "http://localhost:11917/api/categories/" + cat.Id, HttpMethod = "GET", Relation = "Self" });
            cat.HyperLinks.Add(new HyperLink() { HRef = "http://localhost:11917/api/categories", HttpMethod = "POST", Relation = "Create a new Category resource" });
            cat.HyperLinks.Add(new HyperLink() { HRef = "http://localhost:11917/api/categories/" + cat.Id, HttpMethod = "PUT", Relation = "Edit a existing Category resource" });
            cat.HyperLinks.Add(new HyperLink() { HRef = "http://localhost:11917/api/categories/" + cat.Id, HttpMethod = "DELETE", Relation = "Delete a existing Category resource" });
            return Ok(cat);
        }

        [Route("{id}/products")]
        public IHttpActionResult GetProducts(int id)
        {
            return Ok(proRepo.GetProducts(id));
        }

        [Route("")]
        public IHttpActionResult Post(Category cat)
        {
            catRepo.Insert(cat);
            string url = Url.Link("GetCategoryById", new { id = cat.Id });
            return Created(url, cat);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromBody]Category cat, [FromUri]int id)
        {
            cat.Id = id;
            catRepo.Edit(cat);
            return Ok(cat);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            catRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
