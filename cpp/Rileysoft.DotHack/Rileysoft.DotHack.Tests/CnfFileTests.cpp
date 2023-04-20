#include "pch.h"
#include "CppUnitTest.h"
#include "CnfFile.h"
#include <iostream>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace Rileysoft::DotHack::FileFormats::CNF;

namespace RileysoftDotHackTests
{
	struct SerializeTestData
	{
		std::string data[6];
	};
	TEST_CLASS(CnfFileTests)
	{
	public:
		TEST_METHOD(SetData_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfFile* cnfFile = new CnfFile();
				cnfFile->MakeReadonly();

				cnfFile->GetData()->SetBOOT2("test");
			});
		}

		TEST_METHOD(SetData_WhenNotReadonly_Works)
		{
			CnfFile* cnfFile = new CnfFile();
			cnfFile->GetData()->SetBOOT2("test");

			Assert::AreEqual(std::string("test"), cnfFile->GetData()->GetBOOT2());
		}

		TEST_METHOD(Serialize_TestData_ReturnsCorrectOuput)
		{
			std::vector<SerializeTestData> testData = { 
				{ "BOOT2 = abc\r\n", "abc", "", "", "", "" },
				{ "BOOT2 = abc\r\nVER = def\r\n", "abc", "def", "", "", "" },
				{ "BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\n", "abc", "def", "NTSC", "", "" },
				{ "BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\nPARAM2 = 0X10_0:95B938471614330245787F8F4C39ED0E\r\n", "abc", "def", "NTSC", "0X10_0:95B938471614330245787F8F4C39ED0E", "" },
				{ "VMODE = NTSC\r\nPARAM4 = abc\r\n", "", "", "NTSC", "", "abc" }
			};

			for (auto& test : testData)
			{
				CnfFile* cnfFile = new CnfFile();
				CnfData* cnfData = cnfFile->GetData();
				cnfData->SetBOOT2(test.data[1]);
				cnfData->SetVER(test.data[2]);
				cnfData->SetVMODE(test.data[3]);
				cnfData->SetPARAM2(test.data[4]);
				cnfData->SetPARAM4(test.data[5]);

				std::string expected = test.data[0];
				std::string actual = cnfFile->Serialize();

				Assert::AreEqual(expected, actual);
			}
		}

		TEST_METHOD(Deserialize_TestData_ReturnsCorrectOuput)
		{
			std::vector<SerializeTestData> testData = {
				{ "BOOT2 = abc\r\n", "abc", "", "", "", "" },
				{ "BOOT2 = abc\r\nVER = def\r\n", "abc", "def", "", "", "" },
				{ "BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\n", "abc", "def", "NTSC", "", "" },
				{ "BOOT2 = abc\r\nVER = def\r\nVMODE = NTSC\r\nPARAM2 = 0X10_0:95B938471614330245787F8F4C39ED0E\r\n", "abc", "def", "NTSC", "0X10_0:95B938471614330245787F8F4C39ED0E", "" },
				{ "VMODE = NTSC\r\nPARAM4 = abc\r\n", "", "", "NTSC", "", "abc" }
			};

			for (auto& test : testData)
			{
				CnfFile* cnfFile = new CnfFile();
				cnfFile->Deserialize(test.data[0]);

				Assert::AreEqual(test.data[1], cnfFile->GetData()->GetBOOT2());
				Assert::AreEqual(test.data[2], cnfFile->GetData()->GetVER());
				Assert::AreEqual(test.data[3], cnfFile->GetData()->GetVMODE());
				Assert::AreEqual(test.data[4], cnfFile->GetData()->GetPARAM2());
				Assert::AreEqual(test.data[5], cnfFile->GetData()->GetPARAM4());
			}
		}
	};
}
