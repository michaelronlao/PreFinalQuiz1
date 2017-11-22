using PreFinalQuiz1_LaoM.App_Code;
using PreFinalQuiz1_LaoM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreFinalQuiz1_LaoM.Controllers
{
    public class TitlesController : Controller
    {
        public ActionResult Index()
        {

            List<TitlesModels> list = new List<TitlesModels>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT t.titleID, p.pubName, a.authorLN , a.authorFN, t.titleName,
                              t.titlePrice, t.titlePubDate, t.titleNotes
                              FROM titles t
                              INNER JOIN publishers p ON t.pubID = p.pubID
                              INNER JOIN authors a ON t.authorID = a.authorID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new TitlesModels
                            {
                                ID = Convert.ToInt32(data["titleID"].ToString()),
                                Publisher = data["pubName"].ToString(),
                                Author = data["authorLN"].ToString(),
                                AuthorFN = data["authorFN"].ToString(),
                                TitleName = data["titleName"].ToString(),
                                Price = double.Parse(data["titlePrice"].ToString()),
                                Date = DateTime.Parse(data["titlePubDate"].ToString()),
                                Notes = data["titleNotes"].ToString()

                            });
                        }
                    }
                }
            }

            return View(list);
        }
        public ActionResult Add()
        {
            TitlesModels title = new TitlesModels();
            title.Publishers = GetPublishers();
            title.Authors = GetAuthors();

            return View(title);
        }


        [HttpPost]
        public ActionResult Add(TitlesModels title, TitlesModels title1)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO titles VALUES
                       (@pubID, @authorID, @titleName, @titlePrice, 
                        @titlePubDate, @titleNotes)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@pubID", title.pubID);
                    cmd.Parameters.AddWithValue("@authorID", title.authorID);
                    cmd.Parameters.AddWithValue("@titleName", title.TitleName);
                    cmd.Parameters.AddWithValue("@titlePrice", title.Price);
                    cmd.Parameters.AddWithValue("@titlePubDate", title.Date);
                    cmd.Parameters.AddWithValue("@titleNotes", title.Notes);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }

        }
        public List<SelectListItem> GetPublishers()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT pubID, pubName FROM publishers
                             ORDER BY pubName";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = data["pubName"].ToString(),
                                Value = data["pubID"].ToString()

                            });

                        }
                    }
                }
            }
            return items;
        }
        public List<SelectListItem> GetAuthors()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT authorID, authorLN, authorFN FROM authors
                             ORDER BY authorLN";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = data["authorLN"].ToString() + ", " + data["authorFN"].ToString(),
                                Value = data["authorID"].ToString()

                            });

                        }
                    }
                }
            }
            return items;
        }
    }
}