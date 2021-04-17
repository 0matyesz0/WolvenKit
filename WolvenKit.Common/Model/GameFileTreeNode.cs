using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WolvenKit.Common.Model;
using ZeroFormatter;

namespace WolvenKit.Common
{

    [ZeroFormattable]
    public class GameFileTreeNode
    {
        #region Constructors

        public GameFileTreeNode()
        {
            Directories = new Dictionary<string, GameFileTreeNode>();
            Files = new Dictionary<string, List<IGameFile>>();
            Name = "";
        }

        public GameFileTreeNode(EArchiveType bundleType)
        {
            Directories = new Dictionary<string, GameFileTreeNode>();
            Files = new Dictionary<string, List<IGameFile>>();
            Name = "";
        }

        #endregion Constructors

        #region Properties

        [Index(0)] public virtual Dictionary<string, GameFileTreeNode> Directories { get; set; }

        [Index(1)]
        public virtual string Extension
        {
            get => nameof(ECustomImageKeys.ClosedDirImageKey);
            set { }
        }

        [Index(2)] public virtual Dictionary<string, List<IGameFile>> Files { get; set; }

        [Index(3)]
        public virtual string FullPath
        {
            get
            {
                var path = "";
                var current = this;
                while (true)
                {
                    path = Path.Combine(current.Name, path);
                    current = current.Parent;
                    if (current == null)
                        break;
                }
                return path ?? "";
            }
            set { }
        }

        [Index(4)] public virtual string Name { get; set; }

        [Index(5)] public virtual GameFileTreeNode Parent { get; set; }

        [Index(6)]
        public virtual List<GameFileTreeNode> SubDirectories
        {
            get { return Directories.Values.OrderBy(_ => _.Name).ToList(); }
            set { }
        }

        [Index(7)]
        public virtual EArchiveType Type
        {
            get
            {
                // Getting the type of the archive by its path
                string[] bundlenodenames = FullPath.Split(Path.DirectorySeparatorChar);
                string bundlename;
                //At app startup there will be no Root prefixed, but after it will
                if (bundlenodenames.First() == "Root")
                {
                    bundlename = bundlenodenames.Skip(1).First();
                }
                else
                {
                    bundlename = bundlenodenames.First();
                }

                if (String.IsNullOrEmpty(bundlename) || bundlename.ToUpper() == "ROOT" || bundlename.ToUpper() == "DEPOT")
                    bundlename = EArchiveType.ANY.ToString();

                return (EArchiveType)Enum.Parse(typeof(EArchiveType), bundlename);
            }
            set { }
        }

        #endregion Properties

        #region Methods

        public List<AssetBrowserData> ToAssetBrowserData()
        {
            var ret = new List<AssetBrowserData>();
            ret.Add(new AssetBrowserData()
            {
                Name = "..",
                Extension = nameof(ECustomImageKeys.OpenDirImageKey),
                Type = EntryType.MoveUP,
                This = this,
                Parent = this.Parent
            });
            ret.AddRange(Directories.Select(d => new AssetBrowserData()
            {
                Name = d.Key,
                Size = d.Value.Directories.Count + " directories, " + d.Value.Files.Count + " files",
                Parent = this.Parent,
                Children = d.Value,
                Extension = nameof(ECustomImageKeys.ClosedDirImageKey),
                This = this,
                Type = EntryType.Directory
            }).OrderBy(_ => _.Name));

            ret.AddRange(Files.Select(f => new AssetBrowserData()
            {
                Name = f.Key,
                Size = FormatSize(f.Value[0].Size),
                This = this,
                Extension = Path.GetExtension(f.Key),
                Type = EntryType.File,
                Parent = this.Parent
            }).OrderBy(_ => _.Name));

            return ret;

            string FormatSize(uint size)
            {
                string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

                var counter = 0;
                var number = (decimal)size;
                while (Math.Round(number / 1024) >= 1)
                {
                    number = number / 1024;
                    counter++;
                }
                return $"{number:n1} {suffixes[counter]}";
            }
        }

        #endregion Methods
    }
}
