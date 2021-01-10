using System;
using System.Collections.Generic;
using System.Text;

namespace ArchPM.NetCore.Tests.TestModels
{
    public class SampleUser : SampleUserShort
    {
        public SampleUser(SampleUserShort instaUserShort)
        {
            Pk = instaUserShort.Pk;
            UserName = instaUserShort.UserName;
            FullName = instaUserShort.FullName;
            IsPrivate = instaUserShort.IsPrivate;
            ProfilePicture = instaUserShort.ProfilePicture;
            ProfilePictureId = instaUserShort.ProfilePictureId;
            IsVerified = instaUserShort.IsVerified;
        }

        public bool HasAnonymousProfilePicture { get; set; }
        public int FollowersCount { get; set; }
        public string FollowersCountByLine { get; set; }
        public string SocialContext { get; set; }
        public string SearchSocialContext { get; set; }
        public int MutualFollowers { get; set; }
        public int UnseenCount { get; set; }
    }
    
    [Serializable]
    public class SampleUserShort
    {
        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public long Pk { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePicUrl { get; set; }
        public string ProfilePictureId { get; set; } = "unknown";
        public string UserName { get; set; }
        public string FullName { get; set; }

        public static SampleUserShort Empty => new SampleUserShort {FullName = string.Empty, UserName = string.Empty};

        public bool Equals(SampleUserShort user)
        {
            return Pk == user?.Pk;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SampleUserShort);
        }

        public override int GetHashCode()
        {
            return Pk.GetHashCode();
        }
    }
}
