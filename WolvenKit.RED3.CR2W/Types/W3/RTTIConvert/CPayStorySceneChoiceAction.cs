using System.IO;
using System.Runtime.Serialization;
using WolvenKit.RED3.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED3.CR2W.Types.Enums;


namespace WolvenKit.RED3.CR2W.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CPayStorySceneChoiceAction : CStorySceneChoiceLineActionScripted
	{
		[Ordinal(1)] [RED("money")] 		public CInt32 Money { get; set;}

		[Ordinal(2)] [RED("dontGrantExp")] 		public CBool DontGrantExp { get; set;}

		[Ordinal(3)] [RED("actionIcon")] 		public CEnum<EDialogActionIcon> ActionIcon { get; set;}

		public CPayStorySceneChoiceAction(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CPayStorySceneChoiceAction(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}