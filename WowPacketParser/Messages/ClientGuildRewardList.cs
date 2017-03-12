using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildRewardList
    {
        public List<GuildRewardItem> RewardItems;
        public UnixTime Version;
    }
}