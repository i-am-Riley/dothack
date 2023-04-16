#include "pch.h"
#include "CppUnitTest.h"
#include "CnfFile.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace Rileysoft::DotHack::FileFormats::CNF;

namespace RileysoftDotHackTests
{
	TEST_CLASS(RileysoftDotHackTests)
	{
	public:
		
		TEST_METHOD(TestMethod1)
		{
			CnfFile* file = new CnfFile();
			file->Dummy();
		}
	};
}
