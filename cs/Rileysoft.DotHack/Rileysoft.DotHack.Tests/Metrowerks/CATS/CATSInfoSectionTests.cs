using Rileysoft.DotHack.Metrowerks.CATS;
using System.Collections.ObjectModel;

namespace Rileysoft.DotHack.Tests.Metrowerks.CATS
{
    [TestClass]
    public class CATSInfoSectionTests
    {
        [DataTestMethod]
        [DataRow(
            new byte[] { 0x02, 0x00, 0x3C, 0x00, 0xD0, 0x00, 0x10, 0x00 },
            (byte)2, (byte)0, (ushort)0x3C, 0x001000d0, new int[] { },
            DisplayName = "Normal Exit")]
        [DataRow(
            new byte[] { 0x02, 0x01, 0x14, 0x00, 0x80, 0x0B, 0x10, 0x00, 0x0C, 0x00, 0x00, 0x00 },
            (byte)2, (byte)1, (ushort)0x14, 0x00100b80, new int[] { 0x0c },
            DisplayName = "NSTD Exit")]
        [DataRow(
            new byte[] { 0x02, 0x02, 0x14, 0x00, 0x80, 0x0B, 0x10, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x0D, 0x00, 0x00, 0x00 },
            (byte)2, (byte)2, (ushort)0x14, 0x00100b80, new int[] { 0x0c, 0x0d },
            DisplayName = "NSTD Exit 2")]
        public void ReadFromStream_Inputs_ReturnsCorrectResult(
            byte[] testData,
            byte SectionType,
            byte NSTDExit,
            ushort Size,
            int Address,
            int[] Offsets
        )
        {
            CATSInfoSection catsInfo = new CATSInfoSection();
            using (var ms = new MemoryStream(testData))
            {
                catsInfo.ReadFromStream(ms);
            }

            Collection<int> OffsetCollection = new Collection<int>(Offsets);

            Assert.AreEqual(SectionType, catsInfo.SectionType);
            Assert.AreEqual(NSTDExit, catsInfo.NSTDExit);
            Assert.AreEqual(Size, catsInfo.Size);
            Assert.AreEqual(Address, catsInfo.Address);

            var ActualOffsets = catsInfo.Offsets;
            Assert.AreEqual(ActualOffsets.Count, OffsetCollection.Count);
            for (int i=0; i<ActualOffsets.Count; i++)
            {
                int ExpectedOffset = OffsetCollection[i];
                int ActualOffset = ActualOffsets[i];
                
                Assert.AreEqual(ExpectedOffset, ActualOffset, $"Offset {i} was incorrect");
            }

        }
    }
}
