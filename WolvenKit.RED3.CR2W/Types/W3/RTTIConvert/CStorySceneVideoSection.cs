using System.IO;
using System.Runtime.Serialization;
using WolvenKit.RED3.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED3.CR2W.Types.Enums;


namespace WolvenKit.RED3.CR2W.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneVideoSection : CStorySceneSection
	{
		[Ordinal(1)] [RED("videoFileName")] 		public CString VideoFileName { get; set;}

		[Ordinal(2)] [RED("eventDescription")] 		public CString EventDescription { get; set;}

		[Ordinal(3)] [RED("suppressRendering")] 		public CBool SuppressRendering { get; set;}

		[Ordinal(4)] [RED("extraVideoFileNames", 2,0)] 		public CArray<CString> ExtraVideoFileNames { get; set;}

		public CStorySceneVideoSection(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneVideoSection(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}