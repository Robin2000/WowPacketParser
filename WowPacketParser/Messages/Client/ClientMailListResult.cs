using System.Collections.Generic;
using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMailListResult
    {
        public int TotalNumRecords;
        public List<CliMailListEntry> Mails;
    }
}