using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.User.Models
{
    public class GeneralUser
    {
        public Int64 membershipId { get; set; }
        public String uniqueName { get; set; }
        public String normalizedName { get; set; }
        public String displayName { get; set; }
        public String profilePicture { get; set; }
        public String profileTheme { get; set; }
        public Int32 userTitle { get; set; }
        public Int64 successMessageFlags { get; set; }
        public bool isDeleted { get; set; }
        public String about { get; set; }
        public Nullable<DateTime> firstAccess { get; set; }
        public Nullable<DateTime> lastUpdate { get; set; }
        public Nullable<Int64> legacyPortalUID { get; set; }
        public String psnDisplayName { get; set; }
        public String xboxDisplayName { get; set; }
        public String fbDisplayName { get; set; }
        public Nullable<Boolean> showActivity { get; set; }
        public String locale { get; set; }
        public bool localeInheritDefault { get; set; }
        public Nullable<Int64> lastBanReportId { get; set; }
        public bool showGroupMessaging { get; set; }
        public String profilePicturePath { get; set; }
        public String profilePictureWidePath { get; set; }
        public String profileThemeName { get; set; }
        public String userTitleDisplay { get; set; }
        public String statusText { get; set; }
        public DateTime statusDate { get; set; }
        public Nullable<DateTime> profileBanExpire { get; set; }
        public String blizzardDisplayName { get; set; }


        public String profilePictureUri
        {
            get
            {
                return GetProfilePictureUri();
            }
        }

        private String GetProfilePictureUri()
        {
            return String.Concat("https://www.bungie.net", profilePicturePath);
        }
    }
}
