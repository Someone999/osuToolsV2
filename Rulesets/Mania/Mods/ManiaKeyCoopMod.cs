using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 将列分成两个部分供双人游玩
    /// </summary>
    public class ManiaKeyCoopMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override string Name => "KeyCoop";
        /// <inheritdoc />
        public override string ShortName => "Co-op";
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.KeyCoop;

        public override string Description => "将列分成两个部分供双人游玩";
        public override bool IsRanked => false;
    }
}