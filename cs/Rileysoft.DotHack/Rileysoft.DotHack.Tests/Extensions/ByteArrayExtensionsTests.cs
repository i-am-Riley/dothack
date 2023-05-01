using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Tests.Extensions
{
    [TestClass]
    public class ByteArrayExtensionsTests
    {
        [DataTestMethod]
        [DataRow(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0)]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, -1)]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0x7F }, 2147483647)]
        [DataRow(new byte[] { 0x00, 0x00, 0x00, 0x80 }, -2147483648)]
        public void ReadIntLE_Inputs_ReturnCorrectResults(byte[] bytes, int expected)
        {
            int result = bytes.ReadIntLE();

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new byte[] { 0x00, 0x00 }, (short)0)]
        [DataRow(new byte[] { 0xFF, 0xFF }, (short)-1)]
        [DataRow(new byte[] { 0xFF, 0x7F }, (short)short.MaxValue)]
        [DataRow(new byte[] { 0x00, 0x80 }, (short)short.MinValue)]
        public void ReadShortLE_Inputs_ReturnCorrectResults(byte[] bytes, short expected)
        {
            short result = bytes.ReadShortLE();

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, (long)0)]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, (long)-1)]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F }, (long)long.MaxValue)]
        [DataRow(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 }, (long)long.MinValue)]
        public void ReadLongLE_Inputs_ReturnCorrectResults(byte[] bytes, long expected)
        {
            long result = bytes.ReadLongLE();

            Assert.AreEqual(expected, result);
        }
    }
}
