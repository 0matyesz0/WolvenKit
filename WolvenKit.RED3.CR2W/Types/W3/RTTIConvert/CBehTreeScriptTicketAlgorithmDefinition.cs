using System.IO;
using System.Runtime.Serialization;
using WolvenKit.RED3.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED3.CR2W.Types.Enums;


namespace WolvenKit.RED3.CR2W.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeScriptTicketAlgorithmDefinition : IBehTreeTicketAlgorithmDefinition
	{
		[Ordinal(1)] [RED("scriptDef")] 		public CHandle<ITicketAlgorithmScriptDefinition> ScriptDef { get; set;}

		[Ordinal(2)] [RED("importanceMultiplier")] 		public CBehTreeValFloat ImportanceMultiplier { get; set;}

		public CBehTreeScriptTicketAlgorithmDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeScriptTicketAlgorithmDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}