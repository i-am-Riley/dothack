#include "pch.h"
#include "CppUnitTest.h"
#include "CnfData.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace Rileysoft::DotHack::FileFormats::CNF;

namespace RileysoftDotHackTests
{
	TEST_CLASS(CnfDataTests)
	{
	public:

		TEST_METHOD(SetBOOT2_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfData* cnfData = new CnfData();
				cnfData->MakeReadonly();

				cnfData->SetBOOT2("Test");
			});
		}

		TEST_METHOD(SetVER_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfData* cnfData = new CnfData();
				cnfData->MakeReadonly();

				cnfData->SetVER("Test");
			});
		}

		TEST_METHOD(SetVMODE_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfData* cnfData = new CnfData();
				cnfData->MakeReadonly();

				cnfData->SetVMODE("Test");
			});
		}

		TEST_METHOD(SetPARAM2_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfData* cnfData = new CnfData();
				cnfData->MakeReadonly();

				cnfData->SetPARAM2("Test");
			});
		}

		TEST_METHOD(SetPARAM4_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfData* cnfData = new CnfData();
				cnfData->MakeReadonly();

				cnfData->SetPARAM4("Test");
			});
		}

		TEST_METHOD(SetBOOT2_WhenNotReadonly_Works)
		{
			CnfData* cnfData = new CnfData();
			cnfData->SetBOOT2("somevalue");

			std::string expected = "somevalue";

			Assert::AreEqual(expected, cnfData->GetBOOT2());
		}

		TEST_METHOD(SetVER_WhenNotReadonly_Works)
		{
			CnfData* cnfData = new CnfData();
			cnfData->SetVER("somevalue");

			std::string expected = "somevalue";

			Assert::AreEqual(expected, cnfData->GetVER());
		}

		TEST_METHOD(SetVMODE_WhenNotReadonly_Works)
		{
			CnfData* cnfData = new CnfData();
			cnfData->SetVMODE("somevalue");

			std::string expected = "somevalue";

			Assert::AreEqual(expected, cnfData->GetVMODE());
		}

		TEST_METHOD(SetPARAM2_WhenNotReadonly_Works)
		{
			CnfData* cnfData = new CnfData();
			cnfData->SetPARAM2("somevalue");

			std::string expected = "somevalue";

			Assert::AreEqual(expected, cnfData->GetPARAM2());
		}

		TEST_METHOD(SetPARAM4_WhenNotReadonly_Works)
		{
			CnfData* cnfData = new CnfData();
			cnfData->SetPARAM4("somevalue");

			std::string expected = "somevalue";

			Assert::AreEqual(expected, cnfData->GetPARAM4());
		}
	};
}
