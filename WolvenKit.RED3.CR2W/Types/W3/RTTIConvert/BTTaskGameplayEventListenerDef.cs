using System.IO;
using System.Runtime.Serialization;
using WolvenKit.RED3.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED3.CR2W.Types.Enums;


namespace WolvenKit.RED3.CR2W.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskGameplayEventListenerDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("gameplayEventName")] 		public CBehTreeValCName GameplayEventName { get; set;}

		[Ordinal(2)] [RED("validFor")] 		public CFloat ValidFor { get; set;}

		[Ordinal(3)] [RED("activeFor")] 		public CFloat ActiveFor { get; set;}

		[Ordinal(4)] [RED("clearOnEvent")] 		public CName ClearOnEvent { get; set;}

		public BTTaskGameplayEventListenerDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskGameplayEventListenerDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}