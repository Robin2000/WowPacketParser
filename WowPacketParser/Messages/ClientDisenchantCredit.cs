using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDisenchantCredit
    {
        public ulong Disenchanter;
        public ItemInstance Item;
    }
}