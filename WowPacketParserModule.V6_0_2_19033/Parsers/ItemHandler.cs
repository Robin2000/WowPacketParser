using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
            packet.ReadUInt32("Duration");
            packet.ReadUInt32("Slot");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.CMSG_ITEM_REFUND_INFO)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadEnum<UnknownFlags>("ProficiencyMask", TypeCode.UInt32);
            packet.ReadEnum<ItemClass>("ProficiencyClass", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS)]
        public static void HandleTransmogrifyITems(Packet packet)
        {
            var int16 = packet.ReadInt32("ItemsCount");
            packet.ReadPackedGuid128("Npc");

            for (int i = 0; i < int16; i++)
            {
                packet.ResetBitReader();

                var bit16 = packet.ReadBit("HasSrcItem", i);
                var bit40 = packet.ReadBit("HasSrcVoidItem", i);

                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("RandomPropertiesSeed", i);
                packet.ReadUInt32("RandomPropertiesID", i);
                packet.ResetBitReader();
                var hasBonuses = packet.ReadBit("HasItemBonus", i);
                var hasModifications = packet.ReadBit("HasModifications", i);
                if (hasBonuses)
                {
                    packet.ReadByte("Context", i);

                    var bonusCount = packet.ReadUInt32();
                    for (var j = 0; j < bonusCount; ++j)
                        packet.ReadUInt32("BonusListID", i, j);
                }

                if (hasModifications)
                {
                    var modificationCount = packet.ReadUInt32() / 4;
                    for (var j = 0; j < modificationCount; ++j)
                        packet.ReadUInt32("Modification", i, j);
                }

                packet.ReadInt32("Slot", i);

                if (bit16)
                    packet.ReadPackedGuid128("SrcItemGUID", i);

                if (bit40)
                    packet.ReadPackedGuid128("SrcVoidItemGUID", i);
            }
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ReadUInt32("Amount");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            packet.ReadByte("PackSlot");
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("CastItem");

            SpellHandler.ReadSpellCastRequest(ref packet);
        }
    }
}
