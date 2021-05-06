using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Catel.IoC;
using Catel.Logging;
using WolvenKit.Bundles;
using WolvenKit.Cache;
using WolvenKit.Common;
using WolvenKit.Common.Model;
using WolvenKit.Functionality.Controllers;
using WolvenKit.Functionality.Services;
using WolvenKit.Functionality.WKitGlobal.Helpers;
using WolvenKit.RED3.CR2W;
using WolvenKit.W3Speech;
using WolvenKit.W3Strings;

namespace WolvenKit.MVVM.Model.ProjectManagement.Project
{
    public sealed class Tw3Project : EditorProject, ICloneable
    {
        
        public Tw3Project(string location) : base(location)
        {

        }

        public Tw3Project() : base("")
        {
        }

        [XmlIgnore]
        public WitcherPackSettings PackSettings { get; set; } = new WitcherPackSettings();

        [XmlIgnore]
        public string RadishDirectory
        {
            get
            {
                var dir = Path.Combine(ProjectDirectory, "files", "Radish");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;
            }
        }

        [XmlIgnore]
        public string RawDirectory
        {
            get
            {
                var dir = Path.Combine(FileDirectory, "Raw");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;
            }
        }

        [XmlIgnore]
        public string ModCookedDirectory
        {
            get
            {
                if (!Directory.Exists(Path.Combine(ModDirectory, EProjectFolders.Cooked.ToString())))
                {
                    Directory.CreateDirectory(Path.Combine(ModDirectory, EProjectFolders.Cooked.ToString()));
                }

                return Path.Combine(ModDirectory, EProjectFolders.Cooked.ToString());
            }
        }

        [XmlIgnore]
        public string ModUncookedDirectory
        {
            get
            {
                if (!Directory.Exists(Path.Combine(ModDirectory, EProjectFolders.Uncooked.ToString())))
                {
                    Directory.CreateDirectory(Path.Combine(ModDirectory, EProjectFolders.Uncooked.ToString()));
                }

                return Path.Combine(ModDirectory, EProjectFolders.Uncooked.ToString());
            }
        }

        [XmlIgnore]
        public string DlcCookedDirectory
        {
            get
            {
                if (!Directory.Exists(Path.Combine(DlcDirectory, EProjectFolders.Cooked.ToString())))
                {
                    Directory.CreateDirectory(Path.Combine(DlcDirectory, EProjectFolders.Cooked.ToString()));
                }

                return Path.Combine(DlcDirectory, EProjectFolders.Cooked.ToString());
            }
        }

        [XmlIgnore]
        public string DlcUncookedDirectory
        {
            get
            {
                if (!Directory.Exists(Path.Combine(DlcDirectory, EProjectFolders.Uncooked.ToString())))
                {
                    Directory.CreateDirectory(Path.Combine(DlcDirectory, EProjectFolders.Uncooked.ToString()));
                }

                return Path.Combine(DlcDirectory, EProjectFolders.Uncooked.ToString());
            }
        }

        [XmlIgnore]
        public string RawDlcDirectory
        {
            get
            {
                if (!Directory.Exists(Path.Combine(RawDirectory, "DLC")))
                {
                    Directory.CreateDirectory(Path.Combine(RawDirectory, "DLC"));
                }

                return Path.Combine(RawDirectory, "DLC");
            }
        }

        [XmlIgnore]
        public string RawModDirectory
        {
            get
            {
                if (!Directory.Exists(Path.Combine(RawDirectory, "Mod")))
                {
                    Directory.CreateDirectory(Path.Combine(RawDirectory, "Mod"));
                }

                return Path.Combine(RawDirectory, "Mod");
            }
        }


