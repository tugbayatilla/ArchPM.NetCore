using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace ArchPM.NetCore.Tests.TestModels.ComplexItem
{
    public class ComplexClass : INotifyPropertyChanged
    {
        public long TakenAtUnix { get; set; }
        public DateTime TakenAt { get; set; }
        public string Pk { get; set; }

        public string InstaIdentifier { get; set; }

        public DateTime DeviceTimeStamp { get; set; }
        public InstaMediaType MediaType { get; set; }

        public string Code { get; set; }

        public string ClientCacheKey { get; set; }
        public string FilterType { get; set; }

        public List<InstaImage> Images { get; set; } = new List<InstaImage>();
        public List<InstaVideo> Videos { get; set; } = new List<InstaVideo>();

        public int Width { get; set; }
        public string Height { get; set; }

        public InstaUser User { get; set; }

        public string TrackingToken { get; set; }

        private int _likecount;
        public int LikesCount { get { return _likecount; } set { _likecount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LikesCount")); } }

        public string NextMaxId { get; set; }

        public InstaCaption Caption { get; set; }

        private string _cmcount;
        public string CommentsCount { get => _cmcount; set { _cmcount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommentsCount")); } }

        public bool IsCommentsDisabled { get; set; }

        public bool PhotoOfYou { get; set; }

        private bool _hasliked { get; set; }
        public bool HasLiked { get { return _hasliked; } set { _hasliked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasLiked")); } }

        public List<InstaUserTag> UserTags { get; set; } = new List<InstaUserTag>();

        public InstaUserShortList Likers { get; set; } = new InstaUserShortList();
        public InstaCarousel Carousel { get; set; }

        public int ViewCount { get; set; }

        public bool HasAudio { get; set; }

        public bool IsMultiPost => Carousel != null;
        public List<InstaComment> PreviewComments { get; set; } = new List<InstaComment>();
        public InstaLocation Location { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _play = false;
        /// <summary>
        /// This property is for developer's personal use. 
        /// </summary>
        public bool Play { get { return _play; } set { _play = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Play")); } }


        public bool CommentLikesEnabled { get; set; }

        public bool CommentThreadingEnabled { get; set; }

        public bool HasMoreComments { get; set; }

        public int MaxNumVisiblePreviewComments { get; set; }

        public bool CanViewMorePreviewComments { get; set; }

        public bool CanViewerReshare { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool CanViewerSave { get; set; }

        private bool _hasviewersaved;
        public bool HasViewerSaved { get => _hasviewersaved; set { _hasviewersaved = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasViewerSaved")); } }

        public string Title { get; set; }

        public string ProductType { get; set; }

        public bool NearlyCompleteCopyrightMatch { get; set; }

        public int NumberOfQualities { get; set; }

        public double VideoDuration { get; set; }

        public List<InstaProductTag> ProductTags { get; set; } = new List<InstaProductTag>();

        public bool DirectReplyToAuthorEnabled { get; set; }
    }

    public enum InstaMediaType
    {
        Image = 1,
        Video = 2,
        Carousel = 8
    }

    public class InstaProductTag
    {
        public InstaProduct Product { get; set; }

        public InstaPosition Position { get; set; }
    }

    public class InstaProduct
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string CurrentPrice { get; set; }

        public string FullPrice { get; set; }

        public long ProductId { get; set; }

        public bool HasViewerSaved { get; set; }

        public List<InstaImage> MainImage { get; set; } = new List<InstaImage>();

        public List<InstaImage> ThumbnailImage { get; set; } = new List<InstaImage>();

        public List<InstaImage> ProductImages { get; set; } = new List<InstaImage>();

        public string ReviewStatus { get; set; }

        public string ExternalUrl { get; set; }

        public string CheckoutStyle { get; set; }

        public InstaMerchant Merchant { get; set; }

        public string ProductAppealReviewStatus { get; set; }

        public string FullPriceStripped { get; set; }

        public string CurrentPriceStripped { get; set; }
    }

    public class InstaImage
    {
        public InstaImage(string uri, int width, int height)
        {
            Uri = uri;
            Width = width;
            Height = height;
        }

        public InstaImage()
        {
        }

        public string Uri { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [JsonIgnore]
        /// <summary>
        /// This is only for .NET core apps like UWP(Windows 10) apps
        /// </summary>
        public byte[] ImageBytes { get; set; }
    }

    public class InstaMerchant
    {
        public long Pk { get; set; }

        public string Username { get; set; }

        public string ProfilePicture { get; set; }
    }

    public class InstaPosition
    {
        public InstaPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }

    public class InstaLocation : InstaLocationShort
    {
        public double Rotation { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public long FacebookPlacesId { get; set; }

        public string City { get; set; }

        public long Pk { get; set; }

        public string ShortName { get; set; }
    }

    public class InstaLocationShort
    {
        public string ExternalSource { get; set; }

        public string ExternalId { get; set; }

        public string Address { get; set; }

        public double Lng { get; set; }

        public double Lat { get; set; }

        public string Name { get; set; }
    }

    public class InstaVideo
    {
        public InstaVideo() { }
        public InstaVideo(string url, int width, int height) : this(url, width, height, 3) { }
        public InstaVideo(string url, int width, int height, int type)
        {
            Uri = url;
            Width = width;
            Height = height;
            Type = type;
        }

        public string Uri { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Type { get; set; }

        internal string UploadId { get; set; }

        public double Length { get; set; } = 0;

        [JsonIgnore]
        /// <summary>
        /// This is only for .NET core apps like UWP(Windows 10) apps
        /// </summary>
        public byte[] VideoBytes { get; set; }
    }

    public class InstaUser : InstaUserShort
    {
        public InstaUser(InstaUserShort instaUserShort)
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
        public InstaFriendshipShortStatus FriendshipStatus { get; set; }
    }

    [Serializable]
    public class InstaUserShort
    {
        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public long Pk { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePicUrl { get; set; }
        public string ProfilePictureId { get; set; } = "unknown";
        public string UserName { get; set; }
        public string FullName { get; set; }

        public static InstaUserShort Empty => new InstaUserShort { FullName = string.Empty, UserName = string.Empty };

        public bool Equals(InstaUserShort user)
        {
            return Pk == user?.Pk;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InstaUserShort);
        }

        public override int GetHashCode()
        {
            return Pk.GetHashCode();
        }
    }

    public class InstaFriendshipShortStatusList : List<InstaFriendshipShortStatus> { }

    public class InstaFriendshipShortStatus
    {
        public long Pk { get; set; }

        public bool Following { get; set; }

        public bool IsPrivate { get; set; }

        public bool IncomingRequest { get; set; }

        public bool OutgoingRequest { get; set; }

        public bool IsBestie { get; set; }
    }

    public class InstaCaption
    {
        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstaUserShort User { get; set; }

        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }
    }

    public class InstaUserTag
    {
        public InstaPosition Position { get; set; }

        public string TimeInVideo { get; set; }

        public InstaUserShort User { get; set; }
    }

    public class InstaUserShortList : List<InstaUserShort>, IInstaBaseList
    {
        public string NextMaxId { get; set; }
    }

    public interface IInstaBaseList
    {
        string NextMaxId { get; set; }
    }

    public class InstaCarousel : List<InstaCarouselItem>
    {
    }

    public class InstaCarouselItem
    {
        public string InstaIdentifier { get; set; }

        public InstaMediaType MediaType { get; set; }

        public List<InstaImage> Images { get; set; } = new List<InstaImage>();

        public List<InstaVideo> Videos { get; set; } = new List<InstaVideo>();

        public int Width { get; set; }

        public int Height { get; set; }

        public string Pk { get; set; }

        public string CarouselParentId { get; set; }

        public List<InstaUserTag> UserTags { get; set; } = new List<InstaUserTag>();
    }

    public class InstaComment : INotifyPropertyChanged
    {
        public int Type { get; set; }

        public int BitFlags { get; set; }

        public long UserId { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public int LikesCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstaContentType ContentType { get; set; }
        public InstaUserShort User { get; set; }
        public long Pk { get; set; }
        public string Text { get; set; }

        public bool DidReportAsSpam { get; set; }

        private bool _haslikedcm;
        public bool HasLikedComment { get => _haslikedcm; set { _haslikedcm = value; Update("HasLikedComment"); } }

        public int ChildCommentCount { get; set; }

        //public int NumTailChildComments { get; set; }

        public bool HasMoreTailChildComments { get; set; }

        public bool HasMoreHeadChildComments { get; set; }

        //public string NextMaxChildCursor { get; set; }
        public List<InstaCommentShort> PreviewChildComments { get; set; } = new List<InstaCommentShort>();

        public List<InstaUserShort> OtherPreviewUsers { get; set; } = new List<InstaUserShort>();

        public event PropertyChangedEventHandler PropertyChanged;
        private void Update(string PName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PName)); }

        public bool Equals(InstaComment comment)
        {
            return Pk == comment?.Pk;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InstaComment);
        }

        public override int GetHashCode()
        {
            return Pk.GetHashCode();
        }
    }

    public enum InstaContentType
    {
        Photo = 0,
        Video = 1,
        Comment = 2
    }

    public class InstaCommentShort : INotifyPropertyChanged
    {
        public InstaContentType ContentType { get; set; }

        public InstaUserShort User { get; set; }

        public long Pk { get; set; }

        public string Text { get; set; }

        public int Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public long MediaId { get; set; }

        public string Status { get; set; }

        public long ParentCommentId { get; set; }

        private bool _haslikedcm;
        public bool HasLikedComment { get => _haslikedcm; set { _haslikedcm = value; Update("HasLikedComment"); } }

        public int CommentLikeCount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Update(string memberName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
