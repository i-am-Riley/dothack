using System.Text;
using Rileysoft.DotHack.FileFormats.CNF;

#pragma warning disable IDE0017 // Object initialization can be simplified

namespace Rileysoft.DotHack.Tests.FileFormats.CNF
{
    [TestClass]
    public class CnfFileTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DataSet_WhenReadonly_Throws ()
        {
            CnfFile file = new();
            file.MakeReadonly();
            file.Data = new CnfData();
        }

        [TestMethod]
        public void DataSet_WhenNotReadonly_Works ()
        {
            var file = new CnfFile ();
            file.Data = new CnfData();
        }

        [DataTestMethod]
        [DataRow("BOOT2 = abc\r\n", "abc")]
        [DataRow("BOOT2 = abc\r\nVER = def\r\n", "abc", "def")]
        [DataRow("BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\n", "abc", "def", "NTSC")]
        [DataRow("BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\nPARAM2 = 0X10_0:95B938471614330245787F8F4C39ED0E\r\n", "abc", "def", "NTSC", "0X10_0:95B938471614330245787F8F4C39ED0E")]
        [DataRow("VMODE = NTSC\r\nPARAM4 = abc\r\n", null, null, "NTSC", null, "abc")]
        public void Serialize_TestData_ReturnsCorrectOuput (string expected, string? BOOT2 = null, string? VER = null, string? VMODE = null, string? PARAM2 = null, string? PARAM4 = null)
        {
            byte[] expectedASCII = Encoding.ASCII.GetBytes(expected);

            var data = new CnfData();
            data.BOOT2 = BOOT2;
            data.VER = VER;
            data.VMODE = VMODE;
            data.PARAM2 = PARAM2;
            data.PARAM4 = PARAM4;

            var file = new CnfFile(data);
            var actual = file.Serialize();
            string errorMsg = $"Expected:\n{expected}\nActual:\n{Encoding.ASCII.GetString(actual)}";

            Assert.AreEqual(expectedASCII.Length, actual.Length, errorMsg);

            for (var i=0; i<actual.Length; i++)
                Assert.AreEqual(expectedASCII[i], actual[i], errorMsg);
        }

        [DataTestMethod]
        [DataRow("BOOT2 = abc\r\n", "abc")]
        [DataRow("BOOT2 = abc\r\nVER = def\r\n", "abc", "def")]
        [DataRow("BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\n", "abc", "def", "NTSC")]
        [DataRow("BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\nPARAM2 = 0X10_0:95B938471614330245787F8F4C39ED0E\r\n", "abc", "def", "NTSC", "0X10_0:95B938471614330245787F8F4C39ED0E")]
        [DataRow("VMODE = NTSC\r\nPARAM4 = abc\r\n", null, null, "NTSC", null, "abc")]
        public void Deserialize_TestData_ReturnsCorrectOuput(string expected, string? BOOT2 = null, string? VER = null, string? VMODE = null, string? PARAM2 = null, string? PARAM4 = null)
        {
            CnfFile file = new();

            file.Deserialize(expected);
            Assert.AreEqual(BOOT2, file.Data.BOOT2, "BOOT2 mismatch");
            Assert.AreEqual(VER, file.Data.VER, "VER mismatch");
            Assert.AreEqual(VMODE, file.Data.VMODE, "VMODE mismatch");
            Assert.AreEqual(PARAM2, file.Data.PARAM2, "PARAM2 mismatch");
            Assert.AreEqual(PARAM4, file.Data.PARAM4, "PARAM4 mismatch");
        }
    }
}

#pragma warning restore IDE0017
