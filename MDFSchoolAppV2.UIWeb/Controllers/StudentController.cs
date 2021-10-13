using MDFSchoolAppV2.Entities;
using MDFSchoolAppV2.UIWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MDFSchoolAppV2.UIWeb.Controllers
{
    public class StudentController : Controller
    {
        private ApiRouteValues ApiRoutes { get; }
        public StudentController(IOptions<ApiRouteValues> options)
        {
            ApiRoutes = options.Value;
        }

        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            //get url from settings.
            var url = new Uri(ApiRoutes.BaseUrl + ApiRoutes.StudentEndPoint);

            List<Student> students = new List<Student>();

            //get student list from api.
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if(response.IsSuccessStatusCode)
                    {
                        students = JsonConvert.DeserializeObject<List<Student>>(result);
                    }
                    else
                    {
                        ViewBag.Result = "Internal error";
                        ModelState.AddModelError(string.Empty, result);
                    }
                }
            }

            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Add() => View();

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStudent(IFormCollection collection)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View("Add", collection);
                }

                var student = new Student
                {
                    Address = collection["Address"],
                    Email = collection["Email"],
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Phone = collection["Phone"]
                };

                //Post student
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var uri = new Uri(ApiRoutes.BaseUrl + ApiRoutes.StudentEndPoint);

                        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8);
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                        var result = await httpClient.PostAsync(uri, httpContent);
                        if(result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Result = "Internal Error";
                            ModelState.AddModelError(string.Empty, result.ReasonPhrase);

                            return View("Add", collection);
                        }

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Result = "Internal Error";
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View("Add", collection);
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //get url from settings.
            var url = new Uri(ApiRoutes.BaseUrl + ApiRoutes.StudentEndPoint + "/" + id.ToString());

            Student student = new Student();

            //get student list from api.
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        student = JsonConvert.DeserializeObject<Student>(result);
                    }
                    else
                    {
                        ViewBag.Result = "Internal error";
                        ModelState.AddModelError(string.Empty, result);
                    }
                }
            }

            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Add", collection);
                }

                var student = new Student
                {
                    Id = id,
                    Address = collection["Address"],
                    Email = collection["Email"],
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Phone = collection["Phone"]
                };

                //Post student
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var uri = new Uri(ApiRoutes.BaseUrl + ApiRoutes.StudentEndPoint + "/" + id.ToString());

                        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8);
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                        var result = await httpClient.PutAsync(uri, httpContent);
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Result = "Internal Error";
                            ModelState.AddModelError(string.Empty, result.ReasonPhrase);

                            return View("Add", collection);
                        }

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Result = "Internal Error";
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View("Add", collection);
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //get url from settings.
            var url = new Uri(ApiRoutes.BaseUrl + ApiRoutes.StudentEndPoint + "/" + id.ToString());

            Student student = new Student();

            //get student list from api.
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(url))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Result = "Internal error";
                        ModelState.AddModelError(string.Empty, result);
                    }
                }
            }

            return RedirectToAction("Index");

        }

    }
}
