using System.IO;
using System.Runtime.Serialization;
using WolvenKit.RED3.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED3.CR2W.Types.Enums;


namespace WolvenKit.RED3.CR2W.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class ISpawnTreeSpawnAroundNodeInitializer : ISpawnTreeScriptedInitializer
	{
		[Ordinal(1)] [RED("spawnRadiousMin")] 		public CFloat SpawnRadiousMin { get; set;}

		[Ordinal(2)] [RED("spawnRadiousMAx")] 		public CFloat SpawnRadiousMAx { get; set;}

		[Ordinal(3)] [RED("spawnNodeTag")] 		public CName SpawnNodeTag { get; set;}

		[Ordinal(4)] [RED("spawnNode")] 		public CHandle<CNode> SpawnNode { get; set;}

		public ISpawnTreeSpawnAroundNodeInitializer(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new ISpawnTreeSpawnAroundNodeInitializer(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}