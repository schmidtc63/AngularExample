/*
' Copyright (c) 2018 Indy Music Technologies
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.UI.WebControls;
using IMS.Model.lh;
using System.Data.Entity.Spatial;
using SqlServerTypes;

namespace IMT.LH.FanSignup.Models
{
    
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class FanSignup
    {
        public Fan Fan { get; set; }

        public class EFanToArtist:ArtistTemp
        {

            public bool selected { get; set; } = false;
        }

        public class EFanToAssociation:Associations
        {
            public bool selected { get; set; } = false;
        }

        public class EFanToGenre:Genres
        {
            public bool selected { get; set; } = false;
        }

        public class EFanToRegion:Regions
        {
            public bool selected { get; set; } = false;
        }

        public IList<EFanToArtist> FanToArtists { get; set; }
        public IList<EFanToAssociation> FanToAssociations { get; set; }
        public IList<EFanToGenre> FanToGenres { get; set; }
        public IList<EFanToRegion> FanToRegions { get; set; }

        public FanSignup()
        {
            FanToArtists=new List<EFanToArtist>();
            FanToAssociations=new List<EFanToAssociation>();
            FanToGenres=new List<EFanToGenre>();
            FanToRegions=new List<EFanToRegion>();
        }

        public class ArtistTemp
        {
            public Int32 ArtistID { get; set; }
            public string ArtistName { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string Zip { get; set; }
            public string Phone { get; set; }
            public string Website { get; set; }
            public string EmailAddress { get; set; }
            public string ContactFirstName { get; set; }
            public string ContactLastName { get; set; }
            public string ContactPhone { get; set; }
            public string ContactEmail { get; set; }
            public string PayPalEmail { get; set; }
            public string Facebook { get; set; }
            public string Twitter { get; set; }
            public string Instagram { get; set; }
            public string Google { get; set; }
            public string Bing { get; set; }
            public string Yelp { get; set; }
            public string LinkedIn { get; set; }
            public DateTime? CreateDate { get; set; }
            public DateTime? ModifyDate { get; set; }
            public string Status { get; set; }
            public string slug { get; set; }
            public Int32? zoneid { get; set; }
            public Double? longitude { get; set; }
            public Double? latitude { get; set; }
            //public DbGeography p { get; set; }
            public String Description { get; set; }
            [MaxLength(256)] 
            public String Tagline { get; set; }
            
        }
    }
}
