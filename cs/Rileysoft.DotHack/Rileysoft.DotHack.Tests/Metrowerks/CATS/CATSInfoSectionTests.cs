using Rileysoft.DotHack.Metrowerks.CATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Tests.Metrowerks.CATS
{
    [TestClass]
    public class CATSInfoSectionTests
    {
        [DataTestMethod]
        [DataRow(
            new byte[] { 0x02, 0x00, 0x3C, 0x00, 0xD0, 0x00, 0x10, 0x00 },
            (byte)2, false, (ushort)0x3C, 0x001000d0, -1,
            DisplayName = "Normal Exit")]
        [DataRow(
            new byte[] { 0x02, 0x01, 0x14, 0x00, 0x80, 0x0B, 0x10, 0x00, 0x0C, 0x00, 0x00, 0x00 },
            (byte)2, true, (ushort)0x14, 0x00100b80, 0x0c,
            DisplayName = "NSTD Exit")]
        public void ReadFromStream_Inputs_ReturnsCorrectResult(
            byte[] testData,
            byte SectionType,
            bool NSTDExit,
            ushort Size,
            int Address,
            int Offset
        )
        {
            CATSInfoSection catsInfo = new CATSInfoSection();
            using (var ms = new MemoryStream(testData))
            {
                catsInfo.ReadFromStream(ms);
            }

            Assert.AreEqual(SectionType, catsInfo.SectionType);
            Assert.AreEqual(NSTDExit, catsInfo.NSTDExit);
            Assert.AreEqual(Size, catsInfo.Size);
            Assert.AreEqual(Address, catsInfo.Address);
            Assert.AreEqual(Offset, catsInfo.Offset);
        }
    }
}
