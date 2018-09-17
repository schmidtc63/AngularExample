using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionResults;
using IMS.Model;
using IMS.Model.lh;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace IMT.LH.FanSignup.Controllers
{    
    [DnnHandleError]
    public class FanSignupController : DnnController
    {
        public ActionResult Index()
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string GetSignup()
        {
            var myFan = new Models.FanSignup();
            using (var c = new ArtistListingController())
            {
                c.Gets();
                foreach (var rec in c.Recordset)
                {
                    var n = new Models.FanSignup.EFanToArtist();
                    PopulateDerivedFromBase(rec, n);
                    myFan.FanToArtists.Add(n);
                }
            }

            using (var c = new AssociationsController())
            {
                c.Gets();
                foreach (var rec in c.Recordset)
                {
                    var n = new Models.FanSignup.EFanToAssociation();
                    PopulateDerivedFromBase(rec, n);
                    myFan.FanToAssociations.Add(n);
                }
            }

            using (var c = new RegionsController())
            {
                c.Gets();
                foreach (var rec in c.Recordset)
                {
                    var n = new Models.FanSignup.EFanToRegion();
                    PopulateDerivedFromBase(rec, n);
                    myFan.FanToRegions.Add(n);
                }
            }

            using (var c = new GenresController())
            {
                c.Gets();
                foreach (var rec in c.Recordset)
                {
                    var n = new Models.FanSignup.EFanToGenre();
                    PopulateDerivedFromBase(rec, n);
                    myFan.FanToGenres.Add(n);
                }
            }

            myFan.Fan = new Fan();
            var xx = JsonConvert.SerializeObject(myFan);
            return xx;

        }

        [HttpPost]
        [AllowAnonymous]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        //[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public ActionResult SaveSignup()
        {
            var sFanInfo=Server.UrlDecode(Request.Form["data"]);
            Models.FanSignup fanInfo = JsonConvert.DeserializeObject<Models.FanSignup>(sFanInfo);
            var email = fanInfo.Fan.EmailAddress;
            var status = false;
            try {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                return Json(new {status=status,message="Invalid Email Address"});
            }

             status=saveSignup(fanInfo);
            
            return Json(new {status=status});
        }

        [HttpPost]
        [AllowAnonymous]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult SaveSignupAng(Models.FanSignup fanInfo)
        {
            var email = fanInfo.Fan.EmailAddress;
            var status = false;
            try {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                return Json(new {status=status,message="Invalid Email Address"});
            }
            status=saveSignup(fanInfo);
            return Json(new {data=status});
        }

        private bool saveSignup(Models.FanSignup fanInfo)
        {
            try
            {
                long fanid = -1;
                using (var fc = new FanController())
                {
                    fc.Recordset.Add(fanInfo.Fan);

                    fc.Inserts();
                    fanid = fc.CurrentRec.FanID;
                }

                using (var ftac = new FanToArtistController())
                {
                    foreach (var r in fanInfo.FanToArtists.Where(p => p.selected))
                    {
                        ftac.Recordset.Add(new FanToArtist
                        {
                            FanID = fanid,
                            ArtistID = r.ArtistID
                        });
                    }

                    ftac.Inserts();
                }

                using (var ftac = new FanToAssociationController())
                {
                    foreach (var r in fanInfo.FanToAssociations.Where(p => p.selected))
                    {
                        ftac.Recordset.Add(new FanToAssociation
                        {
                            FanID = fanid,
                            AssociationID = r.AssociationID
                        });
                    }

                    ftac.Inserts();
                }

                using (var ftgc = new FanToGenreController())
                {
                    foreach (var r in fanInfo.FanToGenres.Where(p => p.selected))
                    {
                        ftgc.Recordset.Add(new FanToGenre
                        {
                            FanID = fanid,
                            GenreID = r.GenreID
                        });
                    }

                    ftgc.Inserts();
                }

                using (var ftrc = new FanToRegionController())
                {
                    foreach (var r in fanInfo.FanToRegions.Where(p => p.selected))
                    {
                        ftrc.Recordset.Add(new FanToRegion
                        {
                            FanID = fanid,
                            RegionID = r.RegionID
                        });
                    }

                    ftrc.Inserts();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static void PopulateDerivedFromBase<TB,TD>(TB baseclass,TD derivedclass)
        {
            //get our baseclass properties
            var bprops = baseclass.GetType().GetProperties();
            foreach (var bprop in bprops)
            {
                //get the corresponding property in the derived class
                var dprop = derivedclass.GetType().GetProperty(bprop.Name);
                //if the derived property exists and it's writable, set the value
                if (dprop != null && dprop.CanWrite)
                    dprop.SetValue(derivedclass,bprop.GetValue(baseclass, null),null);
            }
        } 
    }
}