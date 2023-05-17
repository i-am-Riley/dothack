using Rileysoft.DotHack.Metrowerks.CATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Tests.Metrowerks.CATS
{
    [TestClass]
    public class CATSInfoTests
    {
        [DataTestMethod]
        [DataRow(
            new byte[] {
                0x02, 0x00, 0x3C, 0x00, 0xD0, 0x00, 0x10, 0x00,
                0x02, 0x01, 0x14, 0x00, 0x80, 0x0B, 0x10, 0x00, 
                0x0C, 0x00, 0x00, 0x00 }, 2)]
        public void ReadFromStream_Inputs_ReturnsCorrectResult(byte[] bytes, int expectedCount)
        {
            CATSInfo info = new CATSInfo();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                info.ReadFromStream(ms, bytes.LongLength);
            }

            Assert.AreEqual(expectedCount, info.Sections.Count);
        }
    }
}
