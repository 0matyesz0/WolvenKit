using System.IO;
using CP77.CR2W.Reflection;
using FastMember;
using static CP77.CR2W.Types.Enums;

namespace CP77.CR2W.Types
{
	[REDMeta]
	public class AnimFeature_AdHocAnimation : animAnimFeature
	{
		[Ordinal(0)]  [RED("animationIndex")] public CInt32 AnimationIndex { get; set; }
		[Ordinal(1)]  [RED("isActive")] public CBool IsActive { get; set; }
		[Ordinal(2)]  [RED("useBothHands")] public CBool UseBothHands { get; set; }

		public AnimFeature_AdHocAnimation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }
	}
}