        [XmlIgnore]
        public string CookedDlcDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(GetDlcName()))
                {
                    return null;
                }
                var dir = Path.Combine(ProjectDirectory, "cooked", "DLC", GetDlcName(), "content");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;
            }
        }

        [XmlIgnore]
        public string CookedModDirectory
        {
            get
            {
                var dir = Path.Combine(ProjectDirectory, "cooked", "Mods", $"mod{Name}", "content");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;
            }
        }


        public override GameType GameType => GameType.Witcher3;

        [XmlIgnore]
        public override string PackedDlcDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(GetDlcName()))
                {
                    return null;
                }
                var dir = Path.Combine(ProjectDirectory, "packed", "DLC", GetDlcName(), "content");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;
            }
        }

        [XmlIgnore]
        public override string PackedModDirectory
        {
            get
            {
                var dir = Path.Combine(ProjectDirectory, "packed", "Mods", $"mod{Name}", "content");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;
            }
        }

       
        [XmlIgnore]
        public List<string> RadishFiles
        {
            get
            {
                if (!Directory.Exists(RadishDirectory))
                {
                    Directory.CreateDirectory(RadishDirectory);
                }
                return Directory.EnumerateFiles(RadishDirectory, "*", SearchOption.AllDirectories)
                    .Select(file => file[(RadishDirectory.Length + 1)..])
                    .ToList();
            }
        }

        [XmlIgnore]
        public List<string> RawFiles
        {
            get
            {
                if (!Directory.Exists(RawDirectory))
                {
                    Directory.CreateDirectory(RawDirectory);
                }
                return Directory.EnumerateFiles(RawDirectory, "*", SearchOption.AllDirectories)
                    .Select(file => file[(RawDirectory.Length + 1)..])
                    .ToList();
            }
        }


        #region methods

        public void CreateDefaultDirectories()
        {
            // create top-level directories
            _ = ModDirectory;
            _ = DlcDirectory;
            _ = RawDirectory;
            _ = RadishDirectory;

            // create mod-level directories
            _ = ModUncookedDirectory;
            _ = ModCookedDirectory;

            // create dlc-level directories
            _ = DlcUncookedDirectory;
            _ = DlcCookedDirectory;

            // create raw-level directories
            _ = RawModDirectory;
            _ = RawDlcDirectory;
        }

        /// <summary>
        /// Returns the first folder name in the DlcCookedDirectory.
        /// Does not support multiple dlc
        /// </summary>
        /// <returns></returns>
        public string GetDlcCookedRelativePath()
        {
            var relpath = "";
            var di = new DirectoryInfo(DlcCookedDirectory);
            if (di.Exists && di.GetDirectories().Any())
            {
                // support older projects
                if (di.GetDirectories().Any(_ => _.Name == "dlc"))
                {
                    var subdi = di.GetDirectories().First(_ => _.Name == "dlc");
                    if (subdi.Exists && subdi.GetDirectories().Any())
                    {
                        relpath = subdi.GetDirectories().First().FullName;
                        return relpath[(DlcCookedDirectory.Length + 5)..];
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    relpath = di.GetDirectories().First().FullName;
                }

                return relpath[(DlcCookedDirectory.Length + 1)..];
            }
            return relpath;
        }

        /// <summary>
        /// Returns the first relative folder path in the ActiveMod/dlc directory
        /// Does not support multiple DLC
        /// </summary>
        /// <returns></returns>
        public string GetDlcName()
        {
            if (!string.IsNullOrEmpty(GetDlcCookedRelativePath()))
            {
                return GetDlcCookedRelativePath();
            }

            if (!string.IsNullOrEmpty(GetDlcUncookedRelativePath()))
            {
                return GetDlcUncookedRelativePath();
            }

            return "";
        }

        /// <summary>
        /// Returns the first folder name in the DlcUncookedDirectory.
        /// Does not support multiple dlc
        /// </summary>
        /// <returns></returns>
        public string GetDlcUncookedRelativePath()
        {
            var relpath = "";
            var di = new DirectoryInfo(DlcUncookedDirectory);
            if (di.Exists && di.GetDirectories().Any())
            {
                // support older projects
                if (di.GetDirectories().Any(_ => _.Name == "dlc"))
                {
                    var subdi = di.GetDirectories().First(_ => _.Name == "dlc");
                    if (subdi.Exists && subdi.GetDirectories().Any())
                    {
                        relpath = subdi.GetDirectories().First().FullName;
                        return relpath[(DlcUncookedDirectory.Length + 5)..];
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    relpath = di.GetDirectories().First().FullName;
                }

                return relpath[(DlcUncookedDirectory.Length + 1)..];
            }
            return relpath;
        }

        

        #endregion methods

        public object Clone()
        {
            var clone = new Tw3Project()
            {
                Name = Name,
                Author = Author,
                Email = Email,
                Version = Version,
                Location = Location
            };
            return clone;
        }

        public override string ToString() => Location;

    }
}
