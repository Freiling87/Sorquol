using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;
using Sorquol.Content;

namespace CCU.Mutators.Utility
{
	class SortActiveMutatorsByName
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.SortMutatorsByName, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Sorts active Mutators by name."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.SortMutatorsByName,
				});
		}
	}
}